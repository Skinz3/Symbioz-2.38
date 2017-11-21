using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public class ExceptionsHandler
    {
        static Logger logger = new Logger();

        public static void SafeRun()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            logger.Gray("Safe Run handled");
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Error("a fatal error has occured.. restarting application");
            Environment.Exit(1);
        }
    }
}
