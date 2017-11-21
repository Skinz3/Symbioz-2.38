using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Marks;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Maps.Path;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.FightModels
{
    public class FightTeam
    {
        public sbyte Id
        {
            get;
            set;
        }

        public TeamEnum TeamEnum
        {
            get
            {
                return (TeamEnum)Id;
            }
        }

        private List<short> PlacementCells
        {
            get;
            set;
        }

        public AlignmentSideEnum Side
        {
            get;
            private set;
        }

        public void KillTeam()
        {
            bool sequence = Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);

            foreach (var fighter in GetFighters())
            {
                fighter.Stats.CurrentLifePoints = 0;
            }

            Fight.CheckDeads();
            if (sequence)
                Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);

            Fight.CheckFightEnd();
        }

        public TeamTypeEnum Type
        {
            get;
            private set;
        }

        private List<Fighter> Fighters = new List<Fighter>();

        private List<Fighter> Leavers = new List<Fighter>();

        public Fighter Leader
        {
            get
            {
                return Fighters.Count > 0 ? Fighters.First() : null;
            }
        }
        public bool LeaderAsParty
        {
            get
            {
                return Leader is CharacterFighter && (Leader as CharacterFighter).Character.HasParty();
            }
        }
        public Fight Fight
        {
            get;
            set;
        }

        public ushort BladesCellId
        {
            get;
            set;
        }

        public FightTeamOptions Options
        {
            get;
            set;
        }

        public int Alives
        {
            get
            {
                return Fighters.FindAll(x => x.Alive).Count;
            }
        }

        public FightTeam(sbyte id, List<short> placementCells, AlignmentSideEnum side, TeamTypeEnum teamtype)
        {
            this.PlacementCells = placementCells;
            this.Side = side;
            this.Type = teamtype;
            this.Id = id;
            this.Options = new FightTeamOptions(this);
        }
        public void Send(Message message)
        {
            GetFighters<CharacterFighter>(false).ForEach(x => x.Character.Client.Send(message));
        }
        public List<T> GetFighters<T>(bool aliveOnly = true)
        {
            if (aliveOnly)
                return Fighters.FindAll(x => x.Alive == true).OfType<T>().ToList();
            else
                return Fighters.OfType<T>().ToList();
        }
        public Fighter[] GetDeads()
        {
            return Fighters.FindAll(x => !x.Alive).ToArray();
        }
        public void AddLeaver(Fighter fighter)
        {
            Leavers.Add(fighter);
        }
        public Fighter[] GetLeavers()
        {
            return Leavers.ToArray();
        }
        public Fighter[] GetAllFightersWithLeavers()
        {
            List<Fighter> results = new List<Fighter>();
            results.AddRange(Leavers);
            results.AddRange(GetFighters(false));
            return results.Distinct().ToArray();
        }
        public void AddFighter(Fighter fighter)
        {
            fighter.Fight = Fight;
            fighter.Initialize();
            this.Fighters.Add(fighter);

            if (!Fight.Started)
                fighter.OnJoined();
        }

        public void ShowCell(PlayableFighter fighter, ushort cellId)
        {
            this.Send(new ShowCellMessage(fighter.Id, cellId));
        }

        public void RemoveFighter(Fighter fighter)
        {
            Fighters.Remove(fighter);

            Fight.Send(new GameFightRemoveTeamMemberMessage((short)Fight.Id, Id, fighter.Id));

            Fight.UpdateTeams();
        }
        public Fighter GetFighter(Predicate<Fighter> predicate)
        {
            return GetFighters(false).Find(predicate);
        }
        public List<Fighter> GetFighters(bool aliveOnly = true)
        {
            if (aliveOnly)
                return Fighters.FindAll(x => x.Alive == true);
            else
                return Fighters;
        }
        public void OnFighters(Action<Fighter> action, bool aliveOnly = true)
        {
            foreach (var fighter in GetFighters(aliveOnly))
            {
                action(fighter);
            }
        }
        public void Update()
        {
            var msg = new GameFightUpdateTeamMessage((short)Fight.Id, GetFightTeamInformations());
            Fight.Send(msg);

            if (Fight.ShowBlades && !Fight.Started)
                Fight.Map.Instance.Send(msg);
        }
        public short GetPlacementCell()
        {
            List<short> fighterCells = Fighters.ConvertAll<short>(x => (short)x.CellId);
            return PlacementCells.Find(x => !fighterCells.Contains(x));
        }
        public bool IsPlacementCellsFree(int count)
        {
            List<short> fighterCells = Fighters.ConvertAll<short>(x => (short)x.CellId);
            return PlacementCells.Count(x => !fighterCells.Contains(x)) >= count;
        }
        public DirectionsEnum FindPlacementDirection(Fighter fighter)
        {
            Tuple<short, uint> tuple = null;

            foreach (Fighter current in fighter.OposedTeam().Fighters)
            {
                MapPoint point = current.Point;
                if (tuple == null)
                {
                    tuple = Tuple.Create<short, uint>(current.CellId, fighter.Point.DistanceToCell(point));
                }
                else
                {
                    if (fighter.Point.DistanceToCell(point) < tuple.Item2)
                    {
                        tuple = Tuple.Create<short, uint>(current.CellId, fighter.Point.DistanceToCell(point));
                    }
                }
            }
            DirectionsEnum result;
            if (tuple == null)
            {
                result = DirectionsEnum.DIRECTION_SOUTH_WEST;
            }
            else
            {
                result = fighter.Point.OrientationTo(new MapPoint((short)tuple.Item1), false);
            }
            return result;
        }
        public ushort[] GetPlacements()
        {
            return PlacementCells.ConvertAll<ushort>(x => (ushort)x).ToArray();
        }
        public int GetFightersCount(bool aliveOnly)
        {
            return GetFighters(aliveOnly).Count;
        }
        public bool AreAllReady()
        {
            return Fighters.All(x => x.IsReady);
        }
        public bool CanSpawnPortal()
        {
            return GetAllPortals().Length < 4;
        }
        public Portal[] GetAllPortals()
        {
            return Fight.Marks.OfType<Portal>().Where(x => x.Source.Team == this).ToArray();
        }
        public int GetActivePortalCount()
        {
            return GetAllPortals().Count(x => x.Active);
        }
        public void RemoveFirstPortal(Fighter source)
        {
            var portal = GetAllPortals().FirstOrDefault();

            if (portal != null)
            {
                Fight.RemoveMark(source, portal);
            }
        }
        public FightTeamInformations GetFightTeamInformations()
        {
            List<FightTeamMemberInformations> members = new List<FightTeamMemberInformations>();
            Fighters.ForEach(x => members.Add(x.GetFightTeamMemberInformations()));
            var team = new FightTeamInformations(Id, Leader != null ? Leader.Id : 0, (sbyte)Side, (sbyte)Type, 0, members.ToArray());
            return team;
        }
        public FightTeamLightInformations GetFightTeamLightInformations()
        {
            return new FightTeamLightInformations(Id, (double)Leader.Id, (sbyte)Side, (sbyte)Type, 0, (sbyte)Fighters.Count, 50);
        }
        /// <summary>
        /// Joueur ayant la vie la plus basse.
        /// </summary>
        /// <returns></returns>
        public Fighter LowerFighter()
        {
            List<Fighter> fighters = GetFighters(true);
            return fighters.Count == 0 ? null : fighters.Aggregate((f1, f2) => f1.Stats.CurrentLifePoints < f2.Stats.CurrentLifePoints ? f1 : f2);
        }
        /// <summary>
        /// Joueur ayant le pourcentage de vie le plus faible de l'équipe.
        /// </summary>
        /// <returns></returns>
        public Fighter LowerFighterPercentage()
        {
            List<Fighter> fighters = GetFighters(true);
            return fighters.Count == 0 ? null : fighters.Aggregate((f1, f2) => f1.Stats.LifePercentage < f2.Stats.LifePercentage ? f1 : f2);
        }
        /// <summary>
        /// Joueur ayant la vie la plus haute
        /// </summary>
        /// <returns></returns>
        public Fighter HigherFighter()
        {
            List<Fighter> fighters = GetFighters(true);
            return fighters.Count == 0 ? null : fighters.Aggregate((f1, f2) => f1.Stats.CurrentLifePoints > f2.Stats.CurrentLifePoints ? f1 : f2);
        }
        /// <summary>
        /// Joueur ayant le pourcentage de vie le plus haut de l'équipe.
        /// </summary>
        /// <returns></returns>
        public Fighter HigherFighterPercentage()
        {
            List<Fighter> fighters = GetFighters(true);
            return fighters.Count == 0 ? null : fighters.Aggregate((f1, f2) => f1.Stats.LifePercentage > f2.Stats.LifePercentage ? f1 : f2);
        }
        /// <summary>
        /// Joueur étant le plus proche.
        /// </summary>
        /// <returns></returns>
        public Fighter CloserFighter(Fighter source)
        {
            return GetFighters(true).OrderByDescending(x => x.GetMPDistanceBetwenn(source)).LastOrDefault();
        }
        public Fighter[] CloserFighters(Fighter source)
        {
            return GetFighters(true).OrderByDescending(x => x.GetMPDistanceBetwenn(source)).ToArray();
        }
        /// <summary>
        /// Dernier joueur mort (Laisse Spirituelle de l'Osamodas)
        /// </summary>
        /// <returns></returns>
        public Fighter LastDead()
        {
            return Fighters.FindAll(x => !x.Alive).OrderByDescending(x => x.DeathTime).FirstOrDefault();
        }


        internal int GetTeamLevel()
        {
            return Fighters.Sum(x => x.Level);
        }

        public FightTeam OposedTeam()
        {
            return this == Fight.RedTeam ? Fight.BlueTeam : Fight.RedTeam;
        }

        //internal GameFightFighterLightInformations[] GetFighterLightInformations() Spéctateur
        //{
        //    return GetFighters(false).ConvertAll<GameFightFighterLightInformations>(x => x.GetFightFighterLightInformations()).ToArray();
        //}
    }
}
