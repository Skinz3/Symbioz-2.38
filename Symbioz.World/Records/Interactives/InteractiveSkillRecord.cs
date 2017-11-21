using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Core;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Interactives
{
    [Table("InteractiveSkills")]
    public class InteractiveSkillRecord : ITable
    {
        public static List<InteractiveSkillRecord> InteractiveSkills = new List<InteractiveSkillRecord>();

        [Primary]
        public int UID;

        public string ActionType;

        public string Value1;

        public string Value2;

        [Update]
        public int ElementId;

        public ushort SkillId;

        public InteractiveSkillRecord(int uid, string actionType, string value1,
            string value2, int elementId, ushort skillId)
        {
            this.UID = uid;
            this.ActionType = actionType;
            this.Value1 = value1;
            this.Value2 = value2;
            this.ElementId = elementId;
            this.SkillId = skillId;
        }
        public InteractiveElementSkill GetInteractiveElementSkill()
        {
            return new InteractiveElementSkill(SkillId, UID);
        }
        public static List<InteractiveSkillRecord> GetInteractiveSkills(int elementId)
        {
            return InteractiveSkillRecord.InteractiveSkills.FindAll(x => x.ElementId == elementId);
        }
        public static bool OneExist(int elementId)
        {
            return InteractiveElementRecord.InteractiveElements.Exists(x => x.ElementId == elementId && x.Skills.Count > 0);
        }



    }

}
