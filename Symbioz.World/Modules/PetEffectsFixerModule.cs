using Symbioz.Core;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models.Effects;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Modules
{
    class PetEffectsFixerModule
    {
        static Logger logger = new Logger();

        [StartupInvoke("Pet Effects", StartupInvokePriority.Modules)]
        public static void Initialize()
        {
            foreach (var pet in PetRecord.Pets)
            {
                ItemRecord item = ItemRecord.GetItem(pet.Id);

                if (item != null)
                    item.Effects = new List<EffectInstance>(pet.Effects);
                else
                    logger.Alert("Cannot find pet: " + pet.Id);

            }
        }

    }
}
