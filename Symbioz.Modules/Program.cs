using SSync.IO;
using Symbioz.Tools.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Symbioz.Modules
{
    class Program
    {
        static void Main(string[] args)
        {
            string crackHASHModulefile = CrackHASHModuleFile(@"C:\Users\Skinz\Desktop\Symbioz 2.38\app\ui\Ankama_Admin\Admin.swf");

            return;
            D2UIFile file = new D2UIFile(@"C:\Users\Skinz\Desktop\Symbioz 2.38\app\ui\Ankama_Admin\Ankama_Admin.d2UI");
            file.UIListPosition.Remove("gameMenu");
            file.Save();
        }
        static string CrackHASHModuleFile(string path)
        {

          
        }
    }
}