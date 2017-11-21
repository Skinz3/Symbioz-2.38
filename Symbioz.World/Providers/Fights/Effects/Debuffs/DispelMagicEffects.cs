using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;

namespace Symbioz.World.Providers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_DispelMagicEffects)]
    public class DispelMagicEffects : SpellEffectHandler
    {
        public DispelMagicEffects(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect, Fighter[] targets, MapPoint castPoint, bool critical) : base(source, spellLevel, effect, targets, castPoint, critical)
        {
        }

        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                foreach (var buff in target.GetDispelableBuffs(true))
                {
                    target.RemoveAndDispellBuff(buff);
                }
            }
            return true;
        }
    }
}
