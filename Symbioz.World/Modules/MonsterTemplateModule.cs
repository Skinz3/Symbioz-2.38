using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Providers.Maps.Monsters;
using Symbioz.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Modules
{
    public class MonsterTemplateModule
    {
        public const int BossDroppedKamasMultiplicator = 2;

        public const int DroppedKamasRatio = 2;

        static Logger logger = new Logger();
     //   [StartupInvoke(StartupInvokePriority.Modules)]
        public static void FixTemplates()
        {
            AsyncRandom random = new AsyncRandom();

            foreach (var monster in MonsterRecord.Monsters)
            {
                int minDroppedKamas = 0;
                int maxDroppedKamas = 0;
                int level = monster.GetGrade(1).Level;

                minDroppedKamas = random.Next(level * DroppedKamasRatio, level * (DroppedKamasRatio * 2));

                maxDroppedKamas = minDroppedKamas + level * 2;

                if (monster.IsBoss)
                {
                    minDroppedKamas *= BossDroppedKamasMultiplicator;
                    maxDroppedKamas *= BossDroppedKamasMultiplicator;
                }

                monster.MinDroppedKamas = minDroppedKamas / 2;
                monster.MaxDroppedKamas = maxDroppedKamas / 2;
                monster.UpdateInstantElement();
                logger.Gray("Fixed : " + monster.Name);

            }
        }
    }
}
