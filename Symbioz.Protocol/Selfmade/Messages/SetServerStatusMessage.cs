using SSync.IO;
using SSync.Messages;
using Symbioz.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class SetServerStatusMessage : TransitionMessage
    {
        public const ushort Id = 6708;

        public override ushort MessageId
        {
            get { return Id; }
        }
        public ServerStatusEnum Status;

        public SetServerStatusMessage() { }

        public SetServerStatusMessage(ServerStatusEnum status)
        {
            this.Status = status;
        }
        public override void Serialize(ICustomDataOutput writer)
        {
            writer.WriteInt((int)Status);
        }

        public override void Deserialize(ICustomDataInput reader)
        {
            this.Status = (ServerStatusEnum)reader.ReadInt();
        }
    }
}
