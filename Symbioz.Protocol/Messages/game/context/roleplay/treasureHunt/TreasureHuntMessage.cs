


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TreasureHuntMessage : Message
{

public const ushort Id = 6486;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte questType;
        public int startMapId;
        public Types.TreasureHuntStep[] knownStepsList;
        public sbyte totalStepCount;
        public uint checkPointCurrent;
        public uint checkPointTotal;
        public int availableRetryCount;
        public Types.TreasureHuntFlag[] flags;
        

public TreasureHuntMessage()
{
}

public TreasureHuntMessage(sbyte questType, int startMapId, Types.TreasureHuntStep[] knownStepsList, sbyte totalStepCount, uint checkPointCurrent, uint checkPointTotal, int availableRetryCount, Types.TreasureHuntFlag[] flags)
        {
            this.questType = questType;
            this.startMapId = startMapId;
            this.knownStepsList = knownStepsList;
            this.totalStepCount = totalStepCount;
            this.checkPointCurrent = checkPointCurrent;
            this.checkPointTotal = checkPointTotal;
            this.availableRetryCount = availableRetryCount;
            this.flags = flags;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(questType);
            writer.WriteInt(startMapId);
            writer.WriteUShort((ushort)knownStepsList.Length);
            foreach (var entry in knownStepsList)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            writer.WriteSByte(totalStepCount);
            writer.WriteVarUhInt(checkPointCurrent);
            writer.WriteVarUhInt(checkPointTotal);
            writer.WriteInt(availableRetryCount);
            writer.WriteUShort((ushort)flags.Length);
            foreach (var entry in flags)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

questType = reader.ReadSByte();
            if (questType < 0)
                throw new Exception("Forbidden value on questType = " + questType + ", it doesn't respect the following condition : questType < 0");
            startMapId = reader.ReadInt();
            var limit = reader.ReadUShort();
            knownStepsList = new Types.TreasureHuntStep[limit];
            for (int i = 0; i < limit; i++)
            {
                 knownStepsList[i] = Types.ProtocolTypeManager.GetInstance<Types.TreasureHuntStep>(reader.ReadShort());
                 knownStepsList[i].Deserialize(reader);
            }
            totalStepCount = reader.ReadSByte();
            if (totalStepCount < 0)
                throw new Exception("Forbidden value on totalStepCount = " + totalStepCount + ", it doesn't respect the following condition : totalStepCount < 0");
            checkPointCurrent = reader.ReadVarUhInt();
            if (checkPointCurrent < 0)
                throw new Exception("Forbidden value on checkPointCurrent = " + checkPointCurrent + ", it doesn't respect the following condition : checkPointCurrent < 0");
            checkPointTotal = reader.ReadVarUhInt();
            if (checkPointTotal < 0)
                throw new Exception("Forbidden value on checkPointTotal = " + checkPointTotal + ", it doesn't respect the following condition : checkPointTotal < 0");
            availableRetryCount = reader.ReadInt();
            limit = reader.ReadUShort();
            flags = new Types.TreasureHuntFlag[limit];
            for (int i = 0; i < limit; i++)
            {
                 flags[i] = new Types.TreasureHuntFlag();
                 flags[i].Deserialize(reader);
            }
            

}


}


}