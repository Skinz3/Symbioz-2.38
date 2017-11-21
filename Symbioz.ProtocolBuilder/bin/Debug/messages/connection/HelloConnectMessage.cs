


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class HelloConnectMessage : Message
{

public const ushort Id = 3;
public override ushort MessageId
{
    get { return Id; }
}

public string salt;
        public sbyte[] key;
        

public HelloConnectMessage()
{
}

public HelloConnectMessage(string salt, sbyte[] key)
        {
            this.salt = salt;
            this.key = key;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(salt);
            writer.WriteUShort((ushort)key.Length);
            foreach (var entry in key)
            {
                 writer.WriteSByte(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

salt = reader.ReadUTF();
            var limit = reader.ReadUShort();
            key = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 key[i] = reader.ReadSByte();
            }
            

}


}


}