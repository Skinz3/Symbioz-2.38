


















// Generated on 04/27/2016 01:13:13
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

    public class GameRolePlayGroupMonsterInformations : GameRolePlayActorInformations
    {

        public const short Id = 160;
        public override short TypeId
        {
            get { return Id; }
        }

        public bool keyRingBonus;
        public bool hasHardcoreDrop;
        public bool hasAVARewardToken;
        public Types.GroupMonsterStaticInformations staticInfos;
        public double creationDate;
        public uint ageBonusRate;
        public sbyte lootShare;
        public sbyte alignmentSide;


        public GameRolePlayGroupMonsterInformations()
        {
        }

        public GameRolePlayGroupMonsterInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, bool keyRingBonus, bool hasHardcoreDrop, bool hasAVARewardToken, Types.GroupMonsterStaticInformations staticInfos, double creationDate, uint ageBonus, sbyte lootShare, sbyte alignmentSide)
            : base(contextualId, look, disposition)
        {
            this.keyRingBonus = keyRingBonus;
            this.hasHardcoreDrop = hasHardcoreDrop;
            this.hasAVARewardToken = hasAVARewardToken;
            this.staticInfos = staticInfos;
            this.creationDate = creationDate;
            this.ageBonusRate = ageBonus;
            this.lootShare = lootShare;
            this.alignmentSide = alignmentSide;
        }


        public override void Serialize(ICustomDataOutput writer)
        {

            base.Serialize(writer);
            byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, keyRingBonus);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, hasHardcoreDrop);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 2, hasAVARewardToken);
            writer.WriteByte(flag1);
            writer.WriteShort(staticInfos.TypeId);
            staticInfos.Serialize(writer);
            writer.WriteDouble(creationDate);
            writer.WriteUInt(ageBonusRate);
            writer.WriteByte((byte)lootShare);
            writer.WriteByte((byte)alignmentSide);


        }

        public override void Deserialize(ICustomDataInput reader)
        {

            base.Deserialize(reader);
            byte flag1 = reader.ReadByte();
            keyRingBonus = BooleanByteWrapper.GetFlag(flag1, 0);
            hasHardcoreDrop = BooleanByteWrapper.GetFlag(flag1, 1);
            hasAVARewardToken = BooleanByteWrapper.GetFlag(flag1, 2);
            staticInfos = Types.ProtocolTypeManager.GetInstance<Types.GroupMonsterStaticInformations>(reader.ReadShort());
            staticInfos.Deserialize(reader);
            creationDate = reader.ReadDouble();
            ageBonusRate = reader.ReadUInt();
            lootShare = reader.ReadSByte();
            if ((lootShare < -1) || (lootShare > 8))
                throw new Exception("Forbidden value on lootShare = " + lootShare + ", it doesn't respect the following condition : (lootShare < -1) || (lootShare > 8)");
            alignmentSide = reader.ReadSByte();


        }
    }
}