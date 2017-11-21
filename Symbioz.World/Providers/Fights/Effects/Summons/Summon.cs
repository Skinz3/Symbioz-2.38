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
    [SpellEffectHandler(EffectsEnum.Effect_Summon)]
    public class Summon : SpellEffectHandler
    {
        public Summon(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
          
        }
        public override bool Apply(Fighter[] targets)
        {
            var template = MonsterRecord.GetMonster(Effect.DiceMin);
            SummonFighter(template, (sbyte)(template.GradeExist(SpellLevel.Grade) ? SpellLevel.Grade : template.LastGrade().Id),
            Source, CastPoint);
            return true;
        }

        public static SummonedFighter SummonFighter(MonsterRecord template, sbyte gradeId, Fighter source, MapPoint castPoint)
        {
            SummonedFighter fighter = new SummonedFighter(template, gradeId, source, source.Team, castPoint.CellId);
            source.Fight.AddSummon(fighter);
            return fighter;
        }
    }
}
