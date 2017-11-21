


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{
    public class FightResultExperienceData : FightResultAdditionalData
    {

        public const short Id = 192;
        public override short TypeId
        {
            get { return Id; }
        }

        public bool showExperience;
        public bool showExperienceLevelFloor;
        public bool showExperienceNextLevelFloor;
        public bool showExperienceFightDelta;
        public bool showExperienceForGuild;
        public bool showExperienceForMount;
        public bool isIncarnationExperience;
        public ulong experience;
        public ulong experienceLevelFloor;
        public ulong experienceNextLevelFloor;
        public ulong experienceFightDelta;
        public ulong experienceForGuild;
        public ulong experienceForMount;
        public sbyte rerollExperienceMul;


        public FightResultExperienceData()
        {
        }

        public FightResultExperienceData(bool showExperience, bool showExperienceLevelFloor,
            bool showExperienceNextLevelFloor, bool showExperienceFightDelta, bool showExperienceForGuild,
            bool showExperienceForMount, bool isIncarnationExperience, ulong experience,
            ulong experienceLevelFloor, ulong experienceNextLevelFloor, ulong experienceFightDelta,
           ulong experienceForGuild, ulong experienceForMount, sbyte rerollExperienceMul)
        {
            this.showExperience = showExperience;
            this.showExperienceLevelFloor = showExperienceLevelFloor;
            this.showExperienceNextLevelFloor = showExperienceNextLevelFloor;
            this.showExperienceFightDelta = showExperienceFightDelta;
            this.showExperienceForGuild = showExperienceForGuild;
            this.showExperienceForMount = showExperienceForMount;
            this.isIncarnationExperience = isIncarnationExperience;
            this.experience = experience;
            this.experienceLevelFloor = experienceLevelFloor;
            this.experienceNextLevelFloor = experienceNextLevelFloor;
            this.experienceFightDelta = experienceFightDelta;
            this.experienceForGuild = experienceForGuild;
            this.experienceForMount = experienceForMount;
            this.rerollExperienceMul = rerollExperienceMul;
        }


        public override void Serialize(ICustomDataOutput writer)
        {

            base.Serialize(writer);
            byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, showExperience);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, showExperienceLevelFloor);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 2, showExperienceNextLevelFloor);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 3, showExperienceFightDelta);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 4, showExperienceForGuild);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 5, showExperienceForMount);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 6, isIncarnationExperience);
            writer.WriteByte(flag1);
            writer.WriteVarUhLong(experience);
            writer.WriteVarUhLong(experienceLevelFloor);
            writer.WriteVarUhLong(experienceNextLevelFloor);
            writer.WriteVarUhLong(experienceFightDelta);
            writer.WriteVarUhLong(experienceForGuild);
            writer.WriteVarUhLong(experienceForMount);
            writer.WriteSByte(rerollExperienceMul);


        }

        public override void Deserialize(ICustomDataInput reader)
        {


        }



    }
}