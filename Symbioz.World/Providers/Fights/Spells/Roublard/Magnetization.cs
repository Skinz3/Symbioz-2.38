using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using Symbioz.World.Models.Fights.Spells;
using Symbioz.World.Models.Effects;

namespace Symbioz.World.Providers.Fights.Spells.Roublard
{
    [CustomSpellHandler(2801)] // Aimantation 
    public class Magnetization : CustomSpellHandler
    {
        public Magnetization(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit) : base(source, level, castPoint, criticalHit)
        {
        }

        public override void Execute()
        {
            EffectInstance[] effects = GetEffects();
            HandlePullAlliesEnemies(effects);
            HandlePullBombs(effects);
        }
        private void HandlePullAlliesEnemies(EffectInstance[] effects)
        {
            var zone = effects[0].GetZone(Source.Point.OrientationTo(CastPoint));
            var targets = SpellEffectsManager.Instance.GetAffectedFighters(Source, zone, CastPoint, effects[0].TargetMask).ToList();
            targets.Remove(Source);
            targets.Remove(Source.Fight.GetFighter(CastPoint));
            Handler(effects[0], CastPoint, targets.ToArray());
        }
        private void HandlePullBombs(EffectInstance[] effects)
        {
            var zone = effects[1].GetZone(Source.Point.OrientationTo(CastPoint));
            var targets = SpellEffectsManager.Instance.GetAffectedFighters(Source, zone, CastPoint, effects[1].TargetMask).ToList();
            targets.Remove(Source.Fight.GetFighter(CastPoint));
            Handler(effects[1], CastPoint, targets.ToArray());
        }
    }
}
