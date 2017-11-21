using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Brain.Behaviors
{
    [Behavior("Powa")]
    public class Powa : Behavior
    {
        public Powa(BrainFighter fighter)
            : base(fighter)
        {
            base.Fighter.BeforeDeadEvt += Fighter_OnDeadEvt;
            base.Fighter.AfterSlideEvt += Fighter_OnSlideEvt;
            this.Fighter.OnTurnEndEvt += Fighter_OnTurnEndEvt;
            this.Fighter.OnDamageTaken += Fighter_OnDamageTaken;
        }

        private void Fighter_OnDamageTaken(Fighter arg1, Models.Fights.Damages.Damage arg2)
        {


        }

        private void Fighter_OnTurnEndEvt(Fighter obj)
        {

            EffectInstance effect = EffectInstance.New(Protocol.Selfmade.Enums.EffectsEnum.Effect_SwitchPosition, 0, 0, 0, 0, 2, "P1", "a#A");
            var level = CreateBasicSpellLevel(3, new List<EffectInstance>() { effect }, 24);

            var fighter = obj.OposedTeam().LowerFighterPercentage();
            if (fighter != null)
            Fighter.ForceSpellCast(level, fighter.CellId);
        }




        private void Fighter_OnSlideEvt(Fighter target, Fighter source, short startCellId, short endCellId)
        {


            EffectInstance effect = EffectInstance.New(Protocol.Selfmade.Enums.EffectsEnum.Eff_AddShield, 0, 500, 500, 0, 2, "a1", "a");
            var level = CreateBasicSpellLevel(3, new List<EffectInstance>() { effect }, 2216);

            Fighter.ForceSpellCast(level, Fighter.CellId);

        }

        private void Fighter_OnDeadEvt(Fighter obj)
        {

        }
    }
}
