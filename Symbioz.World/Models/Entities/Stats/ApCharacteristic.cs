using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Entities.Stats
{
    public class ApCharacteristic : LimitCharacteristic
    {
        [YAXDontSerialize]
        public override short Limit
        {
            get
            {
                return WorldConfiguration.Instance.ApLimit;
            }
        }

        [YAXDontSerialize]
        public override bool ContextLimit
        {
            get
            {
                return false;
            }
        }
        public static new ApCharacteristic New(short @base)
        {
            return new ApCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }
        public override Characteristic Clone()
        {
            return new ApCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Context = Context,
                Objects = Objects
            };
        }
    }
}
