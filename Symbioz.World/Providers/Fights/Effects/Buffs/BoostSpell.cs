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
    [SpellEffectHandler(EffectsEnum.Effect_SpellBoost)]
    public class BoostSpell : SpellEffectHandler
    {
        public BoostSpell(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                SpellBoost buff = new SpellBoost(target.BuffIdProvider.Pop(), target, Source, SpellLevel, Effect, SpellId, Critical, FightDispellableEnum.REALLY_NOT_DISPELLABLE);
                target.AddAndApplyBuff(buff);
            }
            return true;
        }
    }
}
