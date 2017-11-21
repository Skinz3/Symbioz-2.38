using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Eff_AddShieldPercent)]
    public class ShieldPercent : SpellEffectHandler
    {
        public ShieldPercent(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            double num = (double)Source.Stats.CurrentMaxLifePoints * ((double)Effect.DiceMin / 100.0);

            foreach (Fighter current in targets)
            {
                this.AddShieldBuff(current, FightDispellableEnum.DISPELLABLE, (short)num);
            }
            return true;
        }
    }
    [SpellEffectHandler(EffectsEnum.Eff_AddShield)]
    public class Shield : SpellEffectHandler
    {
        public Shield(Fighter source ,SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter current in targets)
            {
                AddShieldBuff(current, FightDispellableEnum.DISPELLABLE, (short)Effect.DiceMin);
            }
            return true;
        }
    }
}
