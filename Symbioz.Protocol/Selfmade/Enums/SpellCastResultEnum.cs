using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Enums
{
    public enum SpellCastResultEnum
    {
        Ok,
        NotPlaying,
        FightEnded,
        NotEnoughAp,
        NoRange,
        CantBeSeen,
        StateForbidden,
        StateRequired,
        HistoryError,
    }
}
