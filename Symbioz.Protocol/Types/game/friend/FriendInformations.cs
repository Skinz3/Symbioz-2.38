


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class FriendInformations : AbstractContactInformations
{

public const short Id = 78;
public override short TypeId
{
    get { return Id; }
}

public sbyte playerState;
        public ushort lastConnection;
        public int achievementPoints;
        

public FriendInformations()
{
}

public FriendInformations(int accountId, string accountName, sbyte playerState, ushort lastConnection, int achievementPoints)
         : base(accountId, accountName)
        {
            this.playerState = playerState;
            this.lastConnection = lastConnection;
            this.achievementPoints = achievementPoints;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteSByte(playerState);
            writer.WriteVarUhShort(lastConnection);
            writer.WriteInt(achievementPoints);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            playerState = reader.ReadSByte();
            if (playerState < 0)
                throw new Exception("Forbidden value on playerState = " + playerState + ", it doesn't respect the following condition : playerState < 0");
            lastConnection = reader.ReadVarUhShort();
            if (lastConnection < 0)
                throw new Exception("Forbidden value on lastConnection = " + lastConnection + ", it doesn't respect the following condition : lastConnection < 0");
            achievementPoints = reader.ReadInt();
            

}


}


}