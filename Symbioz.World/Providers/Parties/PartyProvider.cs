using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Parties;
using Symbioz.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Parties
{
    public class PartyProvider : Singleton<PartyProvider>
    {
        private UniqueIdProvider IdProvider = new UniqueIdProvider(0);

        public List<AbstractParty> Parties = new List<AbstractParty>();

        public ClassicalParty CreateParty(Character leader)
        {
            return new ClassicalParty(IdProvider.Pop(), leader);
        }
        public ArenaParty CreatePartyArena(Character leader)
        {
            return null;
        }

    
    }
}
