


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class CompassUpdatePvpSeekMessage : CompassUpdateMessage
{

public const ushort Id = 6013;
public override ushort MessageId
{
    get { return Id; }
}

public ulong memberId;
        public string memberName;
        

public CompassUpdatePvpSeekMessage()
{
}

public CompassUpdatePvpSeekMessage(sbyte type, Types.MapCoordinates coords, ulong memberId, string memberName)
         : base(type, coords)
        {
            this.memberId = memberId;
            this.memberName = memberName;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhLong(memberId);
            writer.WriteUTF(memberName);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            memberId = reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9007199254740990)
                throw new Exception("Forbidden value on memberId = " + memberId + ", it doesn't respect the following condition : memberId < 0 || memberId > 9007199254740990");
            memberName = reader.ReadUTF();
            

}


}


}