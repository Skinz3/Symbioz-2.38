using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using Symbioz.Core;

namespace Symbioz.World.Providers.Fights.Spells.Ecaflip
{
    /// <summary>
    /// Todo
    /// </summary>
    [CustomSpellHandler(114)]
    public class Rekop : CustomSpellHandler
    {
        private Fighter Target
        {
            get;
            set;
        }
        public int TurnTrigger
        {
            get;
            private set;
        }
        public Rekop(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit) : base(source, level, castPoint, criticalHit)
        {
            TurnTrigger = new AsyncRandom().Next(1, 4);
            Target = Source.Fight.GetFighter(CastPoint);
        }

        public override void Execute()
        {
            Target.OnTurnStartEvt += Target_OnTurnStartEvt;
        }

        private void Target_OnTurnStartEvt(Fighter obj)
        {
            TurnTrigger--;

            if (TurnTrigger == 0)
            {
                obj.Fight.SequencesManager.StartSequence(Protocol.Enums.SequenceTypeEnum.SEQUENCE_SPELL);
                DefaultHandler(GetEffects().Take(4), Target.Point);
                obj.Fight.SequencesManager.EndSequence(Protocol.Enums.SequenceTypeEnum.SEQUENCE_SPELL);
                Target.OnTurnStartEvt -= Target_OnTurnStartEvt;
            }
        }
    }
}
