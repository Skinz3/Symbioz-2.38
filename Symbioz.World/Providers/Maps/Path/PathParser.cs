using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Path
{
    public class PathParser
    {
        public static short ReadCell(short cell)
        {
            return (short)(cell & 4095);
        }
        public static sbyte GetDirection(int result)
        {
            return (sbyte)(result >> 12);
        }
        public static Dictionary<short, DirectionsEnum> ReturnDispatchedCells(IEnumerable<short> keys)
        {
            var cells = new Dictionary<short, DirectionsEnum>();

            foreach (var key in keys)
            {
                cells.Add((short)(key & 4095), (DirectionsEnum)(key >> 12));
            }
            return cells;
        }
        public static Dictionary<short, DirectionsEnum> FightMove(Dictionary<short, DirectionsEnum> cells)
        {
            var indexedCells = cells.Keys.ToList();
            var Cells = new Dictionary<short, DirectionsEnum>();

            for (var i = 0; i < cells.Count - 1; i++)
            {
                if (indexedCells[i] <= 0 || indexedCells[i] >= 559 || indexedCells[i + 1] <= 0 || indexedCells[i + 1] >= 559)
                    continue;

                var loc1 = CoordCells.Cells.FirstOrDefault(x => x.Id == indexedCells[i]);
                var loc2 = CoordCells.Cells.FirstOrDefault(x => x.Id == indexedCells[i + 1]);


                if (loc1.Y - loc2.Y == 0 && loc1.X - loc2.X < 0)
                {
                    for (var j = (short)(loc1.X + 1); j <= loc2.X; j++)
                    {
                        var celldata = CoordCells.Cells.FindAll(x => x.X == j).FirstOrDefault(y => y.Y == loc1.Y);
                        if (celldata != null)
                            Cells.Add(celldata.Id, DirectionsEnum.DIRECTION_SOUTH_EAST);
                    }
                }
                else if (loc1.X - loc2.X == 0 && loc1.Y - loc2.Y > 0)
                {
                    for (short j = (short)(loc1.Y - 1); j >= loc2.Y; j--)
                    {
                        var celldata = CoordCells.Cells.FindAll(x => x.Y == j).FirstOrDefault(y => y.X == loc1.X);
                        if (celldata != null)
                            Cells.Add(celldata.Id, DirectionsEnum.DIRECTION_SOUTH_WEST);
                    }
                }
                else if (loc1.X - loc2.X == 0 && loc1.Y - loc2.Y < 0)
                {
                    for (var j = (short)(loc1.Y + 1); j <= loc2.Y; j++)
                    {
                        var celldata = CoordCells.Cells.FindAll(x => x.Y == j).FirstOrDefault(y => y.X == loc1.X);
                        if (celldata != null)
                            Cells.Add(celldata.Id, DirectionsEnum.DIRECTION_NORTH_EAST);
                    }
                }
                else if (loc1.Y - loc2.Y == 0 && loc1.X - loc2.X > 0)
                {
                    for (var j = (short)(loc1.X - 1); j >= loc2.X; j--)
                    {
                        var celldata = CoordCells.Cells.FindAll(x => x.X == j).FirstOrDefault(y => y.Y == loc1.Y);
                        if (celldata != null)
                            Cells.Add(celldata.Id, DirectionsEnum.DIRECTION_NORTH_WEST);
                    }
                }
                else
                { Console.WriteLine("ERROR !"); }
            }

            return Cells;
        }
        public static int GetCellXCoord(int cellid)
        {
            int num = 15;
            return checked(cellid - (num - 1) * GetCellYCoord(cellid)) / num;
        }
        public static int GetCellYCoord(int cellid)
        {
            int num = 15;
            checked
            {
                int num2 = cellid / (num * 2 - 1);
                int num3 = cellid - num2 * (num * 2 - 1);
                int num4 = num3 % num;
                return num2 - num4;
            }
        }
        /// <summary>
        /// Distance (en pm) (manhatann)
        /// </summary>
        /// <param name="originCellId"></param>
        /// <param name="destinationCellId"></param>
        /// <returns></returns>
        public static short GetDistanceBetween(short originCellId, short destinationCellId)
        {
            CoordCells.CellData originCell = CoordCells.GetCell(originCellId);
            CoordCells.CellData destinationCell = CoordCells.GetCell(destinationCellId);
            int diffX = Math.Abs(originCell.X - destinationCell.X);
            int diffY = Math.Abs(originCell.Y - destinationCell.Y);
            return (short)(diffX + diffY);
        }

        public static short[] CompressPath(short[] path)
        {
            List<short> compressed = new List<short>();

            foreach (var cellId in path)
            {
                var point = new MapPoint(cellId);

            }

            return compressed.ToArray();
        }
        /*public static function getClientMovement(param1:Vector.<uint>) : MovementPath
      {
         var _loc4_:PathElement = null;
         var _loc5_:* = 0;
         var _loc6_:MapPoint = null;
         var _loc7_:PathElement = null;
         var _loc8_:String = null;
         var _loc9_:PathElement = null;
         var _loc2_:MovementPath = new MovementPath();
         var _loc3_:uint = 0;
         for each(_loc5_ in param1)
         {
            _loc6_ = MapPoint.fromCellId(_loc5_ & 4095);
            _loc7_ = new PathElement();
            _loc7_.step = _loc6_;
            if(_loc3_ == 0)
            {
               _loc2_.start = _loc6_;
            }
            else
            {
               _loc4_.orientation = _loc4_.step.orientationTo(_loc7_.step);
            }
            if(_loc3_ == param1.length - 1)
            {
               _loc2_.end = _loc6_;
               break;
            }<
            _loc2_.addPoint(_loc7_);
            _loc4_ = _loc7_;
            _loc3_++;
         }
         _loc2_.fill();
         if(DEBUG_ADAPTER)
         {
            _loc8_ = "Start : " + _loc2_.start.cellId + " | ";
            for each(_loc9_ in _loc2_.path)
            {
               _loc8_ = _loc8_ + (_loc9_.step.cellId + " > ");
            }
            _log.debug("Received path : " + _loc8_ + " | End : " + _loc2_.end.cellId);
         }
         return _loc2_;
      } */
    }
}




