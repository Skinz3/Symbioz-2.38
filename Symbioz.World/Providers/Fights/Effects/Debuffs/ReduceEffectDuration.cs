using Symbioz.Protocol.Messages;
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

namespace Symbioz.World.Providers.Fights.Effects.Debuffs
{
    [SpellEffectHandler(Protocol.Selfmade.Enums.EffectsEnum.Effect_ReduceEffectsDuration)]
    public class ReduceEffectDuration : SpellEffectHandler
    {
        public ReduceEffectDuration(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
             Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                foreach (var buff in target.GetDispelableBuffs())
                {
                    if (buff.GetDelay() == 0)
                    {
                        buff.Duration -= (short)Effect.DiceMin;

                        if (buff.Duration <= 0 && buff.Effect.Duration != -1)
                        {
                            target.RemoveAndDispellBuff(buff);
                        }
                    }
                }
                Fight.Send(new GameActionFightModifyEffectsDurationMessage(0, Source.Id, target.Id,
                   (short)-Effect.DiceMin));

            }
            return true;
        }
    }
}
