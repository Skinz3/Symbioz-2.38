using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Spells;
using Symbioz.World.Models.Maps;
using Symbioz.World.Providers.Fights.Effects;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Spells.Sadida
{
    /// <summary>
    /// Tremblement	Dans une zone de 3 cases autour de chaque arbre allié :
    /// • Occasionne des dommages Feu.
    /// • Attire les cibles.
    /// • Détecte les invisibles.
    /// </summary>
    [CustomSpellHandler(181)]
    public class Tremor : CustomSpellHandler
    {
        public Tremor(Fighter source, SpellLevelRecord level, MapPoint castPoint, bool criticalHit)
            : base(source, level, castPoint, criticalHit)
        {

        }
        public override void Execute()
        {
            Source.OnTurnEndEvt += OnTurnEnded;
        }
        void OnTurnEnded(Fighter obj)
        {
            var maskEffect = this.GetEffect(EffectsEnum.Effect_CastSpell);

            var treeTargets = SpellEffectsManager.Instance.GetAffectedFighters(Source, maskEffect.GetZone(), CastPoint, maskEffect.TargetMask);

            foreach (var tree in treeTargets)
            {
                SpellEffectsManager.Instance.HandleEffects(tree, Level, tree.Point, CriticalHit);
            }
            Source.OnTurnEndEvt -= OnTurnEnded;
        }
    }
}
