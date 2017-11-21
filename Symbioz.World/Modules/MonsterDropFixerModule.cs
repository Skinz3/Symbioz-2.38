using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Modules
{
    class MonsterDropFixerModule
    {
        [StartupInvoke(StartupInvokePriority.Modules)]
        public static void Fix()
        {
            AddDrop(3752, 694, 12); // Toxoliath Pourpre
            AddDrop(3446, 739, 12);
            AddDrop(3578, 737, 30);
        }


        static void AddDrop(ushort monsterId, ushort itemId, int dropPercent)
        {
            var monster = MonsterRecord.GetMonster(monsterId);

            var drop = new MonsterDrop()
            {
                ItemId = itemId,
                Count = 1,
                DropId = 0,
                DropLimit = 1,
                HasCriteria = false,
                PercentDropForGrade1 = (short)(dropPercent),
                PercentDropForGrade2 = (short)(dropPercent + 2),
                PercentDropForGrade3 = (short)(dropPercent + 4),
                PercentDropForGrade4 = (short)(dropPercent + 6),
                PercentDropForGrade5 = (short)(dropPercent + 8),
                ProspectingLock = 100

            };

            monster.Drops.Add(drop);
        }
    }
}
