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

namespace Symbioz.World.Providers.Fights.Effects.Others
{
    [SpellEffectHandler(EffectsEnum.Effect_SkipTurn)]
    public class SkipTurn : SpellEffectHandler
    {
        public SkipTurn(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
             Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                base.AddTriggerBuff(target, FightDispellableEnum.REALLY_NOT_DISPELLABLE, TriggerType.TURN_BEGIN, TurnBegin);
            }
            return true;
        }
        private bool TurnBegin(TriggerBuff buff, TriggerType trigger, object token)
        {
            return true;
        }
    }
}
