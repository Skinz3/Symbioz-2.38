


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CharactersListWithRemodelingMessage : CharactersListMessage
{

public const ushort Id = 6550;
public override ushort MessageId
{
    get { return Id; }
}

public Types.CharacterToRemodelInformations[] charactersToRemodel;
        

public CharactersListWithRemodelingMessage()
{
}

public CharactersListWithRemodelingMessage(Types.CharacterBaseInformations[] characters, bool hasStartupActions, Types.CharacterToRemodelInformations[] charactersToRemodel)
         : base(characters, hasStartupActions)
        {
            this.charactersToRemodel = charactersToRemodel;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)charactersToRemodel.Length);
            foreach (var entry in charactersToRemodel)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            charactersToRemodel = new Types.CharacterToRemodelInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 charactersToRemodel[i] = new Types.CharacterToRemodelInformations();
                 charactersToRemodel[i].Deserialize(reader);
            }
            

}


}


}