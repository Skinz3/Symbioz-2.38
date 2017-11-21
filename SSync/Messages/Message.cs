using SSync.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.Messages
{
    public abstract class Message
    {
        private const byte BIT_RIGHT_SHIFT_LEN_PACKET_ID = 2;
        private const byte BIT_MASK = 3;

        public abstract ushort MessageId
        {
            get;
        }
        public void Unpack(ICustomDataInput reader)
        {
            this.Deserialize(reader);
        }


        public void Pack(ICustomDataOutput writer)
        {
            var data = new CustomDataWriter();
            Serialize(data);
            var size = data.Data.Length;
            var compute = ComputeTypeLen(size);
            short val = (short)((MessageId << 2) | compute);
            writer.WriteShort(val);
            switch (compute)
            {
                case 1:
                    writer.WriteByte((byte)size);
                    break;
                case 2:
                    writer.WriteUShort((ushort)size);
                    break;
                case 3:
                    writer.WriteByte((byte)((size >> 0x10) & 0xff));
                    writer.WriteUShort((ushort)(size & 0xffff));
                    break;
            }
            writer.WriteBytes(data.Data);
            data.Dispose();
           
        }
        public abstract void Serialize(ICustomDataOutput writer);
        public abstract void Deserialize(ICustomDataInput reader);

        private static byte ComputeTypeLen(int param1)
        {
            byte result;
            if (param1 > 65535)
            {
                result = 3;
            }
            else
            {
                if (param1 > 255)
                {
                    result = 2;
                }
                else
                {
                    if (param1 > 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
            }
            return result;
        }
        private static uint SubComputeStaticHeader(uint id, byte typeLen)
        {
            return id << 2 | (uint)typeLen;
        }
        public override string ToString()
        {
            return base.GetType().Name;
        }
    }
}
