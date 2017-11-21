using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Entities.Stats
{
    public abstract class LimitCharacteristic : Characteristic
    {
        [YAXDontSerialize]
        public abstract short Limit
        {
            get;
        }

        [YAXDontSerialize]
        public abstract bool ContextLimit
        {
            get;
        }

        public override short Total()
        {
            short total = base.Total();
            return total > Limit ? Limit : total;
        }
        public override short TotalInContext()
        {
            if (ContextLimit)
            {
                short total = base.TotalInContext();
                return total > Limit ? Limit : total;
            }
            else
            {
                return base.TotalInContext();
            }
        }
    }
}
