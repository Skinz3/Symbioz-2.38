using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Providers.Fights;
using Symbioz.World.Providers.Fights.Effects;
using Symbioz.World.Records.Spells;
using Symbioz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Records.Items;
using Symbioz.Protocol.Selfmade.Enums;
using static Symbioz.World.Models.Fights.Fighters.Fighter;

namespace Symbioz.World.Models.Fights.Spells
{
    public class SpellEffectsManager : Singleton<SpellEffectsManager>
    {
        public void HandleEffects(Fighter fighter, SpellLevelRecord level, MapPoint castPoint, bool criticalHit, bool applyEffectTrigger = true)
        {
            EffectInstance[] effects = SelectEffects(fighter, criticalHit ? level.CriticalEffects : level.Effects, level, castPoint);

            foreach (var effect in effects)
            {
                Zone zone = effect.GetZone(fighter.Point.OrientationTo(castPoint));

                Fighter[] targets = GetAffectedFighters(fighter, zone, castPoint, effect.TargetMask).Distinct().ToArray();
                var cells = zone.GetCells(castPoint.CellId, fighter.Fight.Map);
                HandleEffect(fighter, level, castPoint, criticalHit, effect, targets);

            }
        }
        public void HandleEffects(Fighter fighter, EffectInstance[] effects, SpellLevelRecord level, MapPoint castPoint,
            string rawZone, string targetMask, bool criticalHit, bool applyEffectTrigger = true)
        {
            EffectInstance[] selectedEffects = SelectEffects(fighter, effects.ToList(), level, castPoint);

            foreach (var effect in selectedEffects)
            {
                Zone zone = new Zone(rawZone[0], byte.Parse(rawZone[1].ToString()), fighter.Point.OrientationTo(castPoint));

                Fighter[] targets = GetAffectedFighters(fighter, zone, castPoint, targetMask).Distinct().ToArray();

                HandleEffect(fighter, level, castPoint, criticalHit, effect, targets);
            }
        }
        public void HandleEffects(Fighter fighter, EffectInstance[] effects, SpellLevelRecord level, MapPoint castPoint,
            string rawZone, bool criticalHit, bool applyEffectTrigger = true)
        {
            EffectInstance[] selectedEffects = SelectEffects(fighter, effects.ToList(), level, castPoint);

            foreach (var effect in selectedEffects)
            {
                Zone zone = new Zone(rawZone[0], byte.Parse(rawZone[1].ToString()), fighter.Point.OrientationTo(castPoint));

                Fighter[] targets = GetAffectedFighters(fighter, zone, castPoint, effect.TargetMask).Distinct().ToArray();

                HandleEffect(fighter, level, castPoint, criticalHit, effect, targets);
            }
        }
        public void HandleEffects(Fighter fighter, EffectInstance[] effects, SpellLevelRecord level, MapPoint castPoint,
          bool criticalHit, bool applyEffectTrigger = true)
        {
            EffectInstance[] selectedEffects = SelectEffects(fighter, effects.ToList(), level, castPoint);

            foreach (var effect in selectedEffects)
            {
                Zone zone = effect.GetZone(fighter.Point.OrientationTo(castPoint));

                Fighter[] targets = GetAffectedFighters(fighter, zone, castPoint, effect.TargetMask).Distinct().ToArray();

                HandleEffect(fighter, level, castPoint, criticalHit, effect, targets);
            }
        }
        public void HandleEffect(Fighter fighter, SpellLevelRecord level, MapPoint castPoint, bool criticalHit, EffectInstance effect, Fighter[] targets)
        {
            if (effect.TriggerTypes != TriggerType.AFTER_DEATH && effect.TriggerTypes != TriggerType.TURN_END) // The only trigger handled for the moment (complicated to handle with Symbioz fight architecture)
            {
                SpellEffectsProvider.Handle(fighter, level, effect, targets, castPoint, criticalHit);
            }
        }
      
       
        private EffectInstance[] SelectEffects(Fighter source, List<EffectInstance> effects, SpellLevelRecord level, MapPoint castPoint)
        {
            List<EffectInstance> results = new List<EffectInstance>();

            results.AddRange(effects.FindAll(x => x.Random == 0));

            List<EffectInstance> randomEffects = effects.FindAll(x => x.Random != 0);

            if (randomEffects.Count > 0)
                results.Add(randomEffects.Random());

            return results.ToArray();
        }
        public Fighter[] GetAffectedFighters(Fighter fighter, Zone zone, MapPoint castPoint, string targetMask)
        {
            short[] cells = zone.GetCells(castPoint.CellId, fighter.Fight.Map);

            List<Fighter> targets = new List<Fighter>();
            List<Fighter> filtreds = new List<Fighter>();

            foreach (var mask in targetMask.Split(TargetMaskSelector.TARGET_MASK_SPLITTER))
            {
                filtreds.AddRange(TargetMaskProvider.Handle(fighter, mask));
            }

            foreach (var cell in cells)
            {
                Fighter target = fighter.Fight.GetFighter(cell);

                if (target != null && filtreds.Contains(target))
                    targets.Add(target);
            }

            return TargetMaskSelector.Custom(fighter, targetMask, TargetMaskSelector.Select(fighter, targets, targetMask), castPoint).ToArray();
        }
    }
}
