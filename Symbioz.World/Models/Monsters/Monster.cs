using Symbioz.Core;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Records;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Monsters
{
    public class Monster
    {
        public MonsterGroup Group
        {
            get;
            private set;
        }
        public MonsterGrade Grade
        {
            get
            {
                return Template.GetGrade(this.GradeId);
            }
        }
        public MonsterRecord Template
        {
            get;
            set;
        }
        public ContextActorLook Look
        {
            get
            {
                return this.Template.Look;
            }
        }
        public sbyte GradeId
        {
            get;
            private set;
        }
        public Monster(MonsterRecord template, MonsterGroup group, sbyte gradeId)
        {
            this.Template = template;
            this.Group = group;
            this.GradeId = gradeId;
        }
        public Monster(MonsterRecord template, MonsterGroup group, bool isCharacterMonster = false)
        {
            this.Template = template;
            this.Group = group;
            this.GradeId = template.RandomGrade(new AsyncRandom());
        }
        public Fighter CreateFighter(FightTeam team)
        {
            return new MonsterFighter(team, this, Group.CellId);
        }
        public MonsterInGroupInformations GetMonsterInGroupInformations()
        {
            return new MonsterInGroupInformations(this.Template.Id, this.GradeId, this.Look.ToEntityLook());
        }
        public MonsterInGroupLightInformations GetMonsterInGroupLightInformations()
        {
            return new MonsterInGroupLightInformations(this.Template.Id, this.GradeId);
        }
        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Template.Name, this.Template.Id);
        }
    }
}
