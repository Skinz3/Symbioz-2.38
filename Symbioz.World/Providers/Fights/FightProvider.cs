using Symbioz.Core.DesignPattern;
using Symbioz.World.Models.Fights;
using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Core;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Maps;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Enums;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Providers.Arena;
using Symbioz.World.Models.Monsters;

namespace Symbioz.World.Providers.Fights
{
    public class FightProvider : Singleton<FightProvider>
    {
        private List<Fight> Fights = new List<Fight>();

        public Fight GetFight(int id)
        {
            return Fights.Find(x => x.Id == id);
        }

        public void RemoveFight(Fight fight)
        {
            Fights.Remove(fight);
        }
        public int PopId()
        {
            lock (this)
            {
                return Fights.DynamicPop(x => x.Id);
            }
        }

        public FightPvM CreateFightPvM(MonsterGroup group, MapRecord map, short cellId)
        {
            FightTeam blueTeam = new FightTeam((sbyte)TeamEnum.TEAM_DEFENDER, map.BlueCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_MONSTER);
            FightTeam redTeam = new FightTeam((sbyte)TeamEnum.TEAM_CHALLENGER, map.RedCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
            var fight = new FightPvM(map, blueTeam, redTeam, cellId, group);
            Fights.Add(fight);
            return fight;
        }
        public FightInstancePvM CreateFightInstancePvM(MonsterRecord[] templates, MapRecord map)
        {
            FightTeam blueTeam = new FightTeam((sbyte)TeamEnum.TEAM_DEFENDER, map.BlueCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_MONSTER);
            FightTeam redTeam = new FightTeam((sbyte)TeamEnum.TEAM_CHALLENGER, map.RedCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
            FightInstancePvM fight = new FightInstancePvM(map, blueTeam, redTeam, templates);
            Fights.Add(fight);
            return fight;
        }
        /// <summary>
        /// Preparation Delay = 0
        /// </summary>
        /// <param name="templates"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public FreeFightInstancePvM CreateFreeFightInstancePvM(MonsterRecord[] templates, MapRecord map)
        {
            FightTeam blueTeam = new FightTeam((sbyte)TeamEnum.TEAM_DEFENDER, map.BlueCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_MONSTER);
            FightTeam redTeam = new FightTeam((sbyte)TeamEnum.TEAM_CHALLENGER, map.RedCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
            FreeFightInstancePvM fight = new FreeFightInstancePvM(map, blueTeam, redTeam, templates);
            Fights.Add(fight);
            return fight;
        }
        public FightDual CreateFightDual(Character source, Character target, short cellId)
        {
            FightTeam blueteam = new FightTeam((sbyte)TeamEnum.TEAM_DEFENDER, source.Map.BlueCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
            FightTeam redteam = new FightTeam((sbyte)TeamEnum.TEAM_CHALLENGER, source.Map.RedCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
            var fight = new FightDual(source.Map, blueteam, redteam, cellId);
            Fights.Add(fight);
            return fight;
        }
        public FightDual CreateFightGvG(MapRecord record)
        {
            FightTeam blueteam = new FightTeam((sbyte)TeamEnum.TEAM_DEFENDER, record.BlueCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
            FightTeam redteam = new FightTeam((sbyte)TeamEnum.TEAM_CHALLENGER, record.RedCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
            var fight = new FightDual(record, blueteam, redteam, 0);
            Fights.Add(fight);
            return fight;
        }
        /*public FightInstancePvM CreateCloneFightPvM(Character source, short cellId)
             {








                 FightTeam blueTeam = new FightTeam((sbyte)TeamEnum.TEAM_DEFENDER, source.Map.BlueCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_MONSTER);
                 FightTeam redTeam = new FightTeam((sbyte)TeamEnum.TEAM_CHALLENGER, source.Map.RedCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
                 FreeFightInstancePvM fight = new FreeFightInstancePvM(source.Map, blueTeam, redTeam, new MonsterRecord[0]);


                 var owner = source.CreateFighter(fight.RedTeam);
                 fight.RedTeam.AddFighter(owner);
                 fight.BlueTeam.AddFighter(new  Symbioz.World.Models.Fights.Fighters.DoubleFighter(owner as Symbioz.World.Models.Fights.Fighters.CharacterFighter, fight.BlueTeam,cellId));
                 Fights.Add(fight);
                 return fight;
             }
             */
        public FightAgression CreateFightAgression(Character source, Character target, short cellId)
        {
            FightTeam blueteam = new FightTeam((sbyte)TeamEnum.TEAM_DEFENDER, source.Map.BlueCells, source.Record.Alignment.Side, TeamTypeEnum.TEAM_TYPE_PLAYER);
            FightTeam redteam = new FightTeam((sbyte)TeamEnum.TEAM_CHALLENGER, source.Map.RedCells, target.Record.Alignment.Side, TeamTypeEnum.TEAM_TYPE_PLAYER);
            var fight = new FightAgression(source.Map, blueteam, redteam, cellId);
            Fights.Add(fight);
            return fight;
        }

        public FightArena CreateFightArena(MapRecord map)
        {
            FightTeam blueTeam = new FightTeam((sbyte)TeamEnum.TEAM_DEFENDER, map.BlueCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_PLAYER);
            FightTeam redTeam = new FightTeam((sbyte)TeamEnum.TEAM_CHALLENGER, map.RedCells, AlignmentSideEnum.ALIGNMENT_WITHOUT, TeamTypeEnum.TEAM_TYPE_BAD_PLAYER);
            var fight = new FightArena(map, blueTeam, redTeam);
            Fights.Add(fight);
            return fight;
        }
    }
}
