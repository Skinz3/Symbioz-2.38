using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Spells.Steamer
{
    /// <summary>
    /// Harponneuse
    /// Pose une tourelle offensive qui vole de la vie aux ennemis
    /// et alliés à portée dans l élément Eau, Terre ou Feu.
    /// Son élément d attaque peut être fixé par son invocateur si
    /// il la cible avec un de ses sorts élémentaires.
    /// </summary>
    [CustomSpellHandler(3212)]
    public class Harpoon : CustomSpellHandler
    {
        public Harpoon(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit)
            : base(source, level, castPoint, criticalHit)
        {

        }
        public override void Execute()
        {
            this.DefaultHandler(GetEffects().Reverse());
        }
    }
    /// <summary>
    /// Gardienne
    /// Pose une tourelle défensive qui soigne les alliés et les ennemis dans sa zone d effet.
    /// Les soins sont indexés sur la vie maximale des cibles.
    /// </summary>
    [CustomSpellHandler(3213)]
    public class Guardian : CustomSpellHandler
    {
        public Guardian(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit)
            : base(source, level, castPoint, criticalHit)
        {

        }
        public override void Execute()
        {
            this.DefaultHandler(GetEffects().Reverse());
        }
    }

    /// <summary>
    /// Tactirelle
    /// Pose une tourelle tactique qui attire ou repousse les ennemis et les alliés.
    /// </summary>
    [CustomSpellHandler(3214)]
    public class Tacticalturret : CustomSpellHandler
    {
        public Tacticalturret(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit)
            : base(source, level, castPoint, criticalHit)
        {

        }
        public override void Execute()
        {
            this.DefaultHandler(GetEffects().Reverse());
        }
    }
}
