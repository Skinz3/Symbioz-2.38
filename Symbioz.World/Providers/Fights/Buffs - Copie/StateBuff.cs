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
        public short StateId
        {
            get
            {
                return StateRecord.Id;
            }
        }
        public SpellStateRecord StateRecord
        {
            get;
            set;
        }

        public StateBuff(Fighter source, Fighter target, short delta,
            EffectInstance effect, ushort spellId,SpellStateRecord stateRecord)
            : base(source, target, delta, effect, spellId)
        {
            this.StateRecord = stateRecord;
        }
        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostStateEffect(UId, Target.Id, Duration, 0, SpellId, Effect.EffectId, 0, Delta, StateId);
        }
        public override void Apply()
        {
            base.Apply();
        }
        public override void Dispell()
        {
            
        }
    }
}
