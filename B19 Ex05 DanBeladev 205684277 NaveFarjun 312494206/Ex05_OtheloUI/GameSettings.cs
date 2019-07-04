using System;
using System.Windows.Forms;
using Ex05_OtheloLogic;

namespace Ex05_OtheloUI
{
    public partial class GameSettings : Form
    {
        private GameConfig m_GameConfig;

        public GameSettings()
        {
            InitializeComponent();
            m_GameConfig = new GameConfig { };
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            if (m_GameConfig.BoardSize == Board.MaxBoardSize)
            {
                m_GameConfig.BoardSize = Board.MinBoardSize;
            }
            else
            {
                m_GameConfig.BoardSize += 2;
            }

            buttonBoardSize.Text = string.Format("Board Size: {0}X{0} (click to increase)", m_GameConfig.BoardSize);
        }

        private void buttonAgainstFriend_Click(object sender, EventArgs e)
        {
            InitFormAfterClick(Player.ePlayerType.Human);
        }

        private void buttonAgainstComputer_Click(object sender, EventArgs e)
        {
            InitFormAfterClick(Player.ePlayerType.Computer);
        }

        private void InitFormAfterClick(Player.ePlayerType i_SecondPlayerType)
        {
            m_GameConfig.SecondPlayerType = i_SecondPlayerType;
            Form gameForm = new GameForm(m_GameConfig);
            gameForm.FormClosed += GameForm_FormClosed;
            Hide();
            gameForm.Show();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void CloseGameSettingsWindow()
        {
            Close();
        }
    }
}
