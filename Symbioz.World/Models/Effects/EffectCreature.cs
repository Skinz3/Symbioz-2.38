using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Effects
{
    public class EffectCreature : Effect
    {
        public ushort MonsterFamilyId
        {
            get;
            set;
        }

        public EffectCreature()
        {
        }

        public EffectCreature(ushort effectId, ushort monsterFamilyId)
            : base(effectId)
        {
            this.MonsterFamilyId = monsterFamilyId;
        }

        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectCreature(EffectId, MonsterFamilyId);
        }

        public override bool Equals(object obj)
        {
            return obj is EffectCreature ? this.Equals(obj as EffectCreature) : false;
        }
        public bool Equals(EffectCreature effect)
        {
            return this.EffectId == effect.EffectId && this.MonsterFamilyId == effect.MonsterFamilyId;
        }
    }
}