using Symbioz.ProtocolBuilder.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.ProtocolBuilder.Profiles
{
    public abstract class ParsingProfile
    {
        public string Name
        {
            get;
            set;
        }

        public string SourcePath
        {
            get;
            set;
        }

        public string OutPutPath
        {
            get;
            set;
        }

        public string OutPutNamespace
        {
            get;
            set;
        }

        public bool EnableParsing
        {
            get;
            set;
        }

        public bool IgnoreMethods
        {
            get;
            set;
        }

        [YAXDontSerialize]
        public SerializableDictionary<string, string> BeforeParsingReplacementRules
        {
            get;
            set;
        }

        [YAXDontSerialize]
        public string[] IgnoredLines
        {
            get;
            set;
        }

        public string TemplatePath
        {
            get;
            set;
        }

        public abstract void ExecuteProfile(Parser parser);

        /// <summary>
        /// Get the path relative to the source directory
        /// </summary>
        public string GetRelativePath(string file)
        {
            string folder = Path.GetDirectoryName(file);
            string[] foldersSplitted = folder.Split(new[] { SourcePath.Replace("/", "\\") }, StringSplitOptions.RemoveEmptyEntries); // cut the source path and the "rest" of the path

            return foldersSplitted.Length > 1 ? foldersSplitted[1] : ""; // return the "rest"
        }
    }
}
