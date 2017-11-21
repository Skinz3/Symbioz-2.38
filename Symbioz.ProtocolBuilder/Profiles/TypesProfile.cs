using Microsoft.VisualStudio.TextTemplating;
using Symbioz.Core;
using Symbioz.ProtocolBuilder.Parsing;
using Symbioz.ProtocolBuilder.Templates;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.ProtocolBuilder.Profiles
{
    public class TypesProfile : ParsingProfile
    {
        Logger logger = new Logger();

        public override void ExecuteProfile(Parser parser)
        {

           // if (parser.Class.Name.Contains("FightCommonInformations"))
          //  {

           // }
            var relativePath = GetRelativePath(parser.Filename);

            string file = Path.Combine(BuilderConfiguration.Instance.Output, OutPutPath, relativePath, Path.GetFileNameWithoutExtension(parser.Filename));
            var xmlType = BuilderConfiguration.Instance.XmlTypesProfile.SearchXmlPattern(Path.GetFileNameWithoutExtension(parser.Filename));

            if (xmlType == null)
                Program.Shutdown(string.Format("File {0} not found", file));

            var engine = new Engine();
            var host = new TemplateHost(TemplatePath);
            host.Session["Type"] = xmlType;
            host.Session["Profile"] = this;
            var output = engine.ProcessTemplate(File.ReadAllText(TemplatePath), host);

            foreach (CompilerError error in host.Errors)
            {
                Console.WriteLine(error.ErrorText);
            }

            if (host.Errors.Count > 0)
                Program.Shutdown();

            File.WriteAllText(file + host.FileExtension, output);

            Logger.Write(string.Format("Wrote {0}", file), ConsoleColor.Gray);
        }
    }
}
