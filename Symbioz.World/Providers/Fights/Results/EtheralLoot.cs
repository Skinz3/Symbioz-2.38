using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using Symbioz.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Results
{
    [CustomLoot("PL>10", FightTypeEnum.FIGHT_TYPE_PvM)]
    public class EtheralLoot : CustomLoot
    {
        public const sbyte MinimumDropPercentage = 5;

        private AsyncRandom Random
        {
            get;
            set;
        }
        public EtheralLoot(FightPlayerResult result)
            : base(result)
        {
            this.Random = new AsyncRandom();
        }
        public override void Apply()
        {
            int percentage = (Result.Level / (20 - Result.Fighter.OposedTeam().GetFightersCount(false)));

            percentage = percentage <= MinimumDropPercentage ? MinimumDropPercentage : percentage;

            if (this.Random.TriggerAleat((sbyte)percentage))
            {
                ItemRecord template = WeaponRecord.Weapons.FindAll(x => x.Etheral).Random().Template;

                this.Add(template, 1);
            }


        }
    }
}
