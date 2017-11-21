using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Spells.d2o", "Spell"), Table("Spells")]
    public class Spells  //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("descriptionId"), i18n]
        public string Description;

        [D2OField("spellLevels")]
        public List<ushort> SpellsLevels = new List<ushort>();

        public int Category;

  
        //[D2OField("iconId")]
        //public int IconId;

        //[D2OField("scriptParams")]
        //public string ScriptParams;
    }
}
