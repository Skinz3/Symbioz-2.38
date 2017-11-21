using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YAXLib;

namespace Symbioz.World.Models.Entities.HumanOptions
{
    public class CharacterHumanOptionFollowers : CharacterHumanOption
    {
        public CharacterHumanOptionFollowers()
        {
            this.Looks = new Dictionary<sbyte, ContextActorLook>();
        }
        public CharacterHumanOptionFollowers(ContextActorLook follower)
        {
            this.Looks = new Dictionary<sbyte, ContextActorLook>();
            AddFollower(follower);
        }
        public Dictionary<sbyte, ContextActorLook> Looks
        {
            get;
            set;
        }
        public void AddFollower(ContextActorLook look)
        {
            if (Looks.Count > 0)
                Looks.Add((sbyte)(Looks.Keys.Last() + 1), look);
            else
                Looks.Add(1, look);
        }
        public void RemoveFollower(ContextActorLook look)
        {
            var data = Looks.FirstOrDefault(x => x.Value.Equals(look));

            if (data.Value != null)
                Looks.Remove(data.Key);
        }
        public override HumanOption GetHumanOption()
        {
            return new HumanOptionFollowers(Looks.ToList().ConvertAll<IndexedEntityLook>(x => new IndexedEntityLook(x.Value.ToEntityLook(), x.Key)).ToArray());
        }
    }
}