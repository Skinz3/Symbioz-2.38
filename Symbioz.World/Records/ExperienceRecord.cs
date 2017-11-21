using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records
{
    [Table("Experiences")]
    public class ExperienceRecord : ITable
    {
        public const ushort MaxCharacterLevel = 200;

        public const ushort MaxMinationLevel = 200;

        public const sbyte MaxGrade = 10;

        public static List<ExperienceRecord> Experiences = new List<ExperienceRecord>();

        public ushort Level;

        public ulong Player;

        public int Honor;

        public ulong Guild;

        public ulong Mount;

        public ulong Job;

        public ExperienceRecord(ushort level, ulong exp, int honor, ulong guild, ulong mount, ulong job)
        {
            this.Level = level;
            this.Player = exp;
            this.Honor = (int)honor;
            this.Guild = guild;
            this.Mount = mount;
            this.Job = job;
        }

        public static ExperienceRecord GetExperienceForLevel(ushort level)
        {
            return Experiences.Find(x => x.Level == level);
        }
        public static ExperienceRecord GetExperienceForNextLevel(ushort level)
        {
            if (level >= MaxCharacterLevel)
                return HighestExperience();
            return GetExperienceForLevel((ushort)(level + 1));
        }
        public static ushort GetHonorForGrade(sbyte grade)
        {
            if (grade > MaxGrade || grade == 0)
                return 0;
            return (ushort)Experiences.Find(x => x.Level == grade).Honor;
        }
        public static ushort GetHonorNextGrade(sbyte grade)
        {
            return GetHonorForGrade((sbyte)(grade + 1));
        }
        public static ulong GetExperienceForGuild(ushort level)
        {
            if (level > MaxCharacterLevel)
                return 0;
            return Experiences.Find(x => x.Level == level).Guild;
        }
        public static ushort GetLevelFromGuildExperience(ulong exp)
        {
            ushort result;

            ExperienceRecord highest = HighestExperience();
            if (exp >= highest.Guild)
            {
                result = highest.Level;
            }
            else
            {
                result = (ushort)(Experiences.FirstOrDefault(x => x.Guild > exp).Level - 1);
            }
            return result;
        }
        public static ExperienceRecord HighestExperience()
        {
            return Experiences.Last();
        }
        public static ExperienceRecord HighestHonorExperience()
        {
            return Experiences.Find(x => x.Level == MaxGrade);
        }
        public static ushort GetCharacterLevel(ulong experience)
        {
            ushort result;

            ExperienceRecord highest = HighestExperience();
            if (experience >= highest.Player)
            {
                result = highest.Level;
            }
            else
            {
                result = (ushort)(Experiences.FirstOrDefault(x => x.Player > experience).Level - 1);
            }
            return result;
        }
        public static ushort GetJobLevel(ulong experience)
        {
            ushort result;
            ExperienceRecord highest = HighestExperience();
            if (experience >= highest.Job)
            {
                result = highest.Level;
            }
            else
            {
                result = (ushort)(Experiences.FirstOrDefault(x => x.Job > experience).Level - 1);
            }
            return result;
        }
        public static sbyte GetGrade(ushort honor)
        {
            sbyte result;
            ExperienceRecord highest = HighestHonorExperience();
            if (honor >= highest.Honor)
            {
                result = MaxGrade;
            }
            else
            {
                result = (sbyte)(Experiences.FirstOrDefault(x => x.Honor > honor).Level - 1);
            }
            return result;
        }
    }
}
