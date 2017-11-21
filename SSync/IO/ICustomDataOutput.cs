using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.IO
{
    public interface ICustomDataOutput : IDataWriter
    {
        void WriteVarInt(int value);

        void WriteVarUhInt(uint value);

        void WriteVarShort(short value);

        void WriteVarUhShort(ushort value);

        void WriteVarLong(long value);

        void WriteVarUhLong(ulong value);
    }
}
