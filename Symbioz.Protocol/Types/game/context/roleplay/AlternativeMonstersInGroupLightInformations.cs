


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class AlternativeMonstersInGroupLightInformations
{

public const short Id = 394;
public virtual short TypeId
{
    get { return Id; }
}

public int playerCount;
        public MonsterInGroupLightInformations[] monsters;
        

public AlternativeMonstersInGroupLightInformations()
{
}

public AlternativeMonstersInGroupLightInformations(int playerCount, MonsterInGroupLightInformations[] monsters)
        {
            this.playerCount = playerCount;
            this.monsters = monsters;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(playerCount);
            writer.WriteUShort((ushort)monsters.Length);
            foreach (var entry in monsters)
            {
                 entry.Serialize(writer);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

playerCount = reader.ReadInt();
            var limit = reader.ReadUShort();
            monsters = new MonsterInGroupLightInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 monsters[i] = new MonsterInGroupLightInformations();
                 monsters[i].Deserialize(reader);
            }
            

}


}


}