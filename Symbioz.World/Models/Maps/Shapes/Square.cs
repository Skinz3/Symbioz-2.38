using Symbioz.Protocol.Enums;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;

namespace Symbioz.World.Models.Maps.Shapes
{
    public class Square : IShape
    {
        public uint Surface
        {
            get
            {
                return (uint)((this.Radius * 2 + 1) * (this.Radius * 2 + 1));
            }
        }
        public byte MinRadius
        {
            get;
            set;
        }
        public DirectionsEnum Direction
        {
            get;
            set;
        }
        public byte Radius
        {
            get;
            set;
        }
        public Square(byte minRadius, byte radius)
        {
            this.MinRadius = minRadius;
            this.Radius = radius;
        }
        public short[] GetCells(short centerCell, MapRecord map)
        {
            MapPoint mapPoint = new MapPoint(centerCell);
            List<short> list = new List<short>();

            list.Add(centerCell);

            for (int i = 1; i < Radius + 1; i++)
            {
                var adj = mapPoint.GetCellInDirection(DirectionsEnum.DIRECTION_EAST, (short)i);
                if (adj != null)
                {
                    list.Add(adj.CellId);

                    MapPoint next = adj;

                    foreach (var cell in next.GetCellsInDirection(DirectionsEnum.DIRECTION_NORTH_WEST, (short)(i * 2)))
                    {
                        if (cell == null)
                        {
                            return new short[0];
                        }
                        if (!list.Contains(cell.CellId))
                        {
                            MapPoint.AddCellIfValid(cell.X, cell.Y, map, list);
                            next = cell;
                        }
                    }

                    foreach (var cell in next.GetCellsInDirection(DirectionsEnum.DIRECTION_SOUTH_WEST, (short)(i * 2)))
                    {
                        if (cell == null)
                        {
                            return new short[0];
                        }
                        if (!list.Contains(cell.CellId))
                        {
                            MapPoint.AddCellIfValid(cell.X, cell.Y, map, list);
                            next = cell;
                        }
                    }

                    foreach (var cell in next.GetCellsInDirection(DirectionsEnum.DIRECTION_SOUTH_EAST, (short)(i * 2)))
                    {
                        if (cell == null)
                        {
                            return new short[0];
                        }
                        if (!list.Contains(cell.CellId))
                        {
                            MapPoint.AddCellIfValid(cell.X, cell.Y, map, list);
                            next = cell;
                        }
                    }

                    foreach (var cell in next.GetCellsInDirection(DirectionsEnum.DIRECTION_NORTH_EAST, (short)(i * 2)))
                    {
                        if (cell == null)
                        {
                            return new short[0];
                        }
                        if (!list.Contains(cell.CellId))
                        {
                            MapPoint.AddCellIfValid(cell.X, cell.Y, map, list);
                            next = cell;
                        }
                    }


                }
                else
                    list.Clear();
            }

            return list.ToArray();
        }

    }
}
