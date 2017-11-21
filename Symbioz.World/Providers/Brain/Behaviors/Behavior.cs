using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Providers.Brain.Actions;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    public abstract class Behavior
    {
        public const ushort InvunlerabilitySpellId = 5199;

        protected BrainFighter Fighter
        {
            get;
            set;
        }
        public Behavior(BrainFighter fighter)
        {
            this.Fighter = fighter;
        }

        public static SpellLevelRecord CreateBasicSpellLevel(short apCost, List<EffectInstance> effects, ushort spellId)
        {
            return new SpellLevelRecord(0, spellId, 1, apCost, 0, 10, true, true, true, 50, false, false, false, false, 10, 10, 10,
                0, 0, 0, new List<short>(), new List<short>(), effects, effects);
        }

        public void MakeInvulnerable(Fighter target, short duration)
        {
            EffectInstance effect = new EffectInstance()
            {
                Delay = 0,
                DiceMax = 0,
                DiceMin = 0,
                Duration = duration,
                EffectElement = 0,
                EffectId = (ushort)EffectsEnum.Effect_AddState,
                EffectUID = 0,
                Random = 0,
                RawZone = "P1",
                TargetMask = "A#a",
                Triggers = "I",
                Value = 56,
            };

            var level = CreateBasicSpellLevel(0, new List<EffectInstance>() { effect }, InvunlerabilitySpellId);

            bool sequence = Fighter.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);

            target.ForceSpellCast(level, target.CellId);

            if (sequence)
                Fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }

        public virtual Dictionary<int, SpellCategoryEnum> GetSpellsCategories()
        {
            return null;
        }

        public void MakeVulnerable(Fighter target)
        { 
            bool sequence = Fighter.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);
            Fighter.DispellSpellBuffs(target, InvunlerabilitySpellId);
            if (sequence)
                Fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);
        }

        public SummonedFighter Summon(Fighter source, ushort monsterId, short cellId)
        {
            SummonedFighter summon = CreateSummoned(source, MonsterRecord.GetMonster(monsterId), cellId);

            bool sequence = Fighter.Fight.SequencesManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL);

            Fighter.Fight.AddSummon(summon);

            if (sequence)
                Fighter.Fight.SequencesManager.EndSequence(SequenceTypeEnum.SEQUENCE_SPELL);

            return summon;
        }

        private SummonedFighter CreateSummoned(Fighter master, MonsterRecord template, short cellId)
        {
            return new SummonedFighter(template, (sbyte)new AsyncRandom().Next(1, 5), master, master.Team, cellId);
        }

        public virtual short? GetTargetCellForSpell(ushort spellId)
        {
            return null;
        }

        public virtual short GetAgressiveCell()
        {
            return -1;
        }
        public virtual short GetBuffCell()
        {
            return -1;
        }
        public virtual short GetTeleportCell()
        {
            return -1;
        }
        public virtual ActionType[] GetSortedActions()
        {
            return null;
        }
    }
}