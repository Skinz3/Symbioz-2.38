using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Core;
using System.Threading.Tasks;
using Symbioz.World.Providers.Items;
using Symbioz.Protocol.Types;
using YAXLib;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.Protocol.Enums;

namespace Symbioz.World.Models.Effects
{
    public class EffectInstance
    {
        [YAXDontSerialize]
        public char ShapeType
        {
            get
            {
                return RawZone[0];
            }
        }
        [YAXDontSerialize]
        public byte Radius
        {
            get
            {
                return byte.Parse(RawZone[1].ToString());
            }
        }
        [YAXDontSerialize]
        public EffectsEnum EffectEnum
        {
            get
            {
                return (EffectsEnum)EffectId;
            }
        }
        public string TargetMask
        {
            get;
            set;
        }

        public uint EffectUID
        {
            get;
            set;
        }

        public string RawZone
        {
            get;
            set;
        }

        public ushort EffectId
        {
            get;
            set;
        }

        public ushort DiceMin
        {
            get;
            set;
        }

        public int Duration
        {
            get;
            set;
        }

        public ushort DiceMax
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

        public short Delay
        {
            get;
            set;
        }

        public sbyte Random
        {
            get;
            set;
        }

        public string Triggers
        {
            get;
            set;
        }
        public TriggerType TriggerTypes
        {
            get
            {
                return ParseTriggers();
            }
        }
        public short EffectElement
        {
            get;
            set;
        }
        public Zone GetZone(DirectionsEnum orientation)
        {
            return new Zone(ShapeType, Radius, orientation);
        }
        public Zone GetZone()
        {
            return new Zone(ShapeType, Radius);
        }
        public Effect GenerateEffect(bool perfect = false)
        {
            if (EffectGenerationProvider.IsHandled(EffectId))
            {
                var effect = EffectGenerationProvider.Handle(this);
                if (effect != null)
                    return effect;
                else
                    return null;
            }

            if (DiceMin > 0 && DiceMax == 0)
                return new EffectInteger(EffectId, DiceMin);
            if (Value > 0)
                return new EffectInteger(EffectId, (ushort)Value);

            if (DiceMin != 0 && DiceMin < DiceMax)
            {
                if (!perfect)
                {
                    ushort value = (ushort)new AsyncRandom().Next(DiceMin, DiceMax + 1);
                    return new EffectInteger(EffectId, value);
                }
                else
                {
                    return new EffectInteger(EffectId, DiceMax);
                }

            }
            return null;
        }
        public ushort RandomizeMinMax()
        {
            return (ushort)(DiceMin < DiceMax ? new AsyncRandom().Next(DiceMin, DiceMax + 1) : DiceMin);
        }
        public ObjectEffect GetTemplateObjectEffect()
        {
            return new ObjectEffectDice(EffectId, DiceMin, DiceMax, (ushort)Value);
        }
        public EffectInstance Clone()
        {
            return new EffectInstance()
            {
                Delay = this.Delay,
                DiceMax = this.DiceMax,
                DiceMin = this.DiceMin,
                TargetMask = this.TargetMask,
                EffectId = this.EffectId,
                Duration = this.Duration,
                EffectElement = this.EffectElement,
                EffectUID = this.EffectUID,
                Random = this.Random,
                RawZone = this.RawZone,
                Value = this.Value,
                Triggers = this.Triggers,
            };
        }
        public override string ToString()
        {
            return EffectEnum.ToString();
        }
        /// <summary>
        /// Spell Context
        /// </summary>
        /// <returns></returns>
        public static EffectInstance New(EffectsEnum effect, short delay, ushort min, ushort max, int value, short duration, string rawZone, string targetMask)
        {
            return new EffectInstance()
            {
                Delay = delay,
                DiceMin = min,
                DiceMax = max,
                Value = value,
                Duration = duration,
                EffectElement = 0,
                EffectId = (ushort)effect,
                EffectUID = 0,
                Random = 0,
                RawZone = rawZone,
                TargetMask = targetMask,
            };
        }


        private TriggerType ParseTriggers()
        {
            if (Triggers == "X")
            {
                return TriggerType.AFTER_DEATH;
            }
            else if (Triggers == "D")
            {
                return TriggerType.BEFORE_ATTACKED;
            }
            else if (Triggers == "I")
            {
                return TriggerType.ON_CAST;
            }
            else if (Triggers == "TE")
            {
                return TriggerType.TURN_END;
            }
            else if (Triggers == "H")
            {
                return TriggerType.ON_HEAL;
            }
            else
            {
                return TriggerType.UNKNOWN;
            }
        }
    }
}
