using Symbioz.World.Models.Maps;
using Symbioz.World.Records;
using Symbioz.World.Records.Maps;
using System;
using System.Linq;
using System.Xml.Serialization;
using YAXLib;

namespace Symbioz.World.Providers.Maps.PlacementPattern
{
    public class PlacementPattern
    {
        public bool Relativ
        {
            get;
            set;
        }

        public System.Drawing.Point[] Blues
        {
            get;
            set;
        }

        public System.Drawing.Point[] Reds
        {
            get;
            set;
        }

        public Point Center
        {
            get;
            set;
        }

        [YAXDontSerialize]
        public int Complexity
        {
            get;
            set;
        }

        public bool TestPattern(MapRecord map)
        {
            bool result;
            try
            {
                bool bluesOk;
                bool redsOk;
                if (this.Relativ)
                {
                    bluesOk = this.Blues.All((System.Drawing.Point entry) => map.Walkable(entry.X + this.Center.X, entry.Y + this.Center.Y));
                    redsOk = this.Reds.All((System.Drawing.Point entry) => map.Walkable(entry.X + this.Center.X, entry.Y + this.Center.Y));
                }
                else
                {
                    bluesOk = this.Blues.All((System.Drawing.Point entry) => map.Walkable(entry.X, entry.Y));
                    redsOk = this.Reds.All((System.Drawing.Point entry) => map.Walkable(entry.X, entry.Y));
                }
                result = (bluesOk && redsOk);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public bool TestPattern(System.Drawing.Point center, MapRecord map)
        {
            bool result;
            try
            {
                bool bluesOk;
                bool redsOk;
                if (this.Relativ)
                {
                    bluesOk = this.Blues.All((System.Drawing.Point entry) => map.Walkable(entry.X + center.X, entry.Y + center.Y));
                    redsOk = this.Reds.All((System.Drawing.Point entry) => map.Walkable(entry.X + center.X, entry.Y + center.Y));
                }
                else
                {
                    bluesOk = this.Blues.All((System.Drawing.Point entry) => map.Walkable(entry.X, entry.Y));
                    redsOk = this.Reds.All((System.Drawing.Point entry) => map.Walkable(entry.X, entry.Y));
                }
                result = (bluesOk && redsOk);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
