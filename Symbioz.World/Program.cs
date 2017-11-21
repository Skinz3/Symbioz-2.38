using SSync;
using Symbioz.Core;
using Symbioz.Core.Commands;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.RawMessages;
using Symbioz.Protocol.Types;
using Symbioz.Tools.D2P;
using Symbioz.Tools.SWL;
using Symbioz.World.Models.Entities.Jobs;
using Symbioz.World.Network;
using Symbioz.World.Providers.Fights.Results;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Guilds;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.World
{
    class Program
    {
        public static Assembly WorldAssembly = Assembly.GetAssembly(typeof(WorldConfiguration));

        static Logger logger = new Logger();

        static void Main(string[] args)
        {
            logger.OnStartup();
            Thread safeThread = new Thread(new ThreadStart(RunMainThread));
            safeThread.Start();
            ConsoleCommands.Instance.WaitHandle();
        }
        static void RunMainThread()
        {
            StartupManager.Instance.Initialize(WorldAssembly);
            TransitionServerManager.Instance.AllowConnections();

        }
        [StartupInvoke(StartupInvokePriority.First)]
        public static void SafeRun()
        {
            if (WorldConfiguration.Instance.SafeRun)
                ExceptionsHandler.SafeRun();
        }
        [StartupInvoke(StartupInvokePriority.First)]
        public static void JoinAuthAuth()
        {
            TransitionServerManager.Instance.TryToJoinAuthServer();
        }
        [StartupInvoke("SSync", StartupInvokePriority.First)]
        public static void InitializeSSync()
        {
            SSyncCore.Initialize(Assembly.GetAssembly(typeof(RawDataMessage)),
                WorldAssembly, WorldConfiguration.Instance.ShowProtocolMessages);
            ProtocolTypeManager.Initialize();
        }
        [StartupInvoke("Server", StartupInvokePriority.Tenth)]
        public static void StartServers()
        {
            WorldServer.Instance.Start();

        }
        [StartupInvoke("SQL Connection", StartupInvokePriority.Second)]
        public static void Connect()
        {

            DatabaseManager manager = new DatabaseManager(WorldAssembly, WorldConfiguration.Instance.DatabaseHost,
                                           WorldConfiguration.Instance.DatabaseName,
                                            WorldConfiguration.Instance.DatabaseUser,
                                           WorldConfiguration.Instance.DatabasePassword);

            manager.UseProvider();

            manager.LoadTables();

        }

        [StartupInvoke("Console Commands", StartupInvokePriority.First)]
        public static void LoadConsoleCommands()
        {
            ConsoleCommands.Instance.Initialize(WorldAssembly);
        }




    }
}
