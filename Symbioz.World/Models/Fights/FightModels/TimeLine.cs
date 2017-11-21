using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights.FightModels
{
    public class TimeLine
    {
        internal List<Fighter> Fighters
        {
            get;
            private set;
        }
        public Fight Fight
        {
            get;
            private set;
        }
        public Fighter Current
        {
            get
            {
                return (this.Index == -1 || this.Index >= this.Fighters.Count) ? null : this.Fighters[this.Index];
            }
        }
        public int Index
        {
            get;
            private set;
        }
        public int Count
        {
            get
            {
                return this.Fighters.Count;
            }
        }
        public uint RoundNumber
        {
            get;
            private set;
        }
        public bool NewRound
        {
            get;
            private set;
        }
        public TimeLine(Fight fight)
        {
            this.Fight = fight;
            this.Fighters = new List<Fighter>();
            this.RoundNumber = 1;
        }
        public bool RemoveFighter(Fighter fighter)
        {
            bool result;
            if (!this.Fighters.Contains(fighter))
            {
                result = false;
            }
            else
            {
                int num = this.Fighters.IndexOf(fighter);
                this.Fighters.Remove(fighter);
                if (num <= this.Index && num > 0)
                {
                    this.Index--;
                }
                result = true;
            }
            return result;
        }
        public bool InsertFighter(Fighter fighter, int index)
        {
            bool result;
            if (index > this.Fighters.Count)
            {
                result = false;
            }
            else
            {
                this.Fighters.Insert(index, fighter);
                if (index <= this.Index)
                {
                    this.Index++;
                }
                result = true;
            }
            return result;
        }
        public bool SelectNextFighter()
        {
            bool result;
            if (this.Fighters.Count == 0)
            {
                this.Index = -1;
                result = false;
            }
            else
            {
                int num = 0;
                int num2 = (this.Index + 1 < this.Fighters.Count) ? (this.Index + 1) : 0;
                if (num2 == 0)
                {
                    this.RoundNumber += 1;
                    this.NewRound = true;
                }
                else
                {
                    this.NewRound = false;
                }
                while (!this.Fighters[num2].CanPlay() && num < this.Fighters.Count)
                {
                    num2 = ((num2 + 1 < this.Fighters.Count) ? (num2 + 1) : 0);
                    if (num2 == 0)
                    {
                        this.RoundNumber += 1;
                        this.NewRound = true;
                    }
                    num++;
                }
                if (!this.Fighters[num2].CanPlay())
                {
                    this.Index = -1;
                    result = false;
                }
                else
                {
                    this.Index = num2;
                    result = true;
                }
            }
            return result;
        }
        public void OrderLine()
        {

            IOrderedEnumerable<Fighter> orderedEnumerable =
                from entry in this.Fight.BlueTeam.GetFighters(false)
                orderby entry.Stats.TotalInitiative descending
                select entry;
            IOrderedEnumerable<Fighter> orderedEnumerable2 =
                from entry in this.Fight.RedTeam.GetFighters(false)
                orderby entry.Stats.TotalInitiative descending
                select entry;

            bool flag = orderedEnumerable.First().Stats.Initiative.Total() > orderedEnumerable2.First().Stats.Initiative.Total();
            System.Collections.Generic.IEnumerator<Fighter> enumerator = orderedEnumerable.GetEnumerator();
            System.Collections.Generic.IEnumerator<Fighter> enumerator2 = orderedEnumerable2.GetEnumerator();
            System.Collections.Generic.List<Fighter> list = new System.Collections.Generic.List<Fighter>();
            bool flag2;
            bool flag3;
            while ((flag2 = enumerator.MoveNext()) | (flag3 = enumerator2.MoveNext()))
            {
                if (flag)
                {
                    if (flag2)
                    {
                        list.Add(enumerator.Current);
                    }
                    if (flag3)
                    {
                        list.Add(enumerator2.Current);
                    }
                }
                else
                {
                    if (flag3)
                    {
                        list.Add(enumerator2.Current);
                    }
                    if (flag2)
                    {
                        list.Add(enumerator.Current);
                    }
                }
            }
            this.Fighters = list;
            this.Index = 0;
        }
        public double[] GetIds()
        {
            return Fighters.FindAll(x=>x.Alive).ConvertAll<double>(x => x.Id).ToArray();
        }
    }
}
