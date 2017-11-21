using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ProtocolBuilder.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.ProtocolBuilder
{
    public class BuilderConfiguration
    {
        public const string CONFIG_NAME = "builder.xml";

        #region Public Static

        [StartupInvoke("Configuration", StartupInvokePriority.Primitive)]
        public static void Initialize()
        {
            Instance = BuilderConfiguration.Load<BuilderConfiguration>(CONFIG_NAME);
        }

        public static BuilderConfiguration Instance = null;


        static Logger logger = new Logger();

        public static void Save(BuilderConfiguration instance, string fileName)
        {
            File.WriteAllText(Environment.CurrentDirectory + "/" + fileName, instance.XMLSerialize());
        }
        public static T ReadConfiguration<T>(string path)
        {
            return File.ReadAllText(path).XMLDeserialize<T>();
        }

        public static T Load<T>(string fileName) where T : BuilderConfiguration
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

        #endregion

        public string Output
        {
            get;
            set;
        }
        public string SourcePath
        {
            get;
            set;
        }
        public string BaseNamespace
        {
            get;
            set;
        }
        public bool UseIEnumerable
        {
            get;
            set;
        }
        public XmlMessagesProfile XmlMessagesProfile
        {
            get;
            set;
        }
        public XmlTypesProfile XmlTypesProfile
        {
            get;
            set;
        }
        public MessagesProfile MessagesProfile
        {
            get;
            set;
        }
        public TypesProfile TypesProfile
        {
            get;
            set;
        }
        public EnumsProfile EnumsProfile
        {
            get;
            set;
        }
        public void Default()
        {
            Output = "./";

            SourcePath = "./sources/";
            BaseNamespace = "Symbioz.Protocol";
            UseIEnumerable = false;

            XmlMessagesProfile =
                new XmlMessagesProfile
                    {
                        Name = "Xml Messages classes",
                        OutPutPath = "messages_xml/",
                        SourcePath = @"com/ankamagames/dofus/network/messages/",
                        EnableParsing = true,
                    };

            XmlTypesProfile =
                new XmlTypesProfile
                    {
                        Name = "Xml Types classes",
                        OutPutPath = "types_xml/",
                        SourcePath = @"com/ankamagames/dofus/network/types/",
                        EnableParsing = true,

                    };

            MessagesProfile =
                new MessagesProfile
                    {
                        Name = "Messages classes",
                        SourcePath = @"com/ankamagames/dofus/network/messages/",
                        TemplatePath = "./Templates/MessageTemplate.tt",
                        OutPutPath = "messages/",
                        OutPutNamespace = ".Messages",
                    };

            TypesProfile =
                new TypesProfile
                    {
                        Name = "Types classes",
                        SourcePath = @"com/ankamagames/dofus/network/types/",
                        TemplatePath = "./Templates/TypeTemplate.tt",
                        OutPutPath = "types/",
                        OutPutNamespace = ".Types",
                    };

            EnumsProfile =
                new EnumsProfile
                    {
                        Name = "Enums",
                        SourcePath = @"com/ankamagames/dofus/network/enums/",
                        OutPutPath = "enums/",
                        OutPutNamespace = ".Enums",
                        TemplatePath = "./Templates/EnumTemplate.tt",
                        EnableParsing = true,
                    };

        }
    }
}
