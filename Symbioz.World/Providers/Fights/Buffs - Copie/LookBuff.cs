using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class LookBuff : Buff
    {
        public LookBuff(Fighter source, Fighter target, short delta,
         EffectInstance effect, ushort spellId)
            : base(source, target, delta, effect, spellId)
        {
        }
        public override void Apply()
        {
            ContextActorLook newLook = FightLookProvider.TransformLook(Target, Target.RealLook.Clone(), SpellId);
            base.Target.ChangeLook(newLook, Source);
            base.Apply();
        }

        public override void Dispell()
        {
            base.Target.ChangeLook(Target.RealLook.Clone(), Source);
        }

        
    }
}
