using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Models.Items;
using Symbioz.World.Providers;
using Symbioz.World.Providers.Fights.Challenges;
using Symbioz.World.Providers.Maps.Monsters;
using Symbioz.World.Records.Idols;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights
{
    public class FightPvM : Fight
    {
        public override FightTypeEnum FightType
        {
            get
            {
                return FightTypeEnum.FIGHT_TYPE_PvM;
            }
        }

        public override bool SpawnJoin
        {
            get
            {
                return true;
            }
        }

        public override bool PvP
        {
            get
            {
                return false;
            }
        }

        public override bool ShowBlades
        {
            get
            {
                return true;
            }
        }

        public MonsterGroup Group
        {
            get;
            set;
        }
        protected Challenge[] Challenges
        {
            get;
            private set;
        }
        public override bool MinationAllowed
        {
            get
            {
                return true;
            }
        }
        public FightPvM(MapRecord map, FightTeam blueTeam, FightTeam redTeam, short cellId,
            MonsterGroup group)
            : base(map, blueTeam, redTeam, cellId)
        {
            this.Group = group;
            this.AgeBonus = group.AgeBonus;
            this.OnFightEndedEvt += FightEnded;
            this.FightStartEvt += FightPvM_FightStartEvt;

   
        }
        public override void StartPlacement()
        {
            base.StartPlacement();
        }
        private void FightPvM_FightStartEvt(Fight obj)
        {
          
        }

        void FightEnded(Fight arg1, bool arg2)
        {
            if (Started && Winners == GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER) && ShowBlades && !GroupExistOnMap())
            {
                Map.Instance.AddEntity(Group);
            }
            else if (!Started && ShowBlades && !GroupExistOnMap())
            {
                Map.Instance.AddEntity(Group);
            }
        }
        public override void Dispose()
        {
            this.OnFightEndedEvt -= FightEnded;
            base.Dispose();
        }
        public bool GroupExistOnMap()
        {
            var templates = Array.ConvertAll(Group.GetMonsters(), x => x.Template);
            return MonsterSpawnManager.Instance.GroupExist(Map.Instance, templates);
        }
        public override int GetPreparationDelay()
        {
            return 30;
        }
        public override void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            fighter.Character.Client.Send(new GameFightJoinMessage(true, !base.Started, false, base.Started, this.GetPlacementTimeLeft(), (sbyte)this.FightType));
        }
        public virtual FightTeam GetTeamChallenged()
        {
            return RedTeam.Type == TeamTypeEnum.TEAM_TYPE_PLAYER ? RedTeam : BlueTeam;
        }
        public override void OnFightStarted()
        {

           
          
            this.Challenges = ChallengeProvider.Instance.PopChallenges(GetTeamChallenged(), GetChallengeCount());
            this.OnChallengePopped();
            base.OnFightStarted();

           

        }
        private int GetChallengeCount()
        {
            FightTeam team = GetTeamChallenged();
            if (team.GetTeamLevel() >= team.OposedTeam().GetTeamLevel())
                return 1;
            else
                return 2;
        }
        public override FightCommonInformations GetFightCommonInformations()
        {
            return new FightCommonInformations(Id, (sbyte)FightType, GetFightTeamInformations(), new ushort[]{
                BlueTeam.BladesCellId,RedTeam.BladesCellId}, GetFightOptionsInformations());
        }
        protected virtual void OnChallengePopped()
        {
            foreach (var challenge in this.Challenges)
            {
                this.GetTeamChallenged().Send(new ChallengeInfoMessage(challenge.Id, challenge.GetTargetId(), (uint)challenge.XpBonusPercent, (uint)challenge.DropBonusPercent));
            }
        }
        protected virtual int GetChallengesDropPercentBonus()
        {
            return Array.FindAll(Challenges, x => x.IsSucces()).Sum(x => x.DropBonusPercent);
        }
        protected virtual int GetChallengesExpPercentBonus()
        {
            return Array.FindAll(Challenges, x => x.IsSucces()).Sum(x => x.XpBonusPercent);
        }
        public override Challenge GetChallenge(ushort id)
        {
            return Array.Find(Challenges, x => x.Id == id);
        }
        protected override IEnumerable<IFightResult> GenerateResults()
        {
            System.Collections.Generic.List<IFightResult> list = new System.Collections.Generic.List<IFightResult>();
            list.AddRange(
                from entry in base.GetAllFightersWithLeavers()
                where !(entry is IOwnable)
                select entry.GetFightResult());

            FightTeam[] teams = new FightTeam[] { BlueTeam, RedTeam };
            for (int i = 0; i < teams.Length; i++)
            {
                int xpBonusPercent = 0;

                int dropBonusPercent = 0;

                if (teams[i] == GetTeamChallenged())
                {
                    xpBonusPercent += GetChallengesExpPercentBonus();
                    dropBonusPercent += GetChallengesDropPercentBonus();

                }

                FightTeam team = teams[i];
                System.Collections.Generic.IEnumerable<Fighter> enumerable = ((team == base.RedTeam) ? base.BlueTeam : base.RedTeam).GetDeads();
                IOrderedEnumerable<IFightResult> orderedEnumerable = list.FindAll(x => x.CanLoot(team)).OrderBy(x => x.Prospecting);

                int teamPP = team.GetFighters(false).Sum((Fighter entry) => entry.Stats.Prospecting.TotalInContext());
                teamPP += teamPP.GetPercentageOf(dropBonusPercent);

                long baseKamas = enumerable.Sum((Fighter entry) => (long)((ulong)entry.GetDroppedKamas()));
                using (System.Collections.Generic.IEnumerator<IFightResult> enumerator = orderedEnumerable.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        IFightResult looter = enumerator.Current;
                        looter.Loot.Kamas = FormulasProvider.Instance.AdjustDroppedKamas(looter, teamPP, baseKamas, dropBonusPercent);
                        System.Collections.Generic.IEnumerable<Fighter> arg_1F0_0 = enumerable;
                        Func<Fighter, System.Collections.Generic.IEnumerable<DroppedItem>> selector = (Fighter dropper) => dropper.RollLoot(looter, dropBonusPercent);
                        foreach (DroppedItem current in arg_1F0_0.SelectMany(selector))
                        {
                            looter.Loot.AddItem(current);
                        }
                        if (looter is FightPlayerResult && looter.Outcome == FightOutcomeEnum.RESULT_VICTORY)
                        {
                            (looter as FightPlayerResult).AddEarnedExperience(xpBonusPercent);
                        }
                    }
                }
            }
            return list;
        }

    }
}
