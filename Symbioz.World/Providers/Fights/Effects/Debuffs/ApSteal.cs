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

namespace Symbioz.World.Providers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_StealAP_440)]
    public class ApSteal : SpellEffectHandler
    {
        public ApSteal(Fighter source,SpellLevelRecord spellLevel, EffectInstance effect,
              Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source,spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter current in targets)
            {
                base.AddStatBuff(current, (short)-Effect.DiceMin, current.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE, 168);

                if (this.Effect.Duration > 0)
                {
                    base.AddStatBuff(Source,
                        (short)Effect.DiceMin, Source.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE, 111);
                }
                else
                {
                    Source.RegainAp(Source.Id, (short)Effect.DiceMin);

                }
            }
            return true;
        }
    }
}