


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DareCreatedListMessage : Message
{

public const ushort Id = 6663;
public override ushort MessageId
{
    get { return Id; }
}

public Types.DareInformations[] daresFixedInfos;
        public Types.DareVersatileInformations[] daresVersatilesInfos;
        

public DareCreatedListMessage()
{
}

public DareCreatedListMessage(Types.DareInformations[] daresFixedInfos, Types.DareVersatileInformations[] daresVersatilesInfos)
        {
            this.daresFixedInfos = daresFixedInfos;
            this.daresVersatilesInfos = daresVersatilesInfos;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)daresFixedInfos.Length);
            foreach (var entry in daresFixedInfos)
            {
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)daresVersatilesInfos.Length);
            foreach (var entry in daresVersatilesInfos)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            daresFixedInfos = new Types.DareInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 daresFixedInfos[i] = new Types.DareInformations();
                 daresFixedInfos[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            daresVersatilesInfos = new Types.DareVersatileInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 daresVersatilesInfos[i] = new Types.DareVersatileInformations();
                 daresVersatilesInfos[i].Deserialize(reader);
            }
            

}


}


}