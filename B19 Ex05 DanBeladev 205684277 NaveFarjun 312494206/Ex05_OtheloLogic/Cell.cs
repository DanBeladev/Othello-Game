using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex05_OtheloLogic
{
    public class Cell
    {
        public delegate void ContentChangedHandler();

        public event ContentChangedHandler ContentChanged;

        private readonly int r_Row;
        private readonly int r_Col;
        private eDiskShape m_Content;

        public Cell(int i_Row, int i_Col, eDiskShape i_Content = eDiskShape.Empty)
        {
            r_Row = i_Row;
            r_Col = i_Col;
            m_Content = i_Content;
        }

        public eDiskShape Content
        {
            get { return m_Content; }

            set
            {
                m_Content = value;
                ContentChanged?.Invoke();
            }
        }

        public int Row
        {
            get { return r_Row; }
        }

        public int Col
        {
            get { return r_Col; }
        }
    }
}
