using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Entities.Stats
{
    public class RangeCharacteristic : LimitCharacteristic
    {
        public const short RANGE_LIMIT = 6;

        [YAXDontSerialize]
        public override short Limit
        {
            get
            {
                return RANGE_LIMIT;
            }
        }

        [YAXDontSerialize]
        public override bool ContextLimit
        {
            get
            {
                return true;
            }
        }

        public static RangeCharacteristic New(short @base)
        {
            return new RangeCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }

        public static RangeCharacteristic Zero()
        {
            return New(0);
        }

        public override Characteristic Clone()
        {
            return new RangeCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Context = Context,
                Objects = Objects
            };
        }
    }
}
