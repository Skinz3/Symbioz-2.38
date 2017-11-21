


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameRolePlayNpcQuestFlag
{

public const short Id = 384;
public virtual short TypeId
{
    get { return Id; }
}

public ushort[] questsToValidId;
        public ushort[] questsToStartId;
        

public GameRolePlayNpcQuestFlag()
{
}

public GameRolePlayNpcQuestFlag(ushort[] questsToValidId, ushort[] questsToStartId)
        {
            this.questsToValidId = questsToValidId;
            this.questsToStartId = questsToStartId;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)questsToValidId.Length);
            foreach (var entry in questsToValidId)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)questsToStartId.Length);
            foreach (var entry in questsToStartId)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            questsToValidId = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 questsToValidId[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            questsToStartId = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 questsToStartId[i] = reader.ReadVarUhShort();
            }
            

}


}


}