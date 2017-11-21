using SSync.IO;
using SSync.Messages;
using Symbioz.Protocol.Selfmade.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class AccountMessage : TransitionMessage
    {
        public const ushort Id = 6702;

        public override ushort MessageId
        {
            get { return Id; ; }
        }

        public AccountData Account;

        public AccountMessage(AccountData account)
        {
            this.Account = account;
        }
        public AccountMessage() { }



        public override void Serialize(ICustomDataOutput writer)
        {
            Account.Serialize(writer);
        }

        public override void Deserialize(ICustomDataInput reader)
        {
            Account = new AccountData();
            Account.Deserialize(reader);
        }
    }
}
