using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ProtocolBuilder.Parsing;
using Symbioz.ProtocolBuilder.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Symbioz.ProtocolBuilder
{
    /// <summary>
    /// !!! avant d'utiliser le protocol builder pour génerer les Enums, veillez a modifier GetMatch() dans Parser.cs
    /// </summary>
    class DofusProtocolBuilder : Singleton<DofusProtocolBuilder>
    {
        Logger logger = new Logger();

        [StartupInvoke("DofusProtocolBuilder", StartupInvokePriority.Second)]
        public void Build()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var profiles =
            new ParsingProfile[]
                {

              BuilderConfiguration.Instance.XmlTypesProfile,
             BuilderConfiguration.Instance.TypesProfile,
             BuilderConfiguration.Instance.XmlMessagesProfile,
             BuilderConfiguration.Instance.XmlTypesProfile,
                };

            foreach (ParsingProfile parsingProfile in profiles)
            {
                if (parsingProfile == null)
                    continue;

                Console.WriteLine("Executing profile \'{0}\' ... ", parsingProfile.Name);

                if (parsingProfile.OutPutNamespace != null)
                    parsingProfile.OutPutNamespace = parsingProfile.OutPutNamespace.Insert(0, BuilderConfiguration.Instance.BaseNamespace);

                if (!Directory.Exists(BuilderConfiguration.Instance.Output))
                    Directory.CreateDirectory(BuilderConfiguration.Instance.Output);

                string path = Path.Combine(BuilderConfiguration.Instance.Output, parsingProfile.OutPutPath);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }

                Directory.CreateDirectory(Path.Combine(BuilderConfiguration.Instance.Output, parsingProfile.OutPutPath));

                IEnumerable<string> files = Directory.EnumerateFiles(
                    Path.Combine(BuilderConfiguration.Instance.SourcePath, parsingProfile.SourcePath), "*.as",
                    SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    string relativePath = parsingProfile.GetRelativePath(file);
                    path = Path.Combine(BuilderConfiguration.Instance.Output, parsingProfile.OutPutPath, relativePath);


                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    var parser = new Parser(file, parsingProfile.BeforeParsingReplacementRules,
                                            parsingProfile.IgnoredLines)
                    { IgnoreMethods = parsingProfile.IgnoreMethods };

                    try
                    {
                        if (parsingProfile.EnableParsing)
                            parser.ParseFile();
                    }
                    catch (InvalidCodeFileException)
                    {
                        Console.WriteLine("File {0} not parsed correctly", Path.GetFileName(file));
                        continue;
                    }

                    parsingProfile.ExecuteProfile(parser);
                }
                logger.White(parsingProfile.Name + " done!");

            }
        }
        private static void DeleteDirectory(string targetDir)
        {
            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, false);
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Program.Shutdown("Unhandled Exception : " + e.ExceptionObject);
        }
    }
}
