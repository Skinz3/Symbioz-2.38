using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Effects
{
    public class EffectMinationLevel : Effect
    {
        public const ushort DummyTextEffectId = 990;

        public const string EffectMessage = "Niveau {0} ({1}%)";

        public ushort Level
        {
            get;
            set;
        }
        public ushort Percentage
        {
            get;
            set;
        }
        public ulong Exp
        {
            get;
            set;
        }
        [YAXLib.YAXDontSerialize]
        public ulong LowerBoundExperience
        {
            get
            {
                return Records.ExperienceRecord.GetExperienceForLevel(Level).Player;
            }
        }
        [YAXLib.YAXDontSerialize]
        public ulong UpperBoundExperience
        {
            get
            {
                return Records.ExperienceRecord.GetExperienceForNextLevel(Level).Player;
            }
        }
        public EffectMinationLevel()
        {

        }
        public EffectMinationLevel(ushort level, ushort percentage, ulong exp)
            : base(DummyTextEffectId)
        {
            this.Level = level;
            this.Percentage = percentage;
            this.Exp = exp;
        }
        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectString(EffectId, string.Format(EffectMessage, Level, Percentage));
        }
        public override bool Equals(object obj)
        {
            return obj is EffectMination ? this.Equals(obj as EffectMination) : false;
        }
        public bool Equals(EffectMinationLevel effect)
        {
            return this.EffectId == effect.EffectId && this.Level == effect.Level && this.Percentage == effect.Percentage;
        }

        public void AddExperience(ulong value)
        {
            if (this.Level >= Records.ExperienceRecord.MaxMinationLevel)
            {
                return;
            }
            Exp += value;


            if (Exp >= this.UpperBoundExperience || Exp < this.LowerBoundExperience)
            {
                this.Level = Records.ExperienceRecord.GetCharacterLevel(this.Exp);
            }
            long neededToUp = (long)(UpperBoundExperience - LowerBoundExperience);
            long current = (long)((neededToUp) - ((long)UpperBoundExperience - (long)Exp));
            this.Percentage = (ushort)Symbioz.Core.Extensions.Percentage(current, neededToUp);

            if (Level >= Records.ExperienceRecord.MaxMinationLevel)
            {
                Level = Records.ExperienceRecord.MaxMinationLevel;
                Exp = Records.ExperienceRecord.GetExperienceForLevel(Records.ExperienceRecord.MaxMinationLevel).Player;
                Percentage = 0;
            }
        }
    }
}
