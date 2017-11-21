using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Monsters;

namespace Symbioz.World.Models.Fights.Fighters
{
    public class MinationMonsterFighter : ControlableMonsterFighter
    {
        public const double PowerRatio = 6.0;

        public override ushort Level
        {
            get
            {
                return MinationLevel;
            }
        }
        public ushort MinationLevel
        {
            get;
            private set;
        }
        public MinationMonsterFighter(FightTeam team, MonsterRecord template, sbyte gradeId, ushort minationLevel, CharacterFighter owner, short summonCellId) : base(team, template, gradeId, owner, summonCellId)
        {
            this.MinationLevel = minationLevel;
        }
        public override void Initialize()
        {
            base.Initialize();
            this.Stats = new Entities.Stats.FighterStats(Grade, (int)(Level*PowerRatio));
        }
    }
}
