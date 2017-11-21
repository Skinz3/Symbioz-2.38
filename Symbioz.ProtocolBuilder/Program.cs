using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.ProtocolBuilder
{
    public class Program
    {
        static Logger logger = new Logger();

        public static Assembly BuilderAssembly = Assembly.GetExecutingAssembly();

        static void Main(string[] args)
        {
            logger.OnStartup();
            logger.Color2("www.github.com/Emudofus/BehaviorIsManaged/", false);
            logger.NewLine();
            StartupManager.Instance.Initialize(BuilderAssembly);
            Console.ReadKey();
        }

        public static void Shutdown(string reason = "")
        {
            logger.Error(string.Format("The program is shutting down {0}", (reason != "" ? ": " + reason : "")));
            logger.White("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
