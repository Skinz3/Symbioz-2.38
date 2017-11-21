using Symbioz.ProtocolBuilder.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Symbioz.ProtocolBuilder.XmlPatterns
{
    public abstract class XmlPatternBuilder
    {
        protected Parser Parser;

        protected XmlPatternBuilder(Parser parser)
        {
            Parser = parser;
        }

        public abstract void WriteToXml(XmlWriter writer);
    }
}
