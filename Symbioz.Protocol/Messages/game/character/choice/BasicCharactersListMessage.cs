


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class BasicCharactersListMessage : Message
{

public const ushort Id = 6475;
public override ushort MessageId
{
    get { return Id; }
}

public Types.CharacterBaseInformations[] characters;
        

public BasicCharactersListMessage()
{
}

public BasicCharactersListMessage(Types.CharacterBaseInformations[] characters)
        {
            this.characters = characters;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)characters.Length);
            foreach (var entry in characters)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            characters = new Types.CharacterBaseInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 characters[i] = Types.ProtocolTypeManager.GetInstance<Types.CharacterBaseInformations>(reader.ReadShort());
                 characters[i].Deserialize(reader);
            }
            

}


}


}