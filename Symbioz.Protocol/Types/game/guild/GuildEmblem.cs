


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GuildEmblem
{

public const short Id = 87;
public virtual short TypeId
{
    get { return Id; }
}

public ushort symbolShape;
        public int symbolColor;
        public sbyte backgroundShape;
        public int backgroundColor;
        

public GuildEmblem()
{
}

public GuildEmblem(ushort symbolShape, int symbolColor, sbyte backgroundShape, int backgroundColor)
        {
            this.symbolShape = symbolShape;
            this.symbolColor = symbolColor;
            this.backgroundShape = backgroundShape;
            this.backgroundColor = backgroundColor;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(symbolShape);
            writer.WriteInt(symbolColor);
            writer.WriteSByte(backgroundShape);
            writer.WriteInt(backgroundColor);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

symbolShape = reader.ReadVarUhShort();
            if (symbolShape < 0)
                throw new Exception("Forbidden value on symbolShape = " + symbolShape + ", it doesn't respect the following condition : symbolShape < 0");
            symbolColor = reader.ReadInt();
            backgroundShape = reader.ReadSByte();
            if (backgroundShape < 0)
                throw new Exception("Forbidden value on backgroundShape = " + backgroundShape + ", it doesn't respect the following condition : backgroundShape < 0");
            backgroundColor = reader.ReadInt();
            

}


}


}