using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.D2OTypes
{
    [D2oType("MonsterDrop")]
    public class MonsterDrop
    {
        [D2OField("dropId")]
        public int DropId { get; set; }

        [D2OField("objectId")]
        public ushort ItemId { get; set; }

        [D2OField("percentDropForGrade1")]
        public short PercentDropForGrade1 { get; set; }

        [D2OField("percentDropForGrade2")]
        public short PercentDropForGrade2 { get; set; }

        [D2OField("percentDropForGrade3")]
        public short PercentDropForGrade3 { get; set; }

        [D2OField("percentDropForGrade4")]
        public short PercentDropForGrade4 { get; set; }

        [D2OField("percentDropForGrade5")]
        public short PercentDropForGrade5 { get; set; }

        [D2OField("count")]
        public int Count { get; set; }

        [D2OField("hasCriteria")]
        public bool HasCriteria { get; set; }

        public int DropLimit { get { return 1; } }

        public int ProspectingLock { get { return 100; } }



    }
}
