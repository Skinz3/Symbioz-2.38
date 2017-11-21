using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class StatBuff : Buff
    {
        public short Value
        {
            get;
            private set;
        }
        public Characteristic Caracteristic
        {
            get;
            set;
        }
        public short Delta { get; private set; }

        public StatBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, short value, Characteristic caracteristic, bool critical, FightDispellableEnum dispelable)
            : base(id, target, caster, level, effect, spellId, critical, dispelable)
        {
            this.Value = value;
            this.Caracteristic = caracteristic;
        }
        public StatBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, short value, Characteristic caracteristic, bool critical, FightDispellableEnum dispelable, short customActionId)
            : base(id, target, caster, level, effect, spellId, critical, dispelable, customActionId)
        {
            this.Value = value;
            this.Caracteristic = caracteristic;
        }
        public override void Apply()
        {
            this.Caracteristic.Context += this.Value;
        }
        public override void Dispell()
        {
            this.Caracteristic.Context -= this.Value;
        }
        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostEffect((uint)base.Id, base.Target.Id, (short)base.Duration, (sbyte)Dispelable, this.SpellId, 0, 0, (short)Math.Abs(this.Value));
        }
    }
}
