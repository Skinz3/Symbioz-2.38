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

namespace Symbioz.World.Providers.Fights.Effects.Others
{
    [SpellEffectHandler(EffectsEnum.Effect_ChangeAppearance_335), SpellEffectHandler(EffectsEnum.Effect_ChangeAppearance)]
    public class Look : SpellEffectHandler
    {
        public Look(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
         Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            if (FightLookProvider.Exist(SpellId))
            {
                foreach (var target in targets)
                {
                    base.AddLookBuff(target, FightDispellableEnum.REALLY_NOT_DISPELLABLE);
                }
            }
            return true;
        }
    }
}
