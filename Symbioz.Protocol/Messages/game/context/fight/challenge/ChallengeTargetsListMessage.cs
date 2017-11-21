


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ChallengeTargetsListMessage : Message
{

public const ushort Id = 5613;
public override ushort MessageId
{
    get { return Id; }
}

public double[] targetIds;
        public short[] targetCells;
        

public ChallengeTargetsListMessage()
{
}

public ChallengeTargetsListMessage(double[] targetIds, short[] targetCells)
        {
            this.targetIds = targetIds;
            this.targetCells = targetCells;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)targetIds.Length);
            foreach (var entry in targetIds)
            {
                 writer.WriteDouble(entry);
            }
            writer.WriteUShort((ushort)targetCells.Length);
            foreach (var entry in targetCells)
            {
                 writer.WriteShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            targetIds = new double[limit];
            for (int i = 0; i < limit; i++)
            {
                 targetIds[i] = reader.ReadDouble();
            }
            limit = reader.ReadUShort();
            targetCells = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 targetCells[i] = reader.ReadShort();
            }
            

}


}


}