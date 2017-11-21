using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class LookBuff : Buff
    {
        public LookBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable)
            : base(id, target, caster, level, effect, spellId, critical, dispelable)
        {
        }
        public override void Apply()
        {
            ContextActorLook newLook = FightLookProvider.TransformLook(Target, Target.RealLook.Clone(), SpellId, Effect);
            base.Target.ChangeLook(newLook, Caster);
        }

        public override void Dispell()
        {
            base.Target.ChangeLook(Target.RealLook.Clone(), Caster);
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostEffect((uint)base.Id, base.Target.Id, (short)base.Duration, (sbyte)Dispelable, this.SpellId, 0, 0, 0);
        }
    }
}
