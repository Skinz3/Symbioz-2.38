using Symbioz.Protocol.Enums;
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
    public abstract class Buff
    {
        public int Id
        {
            get;
            private set;
        }
        public Fighter Target
        {
            get;
            private set;
        }
        public Fighter Caster
        {
            get;
            private set;
        }
        public EffectInstance Effect
        {
            get;
            private set;
        }
        public ushort SpellId
        {
            get;
            private set;
        }
        public SpellLevelRecord Level
        {
            get;
            private set;
        }
        public short Duration
        {
            get;
            set;
        }
        public bool Critical
        {
            get;
            private set;
        }
        public FightDispellableEnum Dispelable
        {
            get;
            set;
        }
        public short? CustomActionId
        {
            get;
            private set;
        }
        public double Efficiency
        {
            get;
            set;
        }
        public bool Active
        {
            get
            {
                return GetDelay() <= 0;
            }
        }
        protected Buff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable)
        {
            this.Id = id;
            this.Target = target;
            this.Caster = caster;
            this.Effect = effect;
            this.SpellId = spellId;
            this.Critical = critical;
            this.Level = level;
            this.Dispelable = dispelable;
            this.Duration = (short)this.Effect.Duration;
            this.Efficiency = 1.0;
        }
        protected Buff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable, short customActionId)
        {
            this.Id = id;
            this.Target = target;
            this.Caster = caster;
            this.Effect = effect;
            this.SpellId = spellId;
            this.Critical = critical;
            this.Level = level;
            this.Dispelable = dispelable;
            this.CustomActionId = new short?(customActionId);
            this.Duration = (short)this.Effect.Duration;
            this.Efficiency = 1.0;
        }
        public bool DecrementDuration()
        {
            return this.Duration != -1 && (this.Duration -= 1) <= 0;
        }

        public virtual short GetDelay()
        {
            return 0;
        }
        public virtual bool IsTrigger()
        {
            return false;
        }

        public abstract void Apply();
        public abstract void Dispell();

        public short GetActionId()
        {
            short result;
            if (this.CustomActionId.HasValue)
            {
                result = this.CustomActionId.Value;
            }
            else
            {
                result = (short)this.Effect.EffectId;
            }
            return result;
        }
        public abstract AbstractFightDispellableEffect GetAbstractFightDispellableEffect();
    }
}
