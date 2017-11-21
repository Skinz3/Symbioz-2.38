
using SSync.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.Messages
{
    public abstract class TransitionMessage : Message
    {
        public Guid Guid = Guid.Empty;

        public bool IsRequest;

      
    }
}
