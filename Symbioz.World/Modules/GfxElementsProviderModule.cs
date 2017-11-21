using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Records.Interactives;
using System;
using System.Collections.Generic;
using Symbioz.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Modules
{
    public class GfxElementsProviderModule
    {
        static Logger logger = new Logger();

        [StartupInvoke("GfxElements Module", StartupInvokePriority.Modules)]
        public static void SynchronizeElements()
        {
       
            foreach (var skill in SkillRecord.Skills)
            {
                if (skill.GfxLookId != -1)
                {
                    List<InteractiveElementRecord> elements = InteractiveElementRecord.GetElementByGfxLookId(skill.GfxLookId);

                    foreach (var element in elements)
                    {
                        if (element.ElementType == -1)
                        {
                            element.ElementType = skill.InteractiveId;


                            if (!InteractiveSkillRecord.OneExist(element.ElementId))
                            {
                                int uid = InteractiveSkillRecord.InteractiveSkills.DynamicPop(w => w.UID);
                                InteractiveSkillRecord record = new InteractiveSkillRecord(uid, "Collect", string.Empty, string.Empty, element.ElementId, skill.Id);
                                record.AddInstantElement();
                            }

                            element.UpdateInstantElement();
                            logger.Gray("Element fixed: " + element.ElementId + " with skill " + skill.Name);
                        }
                    }
                }
            }
        }
    }
}
