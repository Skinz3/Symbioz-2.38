


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ServerOptionalFeaturesMessage : Message
{

public const ushort Id = 6305;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte[] features;
        

public ServerOptionalFeaturesMessage()
{
}

public ServerOptionalFeaturesMessage(sbyte[] features)
        {
            this.features = features;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)features.Length);
            foreach (var entry in features)
            {
                 writer.WriteSByte(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            features = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 features[i] = reader.ReadSByte();
            }
            

}


}


}