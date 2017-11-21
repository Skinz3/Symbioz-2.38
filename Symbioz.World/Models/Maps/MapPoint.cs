
using Symbioz.Protocol.Enums;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace Symbioz.World.Models.Maps
{
    public class MapPoint
    {
        public const uint MapWidth = 14u;
        public const uint MapHeight = 20u;
        public const uint MapSize = 560u;
        private static readonly Point VectorRight = new Point(1, 1);
        private static readonly Point VectorDownRight = new Point(1, 0);
        private static readonly Point VectorDown = new Point(1, -1);
        private static readonly Point VectorDownLeft = new Point(0, -1);
        private static readonly Point VectorLeft = new Point(-1, -1);
        private static readonly Point VectorUpLeft = new Point(-1, 0);
        private static readonly Point VectorUp = new Point(-1, 1);
        private static readonly Point VectorUpRight = new Point(0, 1);
        private static bool m_initialized;
        private static readonly MapPoint[] OrthogonalGridReference = new MapPoint[560];
        private short m_cellId;
        private int m_x;
        private int m_y;

        public Point Coordinates
        {
            get
            {
                return new Point(X, Y);
            }
        }
        public short CellId
        {
            get
            {
                return this.m_cellId;
            }
            set
            {
                this.m_cellId = value;
                this.SetFromCellId();
            }
        }
        public int X
        {
            get
            {
                return this.m_x;
            }
            set
            {
                this.m_x = value;
                this.SetFromCoords();
            }
        }
        public int Y
        {
            get
            {
                return this.m_y;
            }
            set
            {
                this.m_y = value;
                this.SetFromCoords();
            }
        }
        public MapPoint(short cellId)
        {
            this.m_cellId = cellId;
            this.SetFromCellId();
        }
        public MapPoint(int x, int y)
        {
            this.m_x = x;
            this.m_y = y;
            this.SetFromCoords();
        }
        public MapPoint(Point point)
        {
            this.m_x = point.X;
            this.m_y = point.Y;
            this.SetFromCoords();
        }
        private void SetFromCoords()
        {
            if (!MapPoint.m_initialized)
            {
                MapPoint.InitializeStaticGrid();
            }
            this.m_cellId = (short)((long)(this.m_x - this.m_y) * 14L + (long)this.m_y + (long)((this.m_x - this.m_y) / 2));
        }
        private void SetFromCellId()
        {
            if (!MapPoint.m_initialized)
            {
                MapPoint.InitializeStaticGrid();
            }
            if (this.m_cellId < 0 || (long)this.m_cellId > 560L)
            {
                throw new System.IndexOutOfRangeException("Cell identifier out of bounds (" + this.m_cellId + ").");
            }
            MapPoint mapPoint = MapPoint.OrthogonalGridReference[(int)this.m_cellId];
            this.m_x = mapPoint.X;
            this.m_y = mapPoint.Y;
        }

        public bool IsInMap()
        {
            return IsInMap(m_x, m_y);
        }

        public uint DistanceTo(MapPoint point)
        {
            return (uint)System.Math.Sqrt((double)((point.X - this.m_x) * (point.X - this.m_x) + (point.Y - this.m_y) * (point.Y - this.m_y)));
        }
        public uint DistanceToCell(MapPoint point)
        {
            return (uint)(System.Math.Abs(this.m_x - point.X) + System.Math.Abs(this.m_y - point.Y));
        }
        public bool IsAdjacentTo(MapPoint point)
        {
            return this.DistanceToCell(point) == 1u;
        }
        public bool IsInLine(MapPoint point)
        {
            return point.X == this.X || point.Y == this.Y;
        }
        public DirectionsEnum OrientationToAdjacent(MapPoint point)
        {
            Point left = new Point
            {
                X = (point.X > this.m_x) ? 1 : ((point.X < this.m_x) ? -1 : 0),
                Y = (point.Y > this.m_y) ? 1 : ((point.Y < this.m_y) ? -1 : 0)
            };
            DirectionsEnum result;
            if (left == MapPoint.VectorRight)
            {
                result = DirectionsEnum.DIRECTION_EAST;
            }
            else
            {
                if (left == MapPoint.VectorDownRight)
                {
                    result = DirectionsEnum.DIRECTION_SOUTH_EAST;
                }
                else
                {
                    if (left == MapPoint.VectorDown)
                    {
                        result = DirectionsEnum.DIRECTION_SOUTH;
                    }
                    else
                    {
                        if (left == MapPoint.VectorDownLeft)
                        {
                            result = DirectionsEnum.DIRECTION_SOUTH_WEST;
                        }
                        else
                        {
                            if (left == MapPoint.VectorLeft)
                            {
                                result = DirectionsEnum.DIRECTION_WEST;
                            }
                            else
                            {
                                if (left == MapPoint.VectorUpLeft)
                                {
                                    result = DirectionsEnum.DIRECTION_NORTH_WEST;
                                }
                                else
                                {
                                    if (left == MapPoint.VectorUp)
                                    {
                                        result = DirectionsEnum.DIRECTION_NORTH;
                                    }
                                    else
                                    {
                                        if (left == MapPoint.VectorUpRight)
                                        {
                                            result = DirectionsEnum.DIRECTION_NORTH_EAST;
                                        }
                                        else
                                        {
                                            result = DirectionsEnum.DIRECTION_EAST;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        public DirectionsEnum OrientationTo(MapPoint point, bool diagonal = true)
        {
            int num = point.X - this.m_x;
            int num2 = this.m_y - point.Y;
            double num3 = System.Math.Sqrt((double)(num * num + num2 * num2));
            double num4 = System.Math.Acos((double)num / num3);
            double num5 = num4 * 180.0 / 3.1415926535897931;
            double num6 = num5 * (double)((point.Y > this.m_y) ? -1 : 1);
            double num7 = (!diagonal) ? (System.Math.Round(num6 / 90.0) * 2.0 + 1.0) : (System.Math.Round(num6 / 45.0) + 1.0);
            if (num7 < 0.0)
            {
                num7 += 8.0;
            }
            return (DirectionsEnum)((uint)num7);
        }

        public IEnumerable<MapPoint> GetAllCellsInRectangle(MapPoint oppositeCell, bool skipStartAndEndCells = true, Func<MapPoint, bool> predicate = null)
        {
            int iteratorVariable0 = Math.Min(oppositeCell.X, this.X);
            int iteratorVariable1 = Math.Min(oppositeCell.Y, this.Y);
            int iteratorVariable2 = Math.Max(oppositeCell.X, this.X);
            int iteratorVariable3 = Math.Max(oppositeCell.Y, this.Y);
            for (int i = iteratorVariable0; i <= iteratorVariable2; i++)
            {
                for (int j = iteratorVariable1; j <= iteratorVariable3; j++)
                {
                    if (!skipStartAndEndCells || (((i != this.X) || (j != this.Y)) && ((i != oppositeCell.X) || (j != oppositeCell.Y))))
                    {
                        MapPoint point = GetPoint(i, j);
                        if ((point != null) && ((predicate == null) || predicate(point)))
                        {
                            yield return point;
                        }
                    }
                }
            }
        }

        public MapPoint[] GetCellsOnLineBetween(MapPoint destination)
        {
            System.Collections.Generic.List<MapPoint> list = new System.Collections.Generic.List<MapPoint>();
            DirectionsEnum direction = this.OrientationTo(destination, true);
            MapPoint mapPoint = this;
            int num = 0;
            while ((long)num < 140L)
            {
                mapPoint = mapPoint.GetCellInDirection(direction, 1);
                if (mapPoint == null || mapPoint.CellId == destination.CellId)
                {
                    break;
                }
                list.Add(mapPoint);
                num++;
            }
            return list.ToArray();
        }
        public IEnumerable<MapPoint> GetCellsInLine(MapPoint destination)
        {
            int iteratorVariable0 = Math.Abs((int)(destination.X - this.X));
            int iteratorVariable1 = Math.Abs((int)(destination.Y - this.Y));
            int x = this.X;
            int y = this.Y;
            int iteratorVariable4 = (1 + iteratorVariable0) + iteratorVariable1;
            int iteratorVariable5 = (destination.X > this.X) ? 1 : -1;
            int iteratorVariable6 = (destination.Y > this.Y) ? 1 : -1;
            int iteratorVariable7 = iteratorVariable0 - iteratorVariable1;
            iteratorVariable0 *= 2;
            iteratorVariable1 *= 2;
            while (iteratorVariable4 > 0)
            {
                yield return GetPoint(x, y);
                if (iteratorVariable7 <= 0)
                {
                    if (iteratorVariable7 == 0)
                    {
                        x += iteratorVariable5;
                        y += iteratorVariable6;
                        iteratorVariable4--;
                    }
                    else
                    {
                        y += iteratorVariable6;
                        iteratorVariable7 += iteratorVariable0;
                    }
                }
                else
                {
                    x += iteratorVariable5;
                    iteratorVariable7 -= iteratorVariable1;
                }
                iteratorVariable4--;
            }
        }

        public MapPoint GetCellInDirection(DirectionsEnum direction, short step)
        {
            MapPoint mapPoint = null;
            switch (direction)
            {
                case DirectionsEnum.DIRECTION_EAST:
                    mapPoint = MapPoint.GetPoint(this.m_x + (int)step, this.m_y + (int)step);
                    break;
                case DirectionsEnum.DIRECTION_SOUTH_EAST:
                    mapPoint = MapPoint.GetPoint(this.m_x + (int)step, this.m_y);
                    break;
                case DirectionsEnum.DIRECTION_SOUTH:
                    mapPoint = MapPoint.GetPoint(this.m_x + (int)step, this.m_y - (int)step);
                    break;
                case DirectionsEnum.DIRECTION_SOUTH_WEST:
                    mapPoint = MapPoint.GetPoint(this.m_x, this.m_y - (int)step);
                    break;
                case DirectionsEnum.DIRECTION_WEST:
                    mapPoint = MapPoint.GetPoint(this.m_x - (int)step, this.m_y - (int)step);
                    break;
                case DirectionsEnum.DIRECTION_NORTH_WEST:
                    mapPoint = MapPoint.GetPoint(this.m_x - (int)step, this.m_y);
                    break;
                case DirectionsEnum.DIRECTION_NORTH:
                    mapPoint = MapPoint.GetPoint(this.m_x - (int)step, this.m_y + (int)step);
                    break;
                case DirectionsEnum.DIRECTION_NORTH_EAST:
                    mapPoint = MapPoint.GetPoint(this.m_x, this.m_y + (int)step);
                    break;
            }
            MapPoint result;
            if (mapPoint != null)
            {
                if (MapPoint.IsInMap(mapPoint.X, mapPoint.Y))
                {
                    result = mapPoint;
                }
                else
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }
            return result;
        }
        public MapPoint[] GetCellsInDirection(DirectionsEnum direction, short range)
        {
            MapPoint[] points = new MapPoint[range];

            for (short i = 1; i < range + 1; i++)
            {
                points[i - 1] = this.GetCellInDirection(direction, i);
            }
            return points;
        }
        public MapPoint GetNearestCellInDirection(DirectionsEnum direction)
        {
            return this.GetCellInDirection(direction, 1);
        }
        public System.Collections.Generic.IEnumerable<MapPoint> GetAdjacentCells(Func<short, bool> predicate)
        {
            MapPoint mapPoint = new MapPoint(this.m_x + 1, this.m_y);
            if (MapPoint.IsInMap(mapPoint.X, mapPoint.Y) && predicate(mapPoint.CellId))
            {
                yield return mapPoint;
            }
            MapPoint mapPoint2 = new MapPoint(this.m_x, this.m_y - 1);
            if (MapPoint.IsInMap(mapPoint2.X, mapPoint2.Y) && predicate(mapPoint2.CellId))
            {
                yield return mapPoint2;
            }
            MapPoint mapPoint3 = new MapPoint(this.m_x, this.m_y + 1);
            if (MapPoint.IsInMap(mapPoint3.X, mapPoint3.Y) && predicate(mapPoint3.CellId))
            {
                yield return mapPoint3;
            }
            MapPoint mapPoint4 = new MapPoint(this.m_x - 1, this.m_y);
            if (MapPoint.IsInMap(mapPoint4.X, mapPoint4.Y) && predicate(mapPoint4.CellId))
            {
                yield return mapPoint4;
            }
            yield break;
        }
        public static bool IsInMap(int x, int y)
        {
            return x + y >= 0 && x - y >= 0 && (long)(x - y) < 40L && (long)(x + y) < 28L;
        }
        public static uint CoordToCellId(int x, int y)
        {
            if (!MapPoint.m_initialized)
            {
                MapPoint.InitializeStaticGrid();
            }
            return (uint)((long)(x - y) * 14L + (long)y + (long)((x - y) / 2));
        }
        public static Point CellIdToCoord(uint cellId)
        {
            if (!MapPoint.m_initialized)
            {
                MapPoint.InitializeStaticGrid();
            }
            MapPoint point = MapPoint.GetPoint((short)cellId);
            return new Point(point.X, point.Y);
        }
        private static void InitializeStaticGrid()
        {
            MapPoint.m_initialized = true;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            while ((long)num4 < 20L)
            {
                int num5 = 0;
                while ((long)num5 < 14L)
                {
                    MapPoint.OrthogonalGridReference[num3++] = new MapPoint(num + num5, num2 + num5);
                    num5++;
                }
                num++;
                num5 = 0;
                while ((long)num5 < 14L)
                {
                    MapPoint.OrthogonalGridReference[num3++] = new MapPoint(num + num5, num2 + num5);
                    num5++;
                }
                num2--;
                num4++;
            }
        }
        public static MapPoint GetPoint(int x, int y)
        {
            return new MapPoint(x, y);
        }
        public static MapPoint GetPoint(short cell)
        {
            return MapPoint.OrthogonalGridReference[(int)cell];
        }
        public override string ToString()
        {
            return string.Concat(new object[]
            {
                "[MapPoint(x:",
                this.m_x,
                ", y:",
                this.m_y,
                ", id:",
                this.m_cellId,
                ")]"
            });
        }
        public override bool Equals(object obj)
        {
            return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != typeof(MapPoint)) && this.Equals((MapPoint)obj)));
        }
        public bool Equals(MapPoint other)
        {
            return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (other.m_cellId == this.m_cellId && other.m_x == this.m_x && other.m_y == this.m_y));
        }
        public override int GetHashCode()
        {
            int num = (int)this.m_cellId;
            num = (num * 397 ^ this.m_x);
            return num * 397 ^ this.m_y;
        }
        public static void AddCellIfValid(int x, int y, MapRecord map, IList<short> container)
        {
            if (MapPoint.IsInMap(x, y))
            {
                container.Add((short)MapPoint.CoordToCellId(x, y));
            }
        }
        public MapPoint[] GetNearPoints()
        {
            return this.GetAdjacentCells((short entry)
                    => true).ToArray();
        }

        public MapPoint GetCellSymetrieByPortail(MapPoint target, MapPoint cellExit)
        {
            var dir = OrientationTo(target);
            List<DirectionsEnum> dirs = new List<DirectionsEnum>() { dir };
            MapPoint lastCell = this;
            var num = 0;
            while (num < 140)
            {
                lastCell = lastCell.GetCellInDirection(dir, 1);
                dir = lastCell.OrientationTo(target, true);
                if (lastCell.CellId == target.CellId)
                    break;
                dirs.Add(dir);
                num++;
            }
            lastCell = cellExit;
            foreach (var item in dirs)
            {
                lastCell = lastCell.GetCellInDirection(item, 1);
            }

            return lastCell;
        }
    }
}
