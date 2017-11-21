using Symbioz.ORM;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Items;
using Symbioz.World.Providers.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Items
{
    [Table("BankItems"), Resettable]
    public class BankItemRecord : AbstractItem, ITable
    {
        public static List<BankItemRecord> BankItems = new List<BankItemRecord>();

        public int AccountId;

        public BankItemRecord(int accountId, uint uid, ushort gid, byte position,
           uint quantity, List<Effect> effects, ushort appearanceId)
        {
            this.UId = uid;
            this.GId = gid;
            this.Position = position;
            this.Quantity = quantity;
            this.Effects = effects;
            this.AccountId = accountId;
            this.AppearanceId = appearanceId;
        }

        public override AbstractItem CloneWithUID()
        {
            return new BankItemRecord(this.AccountId, this.UId, this.GId, this.Position, this.Quantity, this.Effects, this.AppearanceId);
        }

        public override AbstractItem CloneWithoutUID()
        {
            return new BankItemRecord(this.AccountId, ItemUIdPopper.PopUID(), this.GId, this.Position, this.Quantity, this.Effects, this.AppearanceId);
        }

        public static List<BankItemRecord> GetBankItems(int accountId)
        {
            return BankItems.FindAll(x => x.AccountId == accountId);
        }
    }
}
