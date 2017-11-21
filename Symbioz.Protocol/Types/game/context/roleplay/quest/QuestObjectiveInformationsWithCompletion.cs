


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class QuestObjectiveInformationsWithCompletion : QuestObjectiveInformations
{

public const short Id = 386;
public override short TypeId
{
    get { return Id; }
}

public ushort curCompletion;
        public ushort maxCompletion;
        

public QuestObjectiveInformationsWithCompletion()
{
}

public QuestObjectiveInformationsWithCompletion(ushort objectiveId, bool objectiveStatus, string[] dialogParams, ushort curCompletion, ushort maxCompletion)
         : base(objectiveId, objectiveStatus, dialogParams)
        {
            this.curCompletion = curCompletion;
            this.maxCompletion = maxCompletion;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(curCompletion);
            writer.WriteVarUhShort(maxCompletion);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            curCompletion = reader.ReadVarUhShort();
            if (curCompletion < 0)
                throw new Exception("Forbidden value on curCompletion = " + curCompletion + ", it doesn't respect the following condition : curCompletion < 0");
            maxCompletion = reader.ReadVarUhShort();
            if (maxCompletion < 0)
                throw new Exception("Forbidden value on maxCompletion = " + maxCompletion + ", it doesn't respect the following condition : maxCompletion < 0");
            

}


}


}