using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Entities.Stats
{
    public class Characteristic
    {
        public virtual short Base
        {
            get;
            set;
        }

        public virtual short Additional
        {
            get;
            set;
        }

        public virtual short Objects
        {
            get;
            set;
        }

        [YAXDontSerialize]
        public virtual short Context
        {
            get;
            set;
        }

        public virtual Characteristic Clone()
        {
            return new Characteristic()
            {
                Additional = Additional,
                Base = Base,
                Context = Context,
                Objects = Objects
            };
        }
        public static Characteristic Zero()
        {
            return New(0);
        }
        public static Characteristic New(short @base)
        {
            return new Characteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }
        public CharacterBaseCharacteristic GetBaseCharacteristic()
        {
            return new CharacterBaseCharacteristic(Base, Additional, Objects, Context, Context);
        }
        public virtual short Total()
        {
            return (short)(Base + Additional + Objects);
        }
        public virtual short TotalInContext()
        {
            return (short)(Total() + Context);
        }
    }
}
