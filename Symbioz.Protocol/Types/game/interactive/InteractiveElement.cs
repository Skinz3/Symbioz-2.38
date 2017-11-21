


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

    public class InteractiveElement
    {

        public const short Id = 80;
        public virtual short TypeId
        {
            get { return Id; }
        }

        public int elementId;
        public int elementTypeId;
        public InteractiveElementSkill[] enabledSkills;
        public InteractiveElementSkill[] disabledSkills;
        bool onCurrentMap;

        public InteractiveElement()
        {
        }

        public InteractiveElement(int elementId, int elementTypeId, InteractiveElementSkill[] enabledSkills,
            InteractiveElementSkill[] disabledSkills, bool onCurrentMap)
        {
            this.elementId = elementId;
            this.elementTypeId = elementTypeId;
            this.enabledSkills = enabledSkills;
            this.disabledSkills = disabledSkills;
            this.onCurrentMap = onCurrentMap;
        }


        public virtual void Serialize(ICustomDataOutput writer)
        {

            writer.WriteInt(elementId);
            writer.WriteInt(elementTypeId);
            writer.WriteUShort((ushort)enabledSkills.Length);
            foreach (var entry in enabledSkills)
            {
                writer.WriteShort(entry.TypeId);
                entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)disabledSkills.Length);
            foreach (var entry in disabledSkills)
            {
                writer.WriteShort(entry.TypeId);
                entry.Serialize(writer);
            }

            writer.WriteBoolean(onCurrentMap);


        }

        public virtual void Deserialize(ICustomDataInput reader)
        {

            elementId = reader.ReadInt();
            if (elementId < 0)
                throw new Exception("Forbidden value on elementId = " + elementId + ", it doesn't respect the following condition : elementId < 0");
            elementTypeId = reader.ReadInt();
            var limit = reader.ReadUShort();
            enabledSkills = new InteractiveElementSkill[limit];
            for (int i = 0; i < limit; i++)
            {
                enabledSkills[i] = Types.ProtocolTypeManager.GetInstance<InteractiveElementSkill>(reader.ReadShort());
                enabledSkills[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            disabledSkills = new InteractiveElementSkill[limit];
            for (int i = 0; i < limit; i++)
            {
                disabledSkills[i] = Types.ProtocolTypeManager.GetInstance<InteractiveElementSkill>(reader.ReadShort());
                disabledSkills[i].Deserialize(reader);
            }


        }


    }


}