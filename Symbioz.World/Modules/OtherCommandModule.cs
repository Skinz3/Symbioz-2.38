using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Handlers.RolePlay.Commands;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using System.Drawing;
using Symbioz.World.Records.Characters;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Providers.Fights;
using Symbioz.Core.DesignPattern.StartupEngine;

namespace Symbioz.World.Modules
{
    class OtherCommandModule
    {
        public const ushort TokenId = 17745;

        public const ushort DungeonTokenQuantity = 3;

        public const ushort ExoTokenQuantity = 800;

        public const ushort MaxPrestige = 20;

        static Dictionary<string, int> Destinations = new Dictionary<string, int>()
        {
            {"champs",105381888},
            {"bouftou",121373185},
            {"royalmouth",55050240},
            {"ensablé",105644032},
            {"tofuroyal",96338948},
            {"kwak",64749568},
            {"mou",149423104 },
            {"craqueleurs",106954752},
            {"bworks",104595969},
            {"glours",62130696},
            {"obsi" ,57148161},
            {"kolosso",61865984},
            {"korriandre",62915584},
            {"ratamakna",102760961},
            {"nyée",149160960},
            {"mansots",56098816},
            {"halouine",101192704},
            {"hesque",5243139},
            {"otomai",22282240},
            {"rasboul",22808576},
            {"tynril",21233664},
            {"ancestral" ,149684224},
            {"kimbo",21495808},
            {"moon",157286400},
            {"kanniboul",157548544},
            {"nileza",109576705},
            {"frizz",109838849},
            {"kadorim",152829952},
            {"chateauwa",116392448},
            {"nowel",66585088},
            {"ombre",123207680},
            {"chafer", 87033344},
            {"meulou",11534336},
            {"klime",110362624 },
            {"comte",112198145 },
            {"givre",59511808 },
            {"sylargh",110100480},
            {"dc",72353792 },
            {"koulosse",107216896 },
            {"blops",166986752 },
            {"croca",27787264 },
            {"nidas",129500160},
            {"bulbes", 17564931},
            {"dantinea",169607168 },
            {"toxoliath",136840192 },
            {"xlii",143917569 },
            {"koutoulou", 169345024},
            {"firefoux", 16515841},
            {"ekarlatte",136578048 },
            {"fraktale" ,143138823},
            {"mallefisk",130286592 },
            {"merkator",119276033 }
        };
        [ChatCommand("exo")]
        public static void ExoCommand(string value, WorldClient client)
        {
            if (!client.Character.Inventory.Exist(TokenId, ExoTokenQuantity))
            {
                client.Character.Reply("Vous devez posséder " + ExoTokenQuantity + "x <b>[Ticket Doré]</b>");
                return;
            }

            var splitted = value.Split(null);

            if (splitted.Count() < 2)
            {
                client.Character.Reply("indentifieur invalide, .exo [pa,pm,po] [anneau1,anneau2,coiffe,cape,ceinture,bottes,amulette]");
                return;
            }
            string type = splitted[0];

            string position = splitted[1];

            EffectsEnum effect = 0;

            CharacterInventoryPositionEnum pos = CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;

            switch (type)
            {
                case "pa":
                    effect = EffectsEnum.Effect_AddAP_111;
                    break;
                case "pm":
                    effect = EffectsEnum.Effect_AddMP_128;
                    break;
                case "po":
                    effect = EffectsEnum.Effect_AddRange;
                    break;
                default:
                    client.Character.Reply("indentifieur invalide, .exo [pa,pm,po] [anneau1,anneau2,coiffe,cape,ceinture,bottes,amulette]");
                    return;
            }

            switch (position)
            {
                case "anneau1":
                    pos = CharacterInventoryPositionEnum.INVENTORY_POSITION_RING_LEFT;
                    break;
                case "anneau2":
                    pos = CharacterInventoryPositionEnum.INVENTORY_POSITION_RING_RIGHT;
                    break;
                case "coiffe":
                    pos = CharacterInventoryPositionEnum.ACCESSORY_POSITION_HAT;
                    break;
                case "cape":
                    pos = CharacterInventoryPositionEnum.ACCESSORY_POSITION_CAPE;
                    break;
                case "ceinture":
                    pos = CharacterInventoryPositionEnum.ACCESSORY_POSITION_BELT;
                    break;
                case "bottes":
                    pos = CharacterInventoryPositionEnum.ACCESSORY_POSITION_BOOTS;
                    break;
                case "amulette":
                    pos = CharacterInventoryPositionEnum.ACCESSORY_POSITION_AMULET;
                    break;
                default:
                    client.Character.Reply("indentifieur invalide, .exo [pa,pm,po] [anneau1,anneau2,coiffe,cape,ceinture,bottes,amulette]");
                    break;
            }

            CharacterItemRecord item = client.Character.Inventory.GetEquipedItems().FirstOrDefault(x => x.PositionEnum == pos);

            if (item == null)
            {
                client.Character.Reply("Vous devez équiper un item a cet emplacement.");
                return;
            }

            if (item.FirstEffect<EffectInteger>(effect) != null)
            {
                client.Character.Reply("L'effet est déja présent...");
                return;
            }

            client.Character.Inventory.SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item.Quantity);

