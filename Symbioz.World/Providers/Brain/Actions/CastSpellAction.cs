using Symbioz.Core;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Actions
{
    [Brain(ActionType.CastSpell)]
    public class CastSpellAction : BrainAction
    {
        private Dictionary<int, SpellCategoryEnum> Categories
        {
            get;
            set;
        }
        public CastSpellAction(BrainFighter fighter)
            : base(fighter)
        {

        }
        public override void Analyse()
        {
            this.Categories = EnvironmentAnalyser.Instance.GetSpellsCategories(Fighter);
        }
        public override void Execute()
        {
            if (!Fighter.Alive)
                return;

            foreach (var category in Categories.OrderByDescending(x => x.Key).Reverse())
            {
                var levels = Fighter.Template.SpellRecords.FindAll(x => x.CategoryEnum == category.Value).ConvertAll<SpellLevelRecord>(x => x.GetLastLevel());

                foreach (var level in levels.Shuffle())
                {
                    if (Fighter.Stats.ActionPoints.TotalInContext() >= level.ApCost)
                    {
                        if (Fighter.Fight.Ended)
                            return;

                        short cellId = EnvironmentAnalyser.Instance.GetTargetedCell(Fighter, category.Value, level);

                        if (cellId != -1)
                        {
                            var spell = SpellRecord.GetSpellRecord(level.SpellId);
                            if (spell != null)
                                Fighter.CastSpell(spell, spell.GetLastLevelGrade(), cellId);
                        }
                        else
                            break;
                    }
                }

            }
        }
    }
}
