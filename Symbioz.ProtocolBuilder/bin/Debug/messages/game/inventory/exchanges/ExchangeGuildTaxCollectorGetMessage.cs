


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ExchangeGuildTaxCollectorGetMessage : Message
{

public const ushort Id = 5762;
public override ushort MessageId
{
    get { return Id; }
}

public string collectorName;
        public short worldX;
        public short worldY;
        public int mapId;
        public ushort subAreaId;
        public string userName;
        public ulong callerId;
        public string callerName;
        public double experience;
        public ushort pods;
        public Types.ObjectItemGenericQuantity[] objectsInfos;
        

public ExchangeGuildTaxCollectorGetMessage()
{
}

public ExchangeGuildTaxCollectorGetMessage(string collectorName, short worldX, short worldY, int mapId, ushort subAreaId, string userName, ulong callerId, string callerName, double experience, ushort pods, Types.ObjectItemGenericQuantity[] objectsInfos)
        {
            this.collectorName = collectorName;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.userName = userName;
            this.callerId = callerId;
            this.callerName = callerName;
            this.experience = experience;
            this.pods = pods;
            this.objectsInfos = objectsInfos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUTF(collectorName);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteInt(mapId);
            writer.WriteVarUhShort(subAreaId);
            writer.WriteUTF(userName);
            writer.WriteVarUhLong(callerId);
            writer.WriteUTF(callerName);
            writer.WriteDouble(experience);
            writer.WriteVarUhShort(pods);
            writer.WriteUShort((ushort)objectsInfos.Length);
            foreach (var entry in objectsInfos)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

collectorName = reader.ReadUTF();
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            mapId = reader.ReadInt();
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            userName = reader.ReadUTF();
            callerId = reader.ReadVarUhLong();
            if (callerId < 0 || callerId > 9007199254740990)
                throw new Exception("Forbidden value on callerId = " + callerId + ", it doesn't respect the following condition : callerId < 0 || callerId > 9007199254740990");
            callerName = reader.ReadUTF();
            experience = reader.ReadDouble();
            if (experience < -9007199254740990 || experience > 9007199254740990)
                throw new Exception("Forbidden value on experience = " + experience + ", it doesn't respect the following condition : experience < -9007199254740990 || experience > 9007199254740990");
            pods = reader.ReadVarUhShort();
            if (pods < 0)
                throw new Exception("Forbidden value on pods = " + pods + ", it doesn't respect the following condition : pods < 0");
            var limit = reader.ReadUShort();
            objectsInfos = new Types.ObjectItemGenericQuantity[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectsInfos[i] = new Types.ObjectItemGenericQuantity();
                 objectsInfos[i].Deserialize(reader);
            }
            

}


}


}