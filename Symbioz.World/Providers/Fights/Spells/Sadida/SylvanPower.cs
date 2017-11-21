using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Spells.Sadida
{
    /// <summary>
    /// Puissance Silvestre Invoque une groute qui meurt a la fin de son tour
    /// </summary>
    [CustomSpellHandler(197)]
    public class SylvanPower : CustomSpellHandler
    {
        public SylvanPower(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit)
            : base(source, level, castPoint, criticalHit)
        {

        }
        public override void Execute()
        {
            DefaultHandler();

            var summoned = Source.GetLastSummon();

            if (summoned is ControlableMonsterFighter && ((ControlableMonsterFighter)summoned).MonsterId == 4010)
            {
                summoned.OnTurnEndEvt += Summoned_OnTurnEndEvt;
            }
        }

        private void Summoned_OnTurnEndEvt(Fighter obj)
        {
            obj.Stats.CurrentLifePoints = 0;
            obj.Die(obj);
        }
    }
}
