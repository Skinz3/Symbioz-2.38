using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Instances;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Monsters
{
    public class MonsterSpawnManager : Singleton<MonsterSpawnManager>
    {
        /// <summary>
        /// Nombre maximum de groupe de monstres sur une carte
        /// </summary>
        public const sbyte MaxMonsterGroupPerMap = 4;
        /// <summary>
        /// En secondes
        /// </summary>
        public const int MonsterSpawningPoolInterval = 5;

        /// <summary>
        /// Spawn les groupes de monstres d'une carte.
        /// </summary>
        /// <param name="map"></param>
        public void SpawnMonsters(MapRecord map)
        {
            AsyncRandom random = new AsyncRandom();

            if (map.MonsterSpawnsSubArea.Length > 0)
            {
                for (sbyte groupId = 0; groupId < random.Next(1, MaxMonsterGroupPerMap + 1); groupId++)
                {
                    AddGeneratedMonsterGroup(map.Instance, map.MonsterSpawnsSubArea, true);
                }
            }
        }
        /// <summary>
        /// Spawn un groupe de monstre qui sera généré en fonction des constantes de générations.
        /// </summary>
        /// <param name="spawns"></param>
        /// <param name="quiet"></param>
        public void AddGeneratedMonsterGroup(AbstractMapInstance instance, MonsterSpawnRecord[] spawns, bool quiet)
        {
            AsyncRandom random = new AsyncRandom();

            MonsterGroup group = new MonsterGroup(instance.Record);

            for (int w = 0; w < random.Next(1, instance.Record.BlueCells.Count + 1); w++)
            {
                int max = spawns.Sum((MonsterSpawnRecord entry) => entry.Probability);
                int num = random.Next(0, max);
                int num2 = 0;
                foreach (var monsterRecord in spawns)
                {
                    num2 += monsterRecord.Probability;
                    if (num <= num2)
                    {
                        MonsterRecord template = MonsterRecord.GetMonster(monsterRecord.MonsterId);
                        Monster monster = new Monster(template, group, template.RandomGrade(random));
                        group.AddMonster(monster);
                        break;
                    }
                }
            }

            if (quiet)
                instance.AddQuietEntity(group);
            else
                instance.AddEntity(group);
        }
        /// <summary>
        /// Spawn un groupe fixe de mobs
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="monsterRecords"></param>
        /// <param name="quiet"></param>
        public void AddFixedMonsterGroup(AbstractMapInstance instance, MonsterRecord[] monsterRecords, bool quiet)
        {
            MonsterGroup group = new MonsterGroup(instance.Record);

            foreach (var template in monsterRecords)
            {
                Monster monster = new Monster(template, group);
                group.AddMonster(monster);
            }

            if (quiet)
                instance.AddQuietEntity(group);
            else
                instance.AddEntity(group);

        }
        public void AddFixedMonsterGroup(AbstractMapInstance instance, MonsterRecord[] monsterRecords, sbyte[] grades, bool quiet)
        {
            if (monsterRecords.Length != grades.Length)
            {
                throw new Exception("Record array must have same lenght that grade array.");
            }
            MonsterGroup group = new MonsterGroup(instance.Record);

            for (int i = 0; i < monsterRecords.Length; i++)
            {
                Monster monster = new Monster(monsterRecords[i], group, grades[i]);
                group.AddMonster(monster);
            }


            if (quiet)
                instance.AddQuietEntity(group);
            else
                instance.AddEntity(group);

        }
        public bool GroupExist(AbstractMapInstance instance, MonsterRecord[] monsterRecords)
        {
            foreach (var group in instance.GetEntities<MonsterGroup>())
            {
                if (group.GetMonsters().All(x => monsterRecords.Contains(x.Template)))
                    return true;
            }
            return false;
        }
        public void RemoveGroup(AbstractMapInstance instance, MonsterRecord[] monsterRecords)
        {
            foreach (var group in instance.GetEntities<MonsterGroup>())
            {
                if (group.GetMonsters().All(x => monsterRecords.Contains(x.Template)))
                {
                    instance.RemoveEntity(group);
                    break;
                }
            }
        }
    }
}
