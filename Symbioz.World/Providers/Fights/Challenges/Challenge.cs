using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Fights.Challenges
{
    public abstract class Challenge
    {
        public ushort Id
        {
            get
            {
                return (ushort)Template.Id;
            }
        }
        protected ChallengeRecord Template
        {
            get;
            private set;
        }
        protected Fight Fight
        {
            get
            {
                return Team.Fight;
            }
        }
        protected FightTeam Team
        {
            get;
            private set;
        }
        protected ChallengeResultEnum Result
        {
            get;
            private set;
        }
        public Challenge(ChallengeRecord template, FightTeam team)
        {
            this.Template = template;
            this.Team = team;
        }
        public abstract void BindEvents();

        public abstract void UnBindEvents();

        public virtual void Initialize()
        {
            this.Result = ChallengeResultEnum.UNKNOWN;
            this.Fight.OnFightEndedEvt += OnFightEnded;
            this.BindEvents();
        }

        void OnFightEnded(Fight fight, bool obj)
        {
            if (obj && Result == ChallengeResultEnum.UNKNOWN)
            {
                if (Team == Fight.Winners)
                    OnChallengeResulted(ChallengeResultEnum.SUCCES);
                else
                    OnChallengeResulted(ChallengeResultEnum.FAILED);
            }
        }

        public virtual int GetTargetId()
        {
            return 0;
        }
        public virtual short GetTargetedCell()
        {
            return 0;
        }
        public abstract int XpBonusPercent
        {
            get;
        }

        public abstract int DropBonusPercent
        {
            get;
        }

        public virtual bool IsSucces()
        {
            return this.Result == ChallengeResultEnum.SUCCES;
        }
        /// <summary>
        /// Challenge can be added?
        /// </summary>
        /// <returns></returns>
        public virtual bool Valid()
        {
            return true;
        }

        protected virtual void OnChallengeResulted(ChallengeResultEnum result)
        {
            this.Result = result;

            Fight.Send(new ChallengeResultMessage(Id, this.IsSucces()));

            UnBindEvents();
        }

        protected virtual void OnChallengeTargetUpdated()
        {
            Fight.Send(new ChallengeTargetUpdateMessage(Id, GetTargetId()));
        }
        protected void OnTargetUpdated()
        {
            Fight.Send(new ChallengeTargetUpdateMessage(Id, GetTargetId()));
        }
        internal void ShowTargetsList(PlayableFighter fighter)
        {
            double[] targetIds = new double[] { GetTargetId() };
            short[] targetedCells = new short[] { GetTargetedCell() };
            fighter.Send(new ChallengeTargetsListMessage(targetIds, targetedCells));
        }
    }
}
