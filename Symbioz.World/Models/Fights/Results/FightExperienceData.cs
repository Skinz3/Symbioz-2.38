using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Results
{
    public class FightExperienceData : ResultAdditionalData
    {
        public bool ShowExperience
        {
            get;
            set;
        }
        public bool ShowExperienceLevelFloor
        {
            get;
            set;
        }
        public bool ShowExperienceNextLevelFloor
        {
            get;
            set;
        }
        public bool ShowExperienceFightDelta
        {
            get;
            set;
        }
        public bool ShowExperienceForGuild
        {
            get;
            set;
        }
        public bool ShowExperienceForMount
        {
            get;
            set;
        }
        public bool IsIncarnationExperience
        {
            get;
            set;
        }
        public long ExperienceFightDelta
        {
            get;
            set;
        }
        public int ExperienceForGuild
        {
            get;
            set;
        }
        public long ExperienceForMount
        {
            get;
            set;
        }
        public FightExperienceData(Character character)
            : base(character)
        {
        }
        public override FightResultAdditionalData GetFightResultAdditionalData()
        {
            return new FightResultExperienceData(this.ShowExperience, this.ShowExperienceLevelFloor,
                this.ShowExperienceNextLevelFloor, this.ShowExperienceFightDelta, this.ShowExperienceForGuild,
                this.ShowExperienceForMount, this.IsIncarnationExperience, base.Character.Experience,
                (ulong)base.Character.LowerBoundExperience, (ulong)base.Character.UpperBoundExperience,
                (ulong)this.ExperienceFightDelta, (uint)this.ExperienceForGuild, (uint)this.ExperienceForMount, 1);
        }
        public override void Apply()
        {
            base.Character.AddExperience((ulong)this.ExperienceFightDelta);
            this.Character.AddMinationExperience((ulong)this.ExperienceFightDelta);
            if (base.Character.HasGuild && this.ExperienceForGuild > 0)
            {
                base.Character.GuildMember.AddExp(this.ExperienceForGuild);
            }
        }
    }
}
