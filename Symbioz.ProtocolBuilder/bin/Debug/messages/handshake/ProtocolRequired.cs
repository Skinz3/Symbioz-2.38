


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ProtocolRequired : Message
{

public const ushort Id = 1;
public override ushort MessageId
{
    get { return Id; }
}

public int requiredVersion;
        public int currentVersion;
        

public ProtocolRequired()
{
}

public ProtocolRequired(int requiredVersion, int currentVersion)
        {
            this.requiredVersion = requiredVersion;
            this.currentVersion = currentVersion;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(requiredVersion);
            writer.WriteInt(currentVersion);
            

}

public override void Deserialize(ICustomDataInput reader)
{

requiredVersion = reader.ReadInt();
            if (requiredVersion < 0)
                throw new Exception("Forbidden value on requiredVersion = " + requiredVersion + ", it doesn't respect the following condition : requiredVersion < 0");
            currentVersion = reader.ReadInt();
            if (currentVersion < 0)
                throw new Exception("Forbidden value on currentVersion = " + currentVersion + ", it doesn't respect the following condition : currentVersion < 0");
            

}


}


}