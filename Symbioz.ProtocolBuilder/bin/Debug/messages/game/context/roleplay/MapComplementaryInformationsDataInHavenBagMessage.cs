


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class MapComplementaryInformationsDataInHavenBagMessage : MapComplementaryInformationsDataMessage
{

public const ushort Id = 6622;
public override ushort MessageId
{
    get { return Id; }
}

public Types.CharacterMinimalInformations ownerInformations;
        public sbyte theme;
        public sbyte roomId;
        public sbyte maxRoomId;
        

public MapComplementaryInformationsDataInHavenBagMessage()
{
}

public MapComplementaryInformationsDataInHavenBagMessage(ushort subAreaId, int mapId, Types.HouseInformations[] houses, Types.GameRolePlayActorInformations[] actors, Types.InteractiveElement[] interactiveElements, Types.StatedElement[] statedElements, Types.MapObstacle[] obstacles, Types.FightCommonInformations[] fights, bool hasAggressiveMonsters, Types.CharacterMinimalInformations ownerInformations, sbyte theme, sbyte roomId, sbyte maxRoomId)
         : base(subAreaId, mapId, houses, actors, interactiveElements, statedElements, obstacles, fights, hasAggressiveMonsters)
        {
            this.ownerInformations = ownerInformations;
            this.theme = theme;
            this.roomId = roomId;
            this.maxRoomId = maxRoomId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            ownerInformations.Serialize(writer);
            writer.WriteSByte(theme);
            writer.WriteSByte(roomId);
            writer.WriteSByte(maxRoomId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            ownerInformations = new Types.CharacterMinimalInformations();
            ownerInformations.Deserialize(reader);
            theme = reader.ReadSByte();
            roomId = reader.ReadSByte();
            if (roomId < 0)
                throw new Exception("Forbidden value on roomId = " + roomId + ", it doesn't respect the following condition : roomId < 0");
            maxRoomId = reader.ReadSByte();
            if (maxRoomId < 0)
                throw new Exception("Forbidden value on maxRoomId = " + maxRoomId + ", it doesn't respect the following condition : maxRoomId < 0");
            

}


}


}