using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Entities.Stats
{
    public class ResistanceCharacteristic : LimitCharacteristic
    {
        public const short RESISTANCE_PERCENTAGE_LIMIT = 50;

        [YAXDontSerialize]
        public override short Limit
        {
            get
            {
                return RESISTANCE_PERCENTAGE_LIMIT;
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

        public static new ResistanceCharacteristic New(short @base)
        {
            return new ResistanceCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }

        public static ResistanceCharacteristic Zero()
        {
            return New(0);
        }

        public override Characteristic Clone()
        {
            return new ResistanceCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Context = Context,
                Objects = Objects
            };
        }
    }
}
