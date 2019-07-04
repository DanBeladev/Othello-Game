using Ex05_OtheloLogic;

namespace Ex05_OtheloUI
{
    public class GameConfig
    {
        private int m_BoardSize;
        private Player.ePlayerType m_SecondPlayerType;

        public GameConfig(int i_BoardSize, Player.ePlayerType i_SecondPlayerType)
        {
            m_BoardSize = i_BoardSize;
            m_SecondPlayerType = i_SecondPlayerType;
        }

        public GameConfig() : this(Board.MinBoardSize, Player.ePlayerType.Human)
        {
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }

        public Player.ePlayerType SecondPlayerType
        {
            get { return m_SecondPlayerType; }
            set { m_SecondPlayerType = value; }
        }
    }
}
