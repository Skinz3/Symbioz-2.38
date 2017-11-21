


















// Generated on 04/27/2016 01:13:13
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

    public class GameRolePlayGroupMonsterWaveInformations : GameRolePlayGroupMonsterInformations
    {

        public const short Id = 464;
        public override short TypeId
        {
            get { return Id; }
        }

        public sbyte nbWaves;
        public IEnumerable<Types.GroupMonsterStaticInformations> alternatives;


        public GameRolePlayGroupMonsterWaveInformations()
        {
        }

        public GameRolePlayGroupMonsterWaveInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, bool keyRingBonus, bool hasHardcoreDrop, bool hasAVARewardToken, Types.GroupMonsterStaticInformations staticInfos,double creationDate,uint ageBonus, sbyte lootShare, sbyte alignmentSide, sbyte nbWaves, IEnumerable<Types.GroupMonsterStaticInformations> alternatives)
            : base(contextualId, look, disposition, keyRingBonus, hasHardcoreDrop, hasAVARewardToken, staticInfos,creationDate, ageBonus, lootShare, alignmentSide)
        {
            this.nbWaves = nbWaves;
            this.alternatives = alternatives;
        }


        public override void Serialize(ICustomDataOutput writer)
        {

            base.Serialize(writer);
            writer.WriteSByte(nbWaves);
            writer.WriteUShort((ushort)alternatives.Count());
            foreach (var entry in alternatives)
            {
                writer.WriteShort(entry.TypeId);
                entry.Serialize(writer);
            }


        }

        public override void Deserialize(ICustomDataInput reader)
        {

            base.Deserialize(reader);
            nbWaves = reader.ReadSByte();
            if (nbWaves < 0)
                throw new Exception("Forbidden value on nbWaves = " + nbWaves + ", it doesn't respect the following condition : nbWaves < 0");
            var limit = reader.ReadUShort();
            alternatives = new Types.GroupMonsterStaticInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                (alternatives as Types.GroupMonsterStaticInformations[])[i] = Types.ProtocolTypeManager.GetInstance<Types.GroupMonsterStaticInformations>(reader.ReadShort());
                (alternatives as Types.GroupMonsterStaticInformations[])[i].Deserialize(reader);
            }


        }
    }
}