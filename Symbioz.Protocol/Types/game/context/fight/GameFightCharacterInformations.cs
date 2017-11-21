


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameFightCharacterInformations : GameFightFighterNamedInformations
{

public const short Id = 46;
public override short TypeId
{
    get { return Id; }
}

public byte level;
        public Types.ActorAlignmentInformations alignmentInfos;
        public sbyte breed;
        public bool sex;
        

public GameFightCharacterInformations()
{
}

public GameFightCharacterInformations(double contextualId, Types.EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, string name, Types.PlayerStatus status, byte level, Types.ActorAlignmentInformations alignmentInfos, sbyte breed, bool sex)
         : base(contextualId, look, disposition, teamId, wave, alive, stats, previousPositions, name, status)
        {
            this.level = level;
            this.alignmentInfos = alignmentInfos;
            this.breed = breed;
            this.sex = sex;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteByte(level);
            alignmentInfos.Serialize(writer);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            level = reader.ReadByte();
            if (level < 0 || level > 255)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0 || level > 255");
            alignmentInfos = new Types.ActorAlignmentInformations();
            alignmentInfos.Deserialize(reader);
            breed = reader.ReadSByte();
            sex = reader.ReadBoolean();
            

}


}


}