using SSync;
using Symbioz.Auth;
using Symbioz.Auth.Records;
using Symbioz.Auth.Transition;
using Symbioz.Core;
using Symbioz.Core.Commands;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Network;
using Symbioz.Network.Servers;
using Symbioz.ORM;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Auth
{

    class Program
    {
        public static Assembly AuthAssembly = Assembly.GetAssembly(typeof(AuthConfiguration));

        static Logger logger = new Logger();

        static void Main(string[] args)
        {

            logger.OnStartup();
            StartupManager.Instance.Initialize(Assembly.GetExecutingAssembly());

            ConsoleCommands.Instance.WaitHandle();

        }
        [StartupInvoke(StartupInvokePriority.First)]
        public static void SafeRun()
        {
            if (AuthConfiguration.Instance.SafeRun)
                ExceptionsHandler.SafeRun();
        }
        [StartupInvoke("SSync", StartupInvokePriority.First)]
        public static void InitializeSSync()
        {
            SSyncCore.Initialize(Assembly.GetAssembly(typeof(RawDataMessage)),
                Assembly.GetExecutingAssembly(), AuthConfiguration.Instance.ShowProtocolMessages);
            ProtocolTypeManager.Initialize();
        }
        [StartupInvoke("Server", StartupInvokePriority.Tenth)]
        public static void StartServers()
        {
            TransitionServer.Instance.Start(AuthConfiguration.Instance.TransitionHost, AuthConfiguration.Instance.TransitionPort);
            AuthServer.Instance.Start();

        }
        [StartupInvoke("SQL Connection", StartupInvokePriority.Second)]
        public static void Connect()
        {

            DatabaseManager manager = new DatabaseManager(AuthAssembly,AuthConfiguration.Instance.DatabaseHost,
                                           AuthConfiguration.Instance.DatabaseName,
                                            AuthConfiguration.Instance.DatabaseUser,
                                           AuthConfiguration.Instance.DatabasePassword);
            manager.UseProvider();

            manager.LoadTables();

        }
        [StartupInvoke("Console Commands",StartupInvokePriority.First)]
        public static void LoadConsoleCommands()
        {
            ConsoleCommands.Instance.Initialize(AuthAssembly);
        }
    }
}
