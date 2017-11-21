


















// Generated on 04/27/2016 01:13:18
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class AllianceInsiderPrismInformation : PrismInformation
{

public const short Id = 431;
public override short TypeId
{
    get { return Id; }
}

public int lastTimeSlotModificationDate;
        public uint lastTimeSlotModificationAuthorGuildId;
        public ulong lastTimeSlotModificationAuthorId;
        public string lastTimeSlotModificationAuthorName;
        public Types.ObjectItem[] modulesObjects;
        

public AllianceInsiderPrismInformation()
{
}

public AllianceInsiderPrismInformation(sbyte typeId, sbyte state, int nextVulnerabilityDate, int placementDate, uint rewardTokenCount, int lastTimeSlotModificationDate, uint lastTimeSlotModificationAuthorGuildId, ulong lastTimeSlotModificationAuthorId, string lastTimeSlotModificationAuthorName, Types.ObjectItem[] modulesObjects)
         : base(typeId, state, nextVulnerabilityDate, placementDate, rewardTokenCount)
        {
            this.lastTimeSlotModificationDate = lastTimeSlotModificationDate;
            this.lastTimeSlotModificationAuthorGuildId = lastTimeSlotModificationAuthorGuildId;
            this.lastTimeSlotModificationAuthorId = lastTimeSlotModificationAuthorId;
            this.lastTimeSlotModificationAuthorName = lastTimeSlotModificationAuthorName;
            this.modulesObjects = modulesObjects;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteInt(lastTimeSlotModificationDate);
            writer.WriteVarUhInt(lastTimeSlotModificationAuthorGuildId);
            writer.WriteVarUhLong(lastTimeSlotModificationAuthorId);
            writer.WriteUTF(lastTimeSlotModificationAuthorName);
            writer.WriteUShort((ushort)modulesObjects.Length);
            foreach (var entry in modulesObjects)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            lastTimeSlotModificationDate = reader.ReadInt();
            if (lastTimeSlotModificationDate < 0)
                throw new Exception("Forbidden value on lastTimeSlotModificationDate = " + lastTimeSlotModificationDate + ", it doesn't respect the following condition : lastTimeSlotModificationDate < 0");
            lastTimeSlotModificationAuthorGuildId = reader.ReadVarUhInt();
            if (lastTimeSlotModificationAuthorGuildId < 0)
                throw new Exception("Forbidden value on lastTimeSlotModificationAuthorGuildId = " + lastTimeSlotModificationAuthorGuildId + ", it doesn't respect the following condition : lastTimeSlotModificationAuthorGuildId < 0");
            lastTimeSlotModificationAuthorId = reader.ReadVarUhLong();
            if (lastTimeSlotModificationAuthorId < 0 || lastTimeSlotModificationAuthorId > 9007199254740990)
                throw new Exception("Forbidden value on lastTimeSlotModificationAuthorId = " + lastTimeSlotModificationAuthorId + ", it doesn't respect the following condition : lastTimeSlotModificationAuthorId < 0 || lastTimeSlotModificationAuthorId > 9007199254740990");
            lastTimeSlotModificationAuthorName = reader.ReadUTF();
            var limit = reader.ReadUShort();
            modulesObjects = new Types.ObjectItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 modulesObjects[i] = new Types.ObjectItem();
                 modulesObjects[i].Deserialize(reader);
            }
            

}


}


}