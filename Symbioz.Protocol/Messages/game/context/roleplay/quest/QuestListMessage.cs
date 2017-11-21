


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class QuestListMessage : Message
{

public const ushort Id = 5626;
public override ushort MessageId
{
    get { return Id; }
}

public ushort[] finishedQuestsIds;
        public ushort[] finishedQuestsCounts;
        public Types.QuestActiveInformations[] activeQuests;
        public ushort[] reinitDoneQuestsIds;
        

public QuestListMessage()
{
}

public QuestListMessage(ushort[] finishedQuestsIds, ushort[] finishedQuestsCounts, Types.QuestActiveInformations[] activeQuests, ushort[] reinitDoneQuestsIds)
        {
            this.finishedQuestsIds = finishedQuestsIds;
            this.finishedQuestsCounts = finishedQuestsCounts;
            this.activeQuests = activeQuests;
            this.reinitDoneQuestsIds = reinitDoneQuestsIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)finishedQuestsIds.Length);
            foreach (var entry in finishedQuestsIds)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)finishedQuestsCounts.Length);
            foreach (var entry in finishedQuestsCounts)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)activeQuests.Length);
            foreach (var entry in activeQuests)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)reinitDoneQuestsIds.Length);
            foreach (var entry in reinitDoneQuestsIds)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            finishedQuestsIds = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 finishedQuestsIds[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            finishedQuestsCounts = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 finishedQuestsCounts[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            activeQuests = new Types.QuestActiveInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 activeQuests[i] = Types.ProtocolTypeManager.GetInstance<Types.QuestActiveInformations>(reader.ReadShort());
                 activeQuests[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            reinitDoneQuestsIds = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 reinitDoneQuestsIds[i] = reader.ReadVarUhShort();
            }
            

}


}


}