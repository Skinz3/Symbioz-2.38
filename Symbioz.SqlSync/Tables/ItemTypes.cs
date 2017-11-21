using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("ItemTypes.d2o","ItemType"),Table("ItemTypes")]
    public class ItemTypes// : ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("rawZone")]
        public string RawZone;

        [D2OField("mimickable")]
        public bool Mimickable;
    }
}
