using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities.HumanOptions
{
    public class CharacterHumanOptionTitle : CharacterHumanOption
    {
        public ushort TitleId
        {
            get;
            set;
        }
        public string TitleParam
        {
            get;
            set;
        }
        public CharacterHumanOptionTitle(ushort titleId, string titleParam)
        {
            this.TitleId = titleId;
            this.TitleParam = titleParam;
        }
        public CharacterHumanOptionTitle()
        {

        }
        public override HumanOption GetHumanOption()
        {
            return new HumanOptionTitle(TitleId, TitleParam);
        }
    }
}
