using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex05_OtheloLogic
{
    public class GameManager
    {
        private const int k_NumOfDirections = 8;
        private Board m_BoardGame;
        private Player m_YellowPlayer;
        private Player m_RedPlayer;
        private Player m_curentTurn;
        private bool m_IsGameOver = false;

        public delegate void TurnChangedHandler();

        public delegate void GameFinishedHandler();
    
        public event TurnChangedHandler TurnChanged;

        public event GameFinishedHandler GameFinished;

        public GameManager(Player.ePlayerType i_secondPlayerType, int i_BoardSize)
        {
            m_YellowPlayer = new Player(eDiskShape.Yellow, Player.ePlayerType.Human);
            m_RedPlayer = new Player(eDiskShape.Red, i_secondPlayerType);
            m_BoardGame = new Board(i_BoardSize);
            m_curentTurn = m_YellowPlayer;
        }

        public Player GetWinner()
        {
            if (m_YellowPlayer.Score > m_RedPlayer.Score)
            {
                return m_YellowPlayer;
            }
            else if (m_YellowPlayer.Score < m_RedPlayer.Score)
            {
                return m_RedPlayer;
            }
            else
            {
                return null;
            }
        }

        public Player GetOpposite(Player i_Player)
        {
            if (i_Player.Shape == eDiskShape.Red)
            {
                return m_YellowPlayer;
            }
            else
            {
                return m_RedPlayer;
            }
        }

        public Board Board
        {
            get { return m_BoardGame; }
        }

        public Player YellowPlayer
        {
            get { return m_YellowPlayer; }
            set { m_YellowPlayer = value; }
        }

        public Player RedPlayer
        {
            get { return m_RedPlayer; }
            set { m_RedPlayer = value; }
        }

        public Player CurrentPlayer
        {
            get { return m_curentTurn; }
        }

        public void MakeTurn(Cell i_ChosenCell)
        {
            putDisk(CurrentPlayer, i_ChosenCell);

            changeTurn();
            TurnChanged?.Invoke();

            if (checkIfGameFinished())
            {
                Player winner = GetWinner();
                if (winner != null)
                {
                    winner.NumOfWins++;
                }

                isGameOver = true;
                GameFinished?.Invoke();
            }
            else
            {
                if (CurrentPlayer.TypeOfPlayer == Player.ePlayerType.Computer)
                {
                    var validMoves = MakeListOfOptionalMoves();
                    if (validMoves.Count > 0)
                    {
                        MakeTurn(validMoves[new Random().Next(0, validMoves.Count - 1)]);
                    }
                    else
                    {
                        changeTurn();
                        TurnChanged?.Invoke();
                    }
                }
                else
                {
                    if (!hasValidMoves())
                    {
                        changeTurn();
                        TurnChanged?.Invoke();
                    }
                }
            }
        }

        public void Restart()
        {
            m_YellowPlayer.Score = 2;
            m_RedPlayer.Score = 2;
            m_BoardGame = new Board(m_BoardGame.Size);
            m_curentTurn = m_YellowPlayer;
            m_IsGameOver = false;
        }

        public List<Cell> MakeListOfOptionalMoves()
        {
            List<Cell> optionalCells = new List<Cell>(2);
            Cell currentCell;
            int directionForColl;
            int directionForRow;
            int recDepth = 0;
            bool res = false;
            for (int i = 0; i < m_BoardGame.Size; i++)
            {
                for (int j = 0; j < m_BoardGame.Size; j++)
                {
                    currentCell = m_BoardGame.GetCell(i, j);
                    if (currentCell.Content == eDiskShape.Empty)
                    {
                        for (int k = 0; k < k_NumOfDirections; k++)
                        {
                            recDepth = 0;
                            getDirection(out directionForRow, out directionForColl, k);
                            res = isOptionalCell(currentCell, m_curentTurn, directionForRow, directionForColl, ref recDepth);
                            if (res)
                            {
                                optionalCells.Add(currentCell);
                                break;
                            }
                        }
                    }
                }
            }

            return optionalCells;
        }

        private void getDirection(out int o_DirectionForRow, out int o_DirectionForColl, int i_Index)
        {
            o_DirectionForRow = 0;
            o_DirectionForColl = 0;
            if (i_Index == 0 || i_Index == 1 || i_Index == 2)
            {
                o_DirectionForRow = -1;
            }

            if (i_Index == 0 || i_Index == 3 || i_Index == 5)
            {
                o_DirectionForColl = -1;
            }

            if (i_Index == 5 || i_Index == 6 || i_Index == 7)
            {
                o_DirectionForRow = 1;
            }

            if (i_Index == 2 || i_Index == 4 || i_Index == 7)
            {
                o_DirectionForColl = 1;
            }
        }

        private bool hasValidMoves()
        {
            return MakeListOfOptionalMoves().Count > 0;
        }

        private void changeTurn()
        {
            if (m_curentTurn == m_YellowPlayer)
            {
                m_curentTurn = m_RedPlayer;
            }
            else
            {
                m_curentTurn = m_YellowPlayer;
            }
        }

        private bool putDisk(Player i_PlayerTurn, Cell i_To)
        {
            bool isLegalMove = false;
            int directionForColl;
            int directionForRow;
            int recDepth = 0;
            for (int i = 0; i < k_NumOfDirections; i++)
            {
                recDepth = 0;
                getDirection(out directionForRow, out directionForColl, i);
                if (calculateMove(i_To, i_PlayerTurn, directionForRow, directionForColl, ref recDepth))
                {
                    isLegalMove = true;
                }
            }

            if (isLegalMove)
            {
                i_PlayerTurn.Score++;
            }

            return isLegalMove;
        }

        private bool calculateMove(Cell i_CurrentCell, Player i_PlayerTurn, int i_DirectionForRow, int i_DirectionForColl, ref int io_RecDepth)
        {
            bool res = false;
            int neighborRow = i_CurrentCell.Row + i_DirectionForRow;
            int neighborCol = i_CurrentCell.Col + i_DirectionForColl;
            if ((neighborRow >= 0 && neighborRow < m_BoardGame.Size) &&
                (neighborCol >= 0 && neighborCol < m_BoardGame.Size))
            {
                Cell neighborCell = m_BoardGame.GetCell(neighborRow, neighborCol);
                if (io_RecDepth != 0 && neighborCell.Content == i_PlayerTurn.Shape)
                {
                    changeDiskAndUpdateScore(i_CurrentCell, i_PlayerTurn, io_RecDepth);
                    res = true;
                }
                else if (neighborCell.Content == eDiskShape.Empty || (io_RecDepth == 0 && neighborCell.Content == i_PlayerTurn.Shape))
                {
                    res = false;
                }
                else
                {
                    io_RecDepth++;
                    res = calculateMove(neighborCell, i_PlayerTurn, i_DirectionForRow, i_DirectionForColl, ref io_RecDepth);
                    io_RecDepth--;
                    if (res == true)
                    {
                        changeDiskAndUpdateScore(i_CurrentCell, i_PlayerTurn, io_RecDepth);
                    }
                }
            }

            return res;
        }

        private void changeDiskAndUpdateScore(Cell i_CurrentCell, Player i_PlayerTurn, int i_RecDepth)
        {
            i_CurrentCell.Content = i_PlayerTurn.Shape;
            if (i_RecDepth != 0)
            {
                if (i_PlayerTurn == m_YellowPlayer)
                {
                    m_RedPlayer.Score--;
                }
                else
                {
                    m_YellowPlayer.Score--;
                }

                i_PlayerTurn.Score++;
            }
        }

        private bool isOptionalCell(Cell i_CurrentCell, Player i_PlayerTurn, int i_DirectionForRow, int i_DirectionForColl, ref int io_RecDepth)
        {
            bool res = false;
            int neighborRow = i_CurrentCell.Row + i_DirectionForRow;
            int neighborCol = i_CurrentCell.Col + i_DirectionForColl;

            if ((neighborRow >= 0 && neighborRow < m_BoardGame.Size) &&
                (neighborCol >= 0 && neighborCol < m_BoardGame.Size))
            {
                Cell neighborCell = m_BoardGame.GetCell(neighborRow, neighborCol);
                if (io_RecDepth != 0 && neighborCell.Content == i_PlayerTurn.Shape)
                {
                    return true;
                }

                if (neighborCell.Content == eDiskShape.Empty || (io_RecDepth == 0 && neighborCell.Content == i_PlayerTurn.Shape))
                {
                    return false;
                }
                else
                {
                    io_RecDepth++;
                    res = isOptionalCell(neighborCell, i_PlayerTurn, i_DirectionForRow, i_DirectionForColl, ref io_RecDepth);
                }
            }

            return res;
        }

        private bool isAvailaibleCell(Cell i_InputCell)
        {
            return i_InputCell.Content == eDiskShape.Empty && putDisk(m_curentTurn, i_InputCell);
        }

        private bool isGameOver
        {
            get { return m_IsGameOver; }
            set { m_IsGameOver = value; }
        }

        private bool checkIfGameFinished()
        {
            bool result = false;
            if (!hasValidMoves())
            {
                changeTurn();
                if (!hasValidMoves())
                {
                    result = true;
                }

                changeTurn(); 
            }

            return result;
        }
    }
}
