


















// Generated on 04/27/2016 01:13:17
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class TaxCollectorMovement
{

public const short Id = 493;
public virtual short TypeId
{
    get { return Id; }
}

public sbyte movementType;
        public TaxCollectorBasicInformations basicInfos;
        public ulong playerId;
        public string playerName;
        

public TaxCollectorMovement()
{
}

public TaxCollectorMovement(sbyte movementType,TaxCollectorBasicInformations basicInfos, ulong playerId, string playerName)
        {
            this.movementType = movementType;
            this.basicInfos = basicInfos;
            this.playerId = playerId;
            this.playerName = playerName;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(movementType);
            basicInfos.Serialize(writer);
            writer.WriteVarUhLong(playerId);
            writer.WriteUTF(playerName);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

movementType = reader.ReadSByte();
            if (movementType < 0)
                throw new Exception("Forbidden value on movementType = " + movementType + ", it doesn't respect the following condition : movementType < 0");
            basicInfos = new TaxCollectorBasicInformations();
            basicInfos.Deserialize(reader);
            playerId = reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
            playerName = reader.ReadUTF();
            

}


}


}