


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayArenaFightPropositionMessage : Message
{

public const ushort Id = 6276;
public override ushort MessageId
{
    get { return Id; }
}

public int fightId;
        public double[] alliesId;
        public ushort duration;
        

public GameRolePlayArenaFightPropositionMessage()
{
}

public GameRolePlayArenaFightPropositionMessage(int fightId, double[] alliesId, ushort duration)
        {
            this.fightId = fightId;
            this.alliesId = alliesId;
            this.duration = duration;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(fightId);
            writer.WriteUShort((ushort)alliesId.Length);
            foreach (var entry in alliesId)
            {
                 writer.WriteDouble(entry);
            }
            writer.WriteVarUhShort(duration);
            

}

public override void Deserialize(ICustomDataInput reader)
{

fightId = reader.ReadInt();
            if (fightId < 0)
                throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
            var limit = reader.ReadUShort();
            alliesId = new double[limit];
            for (int i = 0; i < limit; i++)
            {
                 alliesId[i] = reader.ReadDouble();
            }
            duration = reader.ReadVarUhShort();
            if (duration < 0)
                throw new Exception("Forbidden value on duration = " + duration + ", it doesn't respect the following condition : duration < 0");
            

}


}


}