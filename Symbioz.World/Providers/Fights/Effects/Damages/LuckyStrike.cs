using Symbioz.Core;
using Symbioz.Protocol.Enums;
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

namespace Symbioz.World.Providers.Fights.Effects.Damages
{
    /// <summary>
    /// Exemple : Chance Du Kanigrou
    /// </summary>
    [SpellEffectHandler(EffectsEnum.Effect_LuckyStrike)]
    public class LuckyStrike : SpellEffectHandler
    {
        private AsyncRandom Random
        {
            get;
            set;
        }
        private short HealMultiplicator
        {
            get;
            set;
        }
        private short DamageMultiplicator
        {
            get;
            set;
        }
        private sbyte Percentage
        {
            get;
            set;
        }
        public LuckyStrike(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
            this.Random = new AsyncRandom();
            this.Percentage = (sbyte)Effect.Value;
            this.HealMultiplicator = (short)Effect.DiceMax;
            this.DamageMultiplicator = (short)Effect.DiceMin;
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
            Damage damages = (Damage)token;

            bool heal = Random.TriggerAleat(Percentage);

            if (heal)
            {
                buff.Target.Heal(buff.Caster, (short)(damages.Delta * HealMultiplicator));
                return true;
            }
            else
            {
                damages.Delta *= DamageMultiplicator;
                return false;
            }
        }
    }
}
