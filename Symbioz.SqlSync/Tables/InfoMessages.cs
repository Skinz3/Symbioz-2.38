using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("InfoMessages.d2o","InfoMessage"),Table("InfoMessages")]
    public class InfoMessages //: ID2OTable
    {
        [D2OField("typeId")]
        public int TypeId;

        [D2OField("textId"),i18n]
        public string Text;

        [D2OField("messageId"),Primary]
        public int MessageId;
    }
}
