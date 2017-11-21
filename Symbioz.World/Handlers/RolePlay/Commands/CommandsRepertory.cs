using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Network;
using Symbioz.World.Records;
using Symbioz.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Symbioz.World.Records.Almanach;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Entities;
using Symbioz.World.Providers.Maps;
using Symbioz.World.Records.Interactives;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Items;
using Symbioz.World.Providers.Maps.Monsters;
using Symbioz.World.Providers.Maps.Npcs;
using Symbioz.World.Records.Npcs;
using Symbioz.World.Records.Characters;
using Symbioz.ORM;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Records.Spells;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Handlers.Approach;
using Symbioz.World.Providers.Fights.Results;
using Symbioz.World.Models;
using Symbioz.World.Providers.Delayed;
using Symbioz.World.Providers.Fights;
using Symbioz.World.Models.Monsters;
using Symbioz.World.Providers.Guilds;

namespace Symbioz.World.Handlers.RolePlay.Commands
{
    class CommandsRepertory
    {
        [ChatCommand("map", ServerRoleEnum.Administrator)]
        public static void MapInformations(string value, WorldClient client)
        {
            string message = string.Empty;

            foreach (var entity in client.Character.Map.Instance.GetEntities())
            {
                client.Character.Reply(entity.ToString(), Color.CornflowerBlue);
            }
        }
        [ChatCommand("recipe", ServerRoleEnum.Administrator)]
        public static void RecipeCommand(string value, WorldClient client)
        {
            for (int i = 0; i < 50; i++)
            {
                var recipe = RecipeRecord.GetRecipe(ushort.Parse(value));

                foreach (var item in recipe.Ingredients)
                {
                    client.Character.Inventory.AddItem(item.Key, item.Value);
                }
            }


        }
        [ChatCommand("itemlist", ServerRoleEnum.Administrator)]
        public static void ItemListCommand(string value, WorldClient client)
        {
            var itemType = (ItemTypeEnum)int.Parse(value);

            var itemIds = Array.ConvertAll<ItemRecord, ushort>(ItemRecord.GetItems(itemType), x => x.Id);

            client.Character.Reply("Items for type: " + itemType + Environment.NewLine + itemIds.ToCSV());
        }
        [ChatCommand("nadd", ServerRoleEnum.Fondator)]
        public static void NpcCommand(string value, WorldClient client)
        {
            NpcSpawnsManager.Instance.Spawn(ushort.Parse(value), client.Character.Map, client.Character.CellId, client.Character.Record.Direction);
        }
        [ChatCommand("dialog", ServerRoleEnum.Fondator)]
        public static void DialogCommand(string value, WorldClient client)
        {
            if (client.Character.Dialog != null)
            {
                client.Character.Reply("Dialog: " + client.Character.Dialog.GetType().Name);
            }
            if (client.Character.RequestBox != null)
            {
                client.Character.Reply("RequestBox: " + client.Character.RequestBox.GetType().Name);
            }
        }
        [ChatCommand("monsters", ServerRoleEnum.Moderator)]
        public static void MonsterCommand(string value, WorldClient client)
        {
            if (client.Character.Map.Instance.MonsterGroupCount >= MonsterSpawnManager.MaxMonsterGroupPerMap)
            {
                client.Character.ReplyError("Impossible d'ajouter un groupe de monstres a la carte, celle-ci est déja complete.");
                return;
            }
            List<MonsterSpawnRecord> spawns = new List<MonsterSpawnRecord>();

            foreach (var monsterId in value.Split(','))
            {
                spawns.Add(new MonsterSpawnRecord(MonsterSpawnRecord.MonsterSpawns.DynamicPop(x => x.Id),
                   ushort.Parse(monsterId), client.Character.Map.SubAreaId, 100));
            }
            if (spawns.Count > 0)
                MonsterSpawnManager.Instance.AddGeneratedMonsterGroup(client.Character.Map.Instance, spawns.ToArray(), false);
            else
                client.Character.Reply("Specifiez une liste de monstre (id1,id2,id3..etc)");
        }
        [ChatCommand("dj", ServerRoleEnum.Fondator)]
        public static void DJCommand(string value, WorldClient client)
        {
            int id = DelayedActionRecord.DelayedActions.DynamicPop(x => x.Id);
            DelayedActionRecord record = new DelayedActionRecord(id, "Monsters", 30, value, client.Character.Map.Id.ToString());
            record.AddElement();
            DelayedAction action = new DelayedAction(record);
            DelayedActionManager.AddAction(action);
            action.Execute();
        }
        [ChatCommand("reloadAlmanach", ServerRoleEnum.Fondator)]
        public static void Reload(string value, WorldClient client)
        {
            DatabaseManager.GetInstance().Reload<AlmanachRecord>();
        }

