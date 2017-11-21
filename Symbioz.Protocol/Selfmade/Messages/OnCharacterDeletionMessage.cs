using SSync.IO;
using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class OnCharacterDeletionMessage : TransitionMessage
    {
        public const ushort Id = 6711;

        public override ushort MessageId
        {
            get { return Id; }
        }

        public long CharacterId;

        public OnCharacterDeletionMessage()
        {

        }
        public OnCharacterDeletionMessage(long characterId)
        {
            this.CharacterId = characterId;
        }
        public override void Serialize(ICustomDataOutput writer)
        {
            writer.WriteLong(CharacterId);
        }

        public override void Deserialize(ICustomDataInput reader)
        {
            this.CharacterId = reader.ReadLong();
        }
    }
}
