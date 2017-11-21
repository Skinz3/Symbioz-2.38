using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public class ExplosiveControlableMonster : ControlableMonsterFighter
    {
        public ExplosiveControlableMonster(FightTeam team, MonsterRecord template, sbyte gradeId, CharacterFighter owner,
            short summonCellId)
            : base(team, template, gradeId, owner, summonCellId)
        {
            this.OnTurnEndEvt += ExplosiveControlableMonster_TurnEnd;
        }

        void ExplosiveControlableMonster_TurnEnd(Fighter fighter)
        {
            this.Stats.CurrentLifePoints = 0;

            Fight.SequencesManager.StartSequence(Protocol.Enums.SequenceTypeEnum.SEQUENCE_CHARACTER_DEATH);
            this.Die(fighter);
            Fight.SequencesManager.EndSequence(Protocol.Enums.SequenceTypeEnum.SEQUENCE_CHARACTER_DEATH);
        }

    }
}
