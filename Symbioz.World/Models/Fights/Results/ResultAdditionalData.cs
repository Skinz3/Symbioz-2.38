using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Results
{
    public abstract class ResultAdditionalData
    {
        public Character Character
        {
            get;
            private set;
        }
        protected ResultAdditionalData(Character character)
        {
            this.Character = character;
        }
        public abstract FightResultAdditionalData GetFightResultAdditionalData();
        public abstract void Apply();
    }
}
