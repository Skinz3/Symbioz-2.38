


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class UpdateMountBoostMessage : Message
{

public const ushort Id = 6179;
public override ushort MessageId
{
    get { return Id; }
}

public int rideId;
        public Types.UpdateMountBoost[] boostToUpdateList;
        

public UpdateMountBoostMessage()
{
}

public UpdateMountBoostMessage(int rideId, Types.UpdateMountBoost[] boostToUpdateList)
        {
            this.rideId = rideId;
            this.boostToUpdateList = boostToUpdateList;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarInt(rideId);
            writer.WriteUShort((ushort)boostToUpdateList.Length);
            foreach (var entry in boostToUpdateList)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

rideId = reader.ReadVarInt();
            var limit = reader.ReadUShort();
            boostToUpdateList = new Types.UpdateMountBoost[limit];
            for (int i = 0; i < limit; i++)
            {
                 boostToUpdateList[i] = ProtocolTypeManager.GetInstance<Types.UpdateMountBoost>(reader.ReadShort());
                 boostToUpdateList[i].Deserialize(reader);
            }
            

}


}


}