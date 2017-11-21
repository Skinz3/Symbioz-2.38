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

namespace Symbioz.World.Providers.Fights.Spells.Huppermage
{
    [CustomSpellHandler(5847)]
    public class Traverse : CustomSpellHandler
    {
        public const char ZONE_SHAPE = 'L';

        public const byte ZONE_RADIUS = 4;

        public Traverse(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit) : base(source, level, castPoint, criticalHit)
        {

        }

        public override void Execute()
        {
            var zone = new Zone(ZONE_SHAPE, ZONE_RADIUS, CastPoint.OrientationTo(Source.Point, false));
            
            foreach (var effect in GetEffects())
            {
                var targets = SpellEffectsManager.Instance.GetAffectedFighters(Source, zone, CastPoint, effect.TargetMask);
                Handler(effect, CastPoint, targets);
            }
        }
    }
}
