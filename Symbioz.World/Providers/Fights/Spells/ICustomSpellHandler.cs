using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Spells;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Spells
{
    /// <summary>
    /// Représente un sort géré de manière manuelle.
    /// </summary>
    public abstract class CustomSpellHandler
    {
        public CustomSpellHandler(Fighter source, SpellLevelRecord level, MapPoint castPoint,
            bool criticalHit)
        {
            this.Source = source;
            this.Level = level;
            this.CastPoint = castPoint;
            this.CriticalHit = criticalHit;
        }
        public Fighter Source
        {
            get;
            protected set;
        }
        public SpellLevelRecord Level
        {
            get;
            private set;
        }
        public MapPoint CastPoint
        {
            get;
            private set;
        }
        public bool CriticalHit
        {
            get;
            private set;
        }
        public EffectInstance GetEffect(EffectsEnum effect)
        {
            return CriticalHit ? Level.CriticalEffects.FirstOrDefault(x => x.EffectEnum == effect) :
                Level.Effects.FirstOrDefault(x => x.EffectEnum == effect);
        }
        public EffectInstance[] GetEffects()
        {
            return CriticalHit ? Level.CriticalEffects.ToArray() : Level.Effects.ToArray();
        }
        public void DefaultHandler()
        {
            SpellEffectsManager.Instance.HandleEffects(Source, Level, CastPoint, CriticalHit);
        }
        public void DefaultHandler(IEnumerable<EffectInstance> effects)
        {
            SpellEffectsManager.Instance.HandleEffects(Source, effects.ToArray(), Level, CastPoint, CriticalHit);
        }
        public void Handler(EffectInstance effect, MapPoint castPoint, Fighter[] targets)
        {
            SpellEffectsManager.Instance.HandleEffect(Source, Level, castPoint, CriticalHit, effect, targets);
        }
        public void DefaultHandler(EffectInstance effect)
        {
            DefaultHandler(new List<EffectInstance>() { effect });
        }
        public void DefaultHandler(IEnumerable<EffectInstance> effects, MapPoint castPoint)
        {
            SpellEffectsManager.Instance.HandleEffects(Source, effects.ToArray(), Level, castPoint, CriticalHit);
        }

        public TriggerBuff AddTriggerBuff(Fighter target, FightDispellableEnum dispelable, TriggerType trigger, SpellLevelRecord level, EffectInstance effect, ushort spellId, short delay, TriggerBuff.TriggerBuffApplyHandler applyTrigger, short duration)
        {
            int id = target.BuffIdProvider.Pop();
            TriggerBuff triggerBuff = new TriggerBuff(id, target, this.Source, level, effect, spellId, CriticalHit, dispelable, trigger, applyTrigger, delay);
            triggerBuff.Duration = duration;
            target.AddAndApplyBuff(triggerBuff, true);
            return triggerBuff;
        }
        public abstract void Execute();
    }
}
