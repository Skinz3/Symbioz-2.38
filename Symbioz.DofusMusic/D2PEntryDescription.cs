using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.DofusMusic
{
    public class D2PEntryDescription
    {
        public D2PEntryDescription(string fileName,string containerPath,string infos)
        {
            this.FileName = fileName;
            this.ContainerPath = containerPath;
            this.Informations = infos;
        }
        private string ContainerPath
        {
            get;
            set;
        }
        public string ContainerFileName
        {
            get
            {
                return Path.GetFileName(ContainerPath);
            }
        }
        public string FileName
        {
            get;
            private set;
        }
        public string Informations
        {
            get;
            set;
        }
       
        
    }
}
