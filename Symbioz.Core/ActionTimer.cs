using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Symbioz.Core
{
    public class ActionTimer
    {
        private Action m_action;
        private Timer m_timer;


        public ActionTimer(int interval, Action action, bool loop)
        {
            this.m_action = action;
            this.m_timer = new Timer(interval);
            this.m_timer.Elapsed += m_timer_Elapsed;
            this.m_timer.AutoReset = loop;
        }
        /// <summary>
        /// En secondes
        /// </summary>
        public double Interval
        {
            get
            {
                return m_timer.Interval;
            }
            set
            {
                m_timer.Interval = value * 1000;
            }
        }
        void m_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m_action();

            if (!m_timer.AutoReset)
            {
                Stop();

            }
        }

        public void Start()
        {
            m_timer.Start();
        }
        public void Pause()
        {
            m_timer.Stop();
        }
        public void Stop()
        {
            m_timer.Dispose();
            m_action = null;
        }
    }
}
