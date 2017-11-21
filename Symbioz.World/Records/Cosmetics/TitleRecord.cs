using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Cosmetics
{
    [Table("titles")]
    public class TitleRecord : ITable
    {
        public static List<TitleRecord> Titles = new List<TitleRecord>();

        [Primary]
        public ushort Id;

        public string NameMale;

        public string NameFemale;

        public int CategoryId;

        public bool Visible;

        public TitleRecord(ushort id,string nameMale,string nameFemale,int categoryId,bool visible)
        {
            this.Id = id;
            this.NameMale = nameMale;
            this.NameFemale = nameFemale;
            this.CategoryId = categoryId;
            this.Visible = visible;
        }
    }
}
