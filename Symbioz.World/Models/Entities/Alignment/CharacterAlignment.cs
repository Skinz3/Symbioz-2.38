using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Core;
using Symbioz.Protocol.Enums;
using System.Threading.Tasks;
using Symbioz.Protocol.Types;
using Symbioz.World.Records;
using YAXLib;

namespace Symbioz.World.Models.Entities.Alignment
{
    public class CharacterAlignment
    {
        public AlignmentSideEnum Side
        {
            get;
            set;
        }

        public sbyte Value
        {
            get;
            set;
        }

        [YAXDontSerialize]
        public sbyte Grade
        {
            get
            {
                return ExperienceRecord.GetGrade(Honor);
            }
        }

        public double CharacterPower
        {
            get;
            set;
        }

        public ushort Honor
        {
            get;
            set;
        }

        [YAXDontSerialize]
        public ushort HonorGradeFloor
        {
            get
            {
                return ExperienceRecord.GetHonorForGrade(Grade);
            }
        }

        [YAXDontSerialize]
        public ushort HonorGradeNextFloor
        {
            get
            {
                return ExperienceRecord.GetHonorNextGrade(Grade);
            }
        }

        public AggressableStatusEnum Agressable
        {
            get;
            set;
        }

        public ActorExtendedAlignmentInformations GetActorExtendedAlignement()
        {
            return new ActorExtendedAlignmentInformations((sbyte)Side, Value, Grade, CharacterPower, Honor,
                HonorGradeFloor, HonorGradeNextFloor, (sbyte)Agressable);
        }
        public ActorAlignmentInformations GetActorAlignmentInformations()
        {
            return Agressable == AggressableStatusEnum.PvP_ENABLED_AGGRESSABLE ? new ActorAlignmentInformations((sbyte)Side, Value, Grade, CharacterPower)
                : new ActorAlignmentInformations(0, 0, 0, 0);
        }

        public static CharacterAlignment New()
        {
            return new CharacterAlignment()
            {
                Agressable = AggressableStatusEnum.NON_AGGRESSABLE,
                CharacterPower = 0,
                Honor = 0,
                Side = AlignmentSideEnum.ALIGNMENT_NEUTRAL,
                Value = 0
            };
        }
    }
}
