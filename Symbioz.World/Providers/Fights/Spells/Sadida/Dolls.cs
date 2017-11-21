using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using Symbioz.World.Providers.Fights.Effects.Summons;
using Symbioz.World.Records.Monsters;

namespace Symbioz.World.Providers.Fights.Spells.Sadida
{
    /// <summary>
    /// A la mort d'une poupée, elle redevient un arbre.
    /// </summary>
    [CustomSpellHandler(182)]
    [CustomSpellHandler(187)]
    [CustomSpellHandler(189)]
    [CustomSpellHandler(190)]
    [CustomSpellHandler(193)]
    public class Dolls : CustomSpellHandler
    {
        public const ushort TREE_SPELLID = 282;

        public Dolls(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit) : base(source, level, castPoint, criticalHit)
        {
        }

        public override void Execute()
        {
            DefaultHandler();

            var summon = Source.GetLastSummon() as SummonedFighter;

            if (summon != null && summon.CellId == CastPoint.CellId)
            {
                summon.AfterDeadEvt += Summon_AfterDeadEvt;
            }
        }

        private void Summon_AfterDeadEvt(Fighter summon,bool recursiveCall)
        {
            if (!summon.Fight.CheckFightEnd() && !recursiveCall)
            {
                Summon.SummonFighter(MonsterRecord.GetMonster(TREE_SPELLID), (summon as SummonedFighter).GradeId, Source, summon.Point);
            }
        }
    }
}
