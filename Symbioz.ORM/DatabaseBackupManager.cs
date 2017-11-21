using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.ORM
{
    public class DatabaseBackupProvider
    {
        private const string BackupFileExtension = ".sql";

        static Logger logger = new Logger();

        private static string BackupDirectory;

        public static void Initialize(string directory)
        {
            BackupDirectory = directory;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void Backup()
        {
            try
            {
                DatabaseManager.GetInstance().Backup(BackupDirectory + DateTime.Now.ToFileNameDate() + BackupFileExtension);
                logger.Color2("Database Dumped");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}
