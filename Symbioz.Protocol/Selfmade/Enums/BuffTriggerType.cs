using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Enums
{
    [Flags]
    public enum TriggerType
    {
        ON_CAST = 1,
        TURN_BEGIN = 2,
        TURN_END = 4,
        AFTER_MOVE = 8,
        BEFORE_ATTACKED = 16,
        AFTER_ATTACKED = 32,
        BUFF_ENDED = 64,
        BUFF_ENDED_TURNEND = 128,
        BUFF_DELAY_ENDED = 256,
        BEFORE_MOVE = 512,
        BEFORE_DEATH = 1024,
        AFTER_DEATH = 2048,
        ON_HEAL = 4096,
        UNKNOWN = 2147483647
    }
}
