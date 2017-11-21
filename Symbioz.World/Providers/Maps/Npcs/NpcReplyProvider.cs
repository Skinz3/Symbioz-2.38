using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using Symbioz.World.Records.Npcs;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using Symbioz.Protocol.Messages;
using Symbioz.World.Providers.Fights;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;

namespace Symbioz.World.Providers.Maps.Npcs
{
    class NpcReplyProvider
    {
        public static Dictionary<NpcReplyAttribute, MethodInfo> Handlers = typeof(NpcReplyProvider).MethodsWhereAttributes<NpcReplyAttribute>();

        public static void Handle(Character character, NpcReplyRecord reply)
        {
            var handler = Handlers.FirstOrDefault(x => x.Key.Identifier.ToLower() == reply.ActionType.ToLower());

            if (handler.Value != null)
            {
                handler.Value.Invoke(null, new object[] { character, reply });
            }
            else
            {
                character.ReplyError("No ReplyHandler linked to this actionType. (" + reply.ActionType + ")");
            }
        }

        [NpcReply("Bank")]
        public static void Bank(Character character, NpcReplyRecord reply)
        {
            character.OpenBank();
        }
        [NpcReply("Teleport")]
        public static void Teleport(Character character, NpcReplyRecord reply)
        {
            character.Teleport(int.Parse(reply.Value1), ushort.Parse(reply.Value2));
        }
        [NpcReply("Cinematic")]
        public static void Cinematic(Character character, NpcReplyRecord reply)
        {
            character.PlayCinematic(ushort.Parse(reply.Value1));
        }
        [NpcReply("RemoveItem")]
        public static void RemoveItem(Character character, NpcReplyRecord reply)
        {
            CharacterItemRecord item = character.Inventory.GetFirstItem(ushort.Parse(reply.Value1), uint.Parse(reply.Value2));

            if (item != null)
            {
                uint quantity = uint.Parse(reply.Value2);
                character.Inventory.RemoveItem(item, quantity);
                character.OnItemLost(item.GId, quantity);
            }
        }
        [NpcReply("AddKamas")]
        public static void AddKamas(Character character, NpcReplyRecord reply)
        {
            int amount = int.Parse(reply.Value1);
            character.AddKamas(amount);
            character.OnKamasGained(amount);
        }
        [NpcReply("MonsterFight")]
        public static void MonsterFight(Character character, NpcReplyRecord reply)
        {
            List<MonsterRecord> templates = new List<MonsterRecord>();

            foreach (var monsterId in reply.Value1.FromCSV<ushort>())
            {
                templates.Add(MonsterRecord.GetMonster(monsterId));
            }

            List<MonsterRecord> allyTemplates = new List<MonsterRecord>();

            foreach (var monsterId in reply.Value2.FromCSV<ushort>())
            {
                allyTemplates.Add(MonsterRecord.GetMonster(monsterId));
            }

            FightInstancePvM fight = FightProvider.Instance.CreateFightInstancePvM(templates.ToArray(), character.Map);

            fight.RedTeam.AddFighter(character.CreateFighter(fight.RedTeam));

            var random = new AsyncRandom();

            foreach (var ally in allyTemplates)
            {
                fight.RedTeam.AddFighter(new MonsterFighter(fight.RedTeam, ally, ally.RandomGrade(random), character.CellId));
            }
            foreach (var fighter in fight.Group.CreateFighters(fight.BlueTeam))
            {
                fight.BlueTeam.AddFighter(fighter);
            }

            fight.StartPlacement();
        }
        [NpcReply("AddOrnament")]
        public static void AddOrnamenet(Character character, NpcReplyRecord reply)
        {
            character.LearnOrnament(ushort.Parse(reply.Value1), true);
        }
        [NpcReply("AddItem")]
        public static void AddItem(Character character, NpcReplyRecord reply)
        {
            ushort gid = ushort.Parse(reply.Value1);
            uint quantity = uint.Parse(reply.Value2);
            character.Inventory.AddItem(gid, quantity);
            character.OnItemGained(gid, quantity);

        }
        [NpcReply("AddItems")]
        public static void AddItems(Character character, NpcReplyRecord reply)
        {
            List<CharacterItemRecord> added = new List<CharacterItemRecord>();

            foreach (var itemId in reply.Value1.FromCSV<ushort>())
            {
                added.Add(ItemRecord.GetItem(itemId).GetCharacterItem(character.Id, 1, false));
            }
            character.Inventory.AddItems(added);
            foreach (var item in added)
            {
                character.OnItemGained(item.GId, 1);
            }

        }
        [NpcReply("Reach")]
        public static void ReachObjective(Character character, NpcReplyRecord reply)
        {
            character.ReachObjective(short.Parse(reply.Value1));
        }
        [NpcReply("SendRaw")]
        public static void SendRaw(Character character,NpcReplyRecord reply)
        {
            character.Client.SendRaw(RawDataManager.GetRawData(reply.Value1));
        }
    }
}
