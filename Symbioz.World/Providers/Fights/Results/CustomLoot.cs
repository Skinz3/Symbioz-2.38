using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Results
{
    /// <summary>
    /// Représente un loot exeptionnel.
    /// Ex Officiel : Drop de pépite.
    /// </summary>
    public abstract class CustomLoot
    {
        protected FightPlayerResult Result
        {
            get;
            private set;
        }
        public CustomLoot(FightPlayerResult result)
        {
            this.Result = result;
        }

        public abstract void Apply();

        protected void Add(CharacterItemRecord item)
        {
            Result.Loot.AddItem(item.GId, item.Quantity);
            Result.Character.Inventory.AddItem(item);
        }
        protected void Add(ushort gid, uint quantity)
        {
            ItemRecord template = ItemRecord.GetItem(gid);
            CharacterItemRecord item = template.GetCharacterItem(Result.Id, quantity, false);
            this.Add(item);
        }
        protected void Add(ItemRecord template,uint quantity)
        {
            CharacterItemRecord item = template.GetCharacterItem(Result.Id, quantity, false);
            this.Add(item);
        }
    }
}
