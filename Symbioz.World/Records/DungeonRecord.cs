//using Symbioz.Core.DesignPattern.StartupEngine;
//using Symbioz.ORM;
//using Symbioz.World.Records.Maps;
//using Symbioz.World.Records.Monsters;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Symbioz.Core;

//namespace Symbioz.World.Records
//{
//    [Table("Dungeons")]
//    public class DungeonRecord : ITable
//    {
//        public static List<DungeonRecord> Dungeons = new List<DungeonRecord>();

//        public int Id;

//        public string Name;

//        public List<int> Maps;

//        [Ignore]
//        public Dictionary<sbyte, int> RealMaps;

//        public DungeonRecord(int id, string name, List<int> maps)
//        {
//            this.Id = id;
//            this.Name = name;
//            this.Maps = maps;
//        }
//        public static void Generate()
//        {

//            foreach (var dungeon in Dungeons)
//            {
//                dungeon.RealMaps = new Dictionary<sbyte, int>();

//                int lastMap = 0;

//                foreach (var map in dungeon.Maps)
//                {
//                    var record = MapRecord.GetMap(map);

//                    if (record != null)
//                    {

//                        if (record.Position.Name.ToLower().Contains("première"))
//                        {
//                            dungeon.RealMaps[0] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("deuxième"))
//                        {
//                            dungeon.RealMaps[1] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("troisième"))
//                        {
//                            dungeon.RealMaps[2] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("quatrième"))
//                        {
//                            dungeon.RealMaps[3] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("cinquième"))
//                        {
//                            dungeon.RealMaps[4] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("sixième"))
//                        {
//                            dungeon.RealMaps[5] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("septième"))
//                        {
//                            dungeon.RealMaps[6] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("huitième"))
//                        {
//                            dungeon.RealMaps[7] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("neuvième"))
//                        {
//                            dungeon.RealMaps[8] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("dixième"))
//                        {
//                            dungeon.RealMaps[9] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("onzième"))
//                        {
//                            dungeon.RealMaps[10] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("douzième"))
//                        {
//                            dungeon.RealMaps[11] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("treizième"))
//                        {
//                            dungeon.RealMaps[12] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("quatorzième"))
//                        {
//                            dungeon.RealMaps[13] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("quinzième"))
//                        {
//                            dungeon.RealMaps[14] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("seizième"))
//                        {
//                            dungeon.RealMaps[15] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("dix-septième"))
//                        {
//                            dungeon.RealMaps[16] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("dix-huitième"))
//                        {
//                            dungeon.RealMaps[17] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("dix-neuvième"))
//                        {
//                            dungeon.RealMaps[18] = record.Id;
//                        }
//                        else if (record.Position.Name.ToLower().Contains("sortie"))
//                        {
//                            lastMap = record.Id;
//                        }


//                    }
//                    if (lastMap != 0)
//                    {
//                        sbyte[] key = dungeon.RealMaps.Keys.ToList().OrderByDescending(x => x).ToArray();
//                        dungeon.RealMaps.Add((sbyte)(key.FirstOrDefault() + 1), lastMap);
//                    }
//                }
//                for (sbyte i = 0; i < dungeon.RealMaps.Count - 1; i++)
//                {
//                    if (dungeon.RealMaps.ContainsKey(i) && dungeon.RealMaps.ContainsKey((sbyte)(i + 1)))
//                    {
//                        int mapId = dungeon.RealMaps[i];
//                        int nextMapId = dungeon.RealMaps[(sbyte)(i + 1)];
//                        var mapRecr = MapRecord.GetMap(nextMapId);
//                        int id = EndFightActionRecord.EndFightActions.PopNextId(x => x.Id);
//                        EndFightActionRecord endf = new EndFightActionRecord(id, mapId, nextMapId,
//                            mapRecr.RandomBorderCell());
//                        endf.AddInstantElement();

//                        Logger.Write<DungeonRecord>(mapId + " updated", ConsoleColor.Green);
//                    }
//                }
//            }
//        }
//    }
//}
