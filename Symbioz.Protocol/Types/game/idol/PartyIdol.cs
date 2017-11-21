


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PartyIdol : Idol
{

public const short Id = 490;
public override short TypeId
{
    get { return Id; }
}

public ulong[] ownersIds;
        

public PartyIdol()
{
}

public PartyIdol(ushort id, ushort xpBonusPercent, ushort dropBonusPercent, ulong[] ownersIds)
         : base(id, xpBonusPercent, dropBonusPercent)
        {
            this.ownersIds = ownersIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)ownersIds.Length);
            foreach (var entry in ownersIds)
            {
                 writer.WriteVarUhLong(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            ownersIds = new ulong[limit];
            for (int i = 0; i < limit; i++)
            {
                 ownersIds[i] = reader.ReadVarUhLong();
            }
            

}


}


}