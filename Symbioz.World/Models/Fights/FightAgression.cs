using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Models.Fights.Results;
using Symbioz.Core;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Selfmade.Enums;

namespace Symbioz.World.Models.Fights
{
    public class FightAgression : Fight
    {
        public override FightTypeEnum FightType
        {
            get
            {
                return FightTypeEnum.FIGHT_TYPE_AGRESSION;
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

        public FightAgression(MapRecord map, FightTeam blueTeam, FightTeam redTeam, short cellId)
            : base(map, blueTeam, redTeam, cellId)
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
                FightPlayerResult result = (FightPlayerResult)fighter.GetFightResult();
                result.SetEarnedHonor(CalculateEarnedHonor(fighter), 0);
                results.Add(result);
            }
            return results;
        }
        public override int GetPreparationDelay()
        {
            return 30;
        }
        public short CalculateEarnedHonor(CharacterFighter characterFighter)
        {
            short result;
            if (characterFighter.OposedTeam().Side == AlignmentSideEnum.ALIGNMENT_NEUTRAL || characterFighter.GetFighterOutcome() == FightOutcomeEnum.RESULT_DRAW)
            {
                result = 0;
            }
            else
            {
                double num = (double)base.Winners.GetAllFightersWithLeavers().Sum((Fighter entry) => (int)entry.Level);
                double num2 = (double)base.Losers.GetAllFightersWithLeavers().Sum((Fighter entry) => (int)entry.Level);
                double num3 = System.Math.Floor(System.Math.Sqrt((double)characterFighter.Level) * 10.0 * (num2 / num));
                if (base.Losers == characterFighter.Team)
                {
                    num3 = -num3;
                }
                result = (short)num3;
            }
            return result;
        }
        public override FightCommonInformations GetFightCommonInformations()
        {
            return new FightCommonInformations(Id, (sbyte)FightType, GetFightTeamInformations(), new ushort[]{
                BlueTeam.BladesCellId,RedTeam.BladesCellId}, GetFightOptionsInformations());
        }
    }
}
