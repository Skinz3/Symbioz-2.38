using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Providers.Brain.Actions;
using Symbioz.World.Providers.Brain.Behaviors;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain
{
    public class EnvironmentAnalyser : Singleton<EnvironmentAnalyser>
    {
        Dictionary<TargetSelector, MethodInfo> Handlers;

        [StartupInvoke("Environment Analyser", StartupInvokePriority.Eighth)]
        public void Initialize()
        {
            Handlers = this.GetType().MethodsWhereAttributes<TargetSelector>();
        }
        public const sbyte WarningLifePercentage = 15;

        public List<ActionType> GetSortedActions(BrainFighter fighter)
        {
            List<ActionType> actions = new List<ActionType>();
            if (fighter.Brain.HasBehavior)
            {
                var behaviorActions = fighter.Brain.GetBehavior<Behavior>().GetSortedActions();

                if (behaviorActions != null)
                {
                    return behaviorActions.ToList();
                }
            }

            actions.Add(ActionType.CastSpell);
            actions.Add(ActionType.MoveToEnemy);
            actions.Add(ActionType.CastSpell);
            actions.Add(ActionType.Flee);
            actions.Add(ActionType.CastSpell);

            return actions;
        }
        /// <summary>
        /// Index = Priority
        /// </summary>
        /// <param name="fighter"></param>
        /// <returns></returns>
        public Dictionary<int, SpellCategoryEnum> GetSpellsCategories(BrainFighter fighter)
        {

            Dictionary<int, SpellCategoryEnum> categories;
            if (fighter.Brain.HasBehavior)
            {
                categories = fighter.Brain.GetBehavior<Behavior>().GetSpellsCategories();

                if (categories != null)
                {
                    return categories;
                }
            }

            categories = new Dictionary<int, SpellCategoryEnum>();

            if (fighter.Team.LastDead() != null)
            {
                categories.Add(-1, SpellCategoryEnum.ReviveDeath);
            }

            if (fighter.CanSummon)
                categories.Add(0, SpellCategoryEnum.Summon);

            if (!fighter.IsThereEnemy(fighter.Point.GetNearPoints()))
            {
                categories.Add(1, SpellCategoryEnum.Teleport);
            }

            if (fighter.Stats.LifePercentage > WarningLifePercentage)
            {
                categories.Add(2, SpellCategoryEnum.Agressive);
            }
            else
            {
                categories.Add(5, SpellCategoryEnum.Agressive);
            }

            categories.Add(3, SpellCategoryEnum.Heal);
            categories.Add(4, SpellCategoryEnum.Buff);



            categories.Add(6, SpellCategoryEnum.Unknown);
            return categories;
        }
        public short GetTargetedCell(BrainFighter fighter, SpellCategoryEnum category, SpellLevelRecord level)
        {
            if (fighter.Brain.HasBehavior)
            {
                var cellId = fighter.Brain.GetBehavior<Behavior>().GetTargetCellForSpell(level.SpellId);

                if (cellId != null)
                {
                    return cellId.Value;
                }
            }
            var handler = Handlers.FirstOrDefault(x => x.Key.SpellCategory == category);

            if (handler.Value != null)
            {
                return (short)handler.Value.Invoke(this, new object[] { fighter, level });
            }
            else
                return fighter.CellId;
        }

        [TargetSelector(SpellCategoryEnum.Agressive)]
        public short AgressiveTarget(BrainFighter fighter, SpellLevelRecord level)
        {
            if (fighter.Brain.HasBehavior)
            {
                var agressiveCell = fighter.Brain.GetBehavior<Behavior>().GetAgressiveCell();

                if (agressiveCell != -1)
                {
                    return agressiveCell;
                }
            }

            if (level.MaxRange > 0)
            {
                var targets = fighter.OposedTeam().CloserFighters(fighter);
                var target = targets.LastOrDefault(x => x.Stats.InvisibilityState == Protocol.Enums.GameActionFightInvisibilityStateEnum.VISIBLE);

                if (target != null)
                    return target.CellId;
                else
                    return Array.FindAll(fighter.GetCastZone(fighter.CellId, level), x => fighter.Fight.Map.WalkableDuringFight((ushort)x)).Random();
            }
            else
            {
                return fighter.CellId;
            }
        }

        [TargetSelector(SpellCategoryEnum.Buff)]
        public short BuffTarget(BrainFighter fighter, SpellLevelRecord level)
        {
            if (fighter.Brain.HasBehavior)
            {
                var buffCell = fighter.Brain.GetBehavior<Behavior>().GetBuffCell();

                if (buffCell != -1)
                {
                    return buffCell;
                }
            }

            if (level.MaxRange > 0)
            {
                var target = fighter.Team.LowerFighterPercentage();
                return target != null ? target.CellId : fighter.CellId;
            }
            else
            {
                return fighter.CellId;
            }
        }

        [TargetSelector(SpellCategoryEnum.Teleport)]
        public short TeleportTarget(BrainFighter fighter, SpellLevelRecord level)
        {
            if (fighter.Brain.HasBehavior)
            {
                var teleportCell = fighter.Brain.GetBehavior<Behavior>().GetTeleportCell();

                if (teleportCell != -1)
                {
                    return teleportCell;
                }
            }

            var target = fighter.OposedTeam().LowerFighter();





            if (target != null)
            {
                var points = target.Point.GetNearPoints();

                if (points.Count() > 0)
                {
                    var pt = points.FirstOrDefault(x => fighter.Fight.IsCellFree(x.CellId));

                    if (pt != null)
                    {
                        return pt.CellId;
                    }
                }

            }
            return -1;
        }
        [TargetSelector(SpellCategoryEnum.Summon)]
        public short SummonTarget(BrainFighter fighter, SpellLevelRecord level)
        {
            return fighter.NearFreeCell();

        }
        [TargetSelector(SpellCategoryEnum.ReviveDeath)]
        public short ReviveDeathTarget(BrainFighter fighter, SpellLevelRecord level)
        {
            return fighter.Point.GetNearPoints().FirstOrDefault(x => fighter.Fight.IsCellFree(x.CellId)).CellId;
        }
        [TargetSelector(SpellCategoryEnum.Heal)]
        public short HealTarget(BrainFighter fighter, SpellLevelRecord level)
        {
            return fighter.Team.LowerFighterPercentage().CellId;
        }
        [TargetSelector(SpellCategoryEnum.Unknown)]
        public short UnkownTarget(BrainFighter fighter, SpellLevelRecord level)
        {
            return AgressiveTarget(fighter, level);
        }
    }
}
