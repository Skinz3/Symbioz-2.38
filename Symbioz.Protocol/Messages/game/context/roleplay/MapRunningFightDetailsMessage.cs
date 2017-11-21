


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MapRunningFightDetailsMessage : Message
{

public const ushort Id = 5751;
public override ushort MessageId
{
    get { return Id; }
}

public int fightId;
        public Types.GameFightFighterLightInformations[] attackers;
        public Types.GameFightFighterLightInformations[] defenders;
        

public MapRunningFightDetailsMessage()
{
}

public MapRunningFightDetailsMessage(int fightId, Types.GameFightFighterLightInformations[] attackers, Types.GameFightFighterLightInformations[] defenders)
        {
            this.fightId = fightId;
            this.attackers = attackers;
            this.defenders = defenders;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(fightId);
            writer.WriteUShort((ushort)attackers.Length);
            foreach (var entry in attackers)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)defenders.Length);
            foreach (var entry in defenders)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

fightId = reader.ReadInt();
            if (fightId < 0)
                throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
            var limit = reader.ReadUShort();
            attackers = new Types.GameFightFighterLightInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 attackers[i] = Types.ProtocolTypeManager.GetInstance<Types.GameFightFighterLightInformations>(reader.ReadShort());
                 attackers[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            defenders = new Types.GameFightFighterLightInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 defenders[i] = Types.ProtocolTypeManager.GetInstance<Types.GameFightFighterLightInformations>(reader.ReadShort());
                 defenders[i].Deserialize(reader);
            }
            

}


}


}