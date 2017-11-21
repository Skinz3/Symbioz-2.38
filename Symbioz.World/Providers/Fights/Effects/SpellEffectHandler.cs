using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects
{
    public abstract class SpellEffectHandler
    {
        protected Fighter Source
        {
            get;
            private set;
        }
        protected Fight Fight
        {
            get
            {
                return Source.Fight;
            }
        }
        protected SpellLevelRecord SpellLevel
        {
            get;
            private set;
        }
        protected ushort SpellId
        {
            get;
            private set;
        }
        protected EffectInstance Effect
        {
            get;
            private set;
        }
        private Fighter[] BaseTargets
        {
            get;
            set;
        }
        protected MapPoint CastPoint
        {
            get;
            private set;
        }
        protected bool Critical
        {
            get;
            private set;
        }

        public SpellEffectHandler(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect, Fighter[] targets, MapPoint castPoint, bool critical)
        {
            this.Source = source;
            this.SpellLevel = spellLevel;
            this.SpellId = spellLevel.SpellId;
            this.Effect = effect;
            this.BaseTargets = targets;
            this.CastPoint = castPoint;
            this.Critical = critical;
        }

        public abstract bool Apply(Fighter[] targets);

        public void Execute()
        {
            if (Effect.Delay > 0)
            {
                this.AddTriggerBuff(Source, FightDispellableEnum.REALLY_NOT_DISPELLABLE, TriggerType.BUFF_DELAY_ENDED, delegate (TriggerBuff buff, TriggerType trigger, object token)
                    {
                        Apply(BaseTargets);
                        return false;
                    }, Effect.Delay);
            }
            else
            {
                Apply(BaseTargets);
            }
        }
        public StatBuff AddStatBuff(Fighter target, short value, Characteristic caracteritic, FightDispellableEnum dispelable)
        {
            int id = target.BuffIdProvider.Pop();
            StatBuff statBuff = new StatBuff(id, target, this.Source, this.SpellLevel, this.Effect, this.SpellId, value, caracteritic, this.Critical, dispelable);
            target.AddAndApplyBuff(statBuff, true);
            return statBuff;
        }
        public StatBuff AddStatBuff(Fighter target, short value, Characteristic caracteritic, FightDispellableEnum dispelable, short customActionId)
        {
            int id = target.BuffIdProvider.Pop();
            StatBuff statBuff = new StatBuff(id, target, this.Source, this.SpellLevel, this.Effect, this.SpellId, value, caracteritic, this.Critical, dispelable, customActionId);
            target.AddAndApplyBuff(statBuff, true);
            return statBuff;
        }

        public TriggerBuff AddTriggerBuff(Fighter target, FightDispellableEnum dispelable, TriggerType trigger, TriggerBuff.TriggerBuffApplyHandler applyTrigger)
        {
            int id = target.BuffIdProvider.Pop();
            TriggerBuff triggerBuff = new TriggerBuff(id, target, this.Source, this.SpellLevel, this.Effect, this.SpellId, this.Critical, dispelable, trigger, applyTrigger, -1);
            target.AddAndApplyBuff(triggerBuff, true);
            return triggerBuff;
        }
        /// <summary>
        /// Used with Delayed Effects (Trigger at the end of the delay)
        /// </summary>
        /// <param name="target"></param>
        /// <param name="dispelable"></param>
        /// <param name="trigger"></param>
        /// <param name="applyTrigger"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        protected TriggerBuff AddTriggerBuff(Fighter target, FightDispellableEnum dispelable, TriggerType trigger, TriggerBuff.TriggerBuffApplyHandler applyTrigger, short delay)
        {
            int id = target.BuffIdProvider.Pop();
            TriggerBuff triggerBuff = new TriggerBuff(id, target, this.Source, this.SpellLevel, this.Effect, this.SpellId, this.Critical, dispelable, trigger, applyTrigger, delay);
            target.AddAndApplyBuff(triggerBuff, true);
            return triggerBuff;
        }
        public TriggerBuff AddTriggerBuff(Fighter target, FightDispellableEnum dispelable, TriggerType trigger, TriggerBuff.TriggerBuffApplyHandler applyTrigger, Symbioz.World.Providers.Fights.Buffs.TriggerBuff.TriggerBuffRemoveHandler removeTrigger)
        {
            int id = target.BuffIdProvider.Pop();
            TriggerBuff triggerBuff = new TriggerBuff(id, target, this.Source, this.SpellLevel, this.Effect, this.SpellId, this.Critical, dispelable, trigger, applyTrigger, removeTrigger, -1);
            target.AddAndApplyBuff(triggerBuff, true);
            return triggerBuff;
        }
        public VitalityBuff AddVitalityBuff(Fighter target, FightDispellableEnum dispelable, short num)
        {
            int id = target.BuffIdProvider.Pop();
            VitalityBuff buff = new VitalityBuff(id, target, Source, this.SpellLevel, Effect, this.SpellId, num, this.Critical, dispelable);
            target.AddAndApplyBuff(buff);
            return buff;
        }
        public ShieldBuff AddShieldBuff(Fighter target, FightDispellableEnum dispelable, short num)
        {
            int id = target.BuffIdProvider.Pop();
            ShieldBuff buff = new ShieldBuff(id, target, Source, SpellLevel, Effect, SpellId, num, Critical, dispelable);
            target.AddAndApplyBuff(buff);
            return buff;
        }
        public LookBuff AddLookBuff(Fighter target, FightDispellableEnum dispelable)
        {
            int id = target.BuffIdProvider.Pop();
            LookBuff buff = new LookBuff(id, target, Source, this.SpellLevel, Effect, this.SpellId, Critical, dispelable);
            target.AddAndApplyBuff(buff);
            return buff;
        }
        public StateBuff AddStateBuff(Fighter target, SpellStateRecord stateRecord, FightDispellableEnum dispelable)
        {
            int id = target.BuffIdProvider.Pop();
            StateBuff buff = new StateBuff(id, target, Source, this.SpellLevel, Effect, this.SpellId, Critical, dispelable, stateRecord);
            target.AddAndApplyBuff(buff);
            return buff;
        }
        public StateBuff AddStateBuff(Fighter target, SpellStateRecord stateRecord, short duration, FightDispellableEnum dispelable)
        {
            int id = target.BuffIdProvider.Pop();
            StateBuff buff = new StateBuff(id, target, Source, this.SpellLevel, Effect, this.SpellId, Critical, dispelable, stateRecord)
            {
                Duration = duration
            };
            target.AddAndApplyBuff(buff);
            return buff;
        }

    }
}
