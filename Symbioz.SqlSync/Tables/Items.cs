using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using Symbioz.SqlSync.D2OTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Items.d2o", "Item"), Table("Items")]
    public class Items  //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("typeId")]
        public ushort TypeId;

        [D2OField("appearanceId")]
        public ushort AppearanceId;

        [D2OField("level")]
        public ushort Level;

        [D2OField("price")]
        public int Price;

        [D2OField("realWeight")]
        public int Weight;

        [D2OField("possibleEffects")]
        public string Effects;

        [D2OField("criteria")]
        public string Criteria;

    }
}
