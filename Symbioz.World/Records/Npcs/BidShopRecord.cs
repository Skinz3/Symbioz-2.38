using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Maps.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Npcs
{
    [Table("BidShops")]
    public class BidShopRecord : ITable
    {
        public static List<BidShopRecord> BidShops = new List<BidShopRecord>();

        [Primary]
        public int Id;

        public List<uint> Quantities;

        public List<uint> ItemTypes;

        public uint MaxItemPerAccount;

        public BidShopRecord(int id, List<uint> quantities, List<uint> itemtypes, uint maxitemperaccount)
        {
            this.Id = id;
            this.Quantities = quantities;
            this.ItemTypes = itemtypes;
            this.MaxItemPerAccount = maxitemperaccount;
        }

        public static BidShopRecord GetBidShop(int id)
        {
            return BidShops.Find(x => x.Id == id);
        }
        public SellerBuyerDescriptor GetBuyerDescriptor(int npcid)
        {
            return new SellerBuyerDescriptor(Quantities.ToArray(), ItemTypes.ToArray(), 0, 0,
                200, MaxItemPerAccount, npcid, 50);
        }

    }
}
