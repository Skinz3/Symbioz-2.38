using SSync.IO;
using System.Collections.Generic;


namespace Symbioz.Tools.D2O
{
    public class GameDataProcess
    {
        #region Attributs
        private readonly BigEndianReader m_Reader;
        private List<string> m_QueryableField;
        private Dictionary<string, int> m_SearchFieldIndex;
        private Dictionary<string, int> m_SearchFieldType;
        private Dictionary<string, int> m_SearchFieldCount;
        #endregion

        #region Constructeurs
        public GameDataProcess(BigEndianReader reader)
        {
            m_Reader = reader;

            ParseStream();
        }
        #endregion

        #region Méthodes privées
        private void ParseStream()
        {
            m_QueryableField = new List<string>();
            m_SearchFieldIndex = new Dictionary<string, int>();
            m_SearchFieldType = new Dictionary<string, int>();
            m_SearchFieldCount = new Dictionary<string, int>();

            int position = m_Reader.ReadInt();
            int seachIndex = (int) (m_Reader.Position + position + 4);

            while (position > 0)
            {
                int bytesAvaible = (int) m_Reader.BytesAvailable;
                string field = m_Reader.ReadUTF();

                m_QueryableField.Add(field);
                m_SearchFieldIndex.Add(field, m_Reader.ReadInt() + seachIndex);
                m_SearchFieldType.Add(field, m_Reader.ReadInt());
                m_SearchFieldCount.Add(field, m_Reader.ReadInt());

                position -= (int)(bytesAvaible - m_Reader.BytesAvailable);
            }
        }
        #endregion
    }
}
