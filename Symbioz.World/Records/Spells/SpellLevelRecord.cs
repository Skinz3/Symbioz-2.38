using Symbioz.ORM;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Providers.Fights.Effects.Movements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Spells
{
    [Table("SpellsLevels", true, 4)]
    public class SpellLevelRecord : ITable
    {
        public static List<SpellLevelRecord> SpellsLevels = new List<SpellLevelRecord>();

        [Primary]
        public int Id;

        public ushort SpellId;

        public sbyte Grade;

        public short ApCost;

        public short MinRange;

        public short MaxRange;

        public bool CastInLine;

        public bool CastInDiagonal;

        public bool CastTestLos;

        public short CriticalHitProbability;

        public bool NeedFreeCell;

        public bool NeedTakenCell;

        public bool NeedFreeTrapCell;

        public bool RangeCanBeBoosted;

        public short MaxStacks;

        public short MaxCastPerTurn;

        public short MaxCastPerTarget;

        public short MinCastInterval;

        public short InitialCooldown;

        public short GlobalCooldown;

        public List<short> StatesRequired = new List<short>();

        public List<short> StatesForbidden = new List<short>();

        [Xml, Update]
        public List<EffectInstance> Effects;

        [Xml, Update]
        public List<EffectInstance> CriticalEffects;

        [Ignore]
        public bool Silent
        {
            get
            {
                return !Effects.Any(x => Invisibility.NotSilentEffects.Contains(x.EffectEnum) && x.Delay == 0 && x.Duration == 0);
            }
        }

        public SpellLevelRecord(int id, ushort spellId, sbyte grade, short apCost, short minRange,
          short maxRange, bool castInLine, bool castInDiagonal, bool castTestLos, short criticalHitProbability,
          bool needFreeCell, bool needTakenCell, bool needFreeTrapCell, bool rangeCanBeBoosted, short maxStacks,
          short maxCastPerTurn, short maxCastPerTarget, short minCastInterval, short initialCooldown, short globalCooldown,
          List<short> statesRequired, List<short> statesForbidden, List<EffectInstance> effects, List<EffectInstance> criticalEffects)
        {
            this.Id = id;
            this.SpellId = spellId;
            this.Grade = grade;
            this.ApCost = apCost;
            this.MinRange = minRange;
            this.MaxRange = maxRange;
            this.CastInLine = castInLine;
            this.CastInDiagonal = castInDiagonal;
            this.CastTestLos = castTestLos;
            this.CriticalHitProbability = criticalHitProbability;
            this.NeedFreeCell = needFreeCell;
            this.NeedTakenCell = needTakenCell;
            this.NeedFreeTrapCell = needFreeTrapCell;
            this.RangeCanBeBoosted = rangeCanBeBoosted;
            this.MaxStacks = maxStacks;
            this.MaxCastPerTurn = maxCastPerTurn;
            this.MaxCastPerTarget = maxCastPerTarget;
            this.MinCastInterval = minCastInterval;
            this.InitialCooldown = initialCooldown;
            this.GlobalCooldown = globalCooldown;
            this.StatesRequired = statesRequired;
            this.StatesForbidden = statesForbidden;
            this.Effects = effects;
            this.CriticalEffects = criticalEffects;
        }

        public static ushort GetSpellId(int spellLevelId)
        {
            return SpellsLevels.FirstOrDefault(x => x.Id == spellLevelId).SpellId;
        }
        public static SpellLevelRecord GetSpellLevel(int id)
        {
            return SpellsLevels.Find(x => x.Id == id);
        }
        public static SpellLevelRecord GetSpellLevel(ushort spellId, sbyte grade)
        {
            return SpellsLevels.Find(x => x.SpellId == spellId && x.Grade == grade);
        }
        public static List<SpellLevelRecord> GetSpellLevels(ushort spellId)
        {
            return SpellsLevels.FindAll(x => x.SpellId == spellId);
        }
    
    }
}
