using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities.HumanOptions
{
    public class CharacterHumanOptionOrnament : CharacterHumanOption
    {
        public ushort OrnamentId
        {
            get;
            set;
        }
        public CharacterHumanOptionOrnament(ushort ornamentId)
        {
            this.OrnamentId = ornamentId;
        }
        public CharacterHumanOptionOrnament()
        {

        }
        public override HumanOption GetHumanOption()
        {
            return new HumanOptionOrnament(OrnamentId);
        }
    }
}
