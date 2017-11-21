using Symbioz.ORM;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Maps;
using System;
using Symbioz.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Maps.Instances;

namespace Symbioz.World.Records.Interactives
{
    [Table("InteractiveElements")]
    public class InteractiveElementRecord : ITable
    {
        public static List<InteractiveElementRecord> InteractiveElements = new List<InteractiveElementRecord>();

        public List<InteractiveSkillRecord> Skills
        {
            get
            {
                return InteractiveSkillRecord.GetInteractiveSkills(ElementId);
            }
        }

        [Primary]
        public int UId;

        public int ElementId;

        public int MapId;

        public ushort CellId;

        [Update]
        public int ElementType;

        [Ignore]
        public MapPoint Point;

        public int GfxId;

        public int GfxBonesId;

        [Ignore]
        public bool Stated
        {
            get
            {
                return GfxBonesId != -1;
            }
        }

        public InteractiveElementRecord(int uid, int elementId, int mapId, ushort cellId,
            int elementType, int gfxId, int gfxLookId)
        {
            this.UId = uid;
            this.ElementId = elementId;
            this.MapId = mapId;
            this.CellId = cellId;
            this.ElementType = elementType;
            this.GfxId = gfxId;
            this.GfxBonesId = gfxLookId;
            this.Point = new MapPoint((short)cellId);
        }
        public void AddSmartSkill(string actionType, string value1, string value2)
        {
            InteractiveSkillRecord skill = new InteractiveSkillRecord(InteractiveSkillRecord.InteractiveSkills.DynamicPop(x => x.UID),
              actionType, value1, value2, this.ElementId, 114);

            skill.AddElement();
        }
        public void ChangeType(int type)
        {
            this.ElementType = type;
            this.UpdateElement();
        }
        public static List<InteractiveElementRecord> GetActiveElementsOnMap(int mapid)
        {
            return InteractiveElements.FindAll(x => x.MapId == mapid && x.ElementType != -1);
        }
        public static List<InteractiveElementRecord> GetAllElements(int mapid)
        {
            return InteractiveElements.FindAll(x => x.MapId == mapid);
        }
        public static InteractiveElementRecord GetElement(int id, int mapId)
        {
            return InteractiveElements.Find(x => x.ElementId == id && x.MapId == mapId);
        }
        public static InteractiveElementRecord GetElement(int id)
        {
            return InteractiveElements.Find(x => x.ElementId == id);
        }
        public static List<InteractiveElementRecord> GetByElementType(int elementType)
        {
            return InteractiveElements.FindAll(x => x.ElementType == elementType);
        }
        public static List<InteractiveElementRecord> GetElementByGfxId(int gfxId)
        {
            return InteractiveElements.FindAll(x => x.GfxId == gfxId);
        }
        public static List<InteractiveElementRecord> GetElementByGfxLookId(int gfxLookId)
        {
            return InteractiveElements.FindAll(x => x.GfxBonesId == gfxLookId);
        }
        public MapInteractiveElement GetMapInteractiveElement(AbstractMapInstance mapInstance)
        {
            if (Stated)
            {
                return new MapStatedElement(mapInstance, this);
            }
            else
            {
                return new MapInteractiveElement(mapInstance, this);
            }
        }
        public InteractiveSkillRecord GetSkillByActionType(string actionType)
        {
            return Skills.FirstOrDefault(x => x.ActionType == actionType);
        }


    }
}
