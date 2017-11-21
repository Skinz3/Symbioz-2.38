using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Providers.Fights.Effects.Others
{
    [SpellEffectHandler(EffectsEnum.Effect_MakeControlable)]
    public class MakeControlable : SpellEffectHandler
    {
        public MakeControlable(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect, Fighter[] targets, MapPoint castPoint, bool critical) : base(source, spellLevel, effect, targets, castPoint, critical)
        {
        }

        public override bool Apply(Fighter[] targets)
        {
            if (!(Source is CharacterFighter))
            {
                return false;
            }
            foreach (var target in targets.OfType<SummonedFighter>())
            {
                ControlableMonsterFighter summoned = MakeControlableBuff.MakeSummonControlable((CharacterFighter)Source, target);
                MakeControlableBuff buff = new MakeControlableBuff(summoned.BuffIdProvider.Pop(), summoned, Source, SpellLevel, Effect, SpellId, Critical, FightDispellableEnum.REALLY_NOT_DISPELLABLE);
                summoned.AddAndApplyBuff(buff);
            }

            return true;
        }

   

       
    }
}
