using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers;
using Symbioz.World.Providers.Maps.Path;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.FightModels
{
    /// <summary>
    /// GetDelayedAbstractBuff ()
    /// GetAbstractBuff()
    /// </summary>
    public class Abilities
    {
        private Fighter Fighter
        {
            get;
            set;
        }
        private Fight Fight
        {
            get
            {
                return Fighter.Fight;
            }
        }

        public bool IsMeleeWithEnenmy()
        {
            foreach (var point in Fighter.Point.GetNearPoints())
            {
                var fighter = Fighter.Fight.GetFighter(point.CellId);
                if (fighter != null && !fighter.IsFriendly(Fighter))
                {
                    return true;
                }
            }
            return false;
        }

        public Abilities(Fighter fighter)
        {
            Fighter = fighter;
        }
        public bool MeleeWith(Fighter fighter)
        {
            return fighter.Point.GetNearPoints().FirstOrDefault(x => x.CellId == Fighter.CellId) != null;
        }

        public void PushCaster(Fighter source, short delta, MapPoint castPoint)
        {
            MapPoint startCell;
            MapPoint endCell;
            Fighter FighterCopy;
            MapPoint point = (castPoint.CellId == Fighter.CellId) ? new MapPoint(source.CellId) : castPoint;
            if (point.CellId != Fighter.CellId)
            {
                DirectionsEnum direction = Fighter.Point.OrientationTo(point, false);
                startCell = source.Point;
                MapPoint point2 = startCell;
                for (int i = 0; i < delta; i++)
                {
                    MapPoint nearestCellInDirection = point2.GetNearestCellInDirection(direction);
                    if (nearestCellInDirection == null)
                    {
                        break;
                    }
                    if (Fight.ShouldTriggerOnMove(nearestCellInDirection.CellId))
                    {
                        point2 = nearestCellInDirection;
                        break;
                    }
                    if ((nearestCellInDirection == null) || !Fight.IsCellFree(nearestCellInDirection.CellId))
                    {
                        source.InflictPushDamages(source, (sbyte)(delta - i), true);

                        MapPoint nextNearest = nearestCellInDirection.GetNearestCellInDirection(direction);

                        Fighter nextFighter = Fight.GetFighter(nearestCellInDirection.CellId);

                        if (nextFighter != null)
                        {
                            nextFighter.InflictPushDamages(source, (sbyte)(delta - i), false);
                        }
                        break;
                    }
                    point2 = nearestCellInDirection;
                }
                endCell = point2;
                FighterCopy = Fighter;

                if (startCell != endCell)
                {
                    source.Slide(source, endCell.CellId);

                }
            }
        }
        public void BePulled(Fighter source, short delta, MapPoint castPoint)
        {
            MapPoint point1 = (int)castPoint.CellId != (int)Fighter.CellId ? castPoint : source.Point;
            if ((int)point1.CellId != (int)Fighter.CellId)
            {
                DirectionsEnum direction = point1.OrientationTo(Fighter.Point, false);
                MapPoint point2 = source.Point;
                MapPoint mapPoint1 = point2;
                for (int index = 0; index < (int)delta; ++index)
                {
                    MapPoint nearestCellInDirection = mapPoint1.GetNearestCellInDirection(direction);
                    if (nearestCellInDirection != null)
                    {
                        if (!Fight.ShouldTriggerOnMove(nearestCellInDirection.CellId))
                        {
                            if (Fight.IsCellFree(nearestCellInDirection.CellId))
                                mapPoint1 = nearestCellInDirection;
                            else
                                break;
                        }
                        else
                        {
                            mapPoint1 = nearestCellInDirection;
                            break;
                        }
                    }
                    else
                        break;
                }

                source.Slide(source, mapPoint1.CellId);
            }
        }

        public void PullForward(Fighter source, short delta, MapPoint castPoint)
        {
            MapPoint point1 = (int)castPoint.CellId != (int)Fighter.CellId ? castPoint : source.Point;
            if ((int)point1.CellId != (int)Fighter.CellId)
            {
                DirectionsEnum direction = Fighter.Point.OrientationTo(point1, true);
                MapPoint point2 = Fighter.Point;
                MapPoint mapPoint1 = point2;
                for (int index = 0; index < (int)delta; ++index)
                {
                    MapPoint nearestCellInDirection = mapPoint1.GetNearestCellInDirection(direction);
                    if (nearestCellInDirection != null)
                    {
                        if (!Fight.ShouldTriggerOnMove(nearestCellInDirection.CellId))
                        {
                            if (Fight.IsCellFree(nearestCellInDirection.CellId))
                                mapPoint1 = nearestCellInDirection;
                            else
                                break;
                        }
                        else
                        {
                            mapPoint1 = nearestCellInDirection;
                            break;
                        }
                    }
                    else
                        break;
                }

                Fighter.Slide(source, mapPoint1.CellId);
            }
        }
        public void PushBack(Fighter source, short delta, MapPoint castPoint)
        {
            if (Fighter.HasState(x => x.CantBePushed))
                return;
            MapPoint startCell;
            MapPoint endCell;
            Fighter FighterCopy;
            MapPoint point = (castPoint.CellId == Fighter.CellId) ? new MapPoint(source.CellId) : castPoint;
            if (point.CellId != Fighter.CellId)
            {
                DirectionsEnum direction = point.OrientationTo(Fighter.Point, true);
                startCell = Fighter.Point;
                MapPoint point2 = startCell;
                for (int i = 0; i < delta; i++)
                {
                    MapPoint nearestCellInDirection = point2.GetNearestCellInDirection(direction);

                    if (Fight.ShouldTriggerOnMove(nearestCellInDirection.CellId))
                    {
                        point2 = nearestCellInDirection;
                        break;
                    }
                    if ((nearestCellInDirection == null) || !Fight.IsCellFree(nearestCellInDirection.CellId))
                    {

                        Fighter.InflictPushDamages(source, (sbyte)(delta - i), true);
                        MapPoint nextNearest = nearestCellInDirection.GetNearestCellInDirection(direction);

                        Fighter nextFighter = Fight.GetFighter(nearestCellInDirection.CellId);

                        if (nextFighter != null)
                        {
                            nextFighter.InflictPushDamages(source, (sbyte)(delta - i), false);
                        }
                        break;
                    }
                    point2 = nearestCellInDirection;
                }
                endCell = point2;
                FighterCopy = Fighter;

                if (startCell != endCell)
                {
                    FighterCopy.Slide(source, endCell.CellId);

                }
            }
        }
    }
}
