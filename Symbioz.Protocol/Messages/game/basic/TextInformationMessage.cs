


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class TextInformationMessage : Message
{

public const ushort Id = 780;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte msgType;
        public ushort msgId;
        public string[] parameters;
        

public TextInformationMessage()
{
}

public TextInformationMessage(sbyte msgType, ushort msgId, string[] parameters)
        {
            this.msgType = msgType;
            this.msgId = msgId;
            this.parameters = parameters;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(msgType);
            writer.WriteVarUhShort(msgId);
            writer.WriteUShort((ushort)parameters.Length);
            foreach (var entry in parameters)
            {
                 writer.WriteUTF(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

msgType = reader.ReadSByte();
            if (msgType < 0)
                throw new Exception("Forbidden value on msgType = " + msgType + ", it doesn't respect the following condition : msgType < 0");
            msgId = reader.ReadVarUhShort();
            if (msgId < 0)
                throw new Exception("Forbidden value on msgId = " + msgId + ", it doesn't respect the following condition : msgId < 0");
            var limit = reader.ReadUShort();
            parameters = new string[limit];
            for (int i = 0; i < limit; i++)
            {
                 parameters[i] = reader.ReadUTF();
            }
            

}


}


}