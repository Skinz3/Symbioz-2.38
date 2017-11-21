using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace Symbioz.World.Models.Monsters
{
    public class MonsterDrop
    {
        public int DropId { get; set; }

        public ushort ItemId { get; set; }

        public short PercentDropForGrade1 { get; set; }

        public short PercentDropForGrade2 { get; set; }

        public short PercentDropForGrade3 { get; set; }

        public short PercentDropForGrade4 { get; set; }

        public short PercentDropForGrade5 { get; set; }

        public int ProspectingLock { get; set; }

        public int DropLimit { get; set; }

        public int Count { get; set; }

        public bool HasCriteria { get; set; }

        public double GetDropRate(int grade)
        {
            double result;
            if (grade <= 1)
            {
                result = this.PercentDropForGrade1;
            }
            else
            {
                if (grade == 2)
                {
                    result = this.PercentDropForGrade2;
                }
                else
                {
                    if (grade == 3)
                    {
                        result = this.PercentDropForGrade3;
                    }
                    else
                    {
                        if (grade == 4)
                        {
                            result = this.PercentDropForGrade4;
                        }
                        else
                        {
                            result = this.PercentDropForGrade5;
                        }
                    }
                }
            }
            return (double)result;
        }
    }
}
