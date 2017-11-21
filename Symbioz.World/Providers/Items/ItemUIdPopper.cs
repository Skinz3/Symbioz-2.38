using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Items
{
    public class ItemUIdPopper
    {
        static object Locker = new object();

        static UniqueIdProvider UIDPoper;

        [StartupInvoke(StartupInvokePriority.Ninth)]
        public static void InitIdPoper()
        {
            List<uint> uids = CharacterItemRecord.CharactersItems.ConvertAll<uint>(x => x.UId);
            uids.AddRange(BankItemRecord.BankItems.ConvertAll<uint>(x => x.UId));
            uids.Sort();
            uint lastId = uids.Count == 0 ? 1 : uids.Last();
            UIDPoper = new UniqueIdProvider((int)lastId);
        }
        public static uint PopUID()
        {
            lock (Locker)
                return (uint)UIDPoper.Pop();
        }
    }
}
