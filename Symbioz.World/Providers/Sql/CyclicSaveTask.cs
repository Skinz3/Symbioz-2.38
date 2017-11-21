using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Network;
using Symbioz.World.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Sql
{
    class CyclicSaveTask
    {
        static Logger logger = new Logger();

        static List<MethodInfo> BeforeSavingMethods = new List<MethodInfo>();

        [StartupInvoke("Cyclic Save Task", StartupInvokePriority.Last)]
        public static void Start()
        {
            Thread safeThread = new Thread(new ThreadStart(Init));
            safeThread.Start();
        }
        static void Init()
        {
            try
            {
                SaveTask.OnSaveStarted += Cache_OnSaveStarted;
                SaveTask.OnSaveEnded += Cache_OnSaveEnded;
                SaveTask.Initialize(WorldConfiguration.Instance.SaveInterval);
                DatabaseBackupProvider.Initialize(Environment.CurrentDirectory + "\\Backups\\");
                InitializeSave();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        static void Cache_OnSaveStarted()
        {
            logger.Color2("Saving Server...");
            if (!WorldServer.Instance.IsStatus(ServerStatusEnum.ONLINE))
                return;
            WorldServer.Instance.SetServerStatus(ServerStatusEnum.SAVING);
            WorldServer.Instance.OnClients(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 164));
            BeforeSave();
        }

        static void Cache_OnSaveEnded(int elapsed)
        {
            if (WorldConfiguration.Instance.PerformBackup)
                DatabaseBackupProvider.Backup();

            logger.Color2("Server Saved (" + elapsed + ")s");
            if (!WorldServer.Instance.IsStatus(ServerStatusEnum.SAVING))
                return;

            WorldServer.Instance.SetServerStatus(ServerStatusEnum.ONLINE);
            WorldServer.Instance.OnClients(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 165));
        }
        static void BeforeSave()
        {
            foreach (var method in BeforeSavingMethods)
            {
                method.Invoke(null, new object[0]);
            }
        }
        static void InitializeSave()
        {
            foreach (var type in Assembly.GetAssembly(typeof(CyclicSaveTask)).GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    if (method.GetCustomAttribute(typeof(BeforeSavingAttribute)) != null)
                    {
                        BeforeSavingMethods.Add(method);

                    }
                }

            }
        }



    }
}
