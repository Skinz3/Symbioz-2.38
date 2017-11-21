using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSync.IO
{
    public interface ICustomDataInput : IDataReader
    {
        int ReadVarInt();

        uint ReadVarUhInt();

        short ReadVarShort();

        ushort ReadVarUhShort();

        long ReadVarLong();

        ulong ReadVarUhLong();
    }
}
