using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using Symbioz.World.Models.Effects;
using Symbioz.Protocol.Selfmade.Enums;

namespace Symbioz.World.Providers.Fights.Spells.Special
{
    [CustomSpellHandler(5894)]
    public class Leukide : CustomSpellHandler
    {
        private CharacterFighter Owner
        {
            get;
            set;
        }
        public Leukide(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit) : base(source, level, castPoint, criticalHit)
        {
            this.Owner = (CharacterFighter)source;

            foreach (var enemy in Owner.OposedTeam().GetFighters())
            {
                enemy.OnDamageTaken += Enemy_OnDamageTaken;
            }
        }

        private void Enemy_OnDamageTaken(Fighter arg1, Models.Fights.Damages.Damage arg2)
        {
            if (arg2.Source.IsFriendly(Owner))
            {
                this.Source = arg1;

                foreach (var fighter in Owner.Team.GetFighters())
                {
                    fighter.Abilities.PullForward(arg1, 63, arg1.Point);
                }

                var effect = GetEffect(EffectsEnum.Effect_FinalDamageDamagePercent);
                DefaultHandler(new EffectInstance[] {  effect}, arg1.Point);
            }
        }

        public override void Execute()
        {

        }
    }
    [CustomSpellHandler(5885)]
    public class Dynamo : CustomSpellHandler
    {

        public Dynamo(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit) : base(source, level, castPoint, criticalHit)
        {
           
        }

        private void Enemy_OnDamageTaken(Fighter arg1, Models.Fights.Damages.Damage arg2)
        {
           
        }

        public override void Execute()
        {
          
            foreach (var fighter in Source.OposedTeam().GetFighters())
            {
                DefaultHandler(GetEffects(), fighter.Point);
            }
           
        }
    }
}