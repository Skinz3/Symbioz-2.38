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

namespace Symbioz.World.Providers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_AddResistances)]
    public class Resistances : SpellEffectHandler
    {
        public Resistances(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            foreach (Fighter target in targets)
            {
                if (this.Effect.Duration > 0)
                {
                    ResistancesBuff buff = new ResistancesBuff(target.BuffIdProvider.Pop(), target, Source, SpellLevel, Effect, SpellId, Critical, FightDispellableEnum.DISPELLABLE, (short)Effect.DiceMin);
                    target.AddAndApplyBuff(buff);
                }
            }
            return true;
        }
    }
}
