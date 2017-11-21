using Symbioz.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Results
{
    public class CustomLootAttribute : Attribute
    {
        public string Criteria
        {
            get;
            set;
        }
        public FightTypeEnum FightType
        {
            get;
            set;
        }

        public CustomLootAttribute(string criteria, FightTypeEnum fightType)
        {
            this.Criteria = criteria;
            this.FightType = fightType;
        }
    }
}
