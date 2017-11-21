using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Enums
{
    [Flags]
    public enum MarkTriggerTypeEnum
    {
        ON_CAST = 1,
        AFTER_MOVE = 2,
        ON_TURN_STARTED = 4,
        ON_TURN_ENDED = 8,
        NONE = 2147483647,
    }
}
