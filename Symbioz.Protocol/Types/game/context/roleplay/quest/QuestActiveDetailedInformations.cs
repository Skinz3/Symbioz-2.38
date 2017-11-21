


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class QuestActiveDetailedInformations : QuestActiveInformations
{

public const short Id = 382;
public override short TypeId
{
    get { return Id; }
}

public ushort stepId;
        public QuestObjectiveInformations[] objectives;
        

public QuestActiveDetailedInformations()
{
}

public QuestActiveDetailedInformations(ushort questId, ushort stepId, QuestObjectiveInformations[] objectives)
         : base(questId)
        {
            this.stepId = stepId;
            this.objectives = objectives;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(stepId);
            writer.WriteUShort((ushort)objectives.Length);
            foreach (var entry in objectives)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            stepId = reader.ReadVarUhShort();
            if (stepId < 0)
                throw new Exception("Forbidden value on stepId = " + stepId + ", it doesn't respect the following condition : stepId < 0");
            var limit = reader.ReadUShort();
            objectives = new QuestObjectiveInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectives[i] = Types.ProtocolTypeManager.GetInstance<QuestObjectiveInformations>(reader.ReadShort());
                 objectives[i].Deserialize(reader);
            }
            

}


}


}