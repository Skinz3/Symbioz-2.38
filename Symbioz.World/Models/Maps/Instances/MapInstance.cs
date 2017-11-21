using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Network;
using Symbioz.World.Records;
using System;
using Symbioz.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Providers.Maps;
using Symbioz.World.Providers.Maps.Path;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Maps;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Records.Npcs;
using Symbioz.World.Providers.Maps.Monsters;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Interactives;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Items;
using Symbioz.World.Records.Characters;

namespace Symbioz.World.Models.Maps.Instances
{
    public class MapInstance : AbstractMapInstance
    {
        public MapInstance(MapRecord record) : base(record)
        {

        }
        public override MapComplementaryInformationsDataMessage GetMapComplementaryInformationsDataMessage(Character character)
        {
            return new MapComplementaryInformationsDataMessage(character.SubareaId, Record.Id, GetHousesInformations(), GetGameRolePlayActorsInformations(),
                GetInteractivesElements(character), GetStatedElements(), GetMapObstacles(), GetFightsCommonInformations(), HasAgressiveMonsters());
        }
        

    }
}
