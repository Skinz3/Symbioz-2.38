using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("SpellStates.d2o", "SpellState"), Table("SpellsStates")]
    public class SpellStates //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("preventsSpellCast")]
        public bool PreventsSpellCast;

        [D2OField("preventsFight")]
        public bool PreventsFight;

        [D2OField("isSilent")]
        public bool IsSilent;

        [D2OField("cantBeMoved")]
        public bool CantBeMoved;

        [D2OField("cantBePushed")]
        public bool CantBePushed;

        [D2OField("cantDealDamage")]
        public bool CantDealDamage;

        [D2OField("invulnerable")]
        public bool Invulnerable;

        [D2OField("cantSwitchPosition")]
        public bool CanSwitchPosition;

        [D2OField("incurable")]
        public bool Incurable;

        [D2OField("invulnerableMelee")]
        public bool InvulnerableMelee;

        [D2OField("invulnerableRange")]
        public bool InvulnerableRange;
    }
}
