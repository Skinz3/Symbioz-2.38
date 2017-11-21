using Symbioz.Core;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.FightModels
{
    public class Synchronizer
    {
        public static readonly int CheckTimeout = 6000;

        private readonly List<PlayableFighter> m_fighters;
        private readonly Fight m_fight;
        private bool m_started;
        private ActionTimer m_timer;
        public event System.Action<Synchronizer> Success;
        public event Action<Synchronizer, PlayableFighter[]> Timeout;

        private void NotifySuccess()
        {
            System.Action<Synchronizer> success = this.Success;
            if (success != null)
            {
                success(this);
            }
        }
        private void NotifyTimeout(PlayableFighter[] laggers)
        {
            Action<Synchronizer, PlayableFighter[]> timeout = this.Timeout;
            if (timeout != null)
            {
                timeout(this, laggers);
            }
        }
        public Synchronizer(Fight fight, IEnumerable<PlayableFighter> actors)
        {
            this.m_fight = fight;
            this.m_fighters = actors.ToList<PlayableFighter>();
        }
        public void Start()
        {
            if (!this.m_started)
            {
                this.m_started = true;
                if (this.m_fighters.Count <= 0)
                {
                    this.NotifySuccess();
                }
                else
                {
                    foreach (CharacterFighter current in this.m_fighters)
                    {
                        current.Character.Client.Send(new GameFightTurnReadyRequestMessage(m_fight.FighterPlaying.Id));
                    }

                    this.m_timer = new ActionTimer(Synchronizer.CheckTimeout, this.TimedOut, false);
                    this.m_timer.Start();
                }
            }

        }

        public void Cancel()
        {
            this.m_started = false;
            if (this.m_timer != null)
            {
                this.m_timer.Stop();
            }
        }
        public void ToggleReady(CharacterFighter actor, bool ready = true)
        {
            if (this.m_started)
            {
                if (ready && this.m_fighters.Contains(actor))
                {
                    this.m_fighters.Remove(actor);
                }
                else
                {
                    if (!ready)
                    {
                        this.m_fighters.Add(actor);
                    }
                }
                if (this.m_fighters.Count == 0) 
                {
                    if (this.m_timer != null)
                    {
                        this.m_timer.Stop();
                    }
                    this.NotifySuccess();
                }
            }
        }
        private void TimedOut()
        {
            if (this.m_started && this.m_fighters.Count > 0)
            {
                this.NotifyTimeout(this.m_fighters.ToArray());
            }
        }
        public static Synchronizer RequestCheck(Fight fight, Action success, System.Action<PlayableFighter[]> fail)
        {
            List<PlayableFighter> fighters = fight.GetFighters<CharacterFighter>(false).ConvertAll<PlayableFighter>(x => (PlayableFighter)x);
            return RequestCheck(fight, success, fighters, fail);

        }
        public static Synchronizer RequestCheck(Fight fight, Action success, List<PlayableFighter> fighters, System.Action<PlayableFighter[]> fail)
        {
            Synchronizer readyChecker = new Synchronizer(fight, fighters);
            readyChecker.Success += delegate (Synchronizer chk)
            {
                success();
            };
            readyChecker.Timeout += delegate (Synchronizer chk, PlayableFighter[] laggers)
            {
                fail(laggers);
            };
            readyChecker.Start();
            return readyChecker;
        }
    }
}
