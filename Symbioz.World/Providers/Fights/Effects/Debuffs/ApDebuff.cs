using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Providers.Fights.Effects;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Debuffs
{
    [SpellEffectHandler(EffectsEnum.Effect_SubAP)]
    public class ApDebuff : SpellEffectHandler
    {
        public ApDebuff(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
             Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter current in targets)
            {
                if (this.Effect.Duration > 0)
                {
                    base.AddStatBuff(current, (short)-Effect.DiceMin, current.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE, 168);
                }
                else
                {
                    current.LostAp(Source.Id, (short)Effect.DiceMin);
                }
            }
            return true;
        }
    }
}
