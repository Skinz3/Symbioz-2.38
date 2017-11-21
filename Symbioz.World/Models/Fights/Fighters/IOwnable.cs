using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.Fighters
{
    public interface IOwnable
    {
       bool IsOwner(Fighter fighter);

    }
}
