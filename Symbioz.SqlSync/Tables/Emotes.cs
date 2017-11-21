using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Emoticons.d2o", "Emoticon"), Table("Emotes")]
    public class Emotes// : ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("nameId"),i18n]
        public string Name;

        [D2OField("aura")]
        public bool IsAura;
    }
}
