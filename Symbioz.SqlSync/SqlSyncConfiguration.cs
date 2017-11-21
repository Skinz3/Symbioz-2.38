using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.SqlSync
{
    [YAXComment("SqlSyncConfiguration")]
    public class SqlSyncConfiguration : Configuration
    {
        public string DatabaseHost { get; set; }

        public string DatabaseName { get; set; }

        public string DatabaseUser { get; set; }

        public string DatabasePassword { get; set; }

        public string MapKey { get; set; }

        public const string CONFIG_NAME = "sqlSync.xml";

        [StartupInvoke("Configuration", StartupInvokePriority.Primitive)]
        public static void Initialize()
        {
            Instance = ServerConfiguration.Load<SqlSyncConfiguration>(CONFIG_NAME);
        }

        public static SqlSyncConfiguration Instance = null;

        public override void Default()
        {
            this.DatabaseHost = "127.0.0.1";
            this.DatabaseName = "symbioz_d2o";
            this.DatabaseUser = "root";
            this.DatabasePassword = string.Empty;
            this.MapKey = "649ae451ca33ec53bbcbcc33becf15f4"; 
        }
    }
}
