using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Maps;
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
    public class SummonedFighter : BrainFighter, ISummon<Fighter>
    {

        public Fighter Owner
        {
            get;
            set;
        }
        private short SummonCellId
        {
            get;
            set;
        }


        public SummonedFighter(MonsterRecord template, sbyte gradeId, Fighter owner, FightTeam team, short cellId)
            : base(team, 0, template, gradeId)
        {
            this.Owner = owner;
            this.SummonCellId = cellId;
        }
        public bool IsOwner(Fighter fighter)
        {
            return this.Owner == fighter;
        }
        public override bool Sex
        {
            get
            {
                return false;
            }
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
        public override void Initialize()
        {
            base.Initialize();
            this.CellId = SummonCellId;
            this.FightStartCell = SummonCellId;
            this.Direction = Owner.Point.OrientationTo(this.Point, false);
            this.Stats.InitializeSummon(Owner, true);
        }
        public override void OnTurnStarted()
        {
            base.OnTurnStarted();
            if (!Fight.Ended)
                PassTurn();
        }
        public override bool InsertInTimeline()
        {
            return Template.UseSummonSlot;
        }
        public override GameFightFighterInformations GetFightFighterInformations()
        {
            return new GameFightMonsterInformations(Id, Look.ToEntityLook(), new EntityDispositionInformations((short)CellId, (sbyte)Direction),
               Team.Id, 0, Alive, Stats.GetFightMinimalStats(), new ushort[0], Template.Id, GradeId);
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            throw new NotImplementedException();
        }



    }
}
