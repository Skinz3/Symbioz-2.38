using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Providers.Arena;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights
{
    public class FightArena : Fight
    {
        public override FightTypeEnum FightType
        {
            get
            {
                return FightTypeEnum.FIGHT_TYPE_PVP_ARENA;
            }
        }

        public override bool SpawnJoin
        {
            get
            {
                return true;
            }
        }

        public override bool PvP
        {
            get
            {
                return true;
            }
        }

        public override bool ShowBlades
        {
            get
            {
                return false;
            }
        }

        public override bool MinationAllowed
        {
            get
            {
                return false;
            }
        }
        public FightArena(MapRecord map, FightTeam blueTeam, FightTeam redTeam)
            : base(map, blueTeam, redTeam, 0)
        {
        }


        public override void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            fighter.Character.Client.Send(new GameFightJoinMessage(true, !base.Started, false, base.Started, this.GetPlacementTimeLeft(), (sbyte)this.FightType));
        }

        protected override IEnumerable<IFightResult> GenerateResults()
        {
            List<IFightResult> results = new List<IFightResult>();
            foreach (var fighter in GetFighters<CharacterFighter>(false))
            {
                if (fighter.Team == Winners)
                {
                    var emote = Records.Items.ItemRecord.RandomItem(Protocol.Selfmade.Enums.ItemTypeEnum.PARCHEMIN_D_ATTITUDE).Id;
                    uint quantity = (uint)(fighter.Team.GetTeamLevel() / 2);
                    FightPlayerResult result = (FightPlayerResult)fighter.GetFightResult();
                    result.Loot.AddItem(12736, quantity);
                    result.Loot.AddItem(emote, 1);
                    results.Add(result);
                }
                else
                {
                    FightPlayerResult result = (FightPlayerResult)fighter.GetFightResult();
                    result.Loot.AddItem(12736, 1);
                    results.Add(result);
                }
            }
            return results;
        }
        public override int GetPreparationDelay()
        {
            return 30;
        }
        /// <summary>
        /// ShowBlades = false;
        /// </summary>
        /// <returns></returns>
        public override FightCommonInformations GetFightCommonInformations()
        {
            throw new NotImplementedException();
        }
    }
}
