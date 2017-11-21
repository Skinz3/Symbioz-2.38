using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.Core;
using Symbioz.World.Models.Items;
using Symbioz.World.Providers;
using Symbioz.World.Providers.Fights.Results;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Providers.Guilds;

namespace Symbioz.World.Models.Fights.Results
{
    public class FightPlayerResult : FightResult<CharacterFighter>
    {
        public Character Character
        {
            get
            {
                return base.Fighter.Character;
            }
        }
        public new byte Level
        {
            get
            {
                return (byte)this.Character.Level;
            }
        }
        public FightExperienceData ExperienceData
        {
            get;
            private set;
        }
        public FightPvpData PvpData
        {
            get;
            private set;
        }
        public FightPlayerResult(CharacterFighter fighter, FightOutcomeEnum outcome, Loot loot)
            : base(fighter, outcome, loot)
        {
        }
        public override bool CanLoot(FightTeam team)
        {
            return base.Fighter.Team == team && !base.Fighter.Left;
        }
        public override FightResultListEntry GetFightResultListEntry()
        {
            List<FightResultAdditionalData> list = new System.Collections.Generic.List<FightResultAdditionalData>();

            if (this.ExperienceData != null)
            {
                list.Add(this.ExperienceData.GetFightResultAdditionalData());
            }

            if (this.PvpData != null)
            {
                list.Add(this.PvpData.GetFightResultAdditionalData());
            }
            return new FightResultPlayerListEntry((ushort)base.Outcome, 0, base.Loot.GetFightLoot(), base.Id, base.Alive, this.Level, list.ToArray());
        }
        public override void Apply()
        {
            this.Character.AddKamas((int)base.Loot.Kamas);

            foreach (DroppedItem current in base.Loot.Items.Values)
            {
                this.Character.Inventory.AddItem(current.ItemGId, current.Amount);
            }
            if (this.ExperienceData != null)
            {
                this.ExperienceData.Apply();
            }
            if (this.PvpData != null)
            {
                this.PvpData.Apply();
            }

            if (Outcome == FightOutcomeEnum.RESULT_VICTORY && !Fighter.Left)
                CustomFightLootProvider.Apply(this);

            this.Character.RefreshStats();
        }
        public void AddEarnedExperience(int bonusPercentage)
        {
            long experience = (long)FormulasProvider.Instance.GetPvMExperience(Fighter);

            long bonus = experience.GetPercentageOf(bonusPercentage);

            experience += bonus;

            if (!base.Fighter.Left)
            {
                if (this.ExperienceData == null)
                {
                    this.ExperienceData = new FightExperienceData(this.Character);
                }
                if (this.Character.HasGuild && this.Character.GuildMember.Record.experienceGivenPercent > 0)
                {
                    int num = (int)((double)experience * ((double)this.Character.GuildMember.Record.experienceGivenPercent * 0.01));
                    int num2 = (int)this.Character.Guild.AdjustGivenExperience(this.Character, (long)num);
                    num2 = ((num2 > GuildProvider.MAX_XP) ? GuildProvider.MAX_XP : num2);
                    experience -= num2;
                    if (num2 > 0)
                    {
                        this.ExperienceData.ShowExperienceForGuild = true;
                        this.ExperienceData.ExperienceForGuild += num2;
                        this.Character.Guild.Record.UpdateElement();
                    }
                }
                this.ExperienceData.ShowExperienceFightDelta = true;
                this.ExperienceData.ShowExperience = true;
                this.ExperienceData.ShowExperienceLevelFloor = true;
                this.ExperienceData.ShowExperienceNextLevelFloor = true;


                if (this.Character.HasGuild && this.Character.GuildMember.Record.experienceGivenPercent > 0)
                {
                    long num = (long)((double)experience * (1 - (double)this.Character.GuildMember.Record.experienceGivenPercent * 0.01));
                    this.ExperienceData.ExperienceFightDelta += num;
                }
                else
                {
                    this.ExperienceData.ExperienceFightDelta += experience;
                }
            }
        }
        public void SetEarnedHonor(short honor, short dishonor)
        {
            if (this.PvpData == null)
            {
                this.PvpData = new FightPvpData(this.Character);
            }
            this.PvpData.HonorDelta = honor;
            this.PvpData.Honor = this.Character.Record.Alignment.Honor;
            this.PvpData.Grade = (byte)this.Character.Record.Alignment.Grade;
            this.PvpData.MinHonorForGrade = this.Character.Record.Alignment.HonorGradeFloor;
            this.PvpData.MaxHonorForGrade = this.Character.Record.Alignment.HonorGradeNextFloor;
        }
    }
}
