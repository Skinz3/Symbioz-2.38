using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.IO;
using Symbioz.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using YAXLib;
using Symbioz.Protocol.Types;

namespace Symbioz.Auth
{
    [YAXComment("AuthServer Configuration")]
    public class AuthConfiguration : ServerConfiguration
    {
        public const string CONFIG_NAME = "auth.xml";

        #region Public Static

        [StartupInvoke("Configuration", StartupInvokePriority.Primitive)]
        public static void Initialize()
        {
            Instance = ServerConfiguration.Load<AuthConfiguration>(CONFIG_NAME);
        }

        public static AuthConfiguration Instance = null;


        #endregion

        #region Public

        public int DofusProtocolVersion { get; set; }

        public sbyte VersionInstall { get; set; }

        public sbyte VersionMajor { get; set; }

        public sbyte VersionMinor { get; set; }

        public sbyte VersionRelease { get; set; }

        public sbyte VersionPatch { get; set; }

        public int VersionRevision { get; set; }

        public sbyte VersionTechnology { get; set; }

        public sbyte VersionBuildType { get; set; }

        public VersionExtended GetVersionExtended()
        {
            return new VersionExtended(VersionMajor, VersionMinor, VersionRelease,
                VersionRevision, VersionPatch, VersionBuildType, VersionInstall, VersionTechnology);
        }

        public override void Default()
        {
            
            DatabaseHost = "127.0.0.1";

            DatabaseUser = "root";

            DatabasePassword = string.Empty;

            DatabaseName = "symbioz_auth";

            Host = "127.0.0.1";

            Port = 443;

            ShowProtocolMessages = true;

            SafeRun = false;

            DofusProtocolVersion = 1709; // 2.34

            TransitionHost = "127.0.0.1";

            TransitionPort = 600;

            VersionInstall = 1;

            VersionTechnology = 1;

            VersionBuildType = 0;

            VersionMajor = 2;

            VersionMinor = 34;

            VersionPatch = 2;

            VersionRelease = 2;

            VersionRevision = 103887;

        }

        #endregion
    }
}