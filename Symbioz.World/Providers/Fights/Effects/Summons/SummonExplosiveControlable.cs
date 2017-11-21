using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Summons
{
    [SpellEffectHandler(EffectsEnum.Effect_SummonExpControlable)]
    public class SummonExplosiveControlable : SpellEffectHandler
    {
        public SummonExplosiveControlable(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            MonsterRecord template = MonsterRecord.GetMonster(Effect.DiceMin);

            if (template != null && Source is CharacterFighter)
            {
                ExplosiveControlableMonster fighter = new ExplosiveControlableMonster(Source.Team, template, SpellLevel.Grade,
                    Source as CharacterFighter, CastPoint.CellId);
                Fight.AddSummon(fighter, (CharacterFighter)Source);
            }
            return true;
        }
    }
}
