using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("NpcMessages.d2o", "NpcMessage"), Table("NpcMessages")]
    public class NpcMessages // : ID2OTable
    {
        [D2OField("id")]
        public int Id;

        [D2OField("messageId"), i18n]
        public string Message;
    }
}
