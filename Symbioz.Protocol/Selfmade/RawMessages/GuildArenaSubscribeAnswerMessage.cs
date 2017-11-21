using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSync.IO;

namespace Symbioz.Protocol.Selfmade.RawMessages
{
    public class GuildArenaSubscribeAnswerMessage : RawMessage
    {
        public const short Id = 1;

        public bool Register;

        public GuildArenaSubscribeAnswerMessage()
        {

        }
        public override short GetMessageId()
        {
            return Id;
        }
        public override void Deserialize(BigEndianReader reader)
        {
            this.Register = reader.ReadBoolean();
        }
    }
}
