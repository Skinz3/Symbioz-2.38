using Symbioz.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Symbioz.World.Records.Maps
{
    [Table("MapPositions")]
    public class MapPositionRecord : ITable
    {
        public static List<MapPositionRecord> MapPositions = new List<MapPositionRecord>();

        public int Id;

        public int X;

        public int Y;

        public string Name;

        public bool Outdoor;

        public int Capabilities;

        [Ignore]
        public Point Point
        {
            get
            {
                return new Point(X, Y);
            }
        }

        [Ignore]
        public bool AllowChallenge
        {
            get
            {
                return (this.Capabilities & 1) != 0;
            }
        }
        [Ignore]
        public bool AllowAggression
        {
            get
            {
                return (this.Capabilities & 2) != 0;
            }
        }
        [Ignore]
        public bool AllowTeleportTo
        {
            get
            {
                return (this.Capabilities & 4) != 0;
            }
        }
        [Ignore]
        public bool AllowTeleportFrom
        {
            get
            {
                return (this.Capabilities & 8) != 0;
            }
        }
        [Ignore]
        public bool AllowExchangesBetweenPlayers
        {
            get
            {
                return (this.Capabilities & 16) != 0;
            }
        }
        [Ignore]
        public bool AllowHumanVendor
        {
            get
            {
                return (this.Capabilities & 32) != 0;
            }
        }
        [Ignore]
        public bool AllowCollector
        {
            get
            {
                return (this.Capabilities & 64) != 0;
            }
        }
        [Ignore]
        public bool AllowSoulCapture
        {
            get
            {
                return (this.Capabilities & 128) != 0;
            }
        }
        [Ignore]
        public bool AllowSoulSummon
        {
            get
            {
                return (this.Capabilities & 256) != 0;
            }
        }
        [Ignore]
        public bool AllowTavernRegen
        {
            get
            {
                return (this.Capabilities & 512) != 0;
            }
        }
        [Ignore]
        public bool AllowTombMode
        {
            get
            {
                return (this.Capabilities & 1024) != 0;
            }
        }
        [Ignore]
        public bool AllowTeleportEverywhere
        {
            get
            {
                return (this.Capabilities & 2048) != 0;
            }
        }
        [Ignore]
        public bool AllowFightChallenges
        {
            get
            {
                return (this.Capabilities & 4096) != 0;
            }
        }

        public MapPositionRecord(int id, int x, int y, string name, bool outdoor, int capabilities)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
            this.Name = name;
            this.Outdoor = outdoor;
            this.Capabilities = capabilities;
        }

        public static MapPositionRecord GetMapPosition(int mapId)
        {
            return MapPositions.Find(x => x.Id == mapId);
        }
        public override string ToString()
        {
            return string.Format("[{0},{1}]", X, Y);
        }

    }
}
