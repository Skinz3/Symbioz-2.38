


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SymbioticObjectErrorMessage : ObjectErrorMessage
{

public const ushort Id = 6526;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte errorCode;
        

public SymbioticObjectErrorMessage()
{
}

public SymbioticObjectErrorMessage(sbyte reason, sbyte errorCode)
         : base(reason)
        {
            this.errorCode = errorCode;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(errorCode);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            errorCode = reader.ReadSByte();
            

}


}


}