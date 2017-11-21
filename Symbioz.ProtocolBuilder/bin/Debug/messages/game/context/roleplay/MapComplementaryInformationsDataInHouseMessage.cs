


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MapComplementaryInformationsDataInHouseMessage : MapComplementaryInformationsDataMessage
{

public const ushort Id = 6130;
public override ushort MessageId
{
    get { return Id; }
}

public Types.HouseInformationsInside currentHouse;
        

public MapComplementaryInformationsDataInHouseMessage()
{
}

public MapComplementaryInformationsDataInHouseMessage(ushort subAreaId, int mapId, Types.HouseInformations[] houses, Types.GameRolePlayActorInformations[] actors, Types.InteractiveElement[] interactiveElements, Types.StatedElement[] statedElements, Types.MapObstacle[] obstacles, Types.FightCommonInformations[] fights, bool hasAggressiveMonsters, Types.HouseInformationsInside currentHouse)
         : base(subAreaId, mapId, houses, actors, interactiveElements, statedElements, obstacles, fights, hasAggressiveMonsters)
        {
            this.currentHouse = currentHouse;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            currentHouse.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            currentHouse = new Types.HouseInformationsInside();
            currentHouse.Deserialize(reader);
            

}


}


}