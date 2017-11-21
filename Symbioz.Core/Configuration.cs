using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public abstract class Configuration
    {
        static Logger logger = new Logger();

        public static void Save(Configuration instance, string fileName)
        {
            File.WriteAllText(Environment.CurrentDirectory + "/" + fileName, instance.XMLSerialize());
        }
        public static T ReadConfiguration<T>(string path)
        {
            return File.ReadAllText(path).XMLDeserialize<T>();
        }

        public abstract void Default();


        public static T Load<T>(string fileName) where T : Configuration
        {
            string path = Environment.CurrentDirectory + "/" + fileName;
            if (File.Exists(path))
            {
                try
                {
                    T configuration = ReadConfiguration<T>(path);
                    return configuration;
                }
                catch (Exception ex)
                {
                label:
                    logger.Error(ex.Message);
                    logger.Color2("Unable to load configuration do you want to use default configuration?");
                    logger.Color2("y/n?");
                    ConsoleKeyInfo answer = Console.ReadKey(true);
                    if (answer.Key == ConsoleKey.Y)
                    {
                        File.Delete(path);
                        return Load<T>(fileName);
                    }
                    if (answer.Key == ConsoleKey.N)
                    {
                        Environment.Exit(0);
                    }
                    goto label;
                }
            }
            else
            {
                T configuration = Activator.CreateInstance<T>();
                configuration.Default();
                Save(configuration, fileName);
                logger.Color2("Configuration Created");
                return configuration;
            }

        }
    }
}
