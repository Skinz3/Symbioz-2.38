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
    [SpellEffectHandler(EffectsEnum.Effect_StealAP_84)]
    public class ApStealNonFix : SpellEffectHandler
    {
        public ApStealNonFix(Fighter source,  SpellLevelRecord spellLevel, EffectInstance effect,
              Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter current in targets)
            {
                int num = 0;
                int num2 = 0;

                while (num2 < (int)Effect.DiceMin && num < current.Stats.ActionPoints.TotalInContext())
                {
                    if (current.RollApLose(base.Source))
                    {
                        num++;
                    }
                    num2++;
                }
                short num3 = (short)((int)Effect.DiceMin - num);

                if (num3 > 0)
                {
                    current.OnDodgePointLoss(ActionsEnum.ACTION_FIGHT_SPELL_DODGED_PA, Source, (ushort)num3);
                }
                if (num <= 0)
                {
                    return false;
                }
                base.AddStatBuff(current, (short)-num, current.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE, 169);


                if (this.Effect.Duration > 0)
                {
                    base.AddStatBuff(Source,
                        (short)num, Source.Stats.ActionPoints, FightDispellableEnum.DISPELLABLE, 128);
                }
                else
                {
                    Source.RegainAp(Source.Id, (short)num);

                }
            }
            return true;
        }
    }
}
