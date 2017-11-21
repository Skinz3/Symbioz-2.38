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

namespace Symbioz.World.Providers.Fights.Effects.Summons
{
    [SpellEffectHandler(EffectsEnum.Effect_Revive)]
    public class Revive : SpellEffectHandler
    {
        private Fighter RevivedFighter
        {
            get;
            set;
        }
        public Revive(Fighter source, SpellLevelRecord level, EffectInstance effect,
          Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, level, effect, targets, castPoint, critical)
        {
        }
        public override bool Apply(Fighter[] targets)
        {
            Fighter target = Source.Team.LastDead();

            if (target != null)
            {
                this.RevivedFighter = target;
                this.Fight.ReviveFighter(Source, target, CastPoint.CellId, (short)Effect.DiceMin);
                this.Source.BeforeDeadEvt += Source_OnDeadEvt;
            }

            return true;
        }

        void Source_OnDeadEvt(Fighter obj)
        {
            RevivedFighter.Die(Source);
        }

    }
}
