using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public abstract class Buff
    {
        public virtual FighterEvent EventType
        {
            get
            {
                return FighterEvent.OnCasted;
            }
        }
        public uint UId
        {
            get;
            private set;
        }
        public Fighter Target
        {
            get;
            private set;
        }
        public Fighter Source
        {
            get;
            private set;
        }
        public short Delta
        {
            get;
            private set;
        }
        public short Duration
        {
            get;
            private set;
        }
        public ushort SpellId
        {
            get;
            private set;
        }
        public int Delay
        {
            get;
            private set;
        }
        public EffectInstance Effect
        {
            get;
            private set;
        }
        public virtual ushort ActionId
        {
            get
            {
                if (!m_actionId.HasValue)
                    return Effect.EffectId;
                else
                    return m_actionId.Value;
            }
        }

        private ushort? m_actionId;

        public Buff(Fighter source, Fighter target, short delta, EffectInstance effect, ushort spellId)
        {
            this.Source = source;
            this.Target = target;
            this.Effect = effect;
            this.UId = (uint)Target.BuffIdProvider.Pop();
            this.Delta = delta;
            this.Duration = (short)effect.Duration;
            this.Source = source;
            this.SpellId = spellId;
            this.Delay = effect.Delay;

        }
        public void SetCustomActionId(ActionsEnum action)
        {
            m_actionId = (ushort)action;
        }
        public bool DecrementDuration()
        {
            return (this.Duration -= 1) <= 0;
        }
        public void DecrementDelay()
        {
            Delay--;
        }
        public virtual void Apply()
        {
            Source.Fight.Send(new GameActionFightDispellableEffectMessage(ActionId, Source.Id, GetAbstractFightDispellableEffect()));
        }

        public abstract void Dispell();

        public virtual bool OnTriggered(object arg1, object arg2, object arg3)
        {
            return false;
        }


        public virtual AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostEffect(UId, Target.Id, Duration, 1, SpellId, Effect.EffectId, 0, Math.Abs(Delta));
        }
    }
}
