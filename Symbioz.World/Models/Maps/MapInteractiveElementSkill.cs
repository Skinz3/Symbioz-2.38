using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Providers.Maps.Interactives;
using Symbioz.World.Records.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Maps
{
    public class MapInteractiveElementSkill
    {
        public ushort Id
        {
            get
            {
                return Template.Id;
            }
        }
        public uint UId
        {
            get
            {
                return (uint)Record.UID;
            }
        }
        public string ActionType
        {
            get
            {
                return Record.ActionType;
            }
        }
        public MapInteractiveElement Element
        {
            get;
            private set;
        }
        public InteractiveSkillRecord Record
        {
            get;
            private set;
        }
        public SkillRecord Template
        {
            get;
            private set;
        }

        public MapInteractiveElementSkill(MapInteractiveElement element, InteractiveSkillRecord record)
        {
            this.Record = record;
            this.Element = element;
            this.Template = SkillRecord.GetSkill(record.SkillId);
        }

        public InteractiveElementSkill GetInteractiveElementSkill()
        {
            return new InteractiveElementSkill(Record.SkillId, Record.UID);
        }

        public virtual void Use(Character character)
        {
            InteractiveActionsProvider.Handle(character, this);
        }
        public void Disable()
        {
            lock (this.Element)
            {
                this.Element.EnabledSkills.Remove(this);
                this.Element.DisabledSkills.Add(this);
                this.Element.UpdateElement();
            }
        }
        public void Enable()
        {
            lock (this.Element)
            {
                this.Element.DisabledSkills.Remove(this);
                this.Element.EnabledSkills.Add(this);
                this.Element.UpdateElement();
            }
        }
    }
}
