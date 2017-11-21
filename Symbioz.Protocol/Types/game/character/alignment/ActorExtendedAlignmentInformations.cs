


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ActorExtendedAlignmentInformations : ActorAlignmentInformations
{

public const short Id = 202;
public override short TypeId
{
    get { return Id; }
}

public ushort honor;
        public ushort honorGradeFloor;
        public ushort honorNextGradeFloor;
        public sbyte aggressable;
        

public ActorExtendedAlignmentInformations()
{
}

public ActorExtendedAlignmentInformations(sbyte alignmentSide, sbyte alignmentValue, sbyte alignmentGrade, double characterPower, ushort honor, ushort honorGradeFloor, ushort honorNextGradeFloor, sbyte aggressable)
         : base(alignmentSide, alignmentValue, alignmentGrade, characterPower)
        {
            this.honor = honor;
            this.honorGradeFloor = honorGradeFloor;
            this.honorNextGradeFloor = honorNextGradeFloor;
            this.aggressable = aggressable;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(honor);
            writer.WriteVarUhShort(honorGradeFloor);
            writer.WriteVarUhShort(honorNextGradeFloor);
            writer.WriteSByte(aggressable);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            honor = reader.ReadVarUhShort();
            if (honor < 0 || honor > 20000)
                throw new Exception("Forbidden value on honor = " + honor + ", it doesn't respect the following condition : honor < 0 || honor > 20000");
            honorGradeFloor = reader.ReadVarUhShort();
            if (honorGradeFloor < 0 || honorGradeFloor > 20000)
                throw new Exception("Forbidden value on honorGradeFloor = " + honorGradeFloor + ", it doesn't respect the following condition : honorGradeFloor < 0 || honorGradeFloor > 20000");
            honorNextGradeFloor = reader.ReadVarUhShort();
            if (honorNextGradeFloor < 0 || honorNextGradeFloor > 20000)
                throw new Exception("Forbidden value on honorNextGradeFloor = " + honorNextGradeFloor + ", it doesn't respect the following condition : honorNextGradeFloor < 0 || honorNextGradeFloor > 20000");
            aggressable = reader.ReadSByte();
            if (aggressable < 0)
                throw new Exception("Forbidden value on aggressable = " + aggressable + ", it doesn't respect the following condition : aggressable < 0");
            

}


}


}