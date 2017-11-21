using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Actions
{
    public abstract class BrainAction
    {
        public BrainFighter Fighter
        {
            get;
            set;
        }

        public BrainAction(BrainFighter fighter)
        {
            this.Fighter = fighter;
        }
        /// <summary>
        /// Analyse le contexte 
        /// </summary>
        public abstract void Analyse();
        /// <summary>
        /// Execute l'action
        /// </summary>
        public abstract void Execute();

    }
}
