


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

    public class RawDataMessage : Message
    {

        public const ushort Id = 6253;
        public byte[] _content;
        public override ushort MessageId
        {
            get
            {
                return 6253;
            }
        }
        public RawDataMessage()
        {
        }
        public RawDataMessage(byte[] content)
        {
            this._content = content;
        }
        public override void Serialize(ICustomDataOutput writer)
        {
            writer.WriteVarInt(this._content.Length);
            writer.WriteBytes(this._content);
        }
        public override void Deserialize(ICustomDataInput reader)
        {
            int n = reader.ReadVarInt();
            this._content = reader.ReadBytes((int)n);
        }


    }


}