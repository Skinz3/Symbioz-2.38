


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeStartOkMountMessage : ExchangeStartOkMountWithOutPaddockMessage
{

public const ushort Id = 5979;
public override ushort MessageId
{
    get { return Id; }
}

public Types.MountClientData[] paddockedMountsDescription;
        

public ExchangeStartOkMountMessage()
{
}

public ExchangeStartOkMountMessage(Types.MountClientData[] stabledMountsDescription, Types.MountClientData[] paddockedMountsDescription)
         : base(stabledMountsDescription)
        {
            this.paddockedMountsDescription = paddockedMountsDescription;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)paddockedMountsDescription.Length);
            foreach (var entry in paddockedMountsDescription)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            paddockedMountsDescription = new Types.MountClientData[limit];
            for (int i = 0; i < limit; i++)
            {
                 paddockedMountsDescription[i] = new Types.MountClientData();
                 paddockedMountsDescription[i].Deserialize(reader);
            }
            

}


}


}