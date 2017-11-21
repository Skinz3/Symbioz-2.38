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
    public class ShieldBuff : Buff
    {
        public const ActionsEnum ActionId = ActionsEnum.ACTION_CHARACTER_BOOST_SHIELD;

        public short Value
        {
            get;
            private set;
        }

        public ShieldBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, short value, bool critical, FightDispellableEnum dispelable)
            : base(id, target, caster, level, effect, spellId, critical, dispelable, (short)ActionId)
        {
            this.Value = value;
        }
        public override void Apply()
        {
            Target.Stats.AddShield(Value);
        }
        public override void Dispell()
        {
            Target.Stats.RemoveShield(Value);
        }
        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostEffect((uint)base.Id, base.Target.Id, (short)base.Duration, (sbyte)Dispelable, this.SpellId, 0, (uint)this.Value, (short)this.Value);
        }
    }
}
