using Symbioz.Core;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Providers.Brain;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public abstract class BrainFighter : Fighter, IMonster
    {
        public BrainFighter(FightTeam team, ushort mapCellId, MonsterRecord template, sbyte gradeId)
            : base(team, mapCellId)
        {
            this.Template = template;
            this.GradeId = gradeId;
            this.Grade = Template.GetGrade(this.GradeId);
        }
        public MonsterBrain Brain
        {
            get;
            protected set;
        }
        public MonsterRecord Template
        {
            get;
            private set;
        }
        public sbyte GradeId
        {
            get;
            private set;
        }
        public override string Name
        {
            get
            {
                return Template.Name;
            }
        }

        public override ushort Level
        {
            get
            {
                return Grade.Level;
            }
        }
        /// <summary>
        /// Pas de monstres femelles! que des hommes :D
        /// </summary>
        public override bool Sex
        {
            get
            {
                return false;
            }
        }
        public int Xp
        {
            get
            {
                return Grade.GradeXp;
            }
        }
        public MonsterGrade Grade
        {
            get;
            private set;
        }
        public override void Initialize()
        {
            this.Id = Fight.PopNextContextualId();
            this.Stats = new FighterStats(Grade, Template.Power);
            this.Brain = new MonsterBrain(this);
            this.Look = Template.Look.Clone();
            base.Initialize();
            this.IsReady = true;
        }
        public override void OnTurnStarted()
        {
            base.OnTurnStarted();

            if (Alive && !Fight.Ended)
            {
                try
                {
                    Brain.Play();
                }
                catch (Exception ex)
                {
                    Logger.Write<BrainFighter>(ex.ToString(), ConsoleColor.DarkRed);
                }
            }
        }

        public ushort MonsterId
        {
            get
            {
                return Template.Id;
            }
        }


    }
}
