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
    public class StateBuff : Buff
    {
        public SpellStateRecord StateRecord
        {
            get;
            private set;
        }
        public StateBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect,
            ushort spellId, bool critical, FightDispellableEnum dispelable, SpellStateRecord stateRecord)
            : base(id, target, caster, level, effect, spellId, critical, dispelable)
        {
            this.StateRecord = stateRecord;
        }
        public override void Apply()
        {
            this.Target.AddState(StateRecord);
        }

        public override void Dispell()
        {
            this.Target.RemoveState(StateRecord);
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostStateEffect((uint)Id, Target.Id, Duration, (sbyte)Dispelable, SpellId,(uint) Effect.EffectUID, Effect.EffectUID, StateRecord.Id, StateRecord.Id);
        }
    }
}
