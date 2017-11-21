using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class MakeControlableBuff : Buff
    {
        public MakeControlableBuff(int id, Fighter target, Fighter caster, SpellLevelRecord level, EffectInstance effect, ushort spellId, bool critical, FightDispellableEnum dispelable)
            : base(id, target, caster, level, effect, spellId, critical, dispelable)
        {
        }
        public override void Apply()
        {

        }

        public override void Dispell()
        {
            MakeControlableBrained((CharacterFighter)Caster, Target as ControlableMonsterFighter);
        }
        public static ControlableMonsterFighter MakeSummonControlable(CharacterFighter source, SummonedFighter target)
        {
            target.Die(source);

            ControlableMonsterFighter fighter = new ControlableMonsterFighter(target.Team, target.Template, target.GradeId, source, target.CellId);

            source.Fight.AddSummon(fighter, source);

            return fighter;

        }
        public static SummonedFighter MakeControlableBrained(CharacterFighter source, ControlableMonsterFighter target)
        {
            target.Die(source);

            SummonedFighter fighter = new SummonedFighter(target.Template, target.GradeId, source, target.Team, target.CellId);

            source.Fight.AddSummon(fighter);

            return fighter;
        }
        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostEffect((uint)base.Id, base.Target.Id, (short)base.Duration, (sbyte)Dispelable, this.SpellId, 0, 0, 0);
        }

    }
}
