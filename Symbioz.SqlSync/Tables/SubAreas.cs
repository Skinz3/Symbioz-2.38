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
    [D2O("SubAreas.d2o", "SubArea"), Table("Subareas")]
    public class SubAreas : ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("monsters")]
        public List<ushort> Monsters = new List<ushort>();

        [D2OField("ambientSounds"), Ignore]
        public string AmbientSounds;
    }
}
