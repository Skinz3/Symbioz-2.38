using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class SpellBoost : Buff
    {
        public SpellBoost(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable)
            : base(id, target, caster, level, effect, spellId, critical, dispelable)
        {
        }
        public ushort BoostedSpellId
        {
            get
            {
                return Effect.DiceMin;
            }
        }
        public short Boost
        {
            get
            {
                return (short)Effect.Value;
            }
        }
        public override void Apply()
        {
            Target.BuffSpell(SpellId, Boost);
        }

        public override void Dispell()
        {
            Target.UnBuffSpell(SpellId, Boost);
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporarySpellBoostEffect((uint)Id, Target.Id, Duration, (sbyte)Dispelable, SpellId, 0, 0, Boost, BoostedSpellId);
        }
    }
}
