using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Others
{
    [SpellEffectHandler(EffectsEnum.Effect_Carry)]
    public class Carry : SpellEffectHandler
    {


        public Carry(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
        Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            if (targets.Length > 0)
            {
                if (!Source.CarryFighter)
                {
                    Fighter target = targets.FirstOrDefault();
                    target.Carry(Source, this);
                }
            }
            return true;
        }
    }
}
