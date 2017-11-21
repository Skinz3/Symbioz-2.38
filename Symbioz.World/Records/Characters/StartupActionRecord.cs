using Symbioz.ORM;
using Symbioz.Protocol.Types;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Characters
{
    [Table("StartupActions", false),Resettable]
    public class StartupActionRecord : ITable
    {
        public static List<StartupActionRecord> StartupActions = new List<StartupActionRecord>();

        [Primary]
        public int Id;

        public string Title;

        public int AccountId;

        public List<ushort> GIds;

        public List<uint> Quantities;

        public StartupActionRecord(int id, string title, int accountId, List<ushort> gIds,
            List<uint> quantities)
        {
            this.Id = id;
            this.Title = title;
            this.AccountId = accountId;
            this.GIds = gIds;
            this.Quantities = quantities;
        }
        public StartupActionAddObject GetStartupActionAddObject()
        {
            List<ObjectItemInformationWithQuantity> items = new List<ObjectItemInformationWithQuantity>();

            for (int i = 0; i < GIds.Count; i++)
            {
                ItemRecord item = ItemRecord.GetItem(GIds[i]);

                if (item != null)
                {
                    items.Add(item.GetObjectItemInformationWithQuantity(Quantities[i]));
                }
            }
            return new StartupActionAddObject(Id, Title, string.Empty, string.Empty, string.Empty, items.ToArray());
        }
        public static List<StartupActionRecord> GetStartupActions(int accountId)
        {
            lock (StartupActions)
                return DatabaseReader<StartupActionRecord>.Read("AccountId = " + accountId);
        }
    }
}
