using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Items
{
    public class CertificateProvider : Singleton<CertificateProvider>
    {
        public void OnCertificateDestroyed(Character character, CharacterItemRecord item)
        {
            CharacterMountRecord record = character.Inventory.GetMount(item);
            record.RemoveElement();
            character.Inventory.Mounts.Remove(record);
        }

    }
}
