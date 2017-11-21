using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Providers.Brain.Actions;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Agressive")]
    public class Agressive : Behavior
    {
        public Agressive(BrainFighter fighter) : base(fighter)
        {
        }
        public override ActionType[] GetSortedActions()
        {
            return new ActionType[] { ActionType.MoveToEnemy, ActionType.CastSpell };
        }
        public override Dictionary<int, SpellCategoryEnum> GetSpellsCategories()
        {
            var dic = new Dictionary<int, SpellCategoryEnum>();
            dic.Add(0, SpellCategoryEnum.Agressive);
            dic.Add(1, SpellCategoryEnum.Buff);
            dic.Add(2, SpellCategoryEnum.Heal);

            return dic;
        }
    }
}
