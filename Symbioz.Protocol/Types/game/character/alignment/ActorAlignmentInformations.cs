


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class ActorAlignmentInformations
{

public const short Id = 201;
public virtual short TypeId
{
    get { return Id; }
}

public sbyte alignmentSide;
        public sbyte alignmentValue;
        public sbyte alignmentGrade;
        public double characterPower;
        

public ActorAlignmentInformations()
{
}

public ActorAlignmentInformations(sbyte alignmentSide, sbyte alignmentValue, sbyte alignmentGrade, double characterPower)
        {
            this.alignmentSide = alignmentSide;
            this.alignmentValue = alignmentValue;
            this.alignmentGrade = alignmentGrade;
            this.characterPower = characterPower;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(alignmentSide);
            writer.WriteSByte(alignmentValue);
            writer.WriteSByte(alignmentGrade);
            writer.WriteDouble(characterPower);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

alignmentSide = reader.ReadSByte();
            alignmentValue = reader.ReadSByte();
            if (alignmentValue < 0)
                throw new Exception("Forbidden value on alignmentValue = " + alignmentValue + ", it doesn't respect the following condition : alignmentValue < 0");
            alignmentGrade = reader.ReadSByte();
            if (alignmentGrade < 0)
                throw new Exception("Forbidden value on alignmentGrade = " + alignmentGrade + ", it doesn't respect the following condition : alignmentGrade < 0");
            characterPower = reader.ReadDouble();
            if (characterPower < -9007199254740990 || characterPower > 9007199254740990)
                throw new Exception("Forbidden value on characterPower = " + characterPower + ", it doesn't respect the following condition : characterPower < -9007199254740990 || characterPower > 9007199254740990");
            

}


}


}