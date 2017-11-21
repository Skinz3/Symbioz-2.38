using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Spells;
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
    [SpellEffectHandler(EffectsEnum.Effect_1008)]
    public class SpawnBomb : SpellEffectHandler
    {
        public SpawnBomb(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            var target = Fight.GetFighter(CastPoint);

            if (target != null)
            {
                SpellBombRecord record = SpellBombRecord.GetSpellBombRecord(SpellLevel.SpellId);
                var level = SpellRecord.GetSpellRecord(record.CibleExplosionSpellId).GetLevel(SpellLevel.Grade);
                Source.ForceSpellCast(level, CastPoint.CellId);
            }
            else
            {
                MonsterRecord record = MonsterRecord.GetMonster(Effect.DiceMin);
                BombFighter fighter = new BombFighter(record, Source, Source.Team, CastPoint.CellId, SpellLevel.Grade, SpellLevel);
                Fight.AddBomb(fighter, Source);
            }
            return true;
        }
    }
}
