using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class TriggerBuff : Buff
    {
        public delegate bool TriggerBuffApplyHandler(TriggerBuff buff, TriggerType trigger, object token);

        public delegate void TriggerBuffRemoveHandler(TriggerBuff buff);

        public TriggerType Trigger
        {
            get;
            private set;
        }
        public TriggerBuffApplyHandler ApplyTrigger
        {
            get;
            private set;
        }
        public TriggerBuffRemoveHandler RemoveTrigger
        {
            get;
            private set;
        }
        public short Delay
        {
            get;
            private set;
        }

        public TriggerBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable, TriggerType trigger, TriggerBuffApplyHandler applyTrigger, short delay)
            : base(id, target, caster, level, effect, spellId, critical, dispelable)
        {
            this.Trigger = trigger;
            this.ApplyTrigger = applyTrigger;
            this.Delay = delay;
        }
        public TriggerBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable, TriggerType trigger, TriggerBuffApplyHandler applyTrigger, TriggerBuffRemoveHandler removeTrigger, short delay)
            : base(id, target, caster, level, effect, spellId, critical, dispelable)
        {
            this.Trigger = trigger;
            this.ApplyTrigger = applyTrigger;
            this.RemoveTrigger = removeTrigger;
            this.Delay = delay;
        }
        public TriggerBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable, TriggerType trigger, TriggerBuffApplyHandler applyTrigger, short delay, short customActionId)
            : base(id, target, caster, level, effect, spellId, critical, dispelable, customActionId)
        {
            this.Trigger = trigger;
            this.ApplyTrigger = applyTrigger;
            this.Delay = delay;
        }
        public TriggerBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable, TriggerType trigger, TriggerBuffApplyHandler applyTrigger, TriggerBuffRemoveHandler removeTrigger, short delay, short customActionId)
            : base(id, target, caster, level, effect, spellId, critical, dispelable, customActionId)
        {
            this.Trigger = trigger;
            this.ApplyTrigger = applyTrigger;
            this.RemoveTrigger = removeTrigger;
            this.Delay = delay;
        }
        public bool DecrementDelay()
        {
            return (this.Delay -= 1) == 0;
        }
        public bool IsDelayed()
        {
            return this.Effect.Delay > 0;
        }
        public override bool IsTrigger()
        {
            return true;
        }

        public override short GetDelay()
        {
            return this.Delay;
        }
        public override void Apply()
        {
            if (this.ApplyTrigger != null)
            {
                this.ApplyTrigger(this, TriggerType.UNKNOWN, null);
            }
        }
        public bool Apply(TriggerType trigger)
        {
            if (this.ApplyTrigger != null)
            {
                return this.ApplyTrigger(this, trigger, null);
            }
            return false;
        }
        public bool Apply(TriggerType trigger, object token)
        {
            if (this.ApplyTrigger != null)
            {
                return this.ApplyTrigger(this, trigger, token);
            }
            return false;
        }
        public override void Dispell()
        {
            if (this.RemoveTrigger != null)
            {
                this.RemoveTrigger(this);
            }
        }
        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            ushort[] values = new ushort[] { Effect.DiceMin, Effect.DiceMax, (ushort)Effect.Value };
            return new FightTriggeredEffect((uint)base.Id, (uint)base.Target.Id, (short)base.Duration, (sbyte)Dispelable, (ushort)base.SpellId, (uint)Effect.EffectUID, (uint)Effect.EffectUID, (int)values[0], (int)values[1], (int)values[2], (short)Delay);
        }

    }
}