        [ChatCommand("addOrnament", ServerRoleEnum.Animator)]
        public static void AddOrnament(string value, WorldClient client)
        {
            string[] args = value.Split(' ');
            if (args.Length < 1)
            {
                client.Character.Reply("Specifiez l'id de l'ornamenent.");
                return;
            }
            if (args.Length == 1)
            {
                client.Character.LearnOrnament(ushort.Parse(args[0]), true);
            }

            if (args.Length == 2)
            {

                Character target = WorldServer.Instance.GetOnlineClient(args[0]).Character;
                target.LearnOrnament(ushort.Parse(args[1]), true);
                client.Character.Reply(target.Name + " connais desormais l'ornement " + args[1]);
            }

        }
        [ChatCommand("kick", ServerRoleEnum.Moderator)]
        public static void Kick(string value, WorldClient client)
        {
            WorldClient target = WorldServer.Instance.GetOnlineClient(value);

            if (target != null)
            {
                target.Disconnect();
            }
        }
        [ChatCommand("reloadSubareas", ServerRoleEnum.Fondator)]
        public static void ReloadSubarea(string value, WorldClient client)
        {
            DatabaseManager.GetInstance().Reload<SubareaRecord>();

            foreach (var map in MapRecord.Maps)
            {
                map.SubArea = SubareaRecord.GetSubarea(map.SubAreaId);
            }

            client.Character.Reply("Reloaded");
        }
        [ChatCommand("addTitle", ServerRoleEnum.Animator)]
        public static void AddTitle(string value, WorldClient client)
        {
            string[] args = value.Split(' ');
            if (args.Length < 1)
            {
                client.Character.Reply("Specifiez l'id du titre.");
                return;
            }
            if (args.Length == 1)
            {
                client.Character.LearnTitle(ushort.Parse(args[0]));
            }
            if (args.Length == 2)
            {
                Character target = WorldServer.Instance.GetOnlineClient(args[0]).Character;
                target.LearnTitle(ushort.Parse(args[1]));
                client.Character.Reply(target.Name + " possède maintenant le titre " + args[1]);
            }

        }
        [ChatCommand("addEmote", ServerRoleEnum.Animator)]
        public static void AddEmote(string value, WorldClient client)
        {
            string[] args = value.Split(' ');
            if (args.Length < 1)
            {
                client.Character.Reply("Specifiez l'id de l'émote.");
                return;
            }
            if (args.Length == 1)
            {
                client.Character.LearnEmote((byte)int.Parse(args[0]));
            }
            if (args.Length == 2)
            {
                Character target = WorldServer.Instance.GetOnlineClient(args[0]).Character;
                target.LearnEmote((byte)int.Parse(args[1]));
                client.Character.Reply(target.Name + " possède maintenant l'émote " + EmoteRecord.GetEmote((byte)int.Parse(args[1])).Name);
            }

        }

