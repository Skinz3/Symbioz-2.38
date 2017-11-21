using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public interface ISummon<T> : IOwnable where T : Fighter
    {
        T Owner
        {
            get;
            set;
        }
    }
}
