using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using Symbioz.SqlSync.D2OTypes;
using Symbioz.Tools.D2O;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;

namespace Symbioz.SqlSync.Tables
{
    [D2O("ItemSets.d2o", "ItemSet"), Table("ItemSets")]
    public class ItemSets //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("items")]
        public List<string> Items = new List<string>();

        [D2OField("effects")]
        public string Effects;


        [D2OFieldHandler("effects")]
        public void DeserializeEffects(ArrayList arraylist)
        {
            if (Name == "Panoplie du Champion")
            {

            }
            Effects = string.Empty;

            foreach (ArrayList value in arraylist)
            {
                foreach (DataClass subItem in value)
                {
                    Effects += (D2oTypeManager.Handle(subItem).XMLSerialize()) + ",";
                }
                Effects = Effects.Remove(Effects.Length - 1, 1);
                Effects += "|";


            }
        }
    }
}
