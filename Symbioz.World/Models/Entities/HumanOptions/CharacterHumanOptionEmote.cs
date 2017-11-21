using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities.HumanOptions
{
    public class CharacterHumanOptionEmote : CharacterHumanOption
    {
        public byte EmoteId
        {
            get;
            set;
        }
        public double EmoteStartTime
        {
            get;
            set;
        }

        public CharacterHumanOptionEmote(byte emoteId, double emoteStartTime)
        {
            this.EmoteId = emoteId;
            this.EmoteStartTime = EmoteStartTime;
        }
        public CharacterHumanOptionEmote()
        {

        }
        public override HumanOption GetHumanOption()
        {
            return new HumanOptionEmote(EmoteId, EmoteStartTime);
        }
    }
}
