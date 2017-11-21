


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class StartupActionFinishedMessage : Message
{

public const ushort Id = 1304;
public override ushort MessageId
{
    get { return Id; }
}

public bool success;
        public bool automaticAction;
        public int actionId;
        

public StartupActionFinishedMessage()
{
}

public StartupActionFinishedMessage(bool success, bool automaticAction, int actionId)
        {
            this.success = success;
            this.automaticAction = automaticAction;
            this.actionId = actionId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, success);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, automaticAction);
            writer.WriteByte(flag1);
            writer.WriteInt(actionId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

byte flag1 = reader.ReadByte();
            success = BooleanByteWrapper.GetFlag(flag1, 0);
            automaticAction = BooleanByteWrapper.GetFlag(flag1, 1);
            actionId = reader.ReadInt();
            if (actionId < 0)
                throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
            

}


}


}