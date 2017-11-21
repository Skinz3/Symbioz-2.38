using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Modules
{
    public class SpellsEffectsFixer
    {
        static Logger logger = new Logger();

        [StartupInvoke("SpellEffectsFixer", StartupInvokePriority.Modules)]
        public static void Fix()
        {
            foreach (var spellLevel in SpellLevelRecord.SpellsLevels)
            {
                if (FixEffects(spellLevel.Effects) || FixEffects(spellLevel.CriticalEffects))
                {
                    spellLevel.UpdateInstantElement();
                    logger.White("Some spell fixed! (" + spellLevel.SpellId + ")");
                }
            }

            //Dictionary<EffectsEnum, string> effects = new Dictionary<EffectsEnum, string>();

            //foreach (var spell in SpellLevelRecord.SpellsLevels)
            //{
            //    foreach (var effect in spell.Effects)
            //    {
            //        if (effect.Triggers != "I" && !effects.ContainsKey(effect.EffectEnum))
            //        {
            //            effects.Add(effect.EffectEnum, effect.Triggers + " " + SpellRecord.GetSpellRecord(spell.SpellId).Name);
            //        }
            //    }
            //}
        }
        private static bool FixEffects(List<EffectInstance> effects)
        {
            if (effects.Count(x => x.EffectEnum == EffectsEnum.Effect_ChangeAppearance_335) > 1)
            {
                var effect = effects.Find(x => x.EffectEnum == EffectsEnum.Effect_ChangeAppearance_335);

                if (effect != null)
                {
                    effects.RemoveAll(x => x.EffectEnum == EffectsEnum.Effect_ChangeAppearance_335);
                    effects.Add(effect);


                    return true;
                }
            }
            return false;
        }
    }
}
