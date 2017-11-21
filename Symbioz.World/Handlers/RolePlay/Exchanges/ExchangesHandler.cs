using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Dialogs.DialogBox;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Exchanges;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay.Exchanges
{
    public class ExchangesHandler
    {
        [MessageHandler]
        public static void HandleExchangeCraftCount(ExchangeCraftCountRequestMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.CRAFT))
            {
                client.Character.GetDialog<AbstractCraftExchange>().SetCount(message.count);
            }
        }
        [MessageHandler]
        public static void HandleExchangeSetCraftRecipe(ExchangeSetCraftRecipeMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.CRAFT))
            {
                client.Character.GetDialog<CraftExchange>().SetRecipe(message.objectGID);
            }
        }
        [MessageHandler]
        public static void HandleBidHouseSearch(ExchangeBidHouseSearchMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.BIDHOUSE_BUY))
            {
                client.Character.GetDialog<BuyExchange>().ShowList(message.genId);
            }
        }
        [MessageHandler]
        public static void HandleExchangeBidHouseBuy(ExchangeBidHouseBuyMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.BIDHOUSE_BUY))
            {
                client.Character.GetDialog<BuyExchange>().Buy(message.uid, message.qty, message.price);
            }
        }
        [MessageHandler]
        public static void HandleExchangeBidhouseList(ExchangeBidHouseListMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.BIDHOUSE_BUY))
            {
                client.Character.GetDialog<BuyExchange>().ShowList(message.id);
            }
        }
        [MessageHandler]
        public static void HandleExchangeBidhouseTypes(ExchangeBidHouseTypeMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.BIDHOUSE_BUY))
            {
                client.Character.GetDialog<BuyExchange>().ShowTypes(message.type);
            }
        }
        [MessageHandler]
        public static void HandleExchangeObjectModifyPriced(ExchangeObjectModifyPricedMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.BIDHOUSE_SELL))
            {
                client.Character.GetDialog<SellExchange>().ModifyItemPriced(message.objectUID, message.quantity, message.price);
            }
        }
        [MessageHandler]
        public static void HandleExchangeObjectMovePriced(ExchangeObjectMovePricedMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.BIDHOUSE_SELL))
            {
                client.Character.GetDialog<SellExchange>().MoveItemPriced(message.objectUID, message.quantity, message.price);
            }
        }
        [MessageHandler]
        public static void HandleExchangeObjectMove(ExchangeObjectMoveMessage message, WorldClient client)
        {
            client.Character.GetDialog<Exchange>().MoveItem(message.objectUID, message.quantity);
        }
        [MessageHandler]
        public static void HandleExchangeReady(ExchangeReadyMessage message, WorldClient client)
        {
            if (client.Character.GetDialog<Exchange>() != null)
                client.Character.GetDialog<Exchange>().Ready(message.ready, message.step);
        }
        [MessageHandler]
        public static void HandleExchangeObjectMoveKamas(ExchangeObjectMoveKamaMessage message, WorldClient client)
        {
            if (client.Character.Record.Kamas >= message.quantity && client.Character.GetDialog<Exchange>() != null)
            {
                client.Character.GetDialog<Exchange>().MoveKamas(message.quantity);
            }
        }
        [MessageHandler]
        public static void HandleExchangePlayerRequest(ExchangePlayerRequestMessage message, WorldClient client)
        {
            Character target = client.Character.Map.Instance.GetEntity<Character>((long)message.target);

            if (target == null)
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_IMPOSSIBLE);
                return;
            }
            if (target.Busy)
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_CHARACTER_OCCUPIED);
                return;
            }
            if (target.Map == null || target.Record.MapId != client.Character.Record.MapId)
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_IMPOSSIBLE);
                return;
            }
            if (!target.Map.Position.AllowExchangesBetweenPlayers)
            {
                client.Character.OnExchangeError(ExchangeErrorEnum.REQUEST_IMPOSSIBLE);
                return;
            }

            switch ((ExchangeTypeEnum)message.exchangeType)
            {
                case ExchangeTypeEnum.PLAYER_TRADE:
                    target.OpenRequestBox(new PlayerTradeRequest(client.Character, target));
                    break;
                default:
                    client.Send(new ExchangeErrorMessage((sbyte)ExchangeErrorEnum.REQUEST_IMPOSSIBLE));
                    break;

            }
        }
        [MessageHandler]
        public static void HandleExchangeAccept(ExchangeAcceptMessage message, WorldClient client)
        {
            if (client.Character.RequestBox is PlayerTradeRequest)
                client.Character.RequestBox.Accept();
        }
        [MessageHandler]
        public static void HandleExchangeBuy(ExchangeBuyMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.NPC_SHOP))
            {
                client.Character.GetDialog<NpcShopExchange>().Buy((ushort)message.objectToBuyId, message.quantity);
            }
        }
        [MessageHandler]
        public static void HandleExchangeSell(ExchangeSellMessage message, WorldClient client)
        {
            if (client.Character.IsInExchange(ExchangeTypeEnum.NPC_SHOP))
            {
                client.Character.GetDialog<NpcShopExchange>().Sell(message.objectToSellId, message.quantity);
            }
        }
    }
}
