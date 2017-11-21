using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Context
{
    [Table("Challenges")]
    public class ChallengeRecord : ITable
    {
        public static List<ChallengeRecord> Challenges = new List<ChallengeRecord>();

        public short Id;

        [Ignore]
        public ChallengeTypeEnum ChallengeEnum
        {
            get
            {
                return (ChallengeTypeEnum)Id;
            }
        }

        public string Name;

        public string Description;

        public List<short> IncompatibleChallenges;

        public ChallengeRecord(short id,string name,string description,List<short> incompatibleChallenges)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.IncompatibleChallenges = incompatibleChallenges;
        }

        public static ChallengeRecord GetChallenge(ChallengeTypeEnum type)
        {
            return Challenges.Find(x => x.ChallengeEnum == type);
        }
    }
}
