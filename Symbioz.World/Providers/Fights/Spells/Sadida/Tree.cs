using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Spells;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Providers.Fights.Effects.Summons;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Spells.Sadida
{
    /// <summary>
    /// Invoque un arbre.
    /// Pendant 1 tour, l arbre ne peut pas être utilisé comme intermédiaire pour lancer d autres sorts.
    /// Après 1 tour, l arbre est utilisable et ne peut plus être poussé ou attiré.
    /// </summary>
    [CustomSpellHandler(186)]
    public class Tree : CustomSpellHandler
    {
        public const ushort UpgradedTreeSpellId = 6719;

        public static SpellRecord UpgradedTreeSpellRecord;


        private SummonedFighter SummonedTree
        {
            get;
            set;
        }
        static Tree()
        {
            UpgradedTreeSpellRecord = SpellRecord.GetSpellRecord(UpgradedTreeSpellId);
        }

        public Tree(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit)
            : base(source, level, castPoint, criticalHit)
        {

        }
        public override void Execute()
        {
            var effect = Level.Effects.First();
            var template = MonsterRecord.GetMonster(effect.DiceMin);
            this.SummonedTree = Summon.SummonFighter(template, (sbyte)(template.GradeExist(Level.Grade) ? Level.Grade : template.LastGrade().Id),
              Source, CastPoint);

            SpellLevelRecord upgradeLevel = UpgradedTreeSpellRecord.GetLevel(Level.Grade);

            Source.OnTurnStartEvt += OnTurnStarted;
        }

        void OnTurnStarted(Fighter obj)
        {
            bool succes = obj.Fight.SequencesManager.StartSequence(Protocol.Enums.SequenceTypeEnum.SEQUENCE_TRIGGERED);

            this.SummonedTree.ForceSpellCast(UpgradedTreeSpellRecord, 1, this.SummonedTree.CellId);

            Source.OnTurnStartEvt -= OnTurnStarted;

            if (succes)
                obj.Fight.SequencesManager.EndSequence(Protocol.Enums.SequenceTypeEnum.SEQUENCE_TRIGGERED);


        }
    }
}