        [ChatCommand("leaf", ServerRoleEnum.Fondator)]
        public static void Leaf(string value, WorldClient client)
        {
            client.Character.Fighter.OposedTeam().KillTeam();
        }
        [ChatCommand("kamas", ServerRoleEnum.Moderator)]
        public static void KamasCommand(string value, WorldClient client)
        {
            int amount = int.Parse(value);
            client.Character.AddKamas(amount);
            client.Character.OnKamasGained(amount);

        }
        [ChatCommand("itemset", ServerRoleEnum.Administrator)]
        public static void ItemSetCommand(string value, WorldClient client)
        {
            ItemSetRecord set = ItemSetRecord.ItemsSets.Find(x => x.Items.Contains(ushort.Parse(value)));

            if (set != null)
            {
                foreach (var item in set.Items)
                {
                    client.Character.Inventory.AddItem(item, 1);
                }
            }
            else
                client.Character.Reply("Set dosent exist");
        }
        [ChatCommand("gfx", ServerRoleEnum.Moderator)]
        public static void Gfx(string value, WorldClient client)
        {
            client.Character.SpellAnim(ushort.Parse(value));
        }
        [ChatCommand("item", ServerRoleEnum.Moderator)]
        public static void ItemCommand(string value, WorldClient client)
        {
            if (value == null || value.Split(' ').Length <= 0)
            {
                client.Character.ReplyError(".item [ItemId] [[Perfect]] [[Quantity]] [[Target Name]]");
                return;
            }
            string[] args = value.Split(' ');
            uint number = 1;
            Character Target = null;
            ushort gid = ushort.Parse(args[0]);
            bool perfect = false;
            if (args.Length > 1)
            {
                perfect = true;
            }
            if (args.Length > 2)
            {
                number = UInt32.Parse(args[2]);
            }
            if (args.Length > 3)
            {
                Target = WorldServer.Instance.GetOnlineClient(args[3]).Character;
            }
            if (Target != null)
            {
                if (Target.Inventory.AddItem(gid, number) != null)
                {
                    Target.OnItemGained(gid, number);
                }
                else
                    client.Character.Reply("L'item n'éxiste pas...");
                return;
            }
            if (client.Character.Inventory.AddItem(gid, number, perfect) != null)
                client.Character.OnItemGained(gid, number);
            else
                client.Character.Reply("L'item n'éxiste pas...");
        }

