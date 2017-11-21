using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.Core
{
    public abstract class ServerConfiguration : DatabaseConfiguration
    {
        public int Port { get; set; }

        public bool ShowProtocolMessages { get; set; }

        public string Host { get; set; }

        public bool SafeRun { get; set; }

        public string TransitionHost { get; set; }

        public int TransitionPort { get; set; }


    }
}
