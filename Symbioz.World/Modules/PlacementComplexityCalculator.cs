using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Symbioz.World.Providers.Maps.PlacementPattern
{
    /// <summary>
    /// Bouh2
    /// </summary>
    public class PlacementComplexityCalculator
    {
        private class PointsGroup
        {
            public Point[] Points;

            public Point Center;

            public PointsGroup(Point[] points, Point center)
            {
                this.Points = points;
                this.Center = center;
            }
        }

        private Point[] m_points;

        public PlacementComplexityCalculator(Point[] points)
        {
            this.m_points = points;
        }

        public int Compute()
        {
            PlacementComplexityCalculator.PointsGroup[] groups = this.GetPointsGroups();
            int result;
            if (groups.Length == 0)
            {
                result = 0;
            }
            else
            {
                double distanceSum = 0.0;
                List<PlacementComplexityCalculator.PointsGroup> exclusions = new List<PlacementComplexityCalculator.PointsGroup>();
                PlacementComplexityCalculator.PointsGroup[] array = groups;
                PlacementComplexityCalculator.PointsGroup group;
                for (int i = 0; i < array.Length; i++)
                {
                    group = array[i];
                    distanceSum += (from entry in groups
                                    where !exclusions.Contains(entry)
                                    select entry).Sum((PlacementComplexityCalculator.PointsGroup entry) => this.DistanceTo(entry.Center, group.Center));
                    exclusions.Add(group);
                }
                double distanceAverage = distanceSum / (double)groups.Length;
                int counts = this.m_points.Length;
                result = (int)((double)counts * distanceAverage + (double)groups.Length * groups.Average((PlacementComplexityCalculator.PointsGroup entry) => entry.Points.Length));
            }
            return result;
        }

        private PlacementComplexityCalculator.PointsGroup[] GetPointsGroups()
        {
            List<PlacementComplexityCalculator.PointsGroup> result = new List<PlacementComplexityCalculator.PointsGroup>();
            List<Point> exclusions = new List<Point>();
            Point[] points = this.m_points;
            for (int i = 0; i < points.Length; i++)
            {
                Point point = points[i];
                if (!exclusions.Contains(point))
                {
                    List<Point> adjacents = this.FindAllAdjacentsPoints(point, new List<Point>(new Point[]
					{
						point
					}));
                    adjacents.Add(point);
                    Point[] group = adjacents.ToArray();
                    if (group.Length > 0)
                    {
                        exclusions.Add(point);
                        exclusions.AddRange(adjacents);
                        result.Add(new PlacementComplexityCalculator.PointsGroup(group, this.GetCenter(group)));
                    }
                }
            }
            return result.ToArray();
        }

        private List<Point> FindAllAdjacentsPoints(Point point, List<Point> exclusions)
        {
            List<Point> result = new List<Point>();
            foreach (Point adjacentPoint in from entry in this.GetAdjacentPoints(point)
                                            where this.m_points.Contains(entry)
                                            select entry)
            {
                if (!exclusions.Contains(adjacentPoint))
                {
                    exclusions.Add(adjacentPoint);
                    result.Add(adjacentPoint);
                    result.AddRange(this.FindAllAdjacentsPoints(adjacentPoint, exclusions));
                }
            }
            return result;
        }

        private Point GetCenter(Point[] points)
        {
            return new Point(points.Sum((Point entry) => entry.X) / points.Length, points.Sum((Point entry) => entry.Y) / points.Length);
        }

        private double DistanceTo(Point ptA, Point ptB)
        {
            return Math.Sqrt((double)((ptB.X - ptA.X) * (ptB.X - ptA.X) + (ptB.Y - ptA.Y) * (ptB.Y - ptA.Y)));
        }

        private Point[] GetAdjacentPoints(Point point)
        {
            return new Point[]
			{
				point + new Size(1, 0),
				point + new Size(0, 1),
				point + new Size(-1, 0),
				point + new Size(0, -1),
				point + new Size(1, 1),
				point + new Size(-1, 1),
				point + new Size(1, -1),
				point + new Size(-1, -1)
			};
        }
    }
}
