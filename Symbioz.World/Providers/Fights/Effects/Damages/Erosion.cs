using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_AddErosion)]
    public class Erosion : SpellEffectHandler
    {
        public Erosion(Fighter source, SpellLevelRecord level, EffectInstance effect,
         Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, level, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (var target in targets)
            {
                target.AddAndApplyBuff(new ErosionBuff(target.BuffIdProvider.Pop(), target, Source, SpellLevel, Effect, SpellId,
                    (short)Effect.DiceMin, Critical, FightDispellableEnum.DISPELLABLE));
            }
            return true;
        }
    }
}
