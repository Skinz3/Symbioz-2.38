


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CharacterLevelUpMessage : Message
{

public const ushort Id = 5670;
public override ushort MessageId
{
    get { return Id; }
}

public byte newLevel;
        

public CharacterLevelUpMessage()
{
}

public CharacterLevelUpMessage(byte newLevel)
        {
            this.newLevel = newLevel;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteByte(newLevel);
            

}

public override void Deserialize(ICustomDataInput reader)
{

newLevel = reader.ReadByte();
            if (newLevel < 2 || newLevel > 200)
                throw new Exception("Forbidden value on newLevel = " + newLevel + ", it doesn't respect the following condition : newLevel < 2 || newLevel > 200");
            

}


}


}