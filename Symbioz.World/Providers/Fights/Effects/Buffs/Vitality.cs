using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_AddVitalityPercent)]
    public class VitalityPercent : SpellEffectHandler
    {
        public VitalityPercent(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter current in targets)
            {
                double num = (double)current.Stats.MaxLifePoints * ((double)Effect.DiceMin / 100.0);
                this.AddVitalityBuff(current, FightDispellableEnum.DISPELLABLE, (short)num);

            }
            return true;
        }
    }
    [SpellEffectHandler(EffectsEnum.Effect_AddVitality)]
    public class Vitality : SpellEffectHandler
    {
        public Vitality(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter current in targets)
            {
                this.AddVitalityBuff(current, FightDispellableEnum.DISPELLABLE, (short)Effect.DiceMin);

            }
            return true;
        }
    }
}
