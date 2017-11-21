using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps
{
    class CellShapesProvider
    {
        public static short[] MapBorders;

        [StartupInvoke(StartupInvokePriority.Fifth)]
        public static void Initialize()
        {
            MapBorders = GetMapBorders().ToArray();
        }
        private static List<short> GetMapBorders()
        {
            List<short> results = new List<short>();
            foreach (MapScrollEnum value in Enum.GetValues(typeof(MapScrollEnum)))
            {
                results.AddRange(GetMapBorder(value));
            }
            return results;
        }
        public static bool IsMapBorder(short cellId)
        {
            return MapBorders.Contains(cellId);
        }
        public static List<short> GetMapBorder(MapScrollEnum bordertype)
        {
            List<short> results = new List<short>();
            switch (bordertype)
            {
                case MapScrollEnum.Top:
                    results.AddRange(GetLineFromDirection(0, 14, DirectionsEnum.DIRECTION_EAST));
                    results.AddRange(GetLineFromDirection(14, 14, DirectionsEnum.DIRECTION_EAST));
                    break;
                case MapScrollEnum.Left:
                    results.AddRange(GetLineFromDirection(14, 19, DirectionsEnum.DIRECTION_SOUTH));
                    results.AddRange(GetLineFromDirection(0, 19, DirectionsEnum.DIRECTION_SOUTH));
                    break;
                case MapScrollEnum.Bottom:
                    results.AddRange(GetLineFromDirection(546, 14, DirectionsEnum.DIRECTION_WEST));
                    results.AddRange(GetLineFromDirection(560, 14, DirectionsEnum.DIRECTION_WEST));
                    break;
                case MapScrollEnum.Right:
                    results.AddRange(GetLineFromDirection(27, 19, DirectionsEnum.DIRECTION_SOUTH));
                    results.AddRange(GetLineFromDirection(13, 19, DirectionsEnum.DIRECTION_SOUTH));
                    break;
                default:
                    break;
            }
            return results;
        }
        public static void Verifiy(List<short> cells)
        {
            cells.RemoveAll(x => x < 0 || x > 560);
        }
        #region Simple
        public static List<short> GetRightCells(short startcell, short movedcellamount)
        {
            var list = new List<short>();
            for (short i = 1; i < (movedcellamount + 1); i++)
            {
                list.Add((short)(startcell + i));
            }
            Verifiy(list);
            return list;
        }
        public static List<short> GetLeftCells(short startcell, short movedcellamount)
        {
            var list = new List<short>();
            for (short i = 1; i < (movedcellamount + 1); i++)
            {
                list.Add((short)(startcell - i));
            }
            Verifiy(list);
            return list;
        }
        public static List<short> GetUpCells(short startcell, short movedcellamount)
        {
            var list = new List<short>();
            for (short i = 1; i < (movedcellamount + 1); i++)
            {
                list.Add((short)(startcell - 28 * i));
            }
            Verifiy(list);
            return list;
        }
        public static List<short> GetDownCells(short startcell, short movedcellamount)
        {
            var list = new List<short>();
            for (short i = 1; i < (movedcellamount + 1); i++)
            {
                list.Add((short)(startcell + 28 * i));
            }
            Verifiy(list);
            return list;
        }
        #endregion
        #region FrontDownRight&Left
        public static List<short> GetFrontDownLeftCells(short startcell, short movedcellamout)
        {
            var list = new List<short>();
            var checker = Math.Truncate((decimal)startcell / 14); // on regarde si la rangée de la cell est paire ou non
            var iee = Math.IEEERemainder((short)checker, 2); // on regarde si il y a un reste au nombre
            if (iee == 0) // si le nombre est pair , alors.. je m'y perd..edit : ok trouvé x)
            {
                list.Add((short)(startcell + 13));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] + 14));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] + 13));
                        check = true;
                    }
                }
            }
            else // si il est impaire ,alors
            {
                list.Add((short)(startcell + 14 * 1));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] + 13));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] + 14));
                        check = true;
                    }
                }
            }

            list.Remove(list.Last());
            Verifiy(list);
            return list;
        }
        public static List<short> GetFrontDownRightCells(short startcell, short movedcellamout)
        {
            var list = new List<short>();
            var checker = Math.Truncate((decimal)startcell / 14); // on regarde si la rangée de la cell est paire ou non
            var iee = Math.IEEERemainder((short)checker, 2); // on regarde si il y a un reste au nombre
            if (iee == 0) // si le nombre est pair , alors
            {
                list.Add((short)(startcell + 14));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] + 15));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] + 14));
                        check = true;
                    }
                }
            }
            else // si il est impaire ,alors
            {
                list.Add((short)(startcell + 15 * 1));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] + 14));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] + 15));
                        check = true;
                    }
                }
            }

            list.Remove(list.Last());
            return list;
        }
        #endregion
        #region FrontUpRight&Left
        public static List<short> GetFrontUpLeftCells(short startcell, short movedcellamout)
        {
            var list = new List<short>();
            var checker = Math.Truncate((decimal)startcell / 14); // on regarde si la rangée de la cell est paire ou non
            var iee = Math.IEEERemainder((short)checker, 2); // on regarde si il y a un reste au nombre
            if (iee == 0) // si le nombre est pair , alors
            {
                list.Add((short)(startcell - 15));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] - 14));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] - 15));
                        check = true;
                    }
                }
            }
            else // si il est impaire ,alors
            {
                list.Add((short)(startcell - 14 * 1));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] - 15));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] - 14));
                        check = true;
                    }
                }
            }

            list.Remove(list.Last());
            Verifiy(list);
            return list;
        }
        public static List<short> GetFrontUpRightCells(short startcell, short movedcellamout) // youston on a probleme cell 500
        {
            var list = new List<short>();
            var checker = Math.Truncate((decimal)startcell / 14);
            var iee = Math.IEEERemainder((short)checker, 2);
            if (iee == 0)
            {

                list.Add((short)(startcell - 14));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] - 13));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] - 14));
                        check = true;
                    }
                }
            }
            else
            {
                list.Add((short)(startcell - 13));
                bool check = true;
                for (int i = 0; i < movedcellamout; i++)
                {
                    if (check == true)
                    {
                        list.Add((short)(list[i] - 14));
                        check = false;
                    }
                    else
                    {
                        list.Add((short)(list[i] - 13));
                        check = true;
                    }
                }
            }

            list.Remove(list.Last());
            Verifiy(list);
            return list;
        }
        #endregion
        public static List<short> GetLineFromDirection(short startcell, short movecellamount, DirectionsEnum direction)
        {
            switch (direction)
            {
                case DirectionsEnum.DIRECTION_EAST:
                    return GetRightCells(startcell, movecellamount);
                case DirectionsEnum.DIRECTION_SOUTH_EAST:
                    return GetFrontDownRightCells(startcell, movecellamount);
                case DirectionsEnum.DIRECTION_SOUTH:
                    return GetDownCells(startcell, movecellamount);
                case DirectionsEnum.DIRECTION_SOUTH_WEST:
                    return GetFrontDownLeftCells(startcell, movecellamount);
                case DirectionsEnum.DIRECTION_WEST:
                    return GetLeftCells(startcell, movecellamount);
                case DirectionsEnum.DIRECTION_NORTH_WEST:
                    return GetFrontUpLeftCells(startcell, movecellamount);
                case DirectionsEnum.DIRECTION_NORTH:
                    return GetUpCells(startcell, movecellamount);
                case DirectionsEnum.DIRECTION_NORTH_EAST:
                    return GetFrontUpRightCells(startcell, movecellamount);
                default:
                    return null;
            }
        }

    }
}
