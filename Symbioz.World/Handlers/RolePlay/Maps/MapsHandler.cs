using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Dialogs;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Maps;
using Symbioz.World.Network;
using Symbioz.World.Providers.Fights;
using Symbioz.World.Providers.Maps;
using Symbioz.World.Providers.Maps.Path;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
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
        public static void HandleMapRunningFightList(MapRunningFightListRequestMessage message, WorldClient client)
        {
            client.Send(new MapRunningFightListMessage(client.Character.Map.Instance.GetFightsExternalInformations()));
        }
        [MessageHandler]
        public static void HandleMapRunning(MapRunningFightDetailsRequestMessage message, WorldClient client)
        {
            // var fight = client.Character.Map.Instance.GetFight(message.fightId);


            //client.Send(new MapRunningFightDetailsMessage(message.fightId, fight.RedTeam.GetFighterLightInformations(),
            //    fight.BlueTeam.GetFighterLightInformations()));
        }
        [MessageHandler]
        public static void HandleGameRolePlayAttackMonsterRequest(GameRolePlayAttackMonsterRequestMessage message, WorldClient client)
        {
            if (client.Character.Map != null)
            {
                MonsterGroup group = client.Character.Map.Instance.GetEntity<MonsterGroup>((long)message.monsterGroupId);

                if (group != null && client.Character.CellId == group.CellId)
                {
                    if (client.Character.Map.BlueCells.Count >= group.MonsterCount && client.Character.Map.RedCells.Count() >= client.Character.FighterCount)
                    {

                        client.Character.Map.Instance.RemoveEntity(group);

                        FightPvM fight = FightProvider.Instance.CreateFightPvM(group, client.Character.Map, (short)group.CellId);

                        fight.RedTeam.AddFighter(client.Character.CreateFighter(fight.RedTeam));

                        foreach (var fighter in group.CreateFighters(fight.BlueTeam))
                        {
                            fight.BlueTeam.AddFighter(fighter);
                        }

                        fight.StartPlacement();
                    }
                    else
                    {
                        client.Character.ReplyError("Unable to fight on this map");
                    }
                }
                else
                {
                    client.Character.NoMove();
                }
            }
        }
        [MessageHandler]
        public static void HandleMapGetInformation(MapInformationsRequestMessage message, WorldClient client)
        {
            if (client.Character.Record.MapId == message.mapId)
            {
                client.Character.Map = MapRecord.GetMap(message.mapId);

                if (client.Character.Map == null)
                {
                    client.Character.SpawnPoint();
                    client.Character.Reply("Unknown Map...(" + message.mapId + ")");
                    return;
                }

                client.Character.OnEnterMap();
            }
        }
        [MessageHandler]
        public static void HandleEmotePlay(EmotePlayRequestMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
                client.Character.PlayEmote(message.emoteId);
        }
        [MessageHandler]
        public static void HandleMapMovementRequest(GameMapMovementRequestMessage message, WorldClient client)
        {
            if (client.Character.Fighting)
            {
                if (client.Character.Fighter.Fight.Started && client.Character.Fighter.IsFighterTurn)
                {
                    List<short> path = PathParser.FightMove(PathParser.ReturnDispatchedCells(message.keyMovements)).Keys.ToList();
                    path.Insert(0, (short)client.Character.Fighter.CellId);
                    client.Character.Fighter.Move(path);
                }
            }
            else
            {
                if (!client.Character.ChangeMap && client.Character.Map.Id == message.mapId && !client.Character.Collecting)
                    client.Character.MoveOnMap(message.keyMovements);
                else
                    client.Character.NoMove();
            }
        }
        [MessageHandler]
        public static void HandleMapMovementCancel(GameMapMovementCancelMessage message, WorldClient client)
        {
            client.Character.Record.CellId = message.cellId;
            client.Send(new BasicNoOperationMessage());

        }
        [MessageHandler]
        public static void HandleMapMovementConfirm(GameMapMovementConfirmMessage message, WorldClient client)
        {
            if (client.Character.IsMoving)
                client.Character.EndMove();
        }
        [MessageHandler]
        public static void HandleGameMapChangeOriantation(GameMapChangeOrientationRequestMessage message, WorldClient client)
        {
            client.Character.SetDirection((DirectionsEnum)message.direction);
          
        }
        [MessageHandler]
        public static void HandleTeleportRequest(TeleportRequestMessage message, WorldClient client)
        {
            switch ((TeleporterTypeEnum)message.teleporterType)
            {
                case TeleporterTypeEnum.TELEPORTER_ZAAP:
                    if (client.Character.GetDialog<ZaapDialog>() != null)
                        client.Character.GetDialog<ZaapDialog>().Teleport(MapRecord.GetMap(message.mapId));
                    break;
                case TeleporterTypeEnum.TELEPORTER_SUBWAY:
                    if (client.Character.GetDialog<ZaapiDialog>() != null)
                        client.Character.GetDialog<ZaapiDialog>().Teleport(MapRecord.GetMap(message.mapId));
                    break;
            }
        }
        [MessageHandler]
        public static void ChangeMapMessage(ChangeMapMessage message, WorldClient client)
        {
            MapScrollEnum scrollType = MapScrollEnum.UNDEFINED;
            if (client.Character.Map.LeftMap == message.mapId)
                scrollType = MapScrollEnum.Left;
            if (client.Character.Map.RightMap == message.mapId)
                scrollType = MapScrollEnum.Right;
            if (client.Character.Map.DownMap == message.mapId)
                scrollType = MapScrollEnum.Bottom;
            if (client.Character.Map.TopMap == message.mapId)
                scrollType = MapScrollEnum.Top;

            if (scrollType != MapScrollEnum.UNDEFINED)
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
                    client.Character.Teleport(teleportMapId, cellid);
                }
                else
                {
                    client.Character.ReplyError("This map cannot be founded");
                }
            }
            else
            {
                scrollType = ScrollActionRecord.GetScrollTypeFromCell((short)client.Character.Record.CellId);
                if (scrollType == MapScrollEnum.UNDEFINED)
                {
                    client.Character.ReplyError("Unknown Map Scroll Action...");
                }
                else
                {
                    int overrided = ScrollActionRecord.GetOverrideScrollMapId(client.Character.Map.Id, scrollType);
                    ushort cellid = ScrollActionRecord.GetScrollDefaultCellId(client.Character.Record.CellId, scrollType);
                    MapRecord teleportedMap = MapRecord.GetMap(overrided);
                    if (teleportedMap != null)
                    {
                        client.Character.Record.Direction = ScrollActionRecord.GetScrollDirection(scrollType);
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
