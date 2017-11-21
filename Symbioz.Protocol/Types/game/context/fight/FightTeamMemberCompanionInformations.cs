


















// Generated on 04/27/2016 01:13:11
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FightTeamMemberCompanionInformations : FightTeamMemberInformations
{

public const short Id = 451;
public override short TypeId
{
    get { return Id; }
}

public sbyte companionId;
        public byte level;
        public int masterId;
        

public FightTeamMemberCompanionInformations()
{
}

public FightTeamMemberCompanionInformations(double id, sbyte companionId, byte level, int masterId)
         : base(id)
        {
            this.companionId = companionId;
            this.level = level;
            this.masterId = masterId;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(companionId);
            writer.WriteByte(level);
            writer.WriteInt(masterId);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            companionId = reader.ReadSByte();
            if (companionId < 0)
                throw new Exception("Forbidden value on companionId = " + companionId + ", it doesn't respect the following condition : companionId < 0");
            level = reader.ReadByte();
            if (level < 1 || level > 200)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 1 || level > 200");
            masterId = reader.ReadInt();
            

}


}


}