using SSync.IO;
using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class AccountRequestMessage : TransitionMessage
    {
        public const ushort Id = 6701;

        public override ushort MessageId
        {
            get { return Id; }
        }

        public string Ticket;

        public AccountRequestMessage(string ticket)
        {
            this.Ticket = ticket;
        }
        public AccountRequestMessage() { }



        public override void Serialize(ICustomDataOutput writer)
        {
            writer.WriteUTF(Ticket);
        }

        public override void Deserialize(ICustomDataInput reader)
        {
            this.Ticket = reader.ReadUTF();
        }
    }
}
