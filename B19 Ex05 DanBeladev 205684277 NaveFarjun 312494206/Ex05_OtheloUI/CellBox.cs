using System;
using System.Drawing;
using System.Windows.Forms;
using Ex05_OtheloLogic;

namespace Ex05_OtheloUI
{
    public class CellBox : PictureBox
    {
        private static readonly int sr_ItemSize = 50;
        private readonly EventHandler m_ClickHandler;
        private Cell m_Cell;
        public bool m_InGreenList = false;
        
        public static int ItemSize
        {
            get { return sr_ItemSize; }
        }

        public Cell Cell
        {
            get { return m_Cell; }
        }

        public CellBox(Cell i_Cell, EventHandler i_ClickHandler) : base()
        {
            Size = new Size(sr_ItemSize, sr_ItemSize);
            m_Cell = i_Cell;
            m_Cell.ContentChanged += cellContentChanged;
            m_ClickHandler = i_ClickHandler;

            updateImage();
        }

        private void updateImage()
        {
            if (m_Cell.Content == eDiskShape.Red)
            {
                BackgroundImage = Properties.Resources.CoinRed;
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (m_Cell.Content == eDiskShape.Yellow)
            {
                BackgroundImage = Properties.Resources.CoinYellow;
                BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void cellContentChanged()
        {
            updateImage();
        }

        public void UnMarkAvailable()
        {
            m_InGreenList = false;
        }

        public void MarkAvailable()
        {
            m_InGreenList = true;
        }

        private void enableClick()
        {
            Click -= m_ClickHandler;
            Click += m_ClickHandler;
        }

        private void disableClick()
        {
            Click -= m_ClickHandler;
        }

        protected override void OnPaint(PaintEventArgs i_EventArgs)
        {
            if (m_Cell.Content == eDiskShape.Empty && m_InGreenList)
            {
                BackColor = Color.Green;
                enableClick();
            }
            else
            {
                BackColor = Color.White;
                disableClick();
            }
        }
    }
}
