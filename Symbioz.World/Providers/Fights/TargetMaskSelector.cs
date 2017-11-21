using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Core;
using Symbioz.World.Models.Maps;

namespace Symbioz.World.Providers.Fights
{
    public class TargetMaskSelector
    {
        private static Dictionary<TargetMaskAttribute, MethodInfo> Handlers = typeof(TargetMaskSelector).MethodsWhereAttributes<TargetMaskAttribute>();

        public const char TARGET_MASK_SPLITTER = '#';

        public const char CASTER_PREDICATE = '*';

        public static List<Fighter> Select(Fighter source, List<Fighter> fighters, string targetMasks)
        {
            List<Predicate<Fighter>> predicates = new List<Predicate<Fighter>>();

            foreach (var mask in targetMasks.Split(TARGET_MASK_SPLITTER))
            {

                if (mask.StartsWith(CASTER_PREDICATE.ToString()))
                {
                    var identifier = mask.Remove(0, 1);

                    var handler = Handlers.FirstOrDefault(x => identifier.StartsWith(x.Key.Identifier));

                    if (handler.Value != null)
                    {
                        string value = identifier.Remove(0, handler.Key.Identifier.Length);

                        var predicate = (Predicate<Fighter>)handler.Value.Invoke(null, new object[] { source, new List<Fighter>() { source }, value });

                        if (!predicate(source))
                        {
                            fighters.Clear();
                        }
                    }
                }
                else
                {
                    var handler = Handlers.FirstOrDefault(x => mask.StartsWith(x.Key.Identifier));


                    if (handler.Value != null)
                    {
                        string value = mask.Remove(0, handler.Key.Identifier.Length);

                        var predicate = (Predicate<Fighter>)handler.Value.Invoke(null, new object[] { source, fighters, value });
                        predicates.Add(predicate);
                    }
                }
            }

            fighters.RemoveAll(x => !Extensions.VerifyPredicates(predicates, x));

            return fighters;


        }
        [TargetMask("f")]
        public static Predicate<Fighter> NotMonsterEnemis(Fighter source, List<Fighter> fighters, string value)
        {
            return new Predicate<Fighter>(x => (x is IMonster && ((IMonster)x).MonsterId != ushort.Parse(value)) || !(x is IMonster));
        }
        [TargetMask("E")]
        public static Predicate<Fighter> WithStateEnemies(Fighter source, List<Fighter> fighters, string value)
        {
            return new Predicate<Fighter>(x => x.HasState(short.Parse(value)));
        }
        [TargetMask("e")]
        public static Predicate<Fighter> WithStateAlly(Fighter source, List<Fighter> fighters, string value)
        {
            return new Predicate<Fighter>(x => !x.HasState(short.Parse(value)));
        }
        [TargetMask("K")]
        public static Predicate<Fighter> GetCarried(Fighter source, List<Fighter> fighters, string value)
        {
            return x => x.Carrying == source;
        }
        [TargetMask("U")]
        public static Predicate<Fighter> GetInstantSummoned(Fighter source, List<Fighter> fighters, string value)
        {
            return x => x == source.GetLastSummon();
        }
        private static char GetIdentifier(string targetMaks)
        {
            return targetMaks[0];
        }
        private static string GetValue(string targetMask)
        {
            return targetMask.Remove(0, 1);
        }
        public static List<Fighter> Custom(Fighter fighter, string targetMasks, List<Fighter> targets, MapPoint castPoint)
        {

            if (targetMasks.Contains('F')) // a faire en propre
            {
                bool valid = false;

                foreach (var target in targets.ToArray())
                {
                    foreach (var targetMask in Array.FindAll(targetMasks.Split('#'), x => x[0] == 'F'))
                    {
                        if (target is IMonster && ((IMonster)target).MonsterId == ushort.Parse(GetValue(targetMask)))
                            valid = true;
                    }

                    if (!valid)
                    {
                        targets.Remove(target);
                    }
                }


            }


            if (targetMasks.Contains('K'))
                targets.Add(fighter.Carried);

            if (targetMasks.Contains('C'))
                targets.Add(fighter);

            if (targetMasks.Contains('c'))
            {
                var target = fighter.Fight.GetFighter(castPoint);

                if (target != null)
                    targets = new List<Fighter>() { target };
            }

            return targets;
        }
    }
}
