using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Monsters
{
    class MonstersSpawnsModule
    {
        static Logger logger = new Logger();

     //   [StartupInvoke("Monster Spawns Module",StartupInvokePriority.Modules)]
        public static void SyncMonstersSpawns()
        {

            foreach (var subarea in SubareaRecord.Subareas)
            {
                foreach (var monster in subarea.Monsters)
                {
                    sbyte probability = 0;

                    var template = MonsterRecord.GetMonster(monster);

                    switch ((MonsterRaceEnum)template.Race)
                    {
                        case MonsterRaceEnum.Boss:
                            probability = 2;
                            break;
                        case MonsterRaceEnum.NPCs:
                            probability = 3;
                            break;
                        case MonsterRaceEnum.TerritoryQuestMonsters:
                            probability = 3;
                            break;
                        case MonsterRaceEnum.TutorialMonsters:
                            probability = 3;
                            break;
                        case MonsterRaceEnum.Wanted:
                            probability = 3;
                            break;
                        case MonsterRaceEnum.QuestMonsters:
                            probability = 3;
                            break;
                        case MonsterRaceEnum.KwismasMonsters:
                            probability = 5;
                            break;
                        case MonsterRaceEnum.Archmonsters:
                            probability = 3;
                            break;
                        case MonsterRaceEnum.FrigostWantedMonsters:
                            probability = 2;
                            break;
                        case MonsterRaceEnum.FrigostQuestMonsters:
                            probability = 2;
                            break;
                        case MonsterRaceEnum.TESTTest:
                            probability = 1;
                            break;
                        case MonsterRaceEnum.Undefined:
                            probability = 1;
                            break;
                        case MonsterRaceEnum.OldVersions:
                            probability = 10;
                            break;
                        case MonsterRaceEnum.MonstersfromVulkaniaQuest:
                            probability = 2;
                            break;
                        case MonsterRaceEnum.MonstersfromNowelQuest:
                            probability = 2;
                            break;
                        case MonsterRaceEnum.Undefined2:
                            probability = 1;
                            break;
                        case MonsterRaceEnum.Undefined3:
                            probability = 1;
                            break;
                        case MonsterRaceEnum.MonstersfromAlignQuests:
                            probability = 5;
                            break;
                        case MonsterRaceEnum.IncarnamQuestMonsters:
                            probability = 20;
                            break;
                        default:
                            probability = 100;
                            break;
                    }


                    int id = (int)MonsterSpawnRecord.MonsterSpawns.DynamicPop(x => x.Id);
                    new MonsterSpawnRecord(id, monster, subarea.Id, probability).AddInstantElement();
                }
                logger.White("Subarea: " + subarea.Name + " fixed!");
            }
        }


    }
}
