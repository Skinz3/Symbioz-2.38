


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

    public class FightCommonInformations
    {

        public const short Id = 43;
        public virtual short TypeId
        {
            get { return Id; }
        }

        public int fightId;
        public sbyte fightType;
        public FightTeamInformations[] fightTeams;
        public ushort[] fightTeamsPositions;
        public FightOptionsInformations[] fightTeamsOptions;


        public FightCommonInformations()
        {
        }

        public FightCommonInformations(int fightId, sbyte fightType, FightTeamInformations[] fightTeams, ushort[] fightTeamsPositions, FightOptionsInformations[] fightTeamsOptions)
        {
            this.fightId = fightId;
            this.fightType = fightType;
            this.fightTeams = fightTeams;
            this.fightTeamsPositions = fightTeamsPositions;
            this.fightTeamsOptions = fightTeamsOptions;
        }


        public virtual void Serialize(ICustomDataOutput writer)
        {

            writer.WriteInt(fightId);
            writer.WriteSByte(fightType);
            writer.WriteUShort((ushort)fightTeams.Length);
            foreach (var entry in fightTeams)
            {
                writer.WriteShort(entry.TypeId);
                entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)fightTeamsPositions.Length);
            foreach (var entry in fightTeamsPositions)
            {
                writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)fightTeamsOptions.Length);
            foreach (var entry in fightTeamsOptions)
            {
                entry.Serialize(writer);
            }


        }

        public virtual void Deserialize(ICustomDataInput reader)
        {

            fightId = reader.ReadInt();
            fightType = reader.ReadSByte();
            if (fightType < 0)
                throw new Exception("Forbidden value on fightType = " + fightType + ", it doesn't respect the following condition : fightType < 0");
            var limit = reader.ReadUShort();
            fightTeams = new FightTeamInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                fightTeams[i] = Types.ProtocolTypeManager.GetInstance<FightTeamInformations>(reader.ReadShort());
                fightTeams[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            fightTeamsPositions = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                fightTeamsPositions[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            fightTeamsOptions = new FightOptionsInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                fightTeamsOptions[i] = new FightOptionsInformations();
                fightTeamsOptions[i].Deserialize(reader);
            }


        }


    }


}