


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildInfosUpgradeMessage : Message
{

public const ushort Id = 5636;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte maxTaxCollectorsCount;
        public sbyte taxCollectorsCount;
        public ushort taxCollectorLifePoints;
        public ushort taxCollectorDamagesBonuses;
        public ushort taxCollectorPods;
        public ushort taxCollectorProspecting;
        public ushort taxCollectorWisdom;
        public ushort boostPoints;
        public ushort[] spellId;
        public sbyte[] spellLevel;
        

public GuildInfosUpgradeMessage()
{
}

public GuildInfosUpgradeMessage(sbyte maxTaxCollectorsCount, sbyte taxCollectorsCount, ushort taxCollectorLifePoints, ushort taxCollectorDamagesBonuses, ushort taxCollectorPods, ushort taxCollectorProspecting, ushort taxCollectorWisdom, ushort boostPoints, ushort[] spellId, sbyte[] spellLevel)
        {
            this.maxTaxCollectorsCount = maxTaxCollectorsCount;
            this.taxCollectorsCount = taxCollectorsCount;
            this.taxCollectorLifePoints = taxCollectorLifePoints;
            this.taxCollectorDamagesBonuses = taxCollectorDamagesBonuses;
            this.taxCollectorPods = taxCollectorPods;
            this.taxCollectorProspecting = taxCollectorProspecting;
            this.taxCollectorWisdom = taxCollectorWisdom;
            this.boostPoints = boostPoints;
            this.spellId = spellId;
            this.spellLevel = spellLevel;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(maxTaxCollectorsCount);
            writer.WriteSByte(taxCollectorsCount);
            writer.WriteVarUhShort(taxCollectorLifePoints);
            writer.WriteVarUhShort(taxCollectorDamagesBonuses);
            writer.WriteVarUhShort(taxCollectorPods);
            writer.WriteVarUhShort(taxCollectorProspecting);
            writer.WriteVarUhShort(taxCollectorWisdom);
            writer.WriteVarUhShort(boostPoints);
            writer.WriteUShort((ushort)spellId.Length);
            foreach (var entry in spellId)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)spellLevel.Length);
            foreach (var entry in spellLevel)
            {
                 writer.WriteSByte(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

maxTaxCollectorsCount = reader.ReadSByte();
            if (maxTaxCollectorsCount < 0)
                throw new Exception("Forbidden value on maxTaxCollectorsCount = " + maxTaxCollectorsCount + ", it doesn't respect the following condition : maxTaxCollectorsCount < 0");
            taxCollectorsCount = reader.ReadSByte();
            if (taxCollectorsCount < 0)
                throw new Exception("Forbidden value on taxCollectorsCount = " + taxCollectorsCount + ", it doesn't respect the following condition : taxCollectorsCount < 0");
            taxCollectorLifePoints = reader.ReadVarUhShort();
            if (taxCollectorLifePoints < 0)
                throw new Exception("Forbidden value on taxCollectorLifePoints = " + taxCollectorLifePoints + ", it doesn't respect the following condition : taxCollectorLifePoints < 0");
            taxCollectorDamagesBonuses = reader.ReadVarUhShort();
            if (taxCollectorDamagesBonuses < 0)
                throw new Exception("Forbidden value on taxCollectorDamagesBonuses = " + taxCollectorDamagesBonuses + ", it doesn't respect the following condition : taxCollectorDamagesBonuses < 0");
            taxCollectorPods = reader.ReadVarUhShort();
            if (taxCollectorPods < 0)
                throw new Exception("Forbidden value on taxCollectorPods = " + taxCollectorPods + ", it doesn't respect the following condition : taxCollectorPods < 0");
            taxCollectorProspecting = reader.ReadVarUhShort();
            if (taxCollectorProspecting < 0)
                throw new Exception("Forbidden value on taxCollectorProspecting = " + taxCollectorProspecting + ", it doesn't respect the following condition : taxCollectorProspecting < 0");
            taxCollectorWisdom = reader.ReadVarUhShort();
            if (taxCollectorWisdom < 0)
                throw new Exception("Forbidden value on taxCollectorWisdom = " + taxCollectorWisdom + ", it doesn't respect the following condition : taxCollectorWisdom < 0");
            boostPoints = reader.ReadVarUhShort();
            if (boostPoints < 0)
                throw new Exception("Forbidden value on boostPoints = " + boostPoints + ", it doesn't respect the following condition : boostPoints < 0");
            var limit = reader.ReadUShort();
            spellId = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 spellId[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            spellLevel = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 spellLevel[i] = reader.ReadSByte();
            }
            

}


}


}