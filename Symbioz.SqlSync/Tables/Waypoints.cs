using Symbioz.ORM;
using Symbioz.SqlSync.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.SqlSync.Tables
{
    [D2O("Waypoints.d2o","Waypoint"),Table("Waypoints")]
    public class Waypoints// : ID2OTable
    {
        [D2OField("id"),Primary]
        public int Id;

        [D2OField("mapId")]
        public int MapId;

        [D2OField("subAreaId")]
        public ushort SubAreaId;
    }
}
