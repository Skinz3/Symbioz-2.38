using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Criterias
{
    public abstract class AbstractCriteria
    {
        public const char EqualSymbol = '=';

        public const char InferiorSymbol = '<';

        public const char SuperiorSymbol = '>';

        public char ComparaisonSymbol { get { return CriteriaFull.Remove(0, 2).Take(1).ToArray()[0]; } }

        public string CriteriaValue { get { return CriteriaFull.Remove(0, 3); } }

        public string CriteriaFull { get; set; }

        public abstract bool Eval(WorldClient client);

        public static bool BasicEval(string criteriavalue, char comparaisonsymbol, int delta)
        {
            int criterialDelta = int.Parse(criteriavalue);

            switch (comparaisonsymbol)
            {
                case EqualSymbol:
                    if (delta == criterialDelta)
                        return true;
                    break;
                case InferiorSymbol:
                    if (delta < criterialDelta)
                        return true;
                    break;
                case SuperiorSymbol:
                    if (delta > criterialDelta)
                        return true;
                    break;
            }

            return false;
        }
    }
}
