using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Entities.Stats
{
    public class MpCharacteristic : LimitCharacteristic
    {
        [YAXDontSerialize]
        public override short Limit
        {
            get
            {
                return WorldConfiguration.Instance.MpLimit;
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

        public static MpCharacteristic New(short @base)
        {
            return new MpCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }
        public override Characteristic Clone()
        {
            return new MpCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Context = Context,
                Objects = Objects
            };
        }
    }
}
