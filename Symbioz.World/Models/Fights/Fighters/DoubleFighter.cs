using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.FightModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public class DoubleFighter : Fighter, ISummon<CharacterFighter>
    {
        public CharacterFighter Owner
        {
            get;
            set;
        }
        private short SummonCellId
        {
            get;
            set;
        }
        public DoubleFighter(CharacterFighter owner, FightTeam team, short cellId)
            : base(team, 0)
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
                return Owner.Name;
            }
        }

        public override ushort Level
        {
            get
            {
                return Owner.Level;
            }
        }
        public override void Initialize()
        {
            this.Id = Fight.PopNextContextualId();
            this.Stats = (FighterStats)Owner.Stats.Clone();
            this.Stats.InitializeSummon(Owner,false);
            this.Look = Owner.Look.Clone();
            base.Initialize();
            this.CellId = SummonCellId;
            this.Direction = this.Owner.Point.OrientationTo(this.Point, false);
            this.IsReady = true;
        }
        public override void OnTurnStarted()
        {
            base.OnTurnStarted();
            this.PerformAction();
            this.PassTurn();
        }
        public void PerformAction()
        {
            Fighter target = OposedTeam().HigherFighterPercentage();

            if (target != null)
            {
                var path = target.FindPathTo(this);

                if (path.Count() > 0)
                    this.Move(path.ToList());
            }
        }
        public override GameFightFighterInformations GetFightFighterInformations()
        {
            return new GameFightCharacterInformations(Id, Look.ToEntityLook(), new EntityDispositionInformations(CellId, (sbyte)Direction), Team.Id, 0, Alive, Stats.GetFightMinimalStats(), new ushort[0],
                Name, Owner.Character.GetPlayerStatus(), (byte)Level, Owner.Character.Record.Alignment.GetActorAlignmentInformations(), Owner.Character.Breed.Id, Owner.Sex);
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            throw new NotImplementedException();
        }
    }
}
