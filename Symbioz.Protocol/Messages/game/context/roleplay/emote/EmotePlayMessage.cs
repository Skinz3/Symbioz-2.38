


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class EmotePlayMessage : EmotePlayAbstractMessage
{

public const ushort Id = 5683;
public override ushort MessageId
{
    get { return Id; }
}

public double actorId;
        public int accountId;
        

public EmotePlayMessage()
{
}

public EmotePlayMessage(byte emoteId, double emoteStartTime, double actorId, int accountId)
         : base(emoteId, emoteStartTime)
        {
            this.actorId = actorId;
            this.accountId = accountId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteDouble(actorId);
            writer.WriteInt(accountId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            actorId = reader.ReadDouble();
            if (actorId < -9007199254740990 || actorId > 9007199254740990)
                throw new Exception("Forbidden value on actorId = " + actorId + ", it doesn't respect the following condition : actorId < -9007199254740990 || actorId > 9007199254740990");
            accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
            

}


}


}