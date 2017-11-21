


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FightTemporarySpellImmunityEffect : AbstractFightDispellableEffect
{

public const short Id = 366;
public override short TypeId
{
    get { return Id; }
}

public int immuneSpellId;
        

public FightTemporarySpellImmunityEffect()
{
}

public FightTemporarySpellImmunityEffect(uint uid, double targetId, short turnDuration, sbyte dispelable, ushort spellId, uint effectId, uint parentBoostUid, int immuneSpellId)
         : base(uid, targetId, turnDuration, dispelable, spellId, effectId, parentBoostUid)
        {
            this.immuneSpellId = immuneSpellId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteInt(immuneSpellId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            immuneSpellId = reader.ReadInt();
            

}


}


}