using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Almanach
{
    [Table("Almanach")]
    public class AlmanachRecord : ITable
    {
        public static List<AlmanachRecord> Almanachs = new List<AlmanachRecord>();

        [Primary]
        public int Id;

        public ushort ItemGId;

        public uint Quantity;

        public int XpRewardPercentage;

        public int RewardItemGId;

        public int RewardItemQuantity;

        public AlmanachRecord(int id, ushort itemGId, uint quantity, int xpRewardPercentage, int rewardItemGId, int rewardItemQuantity)
        {
            this.Id = id;
            this.ItemGId = itemGId;
            this.Quantity = quantity;
            this.XpRewardPercentage = xpRewardPercentage;
            this.RewardItemGId = rewardItemGId;
            this.RewardItemQuantity = rewardItemQuantity;
        }


        public static AlmanachRecord GetAlmanachOfTheDay()
        {
            return Almanachs.FirstOrDefault(x => x.Id == DateTime.Now.Day);
        }
    }
}
