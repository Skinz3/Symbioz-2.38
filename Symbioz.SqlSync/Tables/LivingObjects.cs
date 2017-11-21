using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("LivingObjectSkinJntMood.d2o", "LivingObjectSkinJntMood"), Table("LivingObjects")]
    public class LivingObjects //: ID2OTable
    {
        [D2OField("skinId"), Primary]
        public int SkinId;

        [D2OField("moods"), i18n]
        public string Moods;
    }
}
