


















// Generated on 04/27/2016 01:13:13
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameRolePlayNpcInformations : GameRolePlayActorInformations
{

public const short Id = 156;
public override short TypeId
{
    get { return Id; }
}

public ushort npcId;
        public bool sex;
        public ushort specialArtworkId;
        

public GameRolePlayNpcInformations()
{
}

public GameRolePlayNpcInformations(double contextualId, Types.EntityLook look, EntityDispositionInformations disposition, ushort npcId, bool sex, ushort specialArtworkId)
         : base(contextualId, look, disposition)
        {
            this.npcId = npcId;
            this.sex = sex;
            this.specialArtworkId = specialArtworkId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(npcId);
            writer.WriteBoolean(sex);
            writer.WriteVarUhShort(specialArtworkId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            npcId = reader.ReadVarUhShort();
            if (npcId < 0)
                throw new Exception("Forbidden value on npcId = " + npcId + ", it doesn't respect the following condition : npcId < 0");
            sex = reader.ReadBoolean();
            specialArtworkId = reader.ReadVarUhShort();
            if (specialArtworkId < 0)
                throw new Exception("Forbidden value on specialArtworkId = " + specialArtworkId + ", it doesn't respect the following condition : specialArtworkId < 0");
            

}


}


}