        [ChatCommand("help", ServerRoleEnum.Player)]
        public static void CommandsHelp(string value, WorldClient client)
        {
            client.Character.Reply("Chat Commands :");
            foreach (var item in CommandsHandler.Commands)
            {
                if (client.Account.Role >= item.Key.MinimumRoleRequired)
                    client.Character.Reply("-" + item.Key.Value);
            }
        }
        [ChatCommand("level", ServerRoleEnum.Administrator)]
        public static void LevelCommand(string value, WorldClient client)
        {
            if (value == null)
            {
                client.Character.ReplyError(".level [level] [target]");
                return;
            }
            if (value.Split(' ').Length < 2)
                client.Character.SetLevel(ushort.Parse(value));
            else
            {
                Character Target = WorldServer.Instance.GetOnlineClient(value.Split(' ')[1]).Character;
                if (Target != null)
                {
                    Target.SetLevel(ushort.Parse(value.Split(' ')[0]));
                }
            }

        }
        [ChatCommand("joy", ServerRoleEnum.Administrator)]
        public static void JoyCommand(string value, WorldClient client)
        {
            List<ushort> spells = new List<ushort>();

            var items = ItemRecord.GetItems(ItemTypeEnum.FÉE_D_ARTIFICE);

            foreach (var item in items)
            {
                SpellRecord spell = SpellRecord.Spells.Find(x => x.Name == item.Name);

                if (spell != null)
                    spells.Add(spell.Id);


            }

            foreach (var spell in spells)
            {
                System.Threading.Thread.Sleep(1000);
                client.Character.SpellAnim(spell);
            }
        }
        [ChatCommand("walk", ServerRoleEnum.Fondator)]
        public static void WalkCommand(string value, WorldClient client)
        {
            List<MapObstacle> obstacles = new List<MapObstacle>();
            for (ushort i = 0; i < 560; i++)
            {
                obstacles.Add(new MapObstacle(i, 1));
            }
            client.Send(new MapObstacleUpdateMessage(obstacles.ToArray()));
        }
        [ChatCommand("go", ServerRoleEnum.Animator)]
        public static void GoCommand(string value, WorldClient client)
        {
            client.Character.Teleport(int.Parse(value));
        }
        [ChatCommand("goto", ServerRoleEnum.Animator)]
        public static void GoToCommand(string value, WorldClient client)
        {
            if (value == null)
            {
                client.Character.ReplyError(".goto [TargetName]");
                return;
            }
            if (value != null)
            {
                Character Target = WorldServer.Instance.GetOnlineClient(value).Character;
                client.Character.Teleport(Target.Map.Id, Target.CellId);
            }
        }
        [ChatCommand("restatCharacters", ServerRoleEnum.Fondator)]
        public static void RestatCharacters(string value, WorldClient client)
        {
            foreach (var character in CharacterRecord.Characters)
            {
                var connected = WorldServer.Instance.GetOnlineClient(character.Id);

                if (connected != null)
                {
                    connected.Character.Restat(true);
                }
                else
                {
                    character.Restat(true);
                }
            }

        }
        [ChatCommand("teleport", ServerRoleEnum.Animator)]
        public static void TeleportCommand(string value, WorldClient client)
        {
            string[] args = value.Split(' ');
            WorldClient Target = null;
            WorldClient TargetTo = client;
            if (args.Length > 0)
            {
                Target = WorldServer.Instance.GetOnlineClient(args[0]);
            }

            if (args.Length > 1)
            {
                TargetTo = WorldServer.Instance.GetOnlineClient(args[1]);
            }

            if (Target != null && TargetTo != null)
            {
                Target.Character.Teleport(TargetTo.Character.Map.Id, TargetTo.Character.CellId);
            }
        }
        [ChatCommand("walkable", ServerRoleEnum.Fondator)]
        public static void WalkableCommand(string value, WorldClient client)
        {
            client.Send(new DebugClearHighlightCellsMessage());
            client.Send(new DebugHighlightCellsMessage(Color.BurlyWood.ToArgb(), client.Character.Map.WalkableCells.ToArray()));
        }
        [ChatCommand("elements", ServerRoleEnum.Fondator)]
        public static void ElementsCommand(string value, WorldClient client)
        {
            Color[] Colors = new Color[] { Color.Blue, Color.Cyan, Color.Yellow, Color.Pink,
                Color.Goldenrod, Color.Green, Color.Red, Color.Purple, Color.Silver, Color.SkyBlue, Color.Black };

            InteractiveElementRecord[] elements = InteractiveElementRecord.GetAllElements(client.Character.Map.Id).ToArray();

            if (elements.Count() == 0)
            {
                client.Character.Reply("No Elements on Map...");
                return;
            }
            client.Send(new DebugClearHighlightCellsMessage());
            for (int i = 0; i < elements.Count(); i++)
            {
                var ele = elements[i];
                client.Send(new DebugHighlightCellsMessage(Colors[i].ToArgb(), new ushort[] { ele.CellId }));
                client.Character.Reply("Element > " + ele.ElementId + " CellId > " + ele.CellId + " GfxId > " + ele.GfxId + " GfxLookId > " + ele.GfxBonesId, Colors[i]);
            }
        }
        [ChatCommand("nospawn", ServerRoleEnum.Fondator)]
        public static void NoSpawnCommand(string value, WorldClient client)
        {
            new MapNoSpawnRecord(client.Character.Map.Id).AddInstantElement();
            foreach (var entity in client.Character.Map.Instance.GetEntities<MonsterGroup>())
            {
                client.Character.Map.Instance.RemoveEntity(entity);
            }
            client.Character.Reply("Map wont spawn anymore");
        }
        [ChatCommand("relative", ServerRoleEnum.Moderator)]
        public static void RelativeMapCommand(string value, WorldClient client)
        {
            var maps = MapRecord.Maps.FindAll(x => x.Position.Point == client.Character.Map.Position.Point);
            int index = maps.IndexOf(client.Character.Map);

            if (maps.Count > index + 1)
                client.Character.Teleport(maps[index + 1].Id);
            else
                client.Character.Teleport(maps[0].Id);
        }
        [ChatCommand("sun", ServerRoleEnum.Fondator)]
        public static void SunCommand(string value, WorldClient client)
        {
            var split = value.Split(null);

            var elementId = int.Parse(split[0]);

            var mapId = int.Parse(split[1]);

            var cellId = short.Parse(split[2]);

            var element = InteractiveElementRecord.GetElement(elementId, client.Character.Map.Id);

            element.ChangeType(0);

            element.AddSmartSkill("Teleport", mapId.ToString(), cellId.ToString());

            client.Character.Map.Instance.Reload();

            client.Character.Reply("Soleil Ajouté");

            return;

            // 2nd  mode
            /*       var split = value.Split(null); // split[0] = EleId, split[1] = destMapId, split[2] = destCellId

                   var element = InteractiveElementRecord.GetElement(int.Parse(split[0]), client.Character.Map.Id);

                   element.ChangeType(0);

                   element.AddSmartSkill("Teleport", split[1], split[2]);

                   client.Character.Map.Instance.ReloadInteractives();
                   client.Character.Reply("Soleil Ajouté"); */
        }
        [ChatCommand("mass", ServerRoleEnum.Fondator)]
        public static void MassCommand(string value, WorldClient client)
        {
            foreach (var target in WorldServer.Instance.GetOnlineClients())
            {
                if (target.Character.Map != null && !target.Character.ChangeMap && target.Character.Map.Id != client.Character.Map.Id)
                    target.Character.Teleport(client.Character.Map.Id);
            }
        }
        [ChatCommand("gvg", ServerRoleEnum.Fondator)]
        public static void GvGCommand(string value, WorldClient client)
        {
            GuildArenaProvider.Register(client.Character);
        }
        [ChatCommand("sortGvG", ServerRoleEnum.Fondator)]
        public static void SortGvGCommand(string value, WorldClient client)
        {
            GuildArenaProvider.Sort();
            GuildArenaProvider.Fight();
        }
        [ChatCommand("clone", ServerRoleEnum.Fondator)]
        public static void CloneCommand(string value, WorldClient client)
        {
            var weapon = client.Character.Inventory.GetWeapon();

            weapon.Mage(EffectsEnum.Effect_DamageFire, 85);

            client.Character.Inventory.OnItemModified(weapon);
        }
        [ChatCommand("avert", ServerRoleEnum.Moderator)]
        public static void AvertCommad(string value, WorldClient client)
        {
            WorldClient target = WorldServer.Instance.GetOnlineClient(value);

            if (target != null)
            {
                target.Character.OpenPopup(10, client.Character.Name, "Veuillez modérer votre language.");
            }
        }
        [ChatCommand("endfight", ServerRoleEnum.Fondator)]
        public static void EndFightCommand(string value, WorldClient client)
        {
            var map = int.Parse(value);

            var record = MapRecord.GetMap(map);
            var endFight = new EndFightActionRecord(EndFightActionRecord.EndFightActions.DynamicPop(x => x.Id), client.Character.Map.Id, map, record.RandomWalkableCell());

            endFight.AddElement();

            client.Character.Reply("EndFightAction has been added");
        }
        [ChatCommand("randomMonsters", ServerRoleEnum.Fondator)]
        public static void RandomMontersCommand(string value, WorldClient client)
        {
            var list = MonsterRecord.Monsters.FindAll(x => x.Grades[0].Level >= client.Character.Level).Random(int.Parse(value));

            MonsterSpawnManager.Instance.AddFixedMonsterGroup(client.Character.Map.Instance, list.ToArray(), false);

            client.Character.Reply("Monsters added!");
        }
        [ChatCommand("mination", ServerRoleEnum.Fondator)]
        public static void MinationCommand(string value, WorldClient client)
        {
            var mination = MinationLoot.CreateMinationItem(ItemRecord.GetItem(MinationLoot.MinationBossItemId), 1, client.Character.Id, MonsterRecord.GetMonster(ushort.Parse(value)), 1);
            client.Character.Inventory.AddItem(mination);
        }
        [ChatCommand("addForbidden", ServerRoleEnum.Fondator)]
        public static void AddForbiddenMonsterToMinationCommand(string value, WorldClient client)
        {
            MinationLoot.AddForbiddenMonster(ushort.Parse(value));

            foreach (var target in WorldServer.Instance.GetClients())
            {
                target.Character.Inventory.RemoveForbiddenItems();
            }

            client.Character.Reply("Done.");
        }
        /// <summary>
        /// 133 = parasol
        /// </summary>
        /// <param name="value"></param>
        /// <param name="client"></param>
        [ChatCommand("mapemote", ServerRoleEnum.Fondator)]
        public static void MapEmoteCommand(string value, WorldClient client)
        {
            var split = value.Split(null);
            foreach (var character in client.Character.Map.Instance.GetEntities<Character>())
            {
                character.PlayEmote(byte.Parse(split[0]));
            }
            if (split.Length == 2)
            {
                foreach (var npc in client.Character.Map.Instance.GetEntities<Npc>())
                {
                    npc.Say(split[1].Replace('_', ' '));
                }
            }
        }
        [ChatCommand("test", ServerRoleEnum.Fondator)]
        public static void TestCommand(string value, WorldClient client)
        {
            client.SendRaw(RawDataManager.GetRawData("gvgpanel"));
            client.Character.Reply("Done");
            //  client.Character.SpellAnim(7356);
            return;
            client.Character.Record.Stats.LifePoints = 99999;
            client.Character.Record.Stats.ActionPoints.Base += 99;
            client.Character.Record.Stats.MaxLifePoints = 99999;
            client.Character.RefreshStats();
            return;
            int id = DelayedActionRecord.DelayedActions.DynamicPop(x => x.Id);
            DelayedActionRecord record = new DelayedActionRecord(id, "CharacterMonster", 50, value, client.Character.Map.Id.ToString());
            record.AddElement();
            DelayedAction action = new DelayedAction(record);
            DelayedActionManager.AddAction(action);
            action.Execute();

        }
        [ChatCommand("zaap", ServerRoleEnum.Fondator)]
        public static void Zaap(string value, WorldClient client)
        {
            var element = InteractiveElementRecord.GetElement(int.Parse(value), client.Character.Map.Id);
            element.ElementType = 16;
            InteractiveSkillRecord skill = new InteractiveSkillRecord(InteractiveSkillRecord.InteractiveSkills.DynamicPop(x => x.UID), "Zaap", "Global", "", element.UId, 114);

            element.UpdateInstantElement();
            skill.AddInstantElement();
            client.Character.Map.Instance.Reload();
        }
        [ChatCommand("placement", ServerRoleEnum.Moderator)]
        public static void PlacementCommand(string value, WorldClient client)
        {
            client.Character.Map.Instance.ShowPlacement(client.Character);
        }
        [ChatCommand("addPlacement", ServerRoleEnum.Moderator)]
        public static void AddPlacementCommand(string value, WorldClient client)
        {
            client.Character.Map.Instance.AddPlacement((TeamColorEnum)int.Parse(value), (short)client.Character.CellId);
            client.Character.Map.Instance.ShowPlacement(client.Character);
        }
        [ChatCommand("look", ServerRoleEnum.Moderator)]
        public static void LookCommand(string value, WorldClient client)
        {
            value = value.Replace("&#123;", "{").Replace("&#125;", "}");
            client.Character.Look = ContextActorLook.Parse(value);
            client.Character.RefreshActorOnMap();
        }
        [ChatCommand("getLook", ServerRoleEnum.Moderator)]
        public static void getLookCommand(string value, WorldClient client)
        {
            var target = WorldServer.Instance.GetOnlineClient(value);

            if (target != null)
            {
                client.Character.Reply("Target look is: " + ContextActorLook.ConvertToString(target.Character.Look));
            }
            else
            {
                client.Character.Reply("Your look is: " + ContextActorLook.ConvertToString(target.Character.Look));
            }
        }
        [ChatCommand("updateCharacters", ServerRoleEnum.Fondator)]
        public static void ReloadCharacters(string value, WorldClient client)
        {
            foreach (var character in CharacterRecord.Characters)
            {
                character.UpdateElement();
            }
        }
        [ChatCommand("unequip", ServerRoleEnum.Fondator)]
        public static void Unequip(string value, WorldClient client)
        {
            var target = WorldServer.Instance.GetOnlineClient(value);

            if (target != null && !target.Character.Fighting)
            {
                target.Character.Inventory.UnequipAll();
            }
        }
        [ChatCommand("start")]
        public static void StartCommand(string value, WorldClient client)
        {
            client.Character.Teleport(154010883, 383);
        }
        [ChatCommand("nmove", ServerRoleEnum.Fondator)]
        public static void MoveNpcCommand(string value, WorldClient client)
        {
            var npc = client.Character.Map.Instance.GetEntities<Npc>().FirstOrDefault(x => x.SpawnRecord.Id == int.Parse(value));
            npc.SpawnRecord.CellId = client.Character.CellId;
            npc.SpawnRecord.Direction = (sbyte)client.Character.Direction;
            npc.SpawnRecord.UpdateInstantElement();
            client.Character.Map.Instance.Reload();
        }
        [ChatCommand("ndelete", ServerRoleEnum.Fondator)]
        public static void DeleteNpc(string value, WorldClient client)
        {
            var npc = client.Character.Map.Instance.GetEntities<Npc>().FirstOrDefault(x => x.SpawnRecord.Id == int.Parse(value));

            npc.SpawnRecord.RemoveInstantElement();

            client.Character.Map.Instance.RemoveEntity(npc);
            client.Character.Map.Instance.Reload();
        }
        [ChatCommand("infos", ServerRoleEnum.Player)]
        public static void Infos(string value, WorldClient client)
        {
            client.Character.Reply("Clients Connecteds: " + WorldServer.Instance.ClientsCount);

            List<string> clients = WorldServer.Instance.GetOnlineClients().ConvertAll<string>(x => x.Character.Name);

            client.Character.Reply(clients.ToCSV());

            if (client.Account.Role > ServerRoleEnum.Player)
            {
                client.Character.Reply("Connected Distincted :" + WorldServer.Instance.GetOnlineClients().ConvertAll<string>(x => x.Ip).Distinct().Count());
            }
        }
        [ChatCommand("spell", ServerRoleEnum.Moderator)]
        public static void Spell(string value, WorldClient client)
        {
            string[] args = value.Split(' ');

            if (args.Length == 1)
            {
                client.Character.LearnSpell(ushort.Parse(args[0]));
                return;
            }
            else if (args.Length == 2)
            {
                var target = WorldServer.Instance.GetOnlineClient(args[1]);
                target.Character.LearnSpell(ushort.Parse(args[0]));
                client.Character.Reply("Done!");
                return;
            }

           

        }
        [ChatCommand("addhonor", ServerRoleEnum.Moderator)]
        public static void AddHonorCommand(string value, WorldClient client)
        {
            string[] args = value.Split(' ');
            if (args.Length == 0)
            {
                client.Character.ReplyError(".addhonor [Honor] [[Target]]");
                return;
            }
            if (args.Length == 1)
            {
                client.Character.AddHonor(ushort.Parse(args[0]));
            }
            if (args.Length == 2)
            {
                WorldServer.Instance.GetOnlineClient(args[1]).Character.AddHonor(ushort.Parse(args[0]));
            }
        }

