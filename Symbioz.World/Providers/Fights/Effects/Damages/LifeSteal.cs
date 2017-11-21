using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_StealHPFire)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPWater)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPEarth)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPAir)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPNeutral)]
    [SpellEffectHandler(EffectsEnum.Effect_StealHPFix)]
    public class LifeSteal : SpellEffectHandler
    {
        public EffectElementType ElementType
        {
            get;
            set;
        }

        public LifeSteal(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
             Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
            switch (effect.EffectEnum)
            {
                case EffectsEnum.Effect_StealHPEarth:
                    ElementType = EffectElementType.Earth;
                    break;
                case EffectsEnum.Effect_StealHPWater:
                    ElementType = EffectElementType.Water;
                    break;
                case EffectsEnum.Effect_StealHPFire:
                    ElementType = EffectElementType.Fire;
                    break;
                case EffectsEnum.Effect_StealHPAir:
                    ElementType = EffectElementType.Air;
                    break;
                case EffectsEnum.Effect_StealHPNeutral:
                    ElementType = EffectElementType.Neutral;
                    break;
                case EffectsEnum.Effect_StealHPFix:
                    ElementType = EffectElementType.Neutral; // todo, neutral but no jet
                    break;
            }
        }
        public override bool Apply(Fighter[] targets)
        {
            if (ElementType != EffectElementType.Neutral)
            {
                Jet jet = FormulasProvider.Instance.EvaluateJet(Source, ElementType, Effect, SpellLevel.SpellId);

                foreach (var target in targets)
                {
                    short num = (short)(target.InflictDamages(new Damage(Source, target, jet.Clone(), ElementType, Effect, Critical)) / 2);
                    Source.Heal(Source, num);
                }
                return true;
            }
            else
            {
                foreach (var target in targets)
                {
                    short num = (short)(target.InflictDamages((short)Effect.DiceMin, Source) / 2);
                    Source.Heal(Source, num);
                }
                return true;
            }
        }
    }
}
