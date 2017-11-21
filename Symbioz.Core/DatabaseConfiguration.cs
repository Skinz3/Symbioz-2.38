using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public abstract class DatabaseConfiguration : Configuration
    {
        public string DatabaseHost
        {
            get;
            set;
        }

        public string DatabaseUser
        {
            get;
            set;
        }

        public string DatabasePassword
        {
            get;
            set;
        }

        public string DatabaseName
        {
            get;
            set;
        }
    }
}
