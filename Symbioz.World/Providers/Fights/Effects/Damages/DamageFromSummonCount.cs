using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;

namespace Symbioz.World.Providers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_DamageFromSummonsCount)]
    public class DamageFromSummonCount : SpellEffectHandler
    {
        public DamageFromSummonCount(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect, Fighter[] targets, MapPoint castPoint, bool critical) : base(source, spellLevel, effect, targets, castPoint, critical)
        {
        }

        public override bool Apply(Fighter[] targets)
        {
       
            var e = Effect;
            return true;
        }
    }
}
