


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SelectedServerRefusedMessage : Message
{

public const ushort Id = 41;
public override ushort MessageId
{
    get { return Id; }
}

public ushort serverId;
        public sbyte error;
        public sbyte serverStatus;
        

public SelectedServerRefusedMessage()
{
}

public SelectedServerRefusedMessage(ushort serverId, sbyte error, sbyte serverStatus)
        {
            this.serverId = serverId;
            this.error = error;
            this.serverStatus = serverStatus;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(serverId);
            writer.WriteSByte(error);
            writer.WriteSByte(serverStatus);
            

}

public override void Deserialize(ICustomDataInput reader)
{

serverId = reader.ReadVarUhShort();
            if (serverId < 0)
                throw new Exception("Forbidden value on serverId = " + serverId + ", it doesn't respect the following condition : serverId < 0");
            error = reader.ReadSByte();
            if (error < 0)
                throw new Exception("Forbidden value on error = " + error + ", it doesn't respect the following condition : error < 0");
            serverStatus = reader.ReadSByte();
            if (serverStatus < 0)
                throw new Exception("Forbidden value on serverStatus = " + serverStatus + ", it doesn't respect the following condition : serverStatus < 0");
            

}


}


}