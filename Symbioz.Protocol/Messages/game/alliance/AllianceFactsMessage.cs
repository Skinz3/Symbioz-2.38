


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AllianceFactsMessage : Message
{

public const ushort Id = 6414;
public override ushort MessageId
{
    get { return Id; }
}

public Types.AllianceFactSheetInformations infos;
        public Types.GuildInAllianceInformations[] guilds;
        public ushort[] controlledSubareaIds;
        public ulong leaderCharacterId;
        public string leaderCharacterName;
        

public AllianceFactsMessage()
{
}

public AllianceFactsMessage(Types.AllianceFactSheetInformations infos, Types.GuildInAllianceInformations[] guilds, ushort[] controlledSubareaIds, ulong leaderCharacterId, string leaderCharacterName)
        {
            this.infos = infos;
            this.guilds = guilds;
            this.controlledSubareaIds = controlledSubareaIds;
            this.leaderCharacterId = leaderCharacterId;
            this.leaderCharacterName = leaderCharacterName;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteShort(infos.TypeId);
            infos.Serialize(writer);
            writer.WriteUShort((ushort)guilds.Length);
            foreach (var entry in guilds)
            {
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)controlledSubareaIds.Length);
            foreach (var entry in controlledSubareaIds)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteVarUhLong(leaderCharacterId);
            writer.WriteUTF(leaderCharacterName);
            

}

public override void Deserialize(ICustomDataInput reader)
{

infos = Types.ProtocolTypeManager.GetInstance<Types.AllianceFactSheetInformations>(reader.ReadShort());
            infos.Deserialize(reader);
            var limit = reader.ReadUShort();
            guilds = new Types.GuildInAllianceInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 guilds[i] = new Types.GuildInAllianceInformations();
                 guilds[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            controlledSubareaIds = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 controlledSubareaIds[i] = reader.ReadVarUhShort();
            }
            leaderCharacterId = reader.ReadVarUhLong();
            if (leaderCharacterId < 0 || leaderCharacterId > 9007199254740990)
                throw new Exception("Forbidden value on leaderCharacterId = " + leaderCharacterId + ", it doesn't respect the following condition : leaderCharacterId < 0 || leaderCharacterId > 9007199254740990");
            leaderCharacterName = reader.ReadUTF();
            

}


}


}