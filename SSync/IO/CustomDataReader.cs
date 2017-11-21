using SSync.IO.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.IO
{
    public class CustomDataReader : ICustomDataInput, IDisposable
    {
        private static int INT_SIZE = 32;

        private static int SHORT_SIZE = 16;

        private static int SHORT_MAX_VALUE = 32767;

        private static int UNSIGNED_SHORT_MAX_VALUE = 65536;

        private static int CHUNCK_BIT_SIZE = 7;

        private static int MAX_ENCODING_LENGTH = (int)Math.Ceiling((double)INT_SIZE / CHUNCK_BIT_SIZE);

        private static int MASK_10000000 = 128;

        private static int MASK_01111111 = 127;

        private IDataReader _data;

        public CustomDataReader(IDataReader reader)
        {
            _data = reader;
        }

        public CustomDataReader(byte[] data)
        {
            _data = new BigEndianReader(data);
        }

        public int ReadVarInt()
        {
            int b = 0;
            int value = 0;
            int offset = 0;
            bool hasNext = false;
            while (offset < INT_SIZE)
            {
                b = _data.ReadByte();
                hasNext = (b & MASK_10000000) == MASK_10000000;
                if (offset > 0)
                {
                    value = value + ((b & MASK_01111111) << offset);
                }
                else
                {
                    value = value + (b & MASK_01111111);
                }
                offset = offset + CHUNCK_BIT_SIZE;
                if (!hasNext)
                {
                    return value;
                }
            }
            throw new Exception("Too much data");
        }

        public uint ReadVarUhInt()
        {
            int b = 0;
            uint value = 0;
            int offset = 0;
            bool hasNext = false;
            while (offset < INT_SIZE)
            {
                b = _data.ReadByte();
                hasNext = (b & MASK_10000000) == MASK_10000000;
                if (offset > 0)
                {
                    value = (uint)(value + ((b & MASK_01111111) << offset));
                }
                else
                {
                    value = (uint)(value + (b & MASK_01111111));
                }
                offset = offset + CHUNCK_BIT_SIZE;
                if (!hasNext)
                {
                    return value;
                }
            }
            throw new Exception("Too much data");
        }

        public short ReadVarShort()
        {
            int b = 0;
            short value = 0;
            int offset = 0;
            bool hasNext = false;
            while (offset < SHORT_SIZE)
            {
                b = _data.ReadByte();
                hasNext = (b & MASK_10000000) == MASK_10000000;
                if (offset > 0)
                {
                    value = (short)(value + ((b & MASK_01111111) << offset));
                }
                else
                {
                    value = (short)(value + (b & MASK_01111111));
                }
                offset = offset + CHUNCK_BIT_SIZE;
                if (!hasNext)
                {
                    if (value > SHORT_MAX_VALUE)
                    {
                        value = (short)(value - UNSIGNED_SHORT_MAX_VALUE);
                    }
                    return value;
                }
            }
            throw new Exception("Too much data");
        }

        public ushort ReadVarUhShort()
        {
            int b = 0;
            ushort value = 0;
            int offset = 0;
            bool hasNext = false;
            while (offset < SHORT_SIZE)
            {
                b = _data.ReadByte();
                hasNext = (b & MASK_10000000) == MASK_10000000;
                if (offset > 0)
                {
                    value = (ushort)(value + ((b & MASK_01111111) << offset));
                }
                else
                {
                    value = (ushort)(value + (b & MASK_01111111));
                }
                offset = offset + CHUNCK_BIT_SIZE;
                if (!hasNext)
                {
                    if (value > SHORT_MAX_VALUE)
                    {
                        value = (ushort)(value - UNSIGNED_SHORT_MAX_VALUE);
                    }
                    return value;
                }
            }
            throw new Exception("Too much data");
        }

        public long ReadVarLong()
        {
            return readInt64(_data).toNumber();
        }

        public ulong ReadVarUhLong()
        {
            return readUInt64(_data).toNumber();
        }

        public int Position
        {
            get { return _data.Position; }
        }

        public int BytesAvailable
        {
            get { return _data.BytesAvailable; }
        }

        public short ReadShort()
        {
            return _data.ReadShort();
        }

        public int ReadInt()
        {
            return _data.ReadInt();
        }

        public long ReadLong()
        {
            return _data.ReadLong();
        }

        public ushort ReadUShort()
        {
            return _data.ReadUShort();
        }

        public uint ReadUInt()
        {
            return _data.ReadUInt();
        }

        public ulong ReadULong()
        {
            return _data.ReadULong();
        }

        public byte ReadByte()
        {
            return _data.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return _data.ReadSByte();
        }

        public byte[] ReadBytes(int n)
        {
            return _data.ReadBytes(n);
        }

        public bool ReadBoolean()
        {
            return _data.ReadBoolean();
        }

        public char ReadChar()
        {
            return _data.ReadChar();
        }

        public double ReadDouble()
        {
            return _data.ReadDouble();
        }

        public float ReadFloat()
        {
            return _data.ReadFloat();
        }

        public string ReadUTF()
        {
            return _data.ReadUTF();
        }

        public string ReadUTFBytes(ushort len)
        {
            return _data.ReadUTFBytes(len);
        }

        public void Seek(int offset, System.IO.SeekOrigin seekOrigin)
        {
            _data.Seek(offset, seekOrigin);
        }

        public void SkipBytes(int n)
        {
            _data.SkipBytes(n);
        }

        public void Dispose()
        {
            _data.Dispose();
        }

        private static CustomInt64 readInt64(IDataReader input)
        {
            uint b = 0;
            CustomInt64 result = new CustomInt64();
            int i = 0;
            while (true)
            {
                b = input.ReadByte();
                if (i == 28)
                {
                    break;
                }
                if (b >= 128)
                {
                    result.low = result.low | (b & 127) << i;
                    i = i + 7;
                    continue;
                }
                result.low = result.low | b << i;
                return result;
            }

            if (b >= 128)
            {
                b = b & 127;
                result.low = result.low | b << i;
                result.high = b >> 4;
                i = 3;
                while (true)
                {
                    b = input.ReadByte();
                    if (i < 32)
                    {
                        if (b >= 128)
                        {
                            result.high = (uint)(result.high | (b & 127) << i);
                        }
                        else
                        {
                            break;
                        }
                    }
                    i = i + 7;
                }

                result.high = (uint)(result.high | (b << i));
                return result;
            }
            result.low = result.low | b << i;
            result.high = b >> 4;
            return result;
        }

        private CustomUInt64 readUInt64(IDataReader input)
        {
            uint b = 0;
            var result = new CustomUInt64();
            int i = 0;
            while (true)
            {
                b = input.ReadByte();
                if (i == 28)
                {
                    break;
                }
                if (b >= 128)
                {
                    result.low = result.low | (b & 127) << i;
                    i = i + 7;
                    continue;
                }
                result.low = result.low | b << i;
                return result;
            }

            if (b >= 128)
            {
                b = b & 127;
                result.low = result.low | b << i;
                result.high = b >> 4;
                i = 3;
                while (true)
                {
                    b = input.ReadByte();
                    if (i < 32)
                    {
                        if (b >= 128)
                        {
                            result.high = result.high | (b & 127) << i;
                        }
                        else
                        {
                            break;
                        }
                    }
                    i = i + 7;
                }

                result.high = result.high | b << i;
                return result;
            }
            result.low = result.low | b << i;
            result.high = b >> 4;
            return result;
        }
    }
}