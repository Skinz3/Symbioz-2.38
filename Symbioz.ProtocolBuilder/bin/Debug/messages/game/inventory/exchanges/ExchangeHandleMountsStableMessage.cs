


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeHandleMountsStableMessage : Message
{

public const ushort Id = 6562;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte actionType;
        public uint[] ridesId;
        

public ExchangeHandleMountsStableMessage()
{
}

public ExchangeHandleMountsStableMessage(sbyte actionType, uint[] ridesId)
        {
            this.actionType = actionType;
            this.ridesId = ridesId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(actionType);
            writer.WriteUShort((ushort)ridesId.Length);
            foreach (var entry in ridesId)
            {
                 writer.WriteVarUhInt(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

actionType = reader.ReadSByte();
            var limit = reader.ReadUShort();
            ridesId = new uint[limit];
            for (int i = 0; i < limit; i++)
            {
                 ridesId[i] = reader.ReadVarUhInt();
            }
            

}


}


}