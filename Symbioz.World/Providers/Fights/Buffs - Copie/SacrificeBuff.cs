using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Damages;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Buffs
{
    public class SacrificeBuff : Buff
    {
        public override FighterEvent EventType
        {
            get
            {
                return FighterEvent.BeforeAttacked;
            }
        }
        public SacrificeBuff(Fighter source, Fighter target, short delta,
         EffectInstance effect, ushort spellId)
            : base(source, target, delta, effect, spellId)
        {

        }
        public override void Apply()
        {
            base.Apply();
        }

        public override void Dispell()
        {

        }
        public override bool OnTriggered(object arg1, object arg2, object arg3)
        {
            Source.InflictDamages((Damage)arg1);
            return true;
        }
    }
}
