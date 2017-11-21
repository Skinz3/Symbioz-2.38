using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Symbioz.World.Records.Context;
using Symbioz.World.Models.Fights;
using Symbioz.Core;
using Symbioz.World.Models.Fights.FightModels;

namespace Symbioz.World.Providers.Fights.Challenges
{
    public class ChallengeProvider : Singleton<ChallengeProvider>
    {
        private Dictionary<ChallengeRecord, Type> m_challenges = new Dictionary<ChallengeRecord, Type>();

        [StartupInvoke("Fight Challenges", StartupInvokePriority.Eighth)]
        public void Initialize()
        {
            foreach (var type in Program.WorldAssembly.GetTypes())
            {
                ChallengeAttribute attribute = type.GetCustomAttribute<ChallengeAttribute>();

                if (attribute != null)
                {
                    this.m_challenges.Add(ChallengeRecord.GetChallenge(attribute.ChallengeType), type);
                }
            }
        }

        public Challenge[] PopChallenges(FightTeam team, int count)
        {
            List<Challenge> results = new List<Challenge>();

            AsyncRandom random = new AsyncRandom();

            for (int i = 0; i < count; i++)
            {
                var pair = m_challenges.ToList().FindAll(x => CanAddChallenge(results, x.Key)).Random();

                if (pair.Value != null)
                {
                    Challenge challenge = (Challenge)Activator.CreateInstance(pair.Value, new object[] { pair.Key, team });
                    challenge.Initialize();

                    if (challenge.Valid())
                    {
                        results.Add(challenge);
                    }
                    else
                        i--;
                }
            }

            return results.ToArray();
        }
        private bool CanAddChallenge(List<Challenge> results, ChallengeRecord added)
        {
            return !results.Any(x => added.IncompatibleChallenges.Contains((short)x.Id)) && results.Find(x => x.Id == added.Id) == null;
        }
    }
}
