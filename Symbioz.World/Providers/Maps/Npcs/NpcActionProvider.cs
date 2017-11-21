using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Npcs;
using Symbioz.World.Records.Items;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Exchanges;
using Symbioz.World.Providers.Maps.Cinematics;

namespace Symbioz.World.Providers.Maps.Npcs
{
    class NpcActionProvider
    {
        public static Dictionary<NpcActionAttribute, MethodInfo> Handlers = typeof(NpcActionProvider).MethodsWhereAttributes<NpcActionAttribute>();

        public static void Handle(Character character, Npc npc, NpcActionRecord actionRecord)
        {
            var handler = Handlers.FirstOrDefault(x => x.Key.ActionType == actionRecord.ActionIdEnum);

            if (handler.Value != null)
            {
                handler.Value.Invoke(null, new object[] { character, npc, actionRecord });
            }
            else
            {
                character.ReplyError(actionRecord.ActionIdEnum + " is not handled...");
            }
        }

        [NpcAction(NpcActionTypeEnum.Talk)]
        public static void Talk(Character character, Npc npc, NpcActionRecord action)
        {
            character.TalkToNpc(npc, action);
        }
        /// <summary>
        /// Moche, trouver un autre moyen de gérer ça, sans avoir a passer par un split incomphrénsible en bdd
        /// </summary>
        /// <param name="character"></param>
        /// <param name="npc"></param>
        /// <param name="action"></param>
        [NpcAction(NpcActionTypeEnum.BuySell)]
        public static void BuySell(Character character, Npc npc, NpcActionRecord action)
        {
            ushort tokenId = 0;
            bool priceLevel = false;

            if (action.Value2 != null && action.Value2 != string.Empty)
            {
                var splitted = action.Value2.Split(',');
                tokenId = ushort.Parse(splitted[0]);
                priceLevel = bool.Parse(splitted[1]);
            }

            ItemRecord[] items = Array.ConvertAll<ushort, ItemRecord>(action.Value1.FromCSV<ushort>().ToArray(), x => ItemRecord.GetItem(x));

            character.OpenNpcShop(npc, items, tokenId, priceLevel);
        }

        [NpcAction(NpcActionTypeEnum.Buy)]
        public static void Buy(Character character, Npc npc, NpcActionRecord action)
        {
            character.OpenBidhouseBuy(npc, BidShopRecord.GetBidShop(int.Parse(action.Value1)),
                character.IsInExchange(ExchangeTypeEnum.BIDHOUSE_SELL));
        }

        [NpcAction(NpcActionTypeEnum.Sell)]
        public static void Sell(Character character, Npc npc, NpcActionRecord action)
        {
            character.OpenBidhouseSell(npc, BidShopRecord.GetBidShop(int.Parse(action.Value1)),
                character.IsInExchange(ExchangeTypeEnum.BIDHOUSE_BUY));
        }
    }
}
