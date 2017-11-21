using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Servers.d2o", "Server"), Table("Servers")]
    public class Servers //: ID2OTable
    {
        [D2OField("id"),Primary]
        public short Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("commentId"), i18n]
        public string Comment;

        [D2OField("openingDate")]
        public string OpeningDate;

        [D2OField("language")]
        public string Language;

        [D2OField("populationId")]
        public int PopulationId;

        [D2OField("gameTypeId")]
        public sbyte GameTypeId;

        [D2OField("communityId")]
        public short CommunityId;
    }
}
