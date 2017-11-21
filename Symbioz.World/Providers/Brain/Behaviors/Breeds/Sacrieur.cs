using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Providers.Brain.Actions;

namespace Symbioz.World.Providers.Brain.Behaviors.Breeds
{
    /// <summary>
    /// Comportement d'une IA jouant un sacrieur.
    /// </summary>
    [Behavior("Sacrieur")]
    public class Sacrieur : Behavior
    {
        public Sacrieur(BrainFighter fighter) : base(fighter)
        {

        }
        public override short? GetTargetCellForSpell(ushort spellId) // -1 : We dont cast spell
        {
            switch (spellId)
            {
                case 0:
                    return -1;
                case 435:
                    return -1;
                case 438:
                    return -1;
                case 439:
                    return (short)(Fighter.Abilities.IsMeleeWithEnenmy() ? Fighter.CellId : -1);
                case 440:
                    return -1;
                case 450:
                    return -1;

            }

            return base.GetTargetCellForSpell(spellId); // default handler
        }
        public override ActionType[] GetSortedActions()
        {
            return new ActionType[]
            {
                ActionType.CastSpell,
                ActionType.MoveToEnemy,
                ActionType.CastSpell,
            };
        }
        public override Dictionary<int, SpellCategoryEnum> GetSpellsCategories()
        {
            return new Dictionary<int, SpellCategoryEnum>()
            {
                { 0,SpellCategoryEnum.Buff },
                { 1, SpellCategoryEnum.Agressive },
                { 2, SpellCategoryEnum.Heal },
                { 3, SpellCategoryEnum.Summon },
                { 4, SpellCategoryEnum.Unknown }
            };
        }
    }
}
