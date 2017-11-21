


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class StatsUpgradeRequestMessage : Message
{

public const ushort Id = 5610;
public override ushort MessageId
{
    get { return Id; }
}

public bool useAdditionnal;
        public sbyte statId;
        public ushort boostPoint;
        

public StatsUpgradeRequestMessage()
{
}

public StatsUpgradeRequestMessage(bool useAdditionnal, sbyte statId, ushort boostPoint)
        {
            this.useAdditionnal = useAdditionnal;
            this.statId = statId;
            this.boostPoint = boostPoint;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(useAdditionnal);
            writer.WriteSByte(statId);
            writer.WriteVarUhShort(boostPoint);
            

}

public override void Deserialize(ICustomDataInput reader)
{

useAdditionnal = reader.ReadBoolean();
            statId = reader.ReadSByte();
            if (statId < 0)
                throw new Exception("Forbidden value on statId = " + statId + ", it doesn't respect the following condition : statId < 0");
            boostPoint = reader.ReadVarUhShort();
            if (boostPoint < 0)
                throw new Exception("Forbidden value on boostPoint = " + boostPoint + ", it doesn't respect the following condition : boostPoint < 0");
            

}


}


}