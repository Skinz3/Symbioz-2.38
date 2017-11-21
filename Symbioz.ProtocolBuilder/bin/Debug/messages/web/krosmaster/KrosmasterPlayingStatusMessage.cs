


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class KrosmasterPlayingStatusMessage : Message
{

public const ushort Id = 6347;
public override ushort MessageId
{
    get { return Id; }
}

public bool playing;
        

public KrosmasterPlayingStatusMessage()
{
}

public KrosmasterPlayingStatusMessage(bool playing)
        {
            this.playing = playing;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteBoolean(playing);
            

}

public override void Deserialize(ICustomDataInput reader)
{

playing = reader.ReadBoolean();
            

}


}


}