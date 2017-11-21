using SSync.Messages;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Network;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay
{
    class MapsHandler
    {
        [MessageHandler]
        public static void HandleMapGetInformation(MapInformationsRequestMessage message, WorldClient client)
        {
            client.Character.Map = MapRecord.GetMap(message.mapId);

            if (client.Character.Map == null)
            {
                client.Character.SpawnPoint();
                client.Character.Reply("Unknown Map...(" + message.mapId + ")");
                return;
            }

            client.Character.Map.Instance.AddEntity(client.Character);
            client.Character.Map.Instance.MapComplementary(client, message.mapId, client.Character.Map.SubAreaId);
        }
        [MessageHandler]
        public static void HandleMapMovementRequest(GameMapMovementRequestMessage message,WorldClient client)
        {
            
        }
        [MessageHandler]
        public static void ChangeMapMessage(ChangeMapMessage message,WorldClient client)
        {
            MapScrollType scrollType = MapScrollType.UNDEFINED;
            if (client.Character.Map.LeftMap == message.mapId)
                scrollType = MapScrollType.Left;
            if (client.Character.Map.RightMap == message.mapId)
                scrollType = MapScrollType.Right;
            if (client.Character.Map.DownMap == message.mapId)
                scrollType = MapScrollType.Bottom;
            if (client.Character.Map.TopMap == message.mapId)
                scrollType = MapScrollType.Top;

            if (scrollType != MapScrollType.UNDEFINED)
            {
                int overrided = ScrollActionRecord.GetOverrideScrollMapId(client.Character.Map.Id, scrollType);
                ushort cellid = ScrollActionRecord.GetScrollDefaultCellId(client.Character.Record.CellId, scrollType);
                client.Character.Record.Direction = ScrollActionRecord.GetScrollDirection(scrollType);

                int teleportMapId = overrided != -1 ? overrided : message.mapId;
                if (overrided == 0)
                    teleportMapId = message.mapId;
                MapRecord teleportedMap = MapRecord.GetMap(teleportMapId);

                if (teleportedMap != null)
                {
                    cellid = teleportedMap.Walkable(cellid) ? cellid : ScrollActionRecord.SearchScrollCellId(cellid, scrollType, teleportedMap);
                    client.Character.Teleport(teleportMapId,cellid);
                }
                else
                {
                    client.Character.ReplyError("This map cannot be founded");
                }
            }
            else
            {
                scrollType = ScrollActionRecord.GetScrollTypeFromCell((short)client.Character.Record.CellId);
                if (scrollType == MapScrollType.UNDEFINED)
                {
                    client.Character.ReplyError("Unknown Map Scroll Action...");
                }
                else
                {
                    int overrided =ScrollActionRecord.GetOverrideScrollMapId(client.Character.Map.Id, scrollType);
                    ushort cellid = ScrollActionRecord.GetScrollDefaultCellId(client.Character.Record.CellId, scrollType);
                    MapRecord teleportedMap = MapRecord.GetMap(overrided);
                    if (teleportedMap != null)
                    {
                        client.Character.Record.Direction =ScrollActionRecord.GetScrollDirection(scrollType);
                        cellid = teleportedMap.Walkable(cellid) ? cellid : ScrollActionRecord.SearchScrollCellId(cellid, scrollType, teleportedMap);
                        client.Character.Teleport(overrided, cellid);
                    }
                    else
                    {
                        client.Character.ReplyError("This map cannot be founded");
                    }
                }
            }
        }
    }
}
