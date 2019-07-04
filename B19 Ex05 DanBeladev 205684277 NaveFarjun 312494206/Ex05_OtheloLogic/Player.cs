using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex05_OtheloLogic
{
    public class Player
    {
        private readonly ePlayerType r_PlayerType;
        private readonly eDiskShape r_ShapeOfDisk;
        private int m_Score;
        private int m_NumOfWinsGames = 0;

        public enum ePlayerType
        {
            Human = 1,
            Computer = 2
        }

        public Player(eDiskShape i_Shape, ePlayerType i_PlayerType)
        {
            m_Score = 2;
            r_ShapeOfDisk = i_Shape;
            r_PlayerType = i_PlayerType;
        }

        public ePlayerType TypeOfPlayer
        {
            get { return r_PlayerType; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public int NumOfWins
        {
            get { return m_NumOfWinsGames; }
            set { m_NumOfWinsGames = value; }
        }

        public eDiskShape Shape
        {
            get { return r_ShapeOfDisk; }
        }
    }
}
