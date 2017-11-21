using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Modules
{
    public class MonsterPowerCalculator
    {
      //     [StartupInvoke("MonsterPowerCalculator", StartupInvokePriority.Modules)]
        public static void Initialize()
        {
            UpdateLogger logger = new UpdateLogger();
            int i = 0;
            foreach (var monster in MonsterRecord.Monsters)
            {
                i++;

                MonsterGrade grade = monster.GetGrade(5);

                monster.Power = grade.Level * 2;
                monster.Power += (int)(grade.LifePoints / 10);

                if (monster.Power > 1000)
                {
                    monster.Power = 1000;
                }

                if (monster.IsMiniBoss)
                    monster.Power += 300;

                if (monster.IsBoss)
                    monster.Power += 500;

                monster.UpdateInstantElement();

                logger.Update(i.Percentage(MonsterRecord.Monsters.Count));
            }
        }
    }
}
