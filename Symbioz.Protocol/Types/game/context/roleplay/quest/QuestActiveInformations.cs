


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class QuestActiveInformations
{

public const short Id = 381;
public virtual short TypeId
{
    get { return Id; }
}

public ushort questId;
        

public QuestActiveInformations()
{
}

public QuestActiveInformations(ushort questId)
        {
            this.questId = questId;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(questId);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

questId = reader.ReadVarUhShort();
            if (questId < 0)
                throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0");
            

}


}


}