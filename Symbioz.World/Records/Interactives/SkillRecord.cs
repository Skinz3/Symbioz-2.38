using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Interactives
{
    [Table("Skills")]
    public class SkillRecord : ITable
    {
        public static List<SkillRecord> Skills = new List<SkillRecord>();

        public ushort Id;

        public string Name;

        public sbyte ParentJobId;

        [Ignore]
        public JobsTypeEnum ParentJobIdEnum
        {
            get { return (JobsTypeEnum)ParentJobId; }
        }

        public bool IsForgemagus;

        public ushort InteractiveId;

        public int ElementActionId;

        public short GatheredRessourceItem;

        public ushort MinLevel;

        public string UseAnimation;

        public int GfxLookId;

        public ushort Duration;

        public bool CanMove;

        public SkillRecord(ushort id, string name, sbyte parentJobId, bool isForgemagus, ushort interactiveId,
            int elementActionId, short gatheredRessourceItem, ushort minLevel, string useAnimation, int gfxLookId,
           ushort duration, bool canMove)
        {
            this.Id = id;
            this.Name = name;
            this.ParentJobId = parentJobId;
            this.IsForgemagus = isForgemagus;
            this.InteractiveId = interactiveId;
            this.ElementActionId = elementActionId;
            this.GatheredRessourceItem = gatheredRessourceItem;
            this.MinLevel = minLevel;
            this.UseAnimation = useAnimation;
            this.GfxLookId = gfxLookId;
            this.Duration = duration;
            this.CanMove = canMove;
        }

        public static SkillRecord GetSkill(ushort id)
        {
            return Skills.Find(x => x.Id == id);
        }
        public static SkillRecord GetSkill(JobsTypeEnum job)
        {
            return Skills.Find(x => x.ParentJobIdEnum == job);
        }
    }
}
