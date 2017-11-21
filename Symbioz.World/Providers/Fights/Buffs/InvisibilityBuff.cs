using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Buffs
{
    public class InvisibilityBuff : Buff
    {
        public InvisibilityBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect,
            ushort spellId, bool critical, FightDispellableEnum dispelable)
            : base(id, target, caster, level, effect, spellId, critical, dispelable)
        {

        }
        public override void Apply()
        {
            Target.SetInvisiblityState(GameActionFightInvisibilityStateEnum.INVISIBLE, Caster);
        }

        public override void Dispell()
        {
            Target.SetInvisiblityState(GameActionFightInvisibilityStateEnum.VISIBLE, Caster);
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostEffect((uint)Id, Target.Id, Duration, (sbyte)Dispelable, Effect.EffectId, 0, 0, 0);
        }
    }
}
