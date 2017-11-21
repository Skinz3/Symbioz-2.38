using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Fights.Marks;
using Symbioz.World.Models.Maps;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_SpawnPortal)]
    public class PortalSpawn : SpellEffectHandler
    {
        public PortalSpawn(Fighter source, SpellLevelRecord spellLevel, EffectInstance effect,
           Fighter[] targets, MapPoint castPoint, bool critical)
            : base(source, spellLevel, effect, targets, castPoint, critical)
        {

        }
        public override bool Apply(Fighter[] targets)
        {
            if (!Source.Team.CanSpawnPortal())
            {
                Source.Team.RemoveFirstPortal(Source);
            }
            Zone zone = new Zone(Effect.ShapeType, Effect.Radius);
            Color color = GetColorByTeam(Source.Team);
            Portal portal = new Portal(Fight.PopNextMarkId(), Source, SpellLevel, Effect, CastPoint, zone, color);
            Fight.AddMark(portal);

            //if (Source.Team.GetAllPortals().Length <= 2)
            //{
            //    ContextHandler.SendGameActionFightActivateGlyphTrapMessage(Fight.Clients, 1, 1181, Caster, Caster.Fight.GetAllPortalsByTeam(Caster.Team).Count > 1);
            //}
            return true;

        }

        private static Color GetColorByTeam(FightTeam team)
        {
            return team.TeamEnum == TeamEnum.TEAM_CHALLENGER ? Color.Blue : Color.Red;
        }
    }
}
