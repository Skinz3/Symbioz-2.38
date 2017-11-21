


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SequenceEndMessage : Message
{

public const ushort Id = 956;
public override ushort MessageId
{
    get { return Id; }
}

public ushort actionId;
        public double authorId;
        public sbyte sequenceType;
        

public SequenceEndMessage()
{
}

public SequenceEndMessage(ushort actionId, double authorId, sbyte sequenceType)
        {
            this.actionId = actionId;
            this.authorId = authorId;
            this.sequenceType = sequenceType;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(actionId);
            writer.WriteDouble(authorId);
            writer.WriteSByte(sequenceType);
            

}

public override void Deserialize(ICustomDataInput reader)
{

actionId = reader.ReadVarUhShort();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
            authorId = reader.ReadDouble();
            if (authorId < -9007199254740990 || authorId > 9007199254740990)
                throw new Exception("Forbidden value on authorId = " + authorId + ", it doesn't respect the following condition : authorId < -9007199254740990 || authorId > 9007199254740990");
            sequenceType = reader.ReadSByte();
            

}


}


}