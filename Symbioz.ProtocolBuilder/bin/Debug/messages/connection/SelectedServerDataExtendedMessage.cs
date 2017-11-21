


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class SelectedServerDataExtendedMessage : SelectedServerDataMessage
{

public const ushort Id = 6469;
public override ushort MessageId
{
    get { return Id; }
}

public ushort[] serverIds;
        

public SelectedServerDataExtendedMessage()
{
}

public SelectedServerDataExtendedMessage(ushort serverId, string address, ushort port, bool canCreateNewCharacter, sbyte[] ticket, ushort[] serverIds)
         : base(serverId, address, port, canCreateNewCharacter, ticket)
        {
            this.serverIds = serverIds;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUShort((ushort)serverIds.Length);
            foreach (var entry in serverIds)
            {
                 writer.WriteVarUhShort(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            var limit = reader.ReadUShort();
            serverIds = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 serverIds[i] = reader.ReadVarUhShort();
            }
            

}


}


}