


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildInformationsPaddocksMessage : Message
{

public const ushort Id = 5959;
public override ushort MessageId
{
    get { return Id; }
}

public sbyte nbPaddockMax;
        public Types.PaddockContentInformations[] paddocksInformations;
        

public GuildInformationsPaddocksMessage()
{
}

public GuildInformationsPaddocksMessage(sbyte nbPaddockMax, Types.PaddockContentInformations[] paddocksInformations)
        {
            this.nbPaddockMax = nbPaddockMax;
            this.paddocksInformations = paddocksInformations;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteSByte(nbPaddockMax);
            writer.WriteUShort((ushort)paddocksInformations.Length);
            foreach (var entry in paddocksInformations)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

nbPaddockMax = reader.ReadSByte();
            if (nbPaddockMax < 0)
                throw new Exception("Forbidden value on nbPaddockMax = " + nbPaddockMax + ", it doesn't respect the following condition : nbPaddockMax < 0");
            var limit = reader.ReadUShort();
            paddocksInformations = new Types.PaddockContentInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 paddocksInformations[i] = new Types.PaddockContentInformations();
                 paddocksInformations[i].Deserialize(reader);
            }
            

}


}


}