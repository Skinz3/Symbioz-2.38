

using SSync.IO;
using System;

namespace SSync.Messages
{
    public class MessagePart
    {
        private readonly bool m_readData;
        private long m_availableBytes = 0L;
        private bool m_dataMissing = false;
        private byte[] m_data;
        public bool IsValid
        {
            get
            {
                bool arg_69_0;
                if (this.Header.HasValue && this.Length.HasValue && (!this.ReadData || this.Data != null))
                {
                    int? length = this.Length;
                    long num = this.ReadData ? ((long)this.Data.Length) : this.m_availableBytes;
                    arg_69_0 = ((long)length.GetValueOrDefault() == num && length.HasValue);
                }
                else
                {
                    arg_69_0 = false;
                }
                return arg_69_0;
            }
        }
        public int? Header
        {
            get;
            private set;
        }
        public int? MessageId
        {
            get
            {
                int? result;
                if (!this.Header.HasValue)
                {
                    result = null;
                }
                else
                {
                    result = this.Header >> 2;
                }
                return result;
            }
        }
        public int? LengthBytesCount
        {
            get
            {
                int? result;
                if (!this.Header.HasValue)
                {
                    result = null;
                }
                else
                {
                    result = (this.Header & 3);
                }
                return result;
            }
        }
        public int? Length
        {
            get;
            private set;
        }
        public byte[] Data
        {
            get
            {
                return this.m_data;
            }
            private set
            {
                this.m_data = value;
            }
        }

        public bool ReadData
        {
            get
            {
                return this.m_readData;
            }
        }
        public MessagePart(bool readData)
        {
            this.m_readData = readData;
        }
        public bool Build(CustomDataReader reader)
        {
            bool result;
            if (reader.BytesAvailable <= 0L)
            {
                result = false;
            }
            else
            {
                if (this.IsValid)
                {
                    result = true;
                }
                else
                {
                    if (!this.Header.HasValue && reader.BytesAvailable < 2L)
                    {
                        result = false;
                    }
                    else
                    {
                        if (reader.BytesAvailable >= 2L && !this.Header.HasValue)
                        {
                            this.Header = new int?((int)reader.ReadShort());
                        }
                        bool formatedHeader;
                        if (this.LengthBytesCount.HasValue)
                        {
                            long num = reader.BytesAvailable;
                            int? num2 = this.LengthBytesCount;
                            if (num >= (long)num2.GetValueOrDefault() && num2.HasValue)
                            {
                                formatedHeader = this.Length.HasValue;
                                goto CheckHeader;
                            }
                        }
                        formatedHeader = true;
                    CheckHeader:
                        if (!formatedHeader)
                        {
                            if (this.LengthBytesCount < 0 || this.LengthBytesCount > 3)
                            {
                                throw new Exception("Malformated Message Header, invalid bytes number to read message length (inferior to 0 or superior to 3)");
                            }
                            this.Length = new int?(0);
                            for (int i = this.LengthBytesCount.Value - 1; i >= 0; i--)
                            {
                                this.Length |= (int)reader.ReadByte() << i * 8;
                            }
                        }
                        if (this.Length.HasValue && !this.m_dataMissing)
                        {
                            if (this.Length == 0)
                            {
                                if (this.ReadData)
                                {
                                    this.Data = new byte[0];
                                }
                                result = true;
                                return result;
                            }
                            long num = reader.BytesAvailable;
                            int? num2 = this.Length;
                            if (num >= (long)num2.GetValueOrDefault() && num2.HasValue)
                            {
                                if (this.ReadData)
                                {
                                    this.Data = reader.ReadBytes(this.Length.Value);
                                }
                                else
                                {
                                    this.m_availableBytes = reader.BytesAvailable;
                                }
                                result = true;
                                return result;
                            }
                            num2 = this.Length;
                            num = reader.BytesAvailable;
                            if ((long)num2.GetValueOrDefault() > num && num2.HasValue)
                            {
                                if (this.ReadData)
                                {
                                    this.Data = reader.ReadBytes((int)reader.BytesAvailable);
                                }
                                else
                                {
                                    this.m_availableBytes = reader.BytesAvailable;
                                }
                                this.m_dataMissing = true;
                                result = false;
                                return result;
                            }
                        }
                        else
                        {
                            if (this.Length.HasValue && this.m_dataMissing)
                            {
                                long num = (long)(this.ReadData ? this.Data.Length : 0) + reader.BytesAvailable;
                                int? num2 = this.Length;
                                if (num < (long)num2.GetValueOrDefault() && num2.HasValue)
                                {
                                    if (this.ReadData)
                                    {
                                        int destinationIndex = this.m_data.Length;
                                        Array.Resize<byte>(ref this.m_data, (int)((long)this.Data.Length + reader.BytesAvailable));
                                        byte[] array = reader.ReadBytes((int)reader.BytesAvailable);
                                        Array.Copy(array, 0, this.Data, destinationIndex, array.Length);
                                    }
                                    else
                                    {
                                        this.m_availableBytes = reader.BytesAvailable;
                                    }
                                    this.m_dataMissing = true;
                                }
                                num = (long)(this.ReadData ? this.Data.Length : 0) + reader.BytesAvailable;
                                num2 = this.Length;
                                if (num >= (long)num2.GetValueOrDefault() && num2.HasValue)
                                {
                                    if (this.ReadData)
                                    {
                                        int num3 = this.Length.Value - this.Data.Length;
                                        Array.Resize<byte>(ref this.m_data, this.Data.Length + num3);
                                        byte[] array = reader.ReadBytes(num3);
                                        Array.Copy(array, 0, this.Data, this.Data.Length - num3, num3);
                                    }
                                    else
                                    {
                                        this.m_availableBytes = reader.BytesAvailable;
                                    }
                                }
                            }
                        }
                        result = this.IsValid;
                    }
                }
            }
            return result;
        }
    }
}
