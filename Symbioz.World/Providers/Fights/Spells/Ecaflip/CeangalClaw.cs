using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;

namespace Symbioz.World.Providers.Fights.Spells.Ecaflip
{
    [CustomSpellHandler(112)]
    public class CeangalClaw : CustomSpellHandler
    {
        public CeangalClaw(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit) : base(source, level, castPoint, criticalHit)
        {
        }

        public override void Execute()
        {
            var effects = this.GetEffects();
         //   DefaultHandler(e[0]);

        }
    }
}
