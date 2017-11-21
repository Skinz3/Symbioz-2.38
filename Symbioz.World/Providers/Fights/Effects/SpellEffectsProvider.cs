using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Symbioz.World.Network;
using Symbioz.Protocol.Selfmade.Enums;

namespace Symbioz.World.Providers.Fights.Effects
{
    class SpellEffectsProvider
    {
        private static Dictionary<SpellEffectHandlerAttribute[], Type> Handlers = new Dictionary<SpellEffectHandlerAttribute[], Type>();

        [StartupInvoke(StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var type in Program.WorldAssembly.GetTypes())
            {
                SpellEffectHandlerAttribute[] handlers = type.GetCustomAttributes<SpellEffectHandlerAttribute>().ToArray();

                if (handlers.Length > 0)
                {
                    Handlers.Add(handlers, type);
                }
            }
        }
        public static void Handle(Fighter source, SpellLevelRecord level, EffectInstance effect, Fighter[] targets, MapPoint castPoint, bool critical)
        {
            var handlerDatas = Handlers.FirstOrDefault(x => x.Key.FirstOrDefault(w => w.Effect == effect.EffectEnum) != null);

            if (handlerDatas.Value != null)
            {
                SpellEffectHandler handler = (SpellEffectHandler)Activator.CreateInstance(handlerDatas.Value, new object[] { source, level, effect, targets, castPoint, critical });
                handler.Execute();
            }
            else
            {

                var client = WorldServer.Instance.GetClients().FirstOrDefault();

                if (client != null && client.Account.Role == ServerRoleEnum.Fondator)
                {
                    client.Character.Reply("Effect " + effect.EffectEnum + " is not handled.");
                }
            }
        }
    }
}
