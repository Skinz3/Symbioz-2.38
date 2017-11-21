using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities.Look
{
    public class ContextSubEntity
    {
        public SubEntityBindingPointCategoryEnum Category
        {
            get;
            set;
        }

        public sbyte BindingPointIndex
        {
            get;
            set;
        }

        public ContextActorLook SubActorLook
        {
            get;
            set;
        }

        public ContextSubEntity(SubEntity subentity)
        {
            this.Category = (SubEntityBindingPointCategoryEnum)subentity.bindingPointCategory;
            this.BindingPointIndex = subentity.bindingPointIndex;
            this.SubActorLook = new ContextActorLook(subentity.subEntityLook);
        }
        public ContextSubEntity(SubEntityBindingPointCategoryEnum category, sbyte bindingPointIndex,
            ContextActorLook subActorLook)
        {
            this.Category = category;
            this.BindingPointIndex = bindingPointIndex;
            this.SubActorLook = subActorLook;
        }
        public override bool Equals(object obj)
        {
            ContextSubEntity subEntity = obj as ContextSubEntity;

            if (subEntity == null)
            {
                return false;
            }
            else
            {
                return subEntity.BindingPointIndex == this.BindingPointIndex && subEntity.Category == this.Category &&
                    subEntity.SubActorLook == this.SubActorLook;
            }
        }
        public ContextSubEntity Clone()
        {
            return new ContextSubEntity(Category, BindingPointIndex, SubActorLook.Clone());
        }
        public SubEntity ToSubEntity()
        {
            return new SubEntity((sbyte)Category, BindingPointIndex, SubActorLook.ToEntityLook());
        }

    }
}
