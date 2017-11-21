using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class StatBuff : Buff
    {
        public Characteristic BoostedStat
        {
            get;
            set;
        }
        public StatBuff(Fighter source, Fighter target, short delta,
            EffectInstance effect, ushort spellId)
            : base(source, target, delta, effect, spellId)
        {
            this.Init();
        }
        private void Init()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_AddRange:
                    this.BoostedStat = Target.Stats.Range;
                    break;
                case EffectsEnum.Effect_AddRange_136:
                    this.BoostedStat = Target.Stats.Range;
                    break;
                case EffectsEnum.Effect_IncreaseDamage_138:
                    this.BoostedStat = Target.Stats.DamagesBonusPercent;
                    break;
                case EffectsEnum.Effect_AddAgility:
                    this.BoostedStat = Target.Stats.Agility;
                    break;
                case EffectsEnum.Effect_AddChance:
                    this.BoostedStat = Target.Stats.Chance;
                    break;
                case EffectsEnum.Effect_AddIntelligence:
                    this.BoostedStat = Target.Stats.Intelligence;
                    break;
                case EffectsEnum.Effect_AddStrength:
                    this.BoostedStat = Target.Stats.Strength;
                    break;
                case EffectsEnum.Effect_AddDamageBonus:
                    this.BoostedStat = Target.Stats.AllDamagesBonus;
                    break;
                case EffectsEnum.Effect_AddSummonLimit:
                    this.BoostedStat = Target.Stats.SummonableCreaturesBoost;
                    break;
                case EffectsEnum.Effect_IncreaseDamage_1054:
                    this.BoostedStat = Target.Stats.DamagesBonusPercent;
                    break;
                case EffectsEnum.Effect_AddCriticalHit:
                    this.BoostedStat = Target.Stats.CriticalHit;
                    break;
                case EffectsEnum.Effect_RunesBoostPercent:
                    this.BoostedStat = Target.Stats.RuneBonusPercent;
                    break;
                case EffectsEnum.Effect_AddDamageBonus_121:
                    this.BoostedStat = Target.Stats.AllDamagesBonus;
                    break;
                case EffectsEnum.Effect_AddWisdom:
                    this.BoostedStat = Target.Stats.Wisdom;
                    break;
                case EffectsEnum.Effect_AddAP_111:
                    this.BoostedStat = Target.Stats.ActionPoints;
                    break;
                case EffectsEnum.Effect_SubAP:
                    this.BoostedStat = Target.Stats.ActionPoints;
                    break;
                case EffectsEnum.Effect_AddMP:
                    this.BoostedStat = Target.Stats.MovementPoints;
                    break;
                case EffectsEnum.Effect_AddMP_128:
                    this.BoostedStat = Target.Stats.MovementPoints;
                    break;
                default:
                    throw new Exception(Effect.EffectEnum + " is not considered as statBuff.");
            }
        }
        public override void Apply()
        {
            this.BoostedStat.Context += Delta;
            base.Apply();
        }
        public override void Dispell()
        {
            this.BoostedStat.Context -= Delta;
        }

    }
}
