using Symbioz.Protocol.Selfmade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Challenges
{
    public class ChallengeAttribute : Attribute
    {
        public ChallengeTypeEnum ChallengeType
        {
            get;
            private set;
        }

        public ChallengeAttribute(ChallengeTypeEnum type)
        {
            this.ChallengeType = type;
        }
    }
}
