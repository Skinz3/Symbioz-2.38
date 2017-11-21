


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CharacterExperienceGainMessage : Message
{

public const ushort Id = 6321;
public override ushort MessageId
{
    get { return Id; }
}

public ulong experienceCharacter;
        public ulong experienceMount;
        public ulong experienceGuild;
        public ulong experienceIncarnation;
        

public CharacterExperienceGainMessage()
{
}

public CharacterExperienceGainMessage(ulong experienceCharacter, ulong experienceMount, ulong experienceGuild, ulong experienceIncarnation)
        {
            this.experienceCharacter = experienceCharacter;
            this.experienceMount = experienceMount;
            this.experienceGuild = experienceGuild;
            this.experienceIncarnation = experienceIncarnation;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(experienceCharacter);
            writer.WriteVarUhLong(experienceMount);
            writer.WriteVarUhLong(experienceGuild);
            writer.WriteVarUhLong(experienceIncarnation);
            

}

public override void Deserialize(ICustomDataInput reader)
{

experienceCharacter = reader.ReadVarUhLong();
            if (experienceCharacter < 0 || experienceCharacter > 9007199254740990)
                throw new Exception("Forbidden value on experienceCharacter = " + experienceCharacter + ", it doesn't respect the following condition : experienceCharacter < 0 || experienceCharacter > 9007199254740990");
            experienceMount = reader.ReadVarUhLong();
            if (experienceMount < 0 || experienceMount > 9007199254740990)
                throw new Exception("Forbidden value on experienceMount = " + experienceMount + ", it doesn't respect the following condition : experienceMount < 0 || experienceMount > 9007199254740990");
            experienceGuild = reader.ReadVarUhLong();
            if (experienceGuild < 0 || experienceGuild > 9007199254740990)
                throw new Exception("Forbidden value on experienceGuild = " + experienceGuild + ", it doesn't respect the following condition : experienceGuild < 0 || experienceGuild > 9007199254740990");
            experienceIncarnation = reader.ReadVarUhLong();
            if (experienceIncarnation < 0 || experienceIncarnation > 9007199254740990)
                throw new Exception("Forbidden value on experienceIncarnation = " + experienceIncarnation + ", it doesn't respect the following condition : experienceIncarnation < 0 || experienceIncarnation > 9007199254740990");
            

}


}


}