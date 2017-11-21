using Symbioz.Protocol.Enums;
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

namespace Symbioz.World.Providers.Fights.Effects.Summons
{
    [SpellEffectHandler(EffectsEnum.Effect_KillReplacePerInvocation)]
    public class ReplacePerInvocation : SpellEffectHandler
    {
        public ReplacePerInvocation(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {
        }
        public override bool Apply(Fighter[] targets)
        {
            if (targets.Count() > 0)
            {
                short[] cells = new short[targets.Length];

                for (int i = 0; i < targets.Length; i++)
                {
                    targets[i].Stats.CurrentLifePoints = 0;
                    cells[i] = targets[i].CellId;
                }


                Source.Fight.CheckDeads();


                foreach (var cell in cells)
                {
                    SummonedFighter fighter = CreateSummon();
                    Fight.AddSummon(fighter);
                    return true;
                }


            }
            return true;
        }
        private SummonedFighter CreateSummon()
        {
            Symbioz.World.Records.Monsters.MonsterRecord template = Symbioz.World.Records.Monsters.MonsterRecord.GetMonster(Effect.DiceMin);
            sbyte gradeId = (sbyte)(template.GradeExist(SpellLevel.Grade) ? SpellLevel.Grade : template.LastGrade().Id);
            return new SummonedFighter(template, gradeId, Source, Source.Team, CastPoint.CellId);
        }
    }
}
