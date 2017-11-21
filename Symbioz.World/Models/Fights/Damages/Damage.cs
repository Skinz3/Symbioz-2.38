using Symbioz.Core;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Stats;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Damages
{
    public class Damage
    {
        public Fighter Source
        {
            get;
            private set;
        }
        public Fighter Target
        {
            get;
            private set;
        }
        public short Delta
        {
            get
            {
                return Jet.Delta;
            }
            set
            {
                Jet.Delta = value;
            }
        }
        public EffectElementType ElementType
        {
            get;
            private set;
        }
        public EffectInstance Effect
        {
            get;
            private set;
        }
        private AsyncRandom Randomizer
        {
            get;
            set;
        }
        private Jet Jet
        {
            get;
            set;
        }
        private bool Critical
        {
            get;
            set;
        }
        public Damage(Fighter source, Fighter target, Jet jet, EffectElementType elementType, EffectInstance effect, bool critical)
        {
            this.Source = source;
            this.Target = target;
            this.Jet = jet;
            this.Effect = effect;
            this.ElementType = elementType;
            this.Randomizer = new AsyncRandom();
            this.Critical = critical;
        }
        public Damage(Fighter source, Fighter target, short delta, EffectElementType elementType = EffectElementType.Direct)
        {
            this.Source = source;
            this.Target = target;
            this.Jet = new Jet(delta, delta, delta);
            this.Effect = null;
            this.ElementType = elementType;
            this.Randomizer = new AsyncRandom();
            this.Critical = false;
        }
        public void CalculateDamageReduction(ushort erosionDelta)
        {
            short reduction = Target.Stats.GlobalDamageReduction;

            if (reduction > 0)
            {
                if (Delta - reduction < erosionDelta)
                {
                    reduction = (short)(Delta - erosionDelta);
                }
                if (reduction > 0)
                {
                    Delta -= reduction;
                    Target.OnDamageReduced(Target.Stats.GlobalDamageReduction);
                }
            }
        }
        public void CalculateDamageResistance()
        {
            bool pvp = Source.Fight.PvP;
            int resistPercent = 0;
            int elementReduction = 0;

            switch (ElementType)
            {
                case EffectElementType.Earth:
                    resistPercent = GetResistanceValue(Target.Stats.EarthResistPercent, Target.Stats.PvPEarthResistPercent);
                    elementReduction = GetResistanceValue(Target.Stats.EarthReduction, Target.Stats.PvPEarthReduction);
                    break;
                case EffectElementType.Air:
                    resistPercent = GetResistanceValue(Target.Stats.AirResistPercent, Target.Stats.PvPAirResistPercent);
                    elementReduction = GetResistanceValue(Target.Stats.AirReduction, Target.Stats.PvPAirReduction);
                    break;
                case EffectElementType.Water:
                    resistPercent = GetResistanceValue(Target.Stats.WaterResistPercent, Target.Stats.PvPWaterResistPercent);
                    elementReduction = GetResistanceValue(Target.Stats.WaterReduction, Target.Stats.PvPWaterReduction);
                    break;
                case EffectElementType.Fire:
                    resistPercent = GetResistanceValue(Target.Stats.FireResistPercent, Target.Stats.PvPFireResistPercent);
                    elementReduction = GetResistanceValue(Target.Stats.FireReduction, Target.Stats.PvPFireReduction);
                    break;
                case EffectElementType.Neutral:
                    resistPercent = GetResistanceValue(Target.Stats.NeutralResistPercent, Target.Stats.PvPNeutralResistPercent);
                    elementReduction = GetResistanceValue(Target.Stats.NeutralReduction, Target.Stats.PvPNeutralReduction);
                    break;
                case EffectElementType.Direct:
                    break;
            }

            if (Critical) // Avant ou apres le calcul des resistances? on tente avant dans le doute.
            {
                Delta -= Target.Stats.CriticalDamageReduction.TotalInContext();
                Delta += Source.Stats.CriticalDamageBonus.TotalInContext();
            }

            this.Jet.DeltaMin = GetDeltaWithResists(resistPercent, elementReduction, Jet.DeltaMin);
            this.Jet.DeltaMax = GetDeltaWithResists(resistPercent, elementReduction, Jet.DeltaMax);
            this.Delta = GetDeltaWithResists(resistPercent, elementReduction, Delta);

            this.Jet.DeltaMin = (short)(this.Jet.DeltaMin < 0 ? 0 : Jet.DeltaMin);
            this.Jet.DeltaMax = (short)(this.Jet.DeltaMax < 0 ? 0 : Jet.DeltaMax);
            this.Jet.Delta = (short)(this.Jet.Delta < 0 ? 0 : Jet.Delta);

        }
        private short GetDeltaWithResists(int resistPercent, int reduction, short delta)
        {
            return (short)((1.0 - (double)resistPercent / 100.0) * ((double)delta - (double)reduction));
        }
        private int GetResistanceValue(Characteristic resistCharacteristic, Characteristic resistPvPCharacteristic)
        {
            return resistCharacteristic.TotalInContext() + (Source.Fight.PvP ? resistPvPCharacteristic.TotalInContext() : 0);
        }

        public void ApplyFinalBoost()
        {
            this.Delta = (short)((double)this.Delta * Source.Stats.FinalDamageCoefficient);
        }
    }
}
