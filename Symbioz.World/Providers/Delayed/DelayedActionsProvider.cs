using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models;
using Symbioz.World.Providers.Delayed;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Symbioz.Core;
using System.Threading.Tasks;
using Symbioz.World.Network;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Maps;
using Symbioz.World.Providers.Maps.Monsters;
using System.Drawing;
using Symbioz.World.Records.Portals;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Characters;

namespace Symbioz.World.Providers
{
    /// <summary>
    /// Is the timers clean?
    /// </summary>
    public class DelayedActionsProvider
    {
        private static Dictionary<DelayedActionAttribute, MethodInfo> Handlers = typeof(DelayedActionsProvider).MethodsWhereAttributes<DelayedActionAttribute>();

        public static void Handle(DelayedAction action)
        {
            var handler = Handlers.FirstOrDefault(x => x.Key.ActionType.ToLower() == action.Record.ActionType.ToLower());

            if (handler.Value != null)
                handler.Value.Invoke(null, new object[] { action });
            else
                Logger.Write<DelayedActionsProvider>("Unknown delayed actionType: " + action.Record.ActionType, ConsoleColor.Red);
        }

        static void NotifyClients(string message, Color color)
        {
            WorldServer.Instance.OnClients(x => x.Character.Reply(message, color));
        }
        [DelayedAction("Portal")]
        public static void SpawnPortal(DelayedAction action)
        {
            int portalId = int.Parse(action.Record.Value1);

            PortalRecord record = PortalRecord.GetPortal(portalId);

            MapRecord targetedMap = null;

            if (action.Value != null)
            {
                ((MapRecord)action.Value).Instance.RemoveEntity(((MapRecord)action.Value).Instance.GetEntities<Portal>().FirstOrDefault(x => x.Template.Id == int.Parse(action.Record.Value1)));
            }

            if (action.Record.Value2 != string.Empty)
            {
                List<MapRecord> subAreaMaps = MapRecord.GetSubAreaMaps(int.Parse(action.Record.Value2)).ConvertAll<MapRecord>(x => MapRecord.GetMap(x)).FindAll(x => x.Instance.GetEntities<Portal>().FirstOrDefault(w => w.Template.Id == portalId) == null);

                if (subAreaMaps.Count > 0)
                {
                    targetedMap = subAreaMaps.Random();

                    targetedMap.Instance.AddEntity(new Portal(record, targetedMap.Instance));

                    action.Value = targetedMap;
                }
            }
            else
            {
                List<MapRecord> maps = MapRecord.Maps.FindAll(x => x.Instance.GetEntities<Portal>().FirstOrDefault(w => w.Template.Id == portalId) == null).FindAll(x => x.Position.Outdoor);

                if (maps.Count > 0)
                {
                    targetedMap = maps.Random();

                    targetedMap.Instance.AddEntity(new Portal(record, targetedMap.Instance));

                    action.Value = targetedMap;
                }
            }

        }
        [DelayedAction("MonstersSub")]
        public static void MonstersSub(DelayedAction action)
        {
            ushort[] monsterIds = Array.ConvertAll(action.Record.Value1.Split(','), x => ushort.Parse(x));

            MonsterRecord[] templates = new MonsterRecord[monsterIds.Length];

            for (int i = 0; i < monsterIds.Length; i++)
            {
                templates[i] = MonsterRecord.GetMonster(monsterIds[i]);
            }

            if (action.Value != null)
            {
                MapRecord map = (MapRecord)action.Value;
                if (MonsterSpawnManager.Instance.GroupExist(map.Instance, templates))
                    MonsterSpawnManager.Instance.RemoveGroup(map.Instance, templates);
            }

            MapRecord targetedMap;

            int subAreaId = int.Parse(action.Record.Value2);

            var maps = MapRecord.GetSubAreaMaps(subAreaId).ConvertAll<MapRecord>(x => MapRecord.GetMap(x)).FindAll(w => !MonsterSpawnManager.Instance.GroupExist(w.Instance, templates));

            if (maps.Count > 0)
            {
                targetedMap = maps.Random();

                MonsterSpawnManager.Instance.AddFixedMonsterGroup(targetedMap.Instance, templates, false);


                action.Value = targetedMap;
            }
        }
        [DelayedAction("Monsters")]
        public static void SpawnMonsters(DelayedAction action)
        {
            ushort[] monsterIds = Array.ConvertAll(action.Record.Value1.Split(','), x => ushort.Parse(x));

            MonsterRecord[] templates = new MonsterRecord[monsterIds.Length];

            for (int i = 0; i < monsterIds.Length; i++)
            {
                templates[i] = MonsterRecord.GetMonster(monsterIds[i]);
            }

            MapRecord targetedMap;

            if (action.Record.Value2 != string.Empty)
            {
                targetedMap = MapRecord.GetMap(int.Parse(action.Record.Value2));
            }
            else
            {
                targetedMap = MapRecord.RandomOutdoorMap();
            }

            if (!MonsterSpawnManager.Instance.GroupExist(targetedMap.Instance, templates))
            {
                MonsterSpawnManager.Instance.AddFixedMonsterGroup(targetedMap.Instance, templates, false);
            }


        }
    }
}
