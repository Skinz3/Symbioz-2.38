using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Marks;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects
{
    public class BombProvider : Singleton<BombProvider>
    {
        public const int WALL_MAX_DISTANCE = 6;

        public DirectionsEnum[] WallDirections = new DirectionsEnum[]
       {
           DirectionsEnum.DIRECTION_NORTH_EAST,
           DirectionsEnum.DIRECTION_NORTH_WEST,
           DirectionsEnum.DIRECTION_SOUTH_EAST,
           DirectionsEnum.DIRECTION_SOUTH_WEST,
       };


        public void UpdateWalls(BombFighter fighter)
        {
            bool seq = fighter.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);

            foreach (var wall in new List<Wall>(fighter.Walls))
            {
                if (wall.Valid() == false)
                {
                    wall.Destroy();
                }
            }

            foreach (var direction in WallDirections)
            {
                MapPoint current = fighter.Point.GetCellInDirection(direction, 1);

                for (byte i = 0; i < WALL_MAX_DISTANCE; i++)
                {
                    if (current != null)
                    {
                        current = current.GetCellInDirection(direction, 1);

                        if (current != null) // La cell n'existe pas
                        {
                            BombFighter target = fighter.Fight.GetFighter(current.CellId) as BombFighter;

                            if (target != null && target.IsOwner(fighter.Owner) && target.SpellBombRecord.SpellId == fighter.SpellBombRecord.SpellId)
                            {
                                foreach (var targetWall in target.Walls.ToArray())
                                {
                                    if (targetWall.ContainsCell(fighter.CellId))
                                    {
                                        targetWall.Destroy();
                                    }
                                }
                                Wall wall = fighter.Fight.AddWall(fighter.Owner, fighter.WallSpellLevel, fighter.WallSpellLevel.Effects.FirstOrDefault(), fighter, target, i);

                                foreach (var cell in wall.GetCells())
                                {
                                    var wall2 = fighter.Walls.FirstOrDefault(x => x.ContainsCell(cell));

                                    if (wall2 != null)
                                        wall2.Destroy();

                                }

                                fighter.Walls.Add(wall);
                                target.Walls.Add(wall);
                                break;
                            }
                        }
                    }
                }

            }
            if (seq)
                fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }
    }
}
