using System;
using System.Collections.Generic;
using Symbioz.Core;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Symbioz.World.Records;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Records.Maps;

namespace Symbioz.World.Providers.Maps.PlacementPattern
{
    /// <summary>
    /// Bouh2
    /// </summary>
    class PlacementPatternModule
    {
        static Logger logger = new Logger();

        public static bool SortByComplexity = true;

        public static byte SearchDeep = 5;

        public static string PlacementPatternDirectory = Environment.CurrentDirectory + "/PlacementPatterns/";

        //   [StartupInvoke(StartupInvokePriority.Modules)]
        public static void ApplyFix()
        {
            logger.Gray("Fixing Map Placement...");

            List<PlacementPattern> patterns = new List<PlacementPattern>();
            Dictionary<PlacementPattern, string> patternsNames = new Dictionary<PlacementPattern, string>();
            foreach (string file in Directory.EnumerateFiles(PlacementPatternDirectory, "*.xml", SearchOption.AllDirectories))
            {
                try
                {
                    PlacementPattern pattern = (PlacementPattern)File.ReadAllText(file).XMLDeserialize(typeof(PlacementPattern));

                    if (SortByComplexity)
                    {
                        PlacementComplexityCalculator calc = new PlacementComplexityCalculator(pattern.Blues.Concat(pattern.Reds).ToArray<System.Drawing.Point>());
                        pattern.Complexity = calc.Compute();
                    }
                    patterns.Add(pattern);
                    patternsNames.Add(pattern, System.IO.Path.GetFileNameWithoutExtension(file));
                }
                catch
                {
                    logger.Error("Unable to load pattern " + System.IO.Path.GetFileNameWithoutExtension(file));
                }
            }

            Random rand = new Random();

            MapRecord[] maps = MapRecord.Maps.ToArray();

            int succesCount = 0;

            foreach (var map in MapRecord.Maps)
            {
                if (map.BlueCells.Count == 0 || map.RedCells.Count == 0)
                {
                    int[] relativePatternsComplx = (from entry in patterns
                                                    where entry.Relativ
                                                    select entry.Complexity).ToArray<int>();
                    PlacementPattern[] relativPatterns = (from entry in patterns
                                                          where entry.Relativ
                                                          select entry).ShuffleWithProbabilities(relativePatternsComplx).ToArray<PlacementPattern>();
                    Lozenge searchZone = new Lozenge(0, SearchDeep);

                    short[] cells = searchZone.GetCells(300, map);

                    for (int j = 0; j < cells.Length; j++)
                    {
                        short center = cells[j];
                        var pt = MapPoint.CellIdToCoord((uint)center);
                        System.Drawing.Point centerPt = new System.Drawing.Point(pt.X, pt.Y);
                        var success = relativPatterns.FirstOrDefault((PlacementPattern entry) => entry.TestPattern(centerPt, map));
                        if (success != null)
                        {
                            map.BlueCells = (from entry in success.Blues
                                             select (short)MapPoint.CoordToCellId(entry.X + centerPt.X, entry.Y + centerPt.Y)).ToList<short>();
                            map.RedCells = (from entry in success.Reds
                                            select (short)MapPoint.CoordToCellId(entry.X + centerPt.X, entry.Y + centerPt.Y)).ToList<short>();

                            map.UpdateInstantElement();
                            succesCount++;
                            break;
                        }
                    }
                }

            }



            logger.Gray(string.Format("{0} on {1} maps fixed ({2:0.0}%)", succesCount, MapRecord.Maps.Count, (double)succesCount / (double)MapRecord.Maps.Count * 100.0));

        }
    }
}
