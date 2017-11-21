using SSync.IO;
using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class OnCharacterCreationResultMessage : TransitionMessage
    {
        public static ushort Id = 6709;

        public override ushort MessageId
        {
            get { return Id; }
        }
        public bool Succes;

        public OnCharacterCreationResultMessage() { }

        public OnCharacterCreationResultMessage(bool succes)
        {
            this.Succes = succes;
        }

        public override void Serialize(ICustomDataOutput writer)
        {
            writer.WriteBoolean(Succes);
        }

        public override void Deserialize(ICustomDataInput reader)
        {
            this.Succes = reader.ReadBoolean();
        }
    }
}
