


















// Generated on 04/27/2016 01:13:13
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameRolePlayNpcWithQuestInformations : GameRolePlayNpcInformations
{

public const short Id = 383;
public override short TypeId
{
    get { return Id; }
}

public Types.GameRolePlayNpcQuestFlag questFlag;
        

public GameRolePlayNpcWithQuestInformations()
{
}

public GameRolePlayNpcWithQuestInformations(double contextualId, Types.EntityLook look, EntityDispositionInformations disposition, ushort npcId, bool sex, ushort specialArtworkId, Types.GameRolePlayNpcQuestFlag questFlag)
         : base(contextualId, look, disposition, npcId, sex, specialArtworkId)
        {
            this.questFlag = questFlag;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            questFlag.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            questFlag = new Types.GameRolePlayNpcQuestFlag();
            questFlag.Deserialize(reader);
            

}


}


}