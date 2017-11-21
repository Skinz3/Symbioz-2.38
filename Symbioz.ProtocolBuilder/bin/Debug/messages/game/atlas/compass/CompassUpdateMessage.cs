


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CompassUpdateMessage : Message
{

public const ushort Id = 5591;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte type;
        public Types.MapCoordinates coords;
        

public CompassUpdateMessage()
{
}

public CompassUpdateMessage(sbyte type, Types.MapCoordinates coords)
        {
            this.type = type;
            this.coords = coords;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(type);
            writer.WriteShort(coords.TypeId);
            coords.Serialize(writer);
            

}

public override void Deserialize(ICustomDataInput reader)
{

type = reader.ReadSByte();
            if (type < 0)
                throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
            coords = ProtocolTypeManager.GetInstance<Types.MapCoordinates>(reader.ReadShort());
            coords.Deserialize(reader);
            

}


}


}