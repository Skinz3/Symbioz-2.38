using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Damages;
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
    [SpellEffectHandler(EffectsEnum.Effect_Dodge)]
    public class Dodge : SpellEffectHandler
    {
        public Dodge(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
         Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                AddTriggerBuff(target, FightDispellableEnum.REALLY_NOT_DISPELLABLE, TriggerType.BEFORE_ATTACKED, BeforeAttacked);
            }

            return true;
        }
        private bool BeforeAttacked(TriggerBuff buff, TriggerType trigger, object token)
        {
            Damage damage = (Damage)token;

            if (buff.Target.Abilities.MeleeWith(damage.Source))
            {
                DirectionsEnum direction = damage.Source.Point.OrientationTo(buff.Target.Point);
                buff.Target.Abilities.PushBack(damage.Source, (short)buff.Effect.DiceMax, buff.Target.Point);
                return true;
            }

            return false;
        }
    }
}
