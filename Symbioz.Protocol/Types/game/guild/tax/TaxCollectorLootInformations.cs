


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class TaxCollectorLootInformations : TaxCollectorComplementaryInformations
{

public const short Id = 372;
public override short TypeId
{
    get { return Id; }
}

public uint kamas;
        public ulong experience;
        public uint pods;
        public uint itemsValue;
        

public TaxCollectorLootInformations()
{
}

public TaxCollectorLootInformations(uint kamas, ulong experience, uint pods, uint itemsValue)
        {
            this.kamas = kamas;
            this.experience = experience;
            this.pods = pods;
            this.itemsValue = itemsValue;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhInt(kamas);
            writer.WriteVarUhLong(experience);
            writer.WriteVarUhInt(pods);
            writer.WriteVarUhInt(itemsValue);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            kamas = reader.ReadVarUhInt();
            if (kamas < 0)
                throw new Exception("Forbidden value on kamas = " + kamas + ", it doesn't respect the following condition : kamas < 0");
            experience = reader.ReadVarUhLong();
            if (experience < 0 || experience > 9007199254740990)
                throw new Exception("Forbidden value on experience = " + experience + ", it doesn't respect the following condition : experience < 0 || experience > 9007199254740990");
            pods = reader.ReadVarUhInt();
            if (pods < 0)
                throw new Exception("Forbidden value on pods = " + pods + ", it doesn't respect the following condition : pods < 0");
            itemsValue = reader.ReadVarUhInt();
            if (itemsValue < 0)
                throw new Exception("Forbidden value on itemsValue = " + itemsValue + ", it doesn't respect the following condition : itemsValue < 0");
            

}


}


}