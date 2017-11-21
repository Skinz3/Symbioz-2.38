using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class TriggerBuff : Buff
    {
        public BuffTriggerType Trigger
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
        public short Delay { get; private set; }

        public TriggerBuff(int id, FightActor target, FightActor caster, EffectDice effect, Spell spell, bool critical, int dispelable, BuffTriggerType trigger, TriggerBuffApplyHandler applyTrigger)
            : base(id, target, caster, effect, spell, critical, dispelable)
        {
            this.Trigger = trigger;
            this.Dice = effect;
            this.ApplyTrigger = applyTrigger;
        }
        public TriggerBuff(int id, FightActor target, FightActor caster, EffectDice effect, Spell spell, bool critical, int dispelable, BuffTriggerType trigger, TriggerBuffApplyHandler applyTrigger, TriggerBuffRemoveHandler removeTrigger)
            : base(id, target, caster, effect, spell, critical, dispelable)
        {
            this.Trigger = trigger;
            this.Dice = effect;
            this.ApplyTrigger = applyTrigger;
            this.RemoveTrigger = removeTrigger;
        }
        public TriggerBuff(int id, FightActor target, FightActor caster, EffectDice effect, Spell spell, bool critical, int dispelable, BuffTriggerType trigger, TriggerBuffApplyHandler applyTrigger, short customActionId)
            : base(id, target, caster, effect, spell, critical, dispelable, customActionId)
        {
            this.Trigger = trigger;
            this.Dice = effect;
            this.ApplyTrigger = applyTrigger;
        }
        public TriggerBuff(int id, FightActor target, FightActor caster, EffectDice effect, Spell spell, bool critical, int dispelable, BuffTriggerType trigger, TriggerBuffApplyHandler applyTrigger, TriggerBuffRemoveHandler removeTrigger, short customActionId)
            : base(id, target, caster, effect, spell, critical, dispelable, customActionId)
        {
            this.Trigger = trigger;
            this.Dice = effect;
            this.ApplyTrigger = applyTrigger;
            this.RemoveTrigger = removeTrigger;
        }
        public override void Apply()
        {
            if (this.ApplyTrigger != null)
            {
                this.ApplyTrigger(this, BuffTriggerType.UNKNOWN, null);
            }
        }
        public void Apply(BuffTriggerType trigger)
        {
            if (this.ApplyTrigger != null)
            {
                this.ApplyTrigger(this, trigger, null);
            }
        }
        public void Apply(BuffTriggerType trigger, object token)
        {
            if (this.ApplyTrigger != null)
            {
                this.ApplyTrigger(this, trigger, token);
            }
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
            object[] values = base.Effect.GetValues();
            return new FightTriggeredEffect((uint)base.Id, (uint)base.Target.Id, (short)base.Duration, (sbyte)Dispellable, (ushort)base.Spell.Id, 0, (uint)((short)values[0]), (int)((short)values[1]), (int)((short)values[2]), 0, (short)Delay);
        }
    }
}
