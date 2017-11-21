using Symbioz.Core.DesignPattern;
using Symbioz.World.Models.Entities;
using Symbioz.World.Records.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers.Maps.Interactives
{
    public class SkillsProvider : Singleton<SkillsProvider>
    {
        public IEnumerable<ushort> GetAllowedSkills(Character character)
        {
            lock (this)
            {
                foreach (var skill in SkillRecord.Skills)
                {
                    if (skill.ParentJobId != 1)
                    {
                        if (skill.MinLevel <= character.GetJob(skill.ParentJobIdEnum).Level)
                            yield return skill.Id;
                    }
                    else
                        yield return skill.Id;

                }
            }
        }
    }
}
