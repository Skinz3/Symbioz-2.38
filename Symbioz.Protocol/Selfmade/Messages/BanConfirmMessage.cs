using SSync.IO;
using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class BanConfirmMessage : TransitionMessage
    {
        public const ushort Id = 6713;

        public override ushort MessageId
        {
            get
            {
                return Id; ;
            }
        }

        public int AccountId;

        public BanConfirmMessage()
        {
        }

        public override void Serialize(ICustomDataOutput writer)
        {
        }

        public override void Deserialize(ICustomDataInput reader)
        {
        }

    }
}
