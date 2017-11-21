


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class AccountLoggingKickedMessage : Message
{

public const ushort Id = 6029;
public override ushort MessageId
{
    get { return Id; }
}

public ushort days;
        public sbyte hours;
        public sbyte minutes;
        

public AccountLoggingKickedMessage()
{
}

public AccountLoggingKickedMessage(ushort days, sbyte hours, sbyte minutes)
        {
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(days);
            writer.WriteSByte(hours);
            writer.WriteSByte(minutes);
            

}

public override void Deserialize(ICustomDataInput reader)
{

days = reader.ReadVarUhShort();
            if (days < 0)
                throw new Exception("Forbidden value on days = " + days + ", it doesn't respect the following condition : days < 0");
            hours = reader.ReadSByte();
            if (hours < 0)
                throw new Exception("Forbidden value on hours = " + hours + ", it doesn't respect the following condition : hours < 0");
            minutes = reader.ReadSByte();
            if (minutes < 0)
                throw new Exception("Forbidden value on minutes = " + minutes + ", it doesn't respect the following condition : minutes < 0");
            

}


}


}