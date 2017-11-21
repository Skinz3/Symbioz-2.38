


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeMountsTakenFromPaddockMessage : Message
{

public const ushort Id = 6554;
public override ushort MessageId
{
    get { return Id; }
}

public string name;
        public short worldX;
        public short worldY;
        public string ownername;
        

public ExchangeMountsTakenFromPaddockMessage()
{
}

public ExchangeMountsTakenFromPaddockMessage(string name, short worldX, short worldY, string ownername)
        {
            this.name = name;
            this.worldX = worldX;
            this.worldY = worldY;
            this.ownername = ownername;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(name);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteUTF(ownername);
            

}

public override void Deserialize(ICustomDataInput reader)
{

name = reader.ReadUTF();
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            ownername = reader.ReadUTF();
            

}


}


}