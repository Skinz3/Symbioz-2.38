


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GameRolePlayArenaRegistrationStatusMessage : Message
{

public const ushort Id = 6284;
public override ushort MessageId
{
    get { return Id; }
}

public bool registered;
        public sbyte step;
        public int battleMode;
        

public GameRolePlayArenaRegistrationStatusMessage()
{
}

public GameRolePlayArenaRegistrationStatusMessage(bool registered, sbyte step, int battleMode)
        {
            this.registered = registered;
            this.step = step;
            this.battleMode = battleMode;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(registered);
            writer.WriteSByte(step);
            writer.WriteInt(battleMode);
            

}

public override void Deserialize(ICustomDataInput reader)
{

registered = reader.ReadBoolean();
            step = reader.ReadSByte();
            if (step < 0)
                throw new Exception("Forbidden value on step = " + step + ", it doesn't respect the following condition : step < 0");
            battleMode = reader.ReadInt();
            if (battleMode < 0)
                throw new Exception("Forbidden value on battleMode = " + battleMode + ", it doesn't respect the following condition : battleMode < 0");
            

}


}


}