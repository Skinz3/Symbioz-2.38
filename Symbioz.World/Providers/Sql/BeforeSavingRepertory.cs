using Symbioz.ORM;
using Symbioz.World.Network;
using Symbioz.World.Providers.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Cyclics.Sql
{
    public class BeforeSavingRepertory
    {
        [BeforeSaving]
        public static void UpdateCharacters()
        {
            foreach (var client in WorldServer.Instance.GetOnlineClients())
            {
                client.Character.Record.UpdateElement();
            }
        }
    }
}
