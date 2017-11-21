using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.ProtocolBuilder.XmlPatterns
{
    public class XmlType
    {
        public string Name
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public string Heritage
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public XmlField[] Fields
        {
            get;
            set;
        }
    }
}
