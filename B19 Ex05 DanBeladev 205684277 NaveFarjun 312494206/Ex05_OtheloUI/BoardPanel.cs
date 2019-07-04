using System;
using System.Drawing;
using System.Windows.Forms;
using Ex05_OtheloLogic;

namespace Ex05_OtheloUI
{
    public class BoardPanel : TableLayoutPanel
    {
        public delegate void CellClickedHandler(Cell i_Cell);

        public event CellClickedHandler CellClicked;

        private CellBox[,] m_Cells;
        private Board m_Board;

        public BoardPanel(Board i_Board)
        {
            m_Board = i_Board;

            int size = m_Board.Size;         
            m_Cells = new CellBox[size, size];

            for (int i = 0; i < size; i++)
            {
                ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, CellBox.ItemSize));
            }

            for (int i = 0; i < size; i++)
            {
                RowStyles.Add(new RowStyle(SizeType.Absolute, CellBox.ItemSize));
            }

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    CellBox cell = new CellBox(m_Board.GetCell(row, col), new EventHandler(OnCellClicked));
                    m_Cells[row, col] = cell;
                    Controls.Add(cell, col, row);
                }
            }
            
            Padding = new Padding(0);
            Size = new Size(size * CellBox.ItemSize, size * CellBox.ItemSize);
        }        

        public CellBox Get(int i_Row, int i_Col)
        {
            return m_Cells[i_Row, i_Col];
        }

        private void OnCellClicked(object sender, EventArgs e)
        {
            CellBox cellBox = (CellBox)sender;
            CellClicked?.Invoke(cellBox.Cell);
        }
    }
}
