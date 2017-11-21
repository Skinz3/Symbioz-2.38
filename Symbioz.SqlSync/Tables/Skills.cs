using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Skills.d2o", "Skill"), Table("Skills")]
    public class Skills // : ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("nameId"), i18n]
        public string Name;

        [D2OField("parentJobId")]
        public short ParentJobId;

        [D2OField("isForgemagus")]
        public bool IsForgemagus;

        [D2OField("interactiveId")]
        public short InteractiveId;

        [D2OField("elementActionId")]
        public short ElementActionId;

        [D2OField("gatheredRessourceItem")]
        public short GatheredRessourceItem;

        [D2OField("levelMin")]
        public short MinLevel;

        [D2OField("useAnimation")]
        public string UseAnimation;

    }
}
