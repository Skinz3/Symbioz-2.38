using Symbioz.Tools.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.UIModules
{
    public class DofusUIModule
    {
        public string ModuleName
        {
            get;
            private set;
        }
        public string DofusPath
        {
            get;
            private set;
        }
        private string ModulePath
        {
            get
            {
                return Editor.DOFUS_PATH + "/ui/" + ModuleName + "/";
            }
        }
        private string D2UIPath
        {
            get
            {
                return ModulePath + ModuleName + ".d2ui";
            }
        }
        private string DMPath
        {
            get
            {
                return ModulePath + ModuleName + ".dm";
            }
        }
        private string UIPath
        {
            get
            {
                return ModulePath + "/ui/";
            }
        }
        public DofusUIModule(string dofusPath, string moduleName)
        {
            this.ModuleName = moduleName;
            this.DofusPath = dofusPath;
            this.InitializeNewModule();
        }

        private void InitializeNewModule()
        {
            if (!Directory.Exists(ModulePath))
            {
                Directory.CreateDirectory(ModulePath);
                Directory.CreateDirectory(UIPath);
            }
            D2UIFile file = new D2UIFile(D2UIPath);

        }
    }
}
