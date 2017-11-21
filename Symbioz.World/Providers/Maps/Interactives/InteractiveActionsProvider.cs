using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Symbioz.Core;
using System.Text;
using Symbioz.Protocol.Enums;
using System.Threading.Tasks;
using Symbioz.World.Network;
using Symbioz.World.Records;
using Symbioz.Protocol.Messages;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models.Dialogs;
using Symbioz.World.Records.Interactives;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Maps;

namespace Symbioz.World.Providers.Maps.Interactives
{
    class InteractiveActionsProvider
    {
        public static Dictionary<InteractiveActionAttribute, MethodInfo> Handlers = typeof(InteractiveActionsProvider).MethodsWhereAttributes<InteractiveActionAttribute>();

        public static void Handle(Character character, MapInteractiveElementSkill skill)
        {
            var handler = Handlers.FirstOrDefault(x => x.Key.Identifier.ToLower() == skill.ActionType.ToLower());

            if (handler.Value != null)
            {
                character.SendMap(new InteractiveUsedMessage((ulong)character.Id, (uint)skill.Element.Id,
                    skill.Id, skill.Template.Duration, skill.Template.CanMove));
                handler.Value.Invoke(null, new object[] { character, skill });
            }
            else
            {
                character.ReplyError("No Skill Handler linked to this actionType (" + skill.ActionType + ")");
            }
        }

        [InteractiveAction("Teleport")]
        public static void Teleport(Character character, MapInteractiveElementSkill skill)
        {
            character.Teleport(int.Parse(skill.Record.Value1), ushort.Parse(skill.Record.Value2));
        }
        [InteractiveAction("Zaap")]
        public static void Zaap(Character character, MapInteractiveElementSkill skill)
        {
            character.OpenZaap(skill);
        }
        [InteractiveAction("Zaapi")]
        public static void Zaapi(Character character, MapInteractiveElementSkill skill)
        {
            character.OpenZaapi(skill);
        }
        [InteractiveAction("Paddock")]
        public static void Paddock(Character character, MapInteractiveElementSkill skill)
        {
            character.OpenPaddock();
        }
        [InteractiveAction("Book")]
        public static void Book(Character character, MapInteractiveElementSkill skill)
        {
            character.ReadDocument(ushort.Parse(skill.Record.Value1));
        }
        [InteractiveAction("Chest")]
        public static void Chest(Character character, MapInteractiveElementSkill skill)
        {

        }
        [InteractiveAction("Emote")]
        public static void Emote(Character character, MapInteractiveElementSkill skill)
        {
            character.LearnEmote(byte.Parse(skill.Record.Value1));
        }
        [InteractiveAction("Title")]
        public static void Title(Character character, MapInteractiveElementSkill skill)
        {
            character.LearnTitle(ushort.Parse(skill.Record.Value1));
        }
        [InteractiveAction("Ornament")]
        public static void Ornamenet(Character character, MapInteractiveElementSkill skill)
        {
            character.LearnOrnament(ushort.Parse(skill.Record.Value1), true);
        }
        [InteractiveAction("RawData")]
        public static void RawData(Character character, MapInteractiveElementSkill skill)
        {
            byte[] rawData = RawDataManager.GetRawData(skill.Record.Value1);
            character.Client.SendRaw(rawData);
        }
        [InteractiveAction("Collect")]
        public static void Collect(Character character, MapInteractiveElementSkill skill)
        {
            if (skill.Element is MapStatedElement)
            {
                ((MapStatedElement)skill.Element).UseStated(character);
            }
        }
        [InteractiveAction("Craft")]
        public static void Craft(Character character, MapInteractiveElementSkill skill)
        {
            character.OpenCraftPanel(skill.Record.SkillId, skill.Template.ParentJobIdEnum);
        }
        [InteractiveAction("SmithMagic")]
        public static void SmithMagic(Character character, MapInteractiveElementSkill skill)
        {
            character.OpenSmithMagicPanel(skill.Id, skill.Template.ParentJobIdEnum);
        }
    }
}
