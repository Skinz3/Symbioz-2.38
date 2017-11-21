using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Effects
{
    public class EffectLadder : Effect
    {
        public ushort MonsterFamilyId { get; set; }

        public uint MonsterCount { get; set; }

        public EffectLadder() { }

        public EffectLadder(ushort effectId, ushort monsterFamilyId, uint monsterCount)
            : base(effectId)
        {
            this.MonsterFamilyId = monsterFamilyId;
            this.MonsterCount = monsterCount;
        }
        public override ObjectEffect GetObjectEffect()
        {
            return new ObjectEffectLadder(EffectId, MonsterFamilyId, MonsterCount);
        }
        public override bool Equals(object obj)
        {
            return obj is EffectLadder ? this.Equals(obj as EffectLadder) : false;
        }
        public bool Equals(EffectLadder effect)
        {
            return this.EffectId == effect.EffectId && this.MonsterFamilyId == effect.MonsterFamilyId && this.MonsterCount == effect.MonsterCount;
        }
    }
}
