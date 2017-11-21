


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ObjectUseOnCharacterMessage : ObjectUseMessage
{

public const ushort Id = 3003;
public override ushort MessageId
{
    get { return Id; }
}

public ulong characterId;
        

public ObjectUseOnCharacterMessage()
{
}

public ObjectUseOnCharacterMessage(uint objectUID, ulong characterId)
         : base(objectUID)
        {
            this.characterId = characterId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhLong(characterId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            characterId = reader.ReadVarUhLong();
            if (characterId < 0 || characterId > 9007199254740990)
                throw new Exception("Forbidden value on characterId = " + characterId + ", it doesn't respect the following condition : characterId < 0 || characterId > 9007199254740990");
            

}


}


}