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

namespace Symbioz.World.Providers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_TriggeredEffect)]
    public class Friction : SpellEffectHandler
    {
        private short CellsStep
        {
            get;
            set;
        }
        public Friction(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
            this.CellsStep = (short)Effect.DiceMax;
        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                base.AddTriggerBuff(target, 0, TriggerType.AFTER_ATTACKED, AfterAttacked);
            }
            return true;
        }

        private bool AfterAttacked(TriggerBuff buff, TriggerType trigger, object token)
        {
            Damage damages = (Damage)token;

            if (buff.Target.Point.IsInLine(damages.Source.Point))
            {
                buff.Target.Abilities.PullForward(damages.Source, CellsStep, buff.Target.Point);
            }

            return false;
        }
    }
}
