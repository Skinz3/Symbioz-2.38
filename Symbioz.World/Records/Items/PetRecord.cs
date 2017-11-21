using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.World.Models.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Items
{
    [Table("Pets")]
    public class PetRecord : ITable
    {
        public static List<PetRecord> Pets = new List<PetRecord>();

        [Primary]
        public ushort Id;

        [Xml]
        public List<EffectInstance> Effects;

        public PetRecord(ushort id, List<EffectInstance> effects)
        {
            this.Id = id;
            this.Effects = effects;
        }


    }
}
