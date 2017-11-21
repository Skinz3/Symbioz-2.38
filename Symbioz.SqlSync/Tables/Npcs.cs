using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Npcs.d2o", "Npc"), Table("Npcs")]
    public class Npcs  //: ID2OTable
    {
        [D2OField("id"), Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("dialogMessages")]
        public string Messages;

        [D2OField("dialogReplies")]
        public string Replies;

        [D2OField("actions")]
        public List<sbyte> Actions = new List<sbyte>();

        [D2OField("look")]
        public string Look;


    }
}
