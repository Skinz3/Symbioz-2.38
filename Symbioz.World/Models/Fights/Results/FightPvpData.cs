using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Results
{
    public class FightPvpData : ResultAdditionalData
    {
        public byte Grade
        {
            get;
            set;
        }
        public ushort MinHonorForGrade
        {
            get;
            set;
        }
        public ushort MaxHonorForGrade
        {
            get;
            set;
        }
        public ushort Honor
        {
            get;
            set;
        }
        public short HonorDelta
        {
            get;
            set;
        }
        public FightPvpData(Character character)
            : base(character)
        {
        }
        public override FightResultAdditionalData GetFightResultAdditionalData()
        {
            return new FightResultPvpData(this.Grade, this.MinHonorForGrade, this.MaxHonorForGrade, this.Honor, this.HonorDelta);
        }
        public override void Apply()
        {
            if (this.HonorDelta > 0)
            {
                base.Character.AddHonor((ushort)HonorDelta);
            }
            else
            {
                if (this.HonorDelta < 0)
                {
                    base.Character.RemoveHonor((ushort)-HonorDelta);
                }
            }
        }
    }
}
