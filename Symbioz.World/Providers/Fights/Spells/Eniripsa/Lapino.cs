using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Buffs;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Spells.Eniripsa
{
    [CustomSpellHandler(129)]
    public class Lapino : CustomSpellHandler
    {
        private Fighter Summon;

        public Lapino(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit)
            : base(source, level, castPoint, criticalHit)
        {

        }
        public override void Execute()
        {
            var effects = GetEffects().ToList();
            var effect = effects[1];
            effects.RemoveAt(1);
            DefaultHandler(effects);
            this.Summon = Source.GetLastSummon();
            AddTriggerBuff(Summon, 0, TriggerType.BEFORE_DEATH, Level, effect, Level.SpellId, 0, OnLapinoDie, -1);
        }

        private bool OnLapinoDie(TriggerBuff buff, TriggerType trigger, object token)
        {


            DefaultHandler(new EffectInstance[] { GetEffects().ToArray()[1] }, Summon.Point);
            return false;
        }
    }
}
