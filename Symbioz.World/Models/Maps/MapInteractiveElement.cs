using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Maps.Instances;
using Symbioz.World.Models.Maps.Shapes;
using Symbioz.World.Records.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Maps
{
    public class MapInteractiveElement
    {
        public int Id
        {
            get
            {
                return Record.ElementId;
            }
        }
        public InteractiveElementRecord Record
        {
            get;
            private set;
        }
        public List<MapInteractiveElementSkill> EnabledSkills
        {
            get;
            private set;
        }
        public List<MapInteractiveElementSkill> DisabledSkills
        {
            get;
            private set;
        }
        public AbstractMapInstance MapInstance
        {
            get;
            private set;
        }
        public MapInteractiveElement(AbstractMapInstance mapInstance, InteractiveElementRecord record)
        {
            this.Record = record;
            this.MapInstance = mapInstance;
            this.EnabledSkills = record.Skills.ConvertAll(x => new MapInteractiveElementSkill(this, x));
            this.DisabledSkills = new List<MapInteractiveElementSkill>();
        }
        public void UpdateElement()
        {
            MapInstance.Send(new InteractiveElementUpdatedMessage(GetInteractiveElement()));
        }
        public MapInteractiveElementSkill GetSkill(uint uid)
        {
            return EnabledSkills.Find(x => x.UId == uid);
        }
        public InteractiveElement GetInteractiveElement()
        {
            return new InteractiveElement(Id, Record.ElementType, Array.ConvertAll(EnabledSkills.ToArray(), x => x.GetInteractiveElementSkill()),
                Array.ConvertAll(DisabledSkills.ToArray(), x => x.GetInteractiveElementSkill()), true);
        }
        public InteractiveElement GetInteractiveElement(Character character)
        {
            List<MapInteractiveElementSkill> enlabled = new List<MapInteractiveElementSkill>();
            List<MapInteractiveElementSkill> disabled = new List<MapInteractiveElementSkill>();

            disabled.AddRange(DisabledSkills);

            foreach (var skill in EnabledSkills)
            {
                if (character.SkillsAllowed.Contains(skill.Id))
                {
                    enlabled.Add(skill);
                }
                else
                    disabled.Add(skill);
            }

            return new InteractiveElement(Id, Record.ElementType, Array.ConvertAll(enlabled.ToArray(), x => x.GetInteractiveElementSkill()),
               Array.ConvertAll(disabled.ToArray(), x => x.GetInteractiveElementSkill()), true);
        }
        public bool CanUse(Character character)
        {
            short[] zone = new Square(0, 1).GetCells((short)Record.CellId, character.Map);

           
            return zone.Length == 0 || zone.Contains((short)character.Record.CellId);
        }
    }
}
