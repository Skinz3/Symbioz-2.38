using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Records;
using Symbioz.World.Records.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Entities.Jobs
{
    public class CharacterJob
    {
        public sbyte JobId
        {
            get;
            set;
        }

        public ulong Experience
        {
            get;
            set;
        }

        [YAXDontSerialize]
        public ushort Level
        {
            get
            {
                return ExperienceRecord.GetJobLevel(Experience);
            }
        }

        [YAXDontSerialize]
        public JobsTypeEnum JobType
        {
            get
            {
                return (JobsTypeEnum)JobId;
            }
        }

        [YAXDontSerialize]
        public SkillRecord Skill
        {
            get
            {
                return SkillRecord.GetSkill(JobType);
            }
        }

        public JobCrafterDirectorySettings GetDirectorySettings()
        {
            return new JobCrafterDirectorySettings(JobId, (byte)Level, true);
        }
        public JobDescription GetJobDescription()
        {
            return new JobDescription(JobId, new SkillActionDescription[0]);
        }
        public JobExperience GetJobExperience()
        {
            return new JobExperience(JobId, (byte)Level, Experience, ExperienceRecord.GetExperienceForLevel(Level).Job, ExperienceRecord.GetExperienceForNextLevel(Level).Job);
        }
        public static IEnumerable<CharacterJob> New()
        {
            List<CharacterJob> jobs = new List<CharacterJob>();

            foreach (JobsTypeEnum job in Enum.GetValues(typeof(JobsTypeEnum)))
            {
                CharacterJob characterJob = new CharacterJob()
                {
                    JobId = (sbyte)job,
                    Experience = 0,

                };
                yield return characterJob;
            }

        }

    }
}
