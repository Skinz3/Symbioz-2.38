


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class RecycleResultMessage : Message
{

public const ushort Id = 6601;
public override ushort MessageId
{
    get { return Id; }
}

public uint nuggetsForPrism;
        public uint nuggetsForPlayer;
        

public RecycleResultMessage()
{
}

public RecycleResultMessage(uint nuggetsForPrism, uint nuggetsForPlayer)
        {
            this.nuggetsForPrism = nuggetsForPrism;
            this.nuggetsForPlayer = nuggetsForPlayer;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhInt(nuggetsForPrism);
            writer.WriteVarUhInt(nuggetsForPlayer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

nuggetsForPrism = reader.ReadVarUhInt();
            if (nuggetsForPrism < 0)
                throw new Exception("Forbidden value on nuggetsForPrism = " + nuggetsForPrism + ", it doesn't respect the following condition : nuggetsForPrism < 0");
            nuggetsForPlayer = reader.ReadVarUhInt();
            if (nuggetsForPlayer < 0)
                throw new Exception("Forbidden value on nuggetsForPlayer = " + nuggetsForPlayer + ", it doesn't respect the following condition : nuggetsForPlayer < 0");
            

}


}


}