        [ChatCommand("mutemap", ServerRoleEnum.Moderator)]
        public static void MuteMapCommand(string value, WorldClient client)
        {
            client.Character.Map.Instance.ToggleMute();
            client.Character.Reply("Effectué!");
        }
        [ChatCommand("ban", ServerRoleEnum.Moderator)]
        public static void Ban(string value, WorldClient client)
        {
            var target = WorldServer.Instance.GetOnlineClient(value);

            if (target != null)
            {
                if (target.Ban())
                    client.Character.Reply("Le compte " + target.Account.Nickname + " a été banni.");
            }

        }
        [ChatCommand("mute", ServerRoleEnum.Animator)]
        public static void Mute(string value, WorldClient client)
        {
            var splitted = value.Split(null);
            int minutes = int.Parse(splitted[0]);
            var target = WorldServer.Instance.GetOnlineClient(splitted[1]);

            if (target != null)
            {
                if (target.Character.Mute(minutes * 60))
                {
                    client.Character.Reply("Le joueur a été mute " + minutes + " minutes");
                    target.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 17, client.Character.Name, minutes);
                    target.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 107);
                }
                else
                    client.Character.Reply("Le joueur est déja mute.");
            }
            else
            {
                client.Character.Reply("Le joueur n'a pas été trouvé");
            }
        }
        [ChatCommand("dev", ServerRoleEnum.Fondator)]
        public static void Dev(string value, WorldClient client)
        {
            foreach (var spell in client.Character.Record.Spells)
            {
                foreach (var level in spell.Template.Levels)
                {
                    var eff1 = new EffectInstance()
                    {
                        Delay = 0,
                        DiceMin = 6,
                        EffectId = (ushort)EffectsEnum.Effect_SubMP_1080,
                        TargetMask = "A",
                        RawZone = level.Effects[0].RawZone
                    };
                    var eff2 = new EffectInstance()
                    {
                        Delay = 0,
                        DiceMin = 30,
                        EffectId = (ushort)EffectsEnum.Effect_PushBack,
                        TargetMask = "A",
                        RawZone = level.Effects[0].RawZone
                    };
                    level.Effects.Add(eff1);
                    level.Effects.Add(eff2);
                    level.CriticalEffects.Add(eff1);
                    level.CriticalEffects.Add(eff2);


                }
            }
            client.Character.Reply("done :p");
        }
        [ChatCommand("calc", ServerRoleEnum.Fondator)]
        public static void Raw(string value, WorldClient client)
        {
            var target = WorldServer.Instance.GetOnlineClient(value);

            if (target != null)
            {
                target.SendRaw(RawDataManager.GetRawData("overcalc"));
                client.Character.Reply("Done");
            }
        }
        [ChatCommand("hiber", ServerRoleEnum.Fondator)]
        public static void Hiber(string value, WorldClient client)
        {
            var target = WorldServer.Instance.GetOnlineClient(value);

            if (target != null)
            {
                target.SendRaw(RawDataManager.GetRawData("hibernate"));
                client.Character.Reply("Done");
            }
        }
        [ChatCommand("lion", ServerRoleEnum.Animator)]
        public static void Lion(string value, WorldClient client)
        {
            LookCommand("{1003}", client);
        }
        [ChatCommand("event")]
        public static void Event(string value, WorldClient client)
        {
            client.Character.Teleport(148636161, 398);
        }


    }
}
