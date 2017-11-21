


















// Generated on 04/27/2016 01:13:08
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class Achievement
{

public const short Id = 363;
public virtual short TypeId
{
    get { return Id; }
}

public ushort id;
        public AchievementObjective[] finishedObjective;
        public AchievementStartedObjective[] startedObjectives;
        

public Achievement()
{
}

public Achievement(ushort id, AchievementObjective[] finishedObjective, AchievementStartedObjective[] startedObjectives)
        {
            this.id = id;
            this.finishedObjective = finishedObjective;
            this.startedObjectives = startedObjectives;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(id);
            writer.WriteUShort((ushort)finishedObjective.Length);
            foreach (var entry in finishedObjective)
            {
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)startedObjectives.Length);
            foreach (var entry in startedObjectives)
            {
                 entry.Serialize(writer);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

id = reader.ReadVarUhShort();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            var limit = reader.ReadUShort();
            finishedObjective = new AchievementObjective[limit];
            for (int i = 0; i < limit; i++)
            {
                 finishedObjective[i] = new AchievementObjective();
                 finishedObjective[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            startedObjectives = new AchievementStartedObjective[limit];
            for (int i = 0; i < limit; i++)
            {
                 startedObjectives[i] = new AchievementStartedObjective();
                 startedObjectives[i].Deserialize(reader);
            }
            

}


}


}