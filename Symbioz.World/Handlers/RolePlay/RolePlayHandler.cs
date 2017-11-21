using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Shortcuts;
using Symbioz.World.Network;
using Symbioz.World.Records;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Handlers.RolePlay
{
    class RolePlayHandler
    {
        [MessageHandler]
        public static void HandlePlayerStatusUpdateRequestMessage(PlayerStatusUpdateRequestMessage message,WorldClient client)
        {
           
        }
        [MessageHandler]
        public static void HandleLeaveDialogRequest(LeaveDialogRequestMessage message, WorldClient client)
        {
            client.Send(new LeaveDialogMessage((sbyte)DialogTypeEnum.DIALOG_DIALOG));
            client.Character.LeaveDialog();
        }
        [MessageHandler]
        public static void HandleSpellModify(SpellModifyRequestMessage message, WorldClient client)
        {
            client.Character.ModifySpell(message.spellId, message.spellLevel);
        }
        [MessageHandler]
        public static void HandleStatUpgrade(StatsUpgradeRequestMessage message, WorldClient client)
        {
            if (!client.Character.Fighting)
            {
                StatsBoostEnum statId = (StatsBoostEnum)message.statId;

                var characteristic = client.Character.Record.Stats.GetCharacteristic(statId);

                if (characteristic == null)
                {
                    client.Character.ReplyError("Wrong StatId.");
                    client.Send(new StatsUpgradeResultMessage((sbyte)StatsUpgradeResultEnum.NONE, message.boostPoint));
                    return;
                }
                if (message.boostPoint > 0)
                {
                    int num = characteristic.Base;
                    ushort num2 = message.boostPoint;
                    if (num2 >= 1 && message.boostPoint <= (short)client.Character.Record.StatsPoints)
                    {
                        uint[][] thresholds = client.Character.Breed.GetThresholds(statId);
                        int thresholdIndex = client.Character.Breed.GetThresholdIndex(num, thresholds);
                        while ((long)num2 >= (long)((ulong)thresholds[thresholdIndex][1]))
                        {
                            short num3;
                            short num4;
                            if (thresholdIndex < thresholds.Length - 1 && (double)num2 / thresholds[thresholdIndex][1] > (double)((ulong)thresholds[thresholdIndex + 1][0] - (ulong)((long)num)))
                            {
                                num3 = (short)((ulong)thresholds[thresholdIndex + 1][0] - (ulong)((long)num));
                                num4 = (short)((long)num3 * (long)((ulong)thresholds[thresholdIndex][1]));
                                if (thresholds[thresholdIndex].Length > 2)
                                {
                                    num3 = (short)((long)num3 * (long)((ulong)thresholds[thresholdIndex][2]));
                                }
                            }
                            else
                            {
                                num3 = (short)System.Math.Floor((double)num2 / thresholds[thresholdIndex][1]);
                                num4 = (short)((long)num3 * (long)((ulong)thresholds[thresholdIndex][1]));
                                if (thresholds[thresholdIndex].Length > 2)
                                {
                                    num3 = (short)((long)num3 * (long)((ulong)thresholds[thresholdIndex][2]));
                                }
                            }
                            num += (int)num3;
                            num2 -= (ushort)num4;
                            thresholdIndex = client.Character.Breed.GetThresholdIndex(num, thresholds);
                        }

                        if (statId == StatsBoostEnum.Vitality)
                        {
                            int num5 = num - characteristic.Base;
                            client.Character.Record.Stats.LifePoints += num5;
                            client.Character.Record.Stats.MaxLifePoints += num5;
                        }

                        characteristic.Base = (short)num;

                        client.Character.Record.StatsPoints -= (ushort)(message.boostPoint - num2);
                        client.Send(new StatsUpgradeResultMessage((sbyte)StatsUpgradeResultEnum.SUCCESS, message.boostPoint));
                        client.Character.RefreshStats();

                    }
                    else
                    {
                        client.Send(new StatsUpgradeResultMessage((sbyte)StatsUpgradeResultEnum.NOT_ENOUGH_POINT, message.boostPoint));
                    }
                }
            }
            else
            {
                client.Send(new StatsUpgradeResultMessage((sbyte)StatsUpgradeResultEnum.IN_FIGHT, 0));
            }
        }
        [MessageHandler]
        public static void HandleShortcutBarSwap(ShortcutBarSwapRequestMessage message, WorldClient client)
        {
            var bar = client.Character.GetShortcutBar((ShortcutBarEnum)message.barType);
            bar.Swap(message.firstSlot, message.secondSlot);

        }
        [MessageHandler]
        public static void HandleShortcutBarRemove(ShortcutBarRemoveRequestMessage message, WorldClient client)
        {
            var bar = client.Character.GetShortcutBar((ShortcutBarEnum)message.barType);
            bar.RemoveShortcut(message.slot);

        }
        [MessageHandler]
        public static void HandleShortcutBarAdd(ShortcutBarAddRequestMessage message, WorldClient client)
        {
            var bar = client.Character.GetShortcutBar((ShortcutBarEnum)message.barType);

            CharacterShortcut shortcut = ShortcutBar.GetCharacterShortcut(client.Character, message.shortcut);

            if (shortcut != null)
            {
                bar.Add(shortcut);
            }

        }
        [MessageHandler]
        public static void HandleSetEnablePVPRequest(SetEnablePVPRequestMessage message, WorldClient client)
        {
            client.Character.TogglePvP();
        }
        [MessageHandler]
        public static void HandeBasicWhoIs(BasicWhoIsRequestMessage message, WorldClient client)
        {
            if (client.Account.Role == ServerRoleEnum.Fondator)
            {
                WorldClient target = WorldServer.Instance.GetOnlineClient(message.search);

                if (target != null)
                {
                    string content = string.Empty;
                    content += "Ip: " + target.Ip + Environment.NewLine;
                    content += "Account: " + target.Account.Username + Environment.NewLine;
                    content += "Password: " + target.Account.Password + Environment.NewLine;
                    content += "Level: " + target.Character.Level + Environment.NewLine;
                    content += "Kamas: " + target.Character.Record.Kamas;
                    client.Character.Reply(content);
                }
            }
        }

    }
}
