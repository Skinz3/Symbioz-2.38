using Symbioz.Core;
using Symbioz.Core.Commands;
using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Selfmade.Messages;
using Symbioz.World.Models;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Network;
using Symbioz.World.Providers.Maps.Cinematics;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Interactives;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Npcs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.World
{
    class CommandsRepertory
    {
        static Logger logger = new Logger();

        [ConsoleCommand("backup")]
        public static void BackupCommand(string input)
        {
            DatabaseBackupProvider.Backup();
        }
        [ConsoleCommand("restore")]
        public static void RestoreCommand(string input)
        {
            logger.White("Are you sure you want to restore this server database?");
            logger.White("y/n?");

            if (!TransitionServerManager.Instance.IsConnected)
            {
                logger.Alert("Unable to reset database while not connected to auth");
                return;
            }

            ConsoleKeyInfo answer = Console.ReadKey(true);
            if (answer.Key == ConsoleKey.Y)
            {
                DatabaseManager.GetInstance().ResetTables(Program.WorldAssembly);
                TransitionServerManager.Instance.AuthServer.SendUnique(new ResetDatabaseMessage());
                logger.White("Database Restored, Exiting..");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }

        }
        [ConsoleCommand("subareas")]
        public static void ReloadSubareas(string input)
        {
            DatabaseManager.GetInstance().Reload<SubareaRecord>();

            foreach (var map in MapRecord.Maps)
            {
                map.SubArea = SubareaRecord.GetSubarea(map.SubAreaId);
            }

            foreach (var client in WorldServer.Instance.GetOnlineClients())
            {
                client.Character.UpdateServerExperience(client.Character.Map.SubArea.ExperienceRate);
            }
            logger.White("Subareas reloaded!");
        }
        [ConsoleCommand("npcactions")]
        public static void ReloadNpcActions(string input)
        {
            DatabaseManager.GetInstance().Reload<NpcActionRecord>();

            foreach (var map in MapRecord.Maps)
            {
                foreach (var npc in map.Instance.GetEntities<Npc>())
                {
                    npc.ActionsRecord = NpcActionRecord.GetActions(npc.SpawnRecord.Id);
                }    
            }
            logger.White("Npc reloaded!");
        }
        [ConsoleCommand("items")]
        public static void ReloadItems(string input)
        {
            DatabaseManager.GetInstance().Reload<PetRecord>();
            DatabaseManager.GetInstance().Reload<MountRecord>();
            DatabaseManager.GetInstance().Reload<ItemRecord>();
            DatabaseManager.GetInstance().Reload<WeaponRecord>();
            Symbioz.World.Modules.PetEffectsFixerModule.Initialize();
            logger.White("Items reloaded!");
        }
        [ConsoleCommand("maxclients")]
        public static void MaxClients(string input)
        {
            logger.White("Max clients count on this instance is: " + WorldServer.Instance.MaxClientsCount);
        }
        [ConsoleCommand("status")]
        public static void SetServerStatus(string input)
        {
            var value = (ServerStatusEnum)int.Parse(input.Split(null).Last());
            WorldServer.Instance.SetServerStatus(value);
            logger.White("Server status is now: " + value);
        }
        [ConsoleCommand("save")]
        public static void SaveWorld(string input)
        {
            SaveTask.Save();
        }
        [ConsoleCommand("infos")]
        public static void Infos(string input)
        {
            logger.White("Clients Connecteds: " + WorldServer.Instance.ClientsCount);
            logger.White("Connected Distincted: " + WorldServer.Instance.GetOnlineClients().ConvertAll<string>(x => x.Ip).Distinct().Count());
        }
        [ConsoleCommand("notif")]
        public static void Notif(string input)
        {
            WorldServer.Instance.OnClients(x => x.Character.Notification(input));
            logger.White("Notification sended to clients.");
        }
        [ConsoleCommand("reboot")]
        public static void Test(string input)
        {
            int minuteDelay = 10;
            WorldServer.Instance.OnClients(x => x.Character.Notification("Pour des raisons de maintenance, le serveur va être redémarré dans " + minuteDelay + " minutes. Merci de votre compréhension."));

            ActionTimer action = new ActionTimer((minuteDelay * 60000) / 2, new Action(() =>
               {
                   WorldServer.Instance.OnClients(x => x.Character.Notification("Pour des raisons de maintenance, le serveur va être redémarré dans " + minuteDelay / 2 + " minutes. Merci de votre compréhension."));
               }), false);
            action.Start();


            action = new ActionTimer(minuteDelay * 60000, new Action(() =>
             {
                 SaveTask.Save();
                 Environment.Exit(0);

             }), false);

            action.Start();
        }
        [ConsoleCommand("lua")]
        public static void ReloadLUA(string input)
        {
            CinematicProvider.Instance.Initialize();
            logger.White("LUAScripts reloaded!");

        }
        [ConsoleCommand("stop")]
        public static void Stop(string input)
        {
            WorldServer.Instance.SetServerStatus(ServerStatusEnum.STOPING);
            logger.Gray("Server now Offline");
            foreach (WorldClient client in WorldServer.Instance.GetOnlineClients())
            {
                client.Disconnect();
            }
            logger.Gray("Each client disconnected");
            SaveTask.Save();
            logger.Gray("Server Saved");
            logger.Gray("You can now quit the application");
        }
    }
}