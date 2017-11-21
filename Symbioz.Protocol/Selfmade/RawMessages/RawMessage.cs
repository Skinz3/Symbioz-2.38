using SSync.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.RawMessages
{
    public abstract class RawMessage
    {
        public abstract void Deserialize(BigEndianReader reader);

        public abstract short GetMessageId();
    }
}