            var token = client.Character.Inventory.GetFirstItem(TokenId, ExoTokenQuantity);
            client.Character.Inventory.RemoveItem(token.UId, ExoTokenQuantity);

            item.AddEffectInteger(effect, 1);
            client.Character.Inventory.OnItemModified(item);
            client.Character.Reply("L'item a été exo.");
            client.Character.SpellAnim(2158);
        }
        [ChatCommand("enclos")]
        public static void EnclosCommand(string value, WorldClient client)
        {
            client.Character.Teleport(149816, 526);
        }
        [ChatCommand("prestige", ServerRoleEnum.Player)]
        public static void PrestigeCommand(string value, WorldClient client)
        {
            if (client.Character.Level == 200 && !client.Character.Fighting)
            {
                if (client.Character.Record.Prestige < MaxPrestige)
                {
                    client.Character.Inventory.UnequipAll();

                    if (client.Character.Inventory.HasMountEquiped && client.Character.Inventory.Mount.Toggled)
                        client.Character.Inventory.ToggleMount();

                    client.Character.SetDirection(DirectionsEnum.DIRECTION_SOUTH_EAST);
                    client.Character.SpellAnim(7261);

                    client.Character.Record.Prestige++;

                    client.Character.SetLevel(1);
                    client.Character.Record.Stats.ActionPoints.Base -= 1;
                    client.Character.Record.Stats.LifePoints += 100;
                    client.Character.Record.Stats.MaxLifePoints += 100;
                    client.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 13, 100);
                    client.Character.Record.Stats.AllDamagesBonus.Base += 20;
                    client.Character.Reply("Vous avez gagné <b>20</b> points de dommages.");
                    client.Character.Restat(false);
                    client.Character.Inventory.AddItem(12124, (uint)(client.Character.Record.Prestige * 250));

                    if (client.Character.Record.Prestige == MaxPrestige / 2)
                    {
                        client.Character.LearnTitle(51);
                    }
                    if (client.Character.Record.Prestige == MaxPrestige)
                    {
                        client.Character.LearnTitle(50);
                        client.Character.LearnEmote(40);
                        client.Character.LearnSpell(6798);
                        WorldServer.Instance.OnClients(x => x.Character.Reply(client.Character.Name + " vient de passer prestige <b>" + MaxPrestige + "</b> ! Félicitation a lui. QUEL HOMME !!, il obtient une émote et un titre légendaire!", Color.BlueViolet));
                    }
                    else
                    {
                        WorldServer.Instance.OnClients(x => x.Character.Reply(client.Character.Name + " vient de passer prestige " + client.Character.Record.Prestige + "! Félicitation a lui.", Color.BlueViolet));
                    }
                    client.Character.OpenPopup(0, "Amnesia", "Vous venez de passer au rang de prestige " + client.Character.Record.Prestige + Environment.NewLine
                        + "Vous repassez niveau 1 et vous avez acquis des bonus permanents que vous pouvez consulter dans le chat." + Environment.NewLine + " Vous devez vous reconnecter pour actualiser votre niveau.");

                    client.Character.RefreshActorOnMap();
                    client.Character.RefreshStats();
                }

            }
            else
            {
                client.Character.Reply("Vous devez être niveau 200 pour passer un prestige.");
            }
        }
        [ChatCommand("donjon", ServerRoleEnum.Player)]
        public static void DungeonCommand(string value, WorldClient client)
        {
            if (client.Character.Level < 15)
            {
                client.Character.Reply("Votre niveau est trop faible pour utiliser cette commande...");
                return;
            }
            if (value == null || !Destinations.ContainsKey(value))
            {
                client.Character.Reply("Le donjon n'existe pas, liste des donjons disponibles:", Color.Blue);
                client.Character.Reply(Destinations.ToList().ConvertAll<string>(x => x.Key).ToCSV(), Color.Blue);
            }
            else
            {
                if (client.Character.Inventory.Exist(TokenId, DungeonTokenQuantity))
                {
                    int mapId = Destinations[value];
                    client.Character.Teleport(mapId);
                    CharacterItemRecord item = client.Character.Inventory.GetFirstItem(TokenId, DungeonTokenQuantity);
                    client.Character.Inventory.RemoveItem(item, DungeonTokenQuantity);
                    client.Character.OnItemLost(TokenId, DungeonTokenQuantity);
                }
                else
                {
                    client.Character.Reply("Vous devez posséder " + DungeonTokenQuantity + "x <b>[Ticket Doré]</b>");
                }
            }

        }
    }
}
