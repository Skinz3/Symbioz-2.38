using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights
{
    public class FightDual : Fight
    {
        public override FightTypeEnum FightType
        {
            get
            {
                return FightTypeEnum.FIGHT_TYPE_CHALLENGE;
            }
        }

        public override bool SpawnJoin
        {
            get
            {
                return false;
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
                return true;
            }
        }

        public override bool MinationAllowed
        {
            get
            {
                return false;
            }
        }

        public FightDual(MapRecord map, FightTeam blueTeam, FightTeam redTeam, short cellId)
            : base(map, blueTeam, redTeam, cellId)
        {

        }
        public override void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            fighter.Character.Client.Send(new GameFightJoinMessage(true, false, true, false, 0, (sbyte)FightType));
        }
        /// <summary>
        /// Ajouter un drop?
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IFightResult> GenerateResults()
        {
            return
                    from entry in base.GetAllFightersWithLeavers()
                    where !(entry is IOwnable)
                    select entry into fighter
                    select fighter.GetFightResult();

        }

        public override FightCommonInformations GetFightCommonInformations()
        {
            return new FightCommonInformations(Id, (sbyte)FightType, GetFightTeamInformations(),
                new ushort[] { BlueTeam.BladesCellId, RedTeam.BladesCellId }, GetFightOptionsInformations());
        }
    }
}
