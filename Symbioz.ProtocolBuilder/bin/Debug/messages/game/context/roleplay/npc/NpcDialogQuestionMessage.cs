


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class NpcDialogQuestionMessage : Message
{

public const ushort Id = 5617;
public override ushort MessageId
{
    get { return Id; }
}

public ushort messageId;
        public string[] dialogParams;
        public ushort[] visibleReplies;
        

public NpcDialogQuestionMessage()
{
}

public NpcDialogQuestionMessage(ushort messageId, string[] dialogParams, ushort[] visibleReplies)
        {
            this.messageId = messageId;
            this.dialogParams = dialogParams;
            this.visibleReplies = visibleReplies;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(messageId);
            writer.WriteUShort((ushort)dialogParams.Length);
            foreach (var entry in dialogParams)
            {
                 writer.WriteUTF(entry);
            }
            writer.WriteUShort((ushort)visibleReplies.Length);
            foreach (var entry in visibleReplies)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

messageId = reader.ReadVarUhShort();
            if (messageId < 0)
                throw new Exception("Forbidden value on messageId = " + messageId + ", it doesn't respect the following condition : messageId < 0");
            var limit = reader.ReadUShort();
            dialogParams = new string[limit];
            for (int i = 0; i < limit; i++)
            {
                 dialogParams[i] = reader.ReadUTF();
            }
            limit = reader.ReadUShort();
            visibleReplies = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 visibleReplies[i] = reader.ReadVarUhShort();
            }
            

}


}


}