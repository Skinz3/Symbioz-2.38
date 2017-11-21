using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.DofusConfig.LineParser
{
    public struct ConfigLineField
    {
        public string m_key;

        public string m_value;

        public int LineIndex
        {
            get;
            private set;
        }

        public string Line;

        public string Key
        {
            get
            {
                return m_key;
            }
            set
            {
                m_key = value;
                Update();
            }
        }

        public string Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
                Update();
            }
        }

        private DofusConfig Config
        {
            get;
            set;
        }
        public ConfigLineField(DofusConfig config, string line, int lineIndex)
        {
            Line = line;
            LineIndex = lineIndex;
            Config = config;
            m_key = Line.Split('\"')[1];
            m_value = Line.Split('>')[1].Split('<')[0];
        }
        private void Update()
        {
            Line = string.Format("\t<entry key=\"{0}\">{1}</entry>", Key, Value);
            Config.Lines[LineIndex] = Line;
        }

        internal static bool IsValid(string line)
        {
            if (line.Contains("</entry>") && line.Contains("key="))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
