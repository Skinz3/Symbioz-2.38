using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Effects.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Summons
{
    [SpellEffectHandler(EffectsEnum.Effect_Illusions)]
    public class Illusions : SpellEffectHandler
    {
        public Illusions(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
        }
        public override bool Apply(Fighter[] targets)
        {

            var inital = Source.Point;
            InvisibilityBuff buff = new InvisibilityBuff(Source.BuffIdProvider.Pop(), Source, Source, SpellLevel, Effect, SpellId, Critical, FightDispellableEnum.REALLY_NOT_DISPELLABLE);
            Source.AddAndApplyBuff(buff);
            Source.Teleport(Source, CastPoint);

            return true;

        }
    }
}