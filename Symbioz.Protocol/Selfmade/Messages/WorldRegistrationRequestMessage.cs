using SSync.IO;
using SSync.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Protocol.Selfmade.Messages
{
    public class WorldRegistrationRequestMessage : TransitionMessage
    {
        public const ushort Id = 6700;

        public override ushort MessageId
        {
            get { return Id; }
        }

        public ushort ServerId;

        public string Name;

        public sbyte Type;

        public string Host;

        public int Port;

        public WorldRegistrationRequestMessage() { }

        public WorldRegistrationRequestMessage(ushort serverId,string name,sbyte type,string host,int port)
        {
            this.ServerId = serverId;
            this.Name = name;
            this.Type = type;
            this.Host = host;
            this.Port = port;
        }


        public override void Serialize(ICustomDataOutput writer)
        {
            writer.WriteUShort(ServerId);
            writer.WriteUTF(Name);
            writer.WriteSByte(Type);
            writer.WriteUTF(Host);
            writer.WriteInt(Port);
        }

        public override void Deserialize(ICustomDataInput reader)
        {
            this.ServerId = reader.ReadUShort();
            this.Name = reader.ReadUTF();
            this.Type = reader.ReadSByte();
            this.Host = reader.ReadUTF();
            this.Port = reader.ReadInt();
        }
    }
}
