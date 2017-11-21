using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Parties
{
    public class ClassicalParty : AbstractParty
    {
        public override PartyTypeEnum Type
        {
            get
            {
                return PartyTypeEnum.PARTY_TYPE_CLASSICAL;
            }
        }
        public override sbyte MaxParticipants
        {
            get
            {
                return 8+1;
            }
        }
        public ClassicalParty(int partyId, Character leader)
            : base(partyId, leader)
        {
        }



       
    }
}
