using SSync.IO;
using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class OnCharacterCreationMessage : TransitionMessage
    {
        public const ushort Id = 6703;

        public override ushort MessageId
        {
            get { return Id; ; }
        }

        public int AccountId;

        public long CharacterId;

        public OnCharacterCreationMessage(int accountId,long characterId)
        {
            this.AccountId = accountId;
            this.CharacterId = characterId;
        }

        public OnCharacterCreationMessage() { }

        public override void Serialize(ICustomDataOutput writer)
        {
            writer.WriteVarInt(AccountId);
            writer.WriteLong(CharacterId);
        }

        public override void Deserialize(ICustomDataInput reader)
        {
            this.AccountId = reader.ReadVarInt();
            this.CharacterId = reader.ReadLong();
        }
    }
}
