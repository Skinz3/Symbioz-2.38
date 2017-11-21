using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Models.Fights.Spells;
using Symbioz.World.Models.Effects;
using Symbioz.Protocol.Selfmade.Enums;

namespace Symbioz.World.Providers.Fights.Spells.Special
{
    /// <summary>
    /// 6798	Déchaînement de Stasis
    /// </summary>
    [CustomSpellHandler(6798)]
    public class Statis : CustomSpellHandler
    {
        public EffectInstance[] CUSTOM_EFFECTS = new EffectInstance[]
        {
            new EffectInstance()
            {
                Delay = 0,
                DiceMin = 800,
                DiceMax = 0,
                Duration = 0,
                EffectElement =0,
                EffectId = (ushort)EffectsEnum.Effect_StealHPFix,
                Value = 0,
                Triggers = "I",
                EffectUID = 0,
                Random = 0,
                RawZone = "X8",
                TargetMask ="g#A"

            },
             new EffectInstance()
            {
                Delay = 0,
                DiceMin = 2,
                DiceMax = 0,
                Duration = 0,
                EffectElement =0,
                EffectId = (ushort)EffectsEnum.Effect_PullForward,
                Value = 0,
                Triggers = "I",
                EffectUID = 0,
                Random = 0,
                RawZone = "X8",
                TargetMask ="g#A"

            }
        };
        public Statis(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit) : base(source, level, castPoint, criticalHit)
        {

        }

        public override void Execute()
        {
            foreach (var effect in CUSTOM_EFFECTS)
            {
                Zone zone = effect.GetZone(CastPoint.OrientationTo(Source.Point, false));
                var targets = SpellEffectsManager.Instance.GetAffectedFighters(Source, zone, CastPoint, effect.TargetMask);
                Handler(effect, CastPoint, targets);
            }
        }
    }
}
