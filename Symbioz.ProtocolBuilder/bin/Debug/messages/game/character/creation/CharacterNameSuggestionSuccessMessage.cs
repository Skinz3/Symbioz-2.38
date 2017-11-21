


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CharacterNameSuggestionSuccessMessage : Message
{

public const ushort Id = 5544;
public override ushort MessageId
{
    get { return Id; }
}

public string suggestion;
        

public CharacterNameSuggestionSuccessMessage()
{
}

public CharacterNameSuggestionSuccessMessage(string suggestion)
        {
            this.suggestion = suggestion;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(suggestion);
            

}

public override void Deserialize(ICustomDataInput reader)
{

suggestion = reader.ReadUTF();
            

}


}


}