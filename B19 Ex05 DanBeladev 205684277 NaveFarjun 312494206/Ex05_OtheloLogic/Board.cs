using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex05_OtheloLogic
{
    public class Board
    {   
        private static readonly int sr_MinBoardSize = 6;
        private static readonly int sr_MaxBoardSize = 12;
        private readonly int r_SizeMatrix;

        private Cell[,] m_BoardMatrix;

        public Board(int i_SizeOfBoard)
        {
            r_SizeMatrix = i_SizeOfBoard;
            m_BoardMatrix = new Cell[i_SizeOfBoard, i_SizeOfBoard];
            initBoard();
        }

        public static int MinBoardSize
        {
            get { return sr_MinBoardSize; }
        }

        public static int MaxBoardSize
        {
            get { return sr_MaxBoardSize; }
        }

        private void initBoard()
        {
            for (int i = 0; i < r_SizeMatrix; i++)
            {
                for (int j = 0; j < r_SizeMatrix; j++)
                {
                    m_BoardMatrix[i, j] = new Cell(i, j, eDiskShape.Empty);
                }
            }

            m_BoardMatrix[(r_SizeMatrix / 2) - 1, (r_SizeMatrix / 2) - 1].Content = eDiskShape.Red;
            m_BoardMatrix[(r_SizeMatrix / 2), (r_SizeMatrix / 2)].Content = eDiskShape.Red;
            m_BoardMatrix[(r_SizeMatrix / 2) - 1, (r_SizeMatrix / 2)].Content = eDiskShape.Yellow;
            m_BoardMatrix[(r_SizeMatrix / 2), (r_SizeMatrix / 2) - 1].Content = eDiskShape.Yellow;
        }

        public Cell GetCell(int i_Row, int i_Col)
        {
            return m_BoardMatrix[i_Row, i_Col];
        }

        public int Size
        {
            get { return r_SizeMatrix; }
        }
    }
}
