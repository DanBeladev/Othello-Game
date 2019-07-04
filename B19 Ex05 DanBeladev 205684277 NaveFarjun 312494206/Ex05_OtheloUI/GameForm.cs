using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ex05_OtheloLogic;

namespace Ex05_OtheloUI
{
    public delegate void CloseAllWindowsDelegate();

    public partial class GameForm : Form
    {
        private BoardPanel m_BoardPanel;
        private List<Cell> m_OptionalMoves;
        private GameManager m_GameManager;
        private GameConfig m_GameConfig;

        public GameForm(GameConfig i_GameConfig)
        {
            m_GameConfig = i_GameConfig;
            InitializeComponent();

            int gameBorderSize = i_GameConfig.BoardSize * 61;
            MinimumSize = MaximumSize = Size = new Size(gameBorderSize, gameBorderSize);

            m_GameManager = new GameManager(m_GameConfig.SecondPlayerType, m_GameConfig.BoardSize);

            initBoard();
            initEvents();

            updateUiAfterTurnChanged();
        }

        private void initBoard()
        {
            m_BoardPanel = new BoardPanel(m_GameManager.Board);
            m_BoardPanel.Left = (ClientSize.Width - m_BoardPanel.Width) / 2;
            m_BoardPanel.Top = (ClientSize.Height - m_BoardPanel.Height) / 2;
            Controls.Add(m_BoardPanel);
        }

        private void initEvents()
        {
            m_GameManager.TurnChanged -= onTurnChanged;
            m_GameManager.TurnChanged += onTurnChanged;
            m_GameManager.GameFinished -= onGameFinished;
            m_GameManager.GameFinished += onGameFinished;
            m_BoardPanel.CellClicked -= onBoardPanelCellClicked;
            m_BoardPanel.CellClicked += onBoardPanelCellClicked;
        }

        private void restart()
        {
            Controls.Remove(m_BoardPanel);
            m_GameManager.Restart();
            initBoard();
            initEvents();
            updateUiAfterTurnChanged();
        }

        private void handleDialogResult(DialogResult i_DialogResult)
        {
            if (i_DialogResult == DialogResult.Yes)
            {
                restart();
            }
            else
            {
                Close();
            }
        }

        private void onGameFinished()
        {
            Player winner = m_GameManager.GetWinner();
            if (winner == null)
            {
                DialogResult dialogResult = MessageBox.Show("Tie! would you like another round?", "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                handleDialogResult(DialogResult);
            }
            else
            {
                Player looser = m_GameManager.GetOpposite(winner);

                string Message = string.Format(
                    "{0} Won!! ({1}/{2}) ({3}/{4}){5}Would you like another round?",
                    winner.Shape,
                    winner.Score,
                    looser.Score,
                    winner.NumOfWins,
                    looser.NumOfWins,
                    Environment.NewLine);

                DialogResult dialogResult = MessageBox.Show(Message, "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                handleDialogResult(dialogResult);
            }
        }

        private void onTurnChanged()
        {
            updateUiAfterTurnChanged();
        }

        private void updateUiAfterTurnChanged()
        {
            updateTitleForTurn();
            markAvailableOptions();
            m_BoardPanel.Refresh();
            if (m_GameManager.CurrentPlayer.TypeOfPlayer == Player.ePlayerType.Computer)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void markAvailableOptions()
        {
            if (m_OptionalMoves != null)
            {
                foreach (var cell in m_OptionalMoves)
                {
                    m_BoardPanel.Get(cell.Row, cell.Col).UnMarkAvailable();
                }

                m_BoardPanel.Refresh();
                if (m_OptionalMoves != null)
                {
                    foreach (var cell in m_OptionalMoves)
                    {
                        m_BoardPanel.Get(cell.Row, cell.Col).UnMarkAvailable();
                    }
                }
            }

            m_OptionalMoves = m_GameManager.MakeListOfOptionalMoves();

            foreach (var cell in m_OptionalMoves)
            {
                m_BoardPanel.Get(cell.Row, cell.Col).MarkAvailable();
            }
        }

        private void updateTitleForTurn()
        {
            Text = string.Format("Othello - {0}'s Turn", m_GameManager.CurrentPlayer.Shape);
        }

        private void onBoardPanelCellClicked(Cell i_Cell)
        {
            m_GameManager.MakeTurn(i_Cell);
        }
    }
}
