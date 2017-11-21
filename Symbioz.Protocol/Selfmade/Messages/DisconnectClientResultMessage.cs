using SSync.IO;
using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class DisconnectClientResultMessage : TransitionMessage
    {
        public const ushort Id = 6706;

        public override ushort MessageId
        {
            get { return Id; }
        }

        public bool IsSucces;

        public DisconnectClientResultMessage() { }

        public DisconnectClientResultMessage(bool isSucces)
        {
            this.IsSucces = isSucces;
        }
        public override void Serialize(ICustomDataOutput writer)
        {
            writer.WriteBoolean(IsSucces);
        }

        public override void Deserialize(ICustomDataInput reader)
        {
            this.IsSucces = reader.ReadBoolean();
        }
    }
}
