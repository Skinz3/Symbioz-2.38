using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.SqlSync.D2OTypes;
using Symbioz.SqlSync.Tables;
using Symbioz.Tools.D2O;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync
{
    class Program
    {
        static Logger logger = new Logger();

        static void Main(string[] args)
        {
            logger.OnStartup();
            StartupManager.Instance.Initialize(Assembly.GetExecutingAssembly());
            Console.ReadKey();
        }

        [StartupInvoke("Synchroniser", StartupInvokePriority.Second)]
        public static void Synchronise()
        {
            DatabaseSynchroniser synchroniser = new DatabaseSynchroniser(true, false);
            synchroniser.Launch();
            //   synchroniser.Synchronize();
            var arze = synchroniser.GetD2Os<SubAreas>();

            Dictionary<string, string> parser = new Dictionary<string, string>();

            foreach (var sub in arze)
            {
                var o = Symbioz.Core.Extensions.XMLDeserialize<List<AmbientSound>>(sub.AmbientSounds).ConvertAll(x => x.Id);

                if (!parser.ContainsKey(sub.Name))
                    parser.Add(sub.Name, o.ToCSV());
                else
                    parser[sub.Name] += "," + o.ToCSV();
            }

            string contentFinal = string.Empty;
            foreach (var info in parser)
            {
                contentFinal += "{"+"\"" + info.Key + "\"" + ", new List<int>(){" + info.Value + "}"+"}," + Environment.NewLine;
            }

            File.WriteAllText(Environment.CurrentDirectory + "/a.txt", contentFinal);
            Process.Start(Environment.CurrentDirectory + "/a.txt");
        }

        [StartupInvoke("SQL Connection", StartupInvokePriority.First)]
        public static void ConnectToDatabase()
        {
            DatabaseManager manager = new DatabaseManager(Assembly.GetExecutingAssembly(),

           SqlSyncConfiguration.Instance.DatabaseHost, SqlSyncConfiguration.Instance.DatabaseName,
           SqlSyncConfiguration.Instance.DatabaseUser, SqlSyncConfiguration.Instance.DatabasePassword);

            try
            {
                logger.White("Trying to connect to database...");
                manager.UseProvider();
                logger.White("Connected!");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
