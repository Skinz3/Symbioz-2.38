using Symbioz.Tools.DLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.MapTables
{
    public class Cell
    {
        public short Id
        {
            get;
            set;
        }

        public short Floor
        {
            get;
            set;
        }

        public byte LosMov
        {
            get;
            set;
        }

        public byte MapChangeData
        {
            get;
            set;
        }

        public byte MoveZone
        {
            get;
            set;
        }
        public override string ToString()
        {
            return Id + "," + Floor + "," + LosMov + "," + MapChangeData + "," + MoveZone + ";";
        }
    }
}
