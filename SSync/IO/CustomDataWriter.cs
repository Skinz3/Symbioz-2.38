using SSync.IO.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.IO
{
    public class CustomDataWriter : ICustomDataOutput, IDisposable
    {
        private static int INT_SIZE = 32;

        private static int SHORT_MIN_VALUE = -32768;

        private static int SHORT_MAX_VALUE = 32767;

        private static int UNSIGNED_SHORT_MAX_VALUE = 65536;

        private static int CHUNCK_BIT_SIZE = 7;

        private static int MAX_ENCODING_LENGTH = (int)Math.Ceiling((double)INT_SIZE / CHUNCK_BIT_SIZE);

        private static int MASK_10000000 = 128;

        private static int MASK_01111111 = 127;

        private IDataWriter _data;

        public CustomDataWriter()
        {
            _data = new BigEndianWriter();
        }

        public CustomDataWriter(Stream stream)
        {
            _data = new BigEndianWriter(stream);
        }

        public void WriteVarInt(int value)
        {
            if (value >= 0 && value <= MASK_01111111)
            {
                _data.WriteByte((byte)value);
                return;
            }
            int b = 0;
            int c = value;
            while (c != 0 && c != -1)
            {
                b = c & MASK_01111111;
                c = c >> CHUNCK_BIT_SIZE;
                if (c > 0)
                {
                    b = b | MASK_10000000;
                }
                _data.WriteByte((byte)b);
            }
        }

        public void WriteVarUhInt(uint value)
        {
            if (value <= MASK_01111111)
            {
                _data.WriteByte((byte)value);
                return;
            }
            uint b = 0;
            uint c = value;
            while (c != 0)
            {
                b = (uint)(c & MASK_01111111);
                c = c >> CHUNCK_BIT_SIZE;
                if (c > 0)
                {
                    b = b | (uint)MASK_10000000;
                }
                _data.WriteByte((byte)b);
            }
        }

        public void WriteVarShort(short value)
        {
            if (value > SHORT_MAX_VALUE || value < SHORT_MIN_VALUE)
            {
                throw new Exception("Forbidden value");
            }
            else
            {
                var b = 0;
                if ((value >= 0) && (value <= MASK_01111111))
                {
                    _data.WriteByte((byte)value);
                    return;
                }
                var c = value & 65535;
                while (c != 0 && c != -1)
                {
                    b = (c & MASK_01111111);
                    c = c >> CHUNCK_BIT_SIZE;
                    if (c > 0)
                    {
                        b = b | MASK_10000000;
                    }
                    _data.WriteByte((byte)b);
                }
            }
        }

        public void WriteVarUhShort(ushort value)
        {
            if (value > UNSIGNED_SHORT_MAX_VALUE || value < SHORT_MIN_VALUE)
            {
                throw new Exception("Forbidden value");
            }
            else
            {
                var b = 0;
                if ((value >= 0) && (value <= MASK_01111111))
                {
                    _data.WriteByte((byte)value);
                    return;
                }
                var c = value & 65535;
                while (c != 0)
                {
                    b = (c & MASK_01111111);
                    c = c >> CHUNCK_BIT_SIZE;
                    if (c > 0)
                    {
                        b = b | MASK_10000000;
                    }
                    _data.WriteByte((byte)b);
                }
            }
        }

        public void WriteVarLong(long value)
        {
            uint i = 0;
            var val = CustomInt64.fromNumber(value);
            if (val.high == 0)
            {
                writeint32(_data, val.low);
            }
            else
            {
                i = 0;
                while (i < 4)
                {
                    this._data.WriteByte((byte)(val.low & 127 | 128));
                    val.low = val.low >> 7;
                    i++;
                }
                if ((val.high & 268435455 << 3) == 0)
                {
                    this._data.WriteByte((byte)(val.high << 4 | val.low));
                }
                else
                {
                    this._data.WriteByte((byte)(((val.high << 4) | val.low) & 127 | 128));
                    writeint32(this._data, val.high >> 3);
                }
            }
        }

        public void WriteVarUhLong(ulong value)
        {
            WriteVarLong((long)value);
        }

        public byte[] Data
        {
            get { return _data.Data; }
        }

        public int Position
        {
            get { return _data.Position; }
        }

        public void WriteShort(short @short)
        {
            _data.WriteShort(@short);
        }

        public void WriteInt(int @int)
        {
            _data.WriteInt(@int);
        }

        public void WriteLong(long @long)
        {
            _data.WriteLong(@long);
        }

        public void WriteUShort(ushort @ushort)
        {
            _data.WriteUShort(@ushort);
        }

        public void WriteUInt(uint @uint)
        {
            _data.WriteUInt(@uint);
        }

        public void WriteULong(ulong @ulong)
        {
            _data.WriteULong(@ulong);
        }

        public void WriteByte(byte @byte)
        {
            _data.WriteByte(@byte);
        }

        public void WriteSByte(sbyte @byte)
        {
            _data.WriteSByte(@byte);
        }

        public void WriteFloat(float @float)
        {
            _data.WriteFloat(@float);
        }

        public void WriteBoolean(bool @bool)
        {
            _data.WriteBoolean(@bool);
        }

        public void WriteChar(char @char)
        {
            _data.WriteChar(@char);
        }

        public void WriteDouble(double @double)
        {
            _data.WriteDouble(@double);
        }

        public void WriteSingle(float single)
        {
            _data.WriteSingle(single);
        }

        public void WriteUTF(string str)
        {
            _data.WriteUTF(str);
        }

        public void WriteUTFBytes(string str)
        {
            _data.WriteUTFBytes(str);
        }

        public void WriteBytes(byte[] data)
        {
            _data.WriteBytes(data);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public void Seek(int offset)
        {
            _data.Seek(offset);
        }

        public void Dispose()
        {
            if (_data is BigEndianWriter)
            {
                (_data as BigEndianWriter).Dispose();
            }
        }

        private static void writeint32(IDataWriter output, uint value)
        {
            while (value >= 128)
            {
                output.WriteByte((byte)(value & 127 | 128));
                value = value >> 7;
            }
            output.WriteByte((byte)value);
        }
    }
}