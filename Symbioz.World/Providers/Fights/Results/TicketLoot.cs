using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Results
{
    [CustomLoot("", FightTypeEnum.FIGHT_TYPE_MXvM)]
    public class DayFightLoot : CustomLoot
    {
        public DayFightLoot(FightPlayerResult result)
            : base(result)
        {
        }
        public override void Apply()
        {
            int count = Result.Fighter.OposedTeam().GetFighters<MonsterFighter>(false).Count * 6;
            if (count > 0)
                this.Add(17745, (uint)count);
        }
    }


    [CustomLoot("", FightTypeEnum.FIGHT_TYPE_PvM)]
    public class TicketLoot : CustomLoot
    {
        public static ushort[] NonLootable = new ushort[]
        {
             494,1003,2785,2843,3421,686,685,684,692,2843,3421,4119
        };
        public TicketLoot(FightPlayerResult result)
            : base(result)
        {

        }
        public override void Apply()
        {
            var random = new AsyncRandom();

            foreach (var monster in Result.Fighter.OposedTeam().GetFighters<MonsterFighter>(false))
            {
                if (NonLootable.Contains(monster.Template.Id))
                {
                    return;
                }
            }


            this.Add(17745, GetGoldenTicketsCount(Result.ExperienceData.ExperienceFightDelta));

        }
        public uint GetGoldenTicketsCount(long experience)
        {
            if (experience <= 0)
                return 0;
            var ratio = Math.Floor(Math.Log10(experience) + 1);

            if (ratio <= 3)
            {
                return 1;
            }
            if (ratio == 4)
            {
                return 2;
            }
            ratio = Math.Pow(ratio, 2) * 0.4;
            return (uint)ratio;
        }
    }
}
