using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights
{
    /// <summary>
    /// Une instance de FightPvM par carte pouvant être rejoint simplement si 
    /// le combat n'a pas encore commencé 
    /// Je ne sais pas encore pourquoi j'ai codé ce truc x) mais il y a une idée a 
    /// trouver ;)
    /// </summary>
    public class PvMInstancesManager : Singleton<PvMInstancesManager>
    {
        private object m_locker = new object();

        private Dictionary<int, FreeFightInstancePvM> m_fights = new Dictionary<int, FreeFightInstancePvM>();

        public void AppendFight(Character character, int mapId, ushort[] monsterIds)
        {
            if (monsterIds.Length == 0)
            {
                throw new Exception("there must be one monster minimum in a PvMFight...");
            }
            MonsterRecord[] templates = new MonsterRecord[monsterIds.Length];
            for (int i = 0; i < monsterIds.Length; i++)
            {
                templates[i] = MonsterRecord.GetMonster(monsterIds[i]);
            }
            this.AppendFight(character, mapId, templates);
        }
        public void AppendFight(Character character, int mapId, MonsterRecord[] templates)
        {
            if (Exist(mapId, character))
            {
                throw new Exception(string.Format("Instance on mapId: {0} already exist!", mapId));
            }
            else
            {
                character.Teleport(mapId);

                MapRecord map = MapRecord.GetMap(mapId);

                var newFight = FightProvider.Instance.CreateFreeFightInstancePvM(templates, map);

                FightTeam characterTeam = newFight.GetTeam(TeamTypeEnum.TEAM_TYPE_PLAYER);
                FightTeam monsterTeam = newFight.GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER);

                characterTeam.AddFighter(character.CreateFighter(characterTeam));

                foreach (var monster in newFight.Group.CreateFighters(monsterTeam))
                {
                    monsterTeam.AddFighter(monster);
                }

                newFight.FightStartEvt += newFight_FightStartEvt;
                newFight.OnFightEndedEvt += newFight_OnFightEndedEvt;
                newFight.StartPlacement();

                m_fights.Add(mapId, newFight);
            }

        }
        public void Join(Character character, int mapId)
        {
            character.Teleport(mapId);
            FightTeam team = m_fights[mapId].GetTeam(TeamTypeEnum.TEAM_TYPE_PLAYER);

            if (team.GetFighter(x => x.Id == character.Id) != null)
            {
                return;
            }
            else
            {
                team.AddFighter(character.CreateFighter(team));
            }
        }

        public void Dayfight(Character character)
        {
            lock (m_locker)
            {
                DayFightRecord dayFight = DayFightRecord.GetDayFight();

                if (dayFight != null && dayFight.Monsters.Count > 0)
                {
                    if (!Instance.Exist(dayFight.MapId, character))
                    {
                        Instance.AppendFight(character, dayFight.MapId, dayFight.Monsters.ToArray());
                    }
                    else
                    {
                        Instance.Join(character, dayFight.MapId);
                    }
                }
            }
        }

        public bool Exist(int mapId, Character character)
        {
            return m_fights.ContainsKey(mapId) && m_fights[mapId].GetTeam(TeamTypeEnum.TEAM_TYPE_PLAYER).IsPlacementCellsFree(character.FighterCount);
        }
        void newFight_OnFightEndedEvt(Fight arg1, bool isStarted)
        {

            m_fights.Remove(arg1.Map.Id);
        }

        void newFight_FightStartEvt(Fight obj)
        {
            m_fights.Remove(obj.Map.Id);
        }

    }
}
