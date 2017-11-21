using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Damages
{
    /// <summary>
    /// a voir, ne correspond pas a la prévisualisation client
    /// </summary>
    [SpellEffectHandler(EffectsEnum.Effect_Punishment_Damage)]
    public class PunishementDamage : SpellEffectHandler
    {
        public PunishementDamage(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
            Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            double num = 0.0;
            double num2 = (double)Source.Stats.CurrentLifePoints / (double)Source.Stats.CurrentMaxLifePoints;
            if (num2 <= 0.5)
            {
                num = 2.0 * num2;
            }
            else
            {
                if (num2 > 0.5)
                {
                    num = 1.0 + (num2 - 0.5) * -2.0;
                }
            }
            short jet = (short)((double)Source.Stats.CurrentMaxLifePoints * num * (double)Effect.DiceMin / 100.0);

            foreach (var target in targets)
            {
                target.InflictDamages(new Damage(Source, target, jet, EffectElementType.Neutral));
            }
            return true;
        }
    }
}
