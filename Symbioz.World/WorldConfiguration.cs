using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World
{
    public class WorldConfiguration : ServerConfiguration
    {
        public const string CONFIG_NAME = "world.xml";

        public static WorldConfiguration Instance;

        #region Public Static

        [StartupInvoke("Configuration", StartupInvokePriority.Primitive)]
        public static void Initialize()
        {
            Instance = Configuration.Load<WorldConfiguration>(CONFIG_NAME);
        }



        #endregion

        #region Public

        public bool UseCustomHost
        {
            get;
            set;
        }

        public string CustomHost
        {
            get;
            set;
        }

        public short ServerId
        {
            get;
            set;
        }

        public string ServerName
        {
            get;
            set;
        }

        public sbyte ServerType
        {
            get;
            set;
        }

        public int SaveInterval
        {
            get;
            set;
        }

        public bool PerformBackup
        {
            get;
            set;
        }

        public int StartMapId
        {
            get;
            set;
        }

        public ushort StartCellId
        {
            get;
            set;
        }

        public int StartKamas
        {
            get;
            set;
        }

        public int KamasRate
        {
            get;
            set;
        }

        public int DropsRate
        {
            get;
            set;
        }

        public int CraftRate
        {
            get;
            set;
        }

        public ushort StartLevel
        {
            get;
            set;
        }

        public bool PlayDefaultCinematic
        {
            get;
            set;
        }

        public string WelcomeMessage
        {
            get;
            set;
        }

        public string MapKey
        {
            get;
            set;
        }

        public short ApLimit
        {
            get;
            set;
        }
        public short MpLimit
        {
            get;
            set;
        }


        public override void Default()
        {
            this.DatabaseHost = "127.0.0.1";
            this.DatabaseUser = "root";
            this.DatabasePassword = string.Empty;
            this.DatabaseName = "symbioz_world";
            this.Host = "127.0.0.1";
            this.Port = 5555;
            this.ShowProtocolMessages = true;
            this.SafeRun = true;
            this.StartMapId = 156762120;
            this.StartCellId = 142;
            this.ServerId = 1;
            this.ServerName = "Jiva";
            this.ServerType = 1;
            this.TransitionHost = "127.0.0.1";
            this.TransitionPort = 600;
            this.SaveInterval = 300;
            this.PerformBackup = true;
            this.MapKey = "649ae451ca33ec53bbcbcc33becf15f4";
            this.PlayDefaultCinematic = true;
            this.UseCustomHost = false;
            this.CustomHost = string.Empty;
            this.StartLevel = 1;
            this.StartKamas = 0;
            this.KamasRate = 1;
            this.DropsRate = 1;
            this.CraftRate = 20;
            this.WelcomeMessage = "Welcome on server";
            this.ApLimit = 12;
            this.MpLimit = 6;
        }

        #endregion
    }
}
