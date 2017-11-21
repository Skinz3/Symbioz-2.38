


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class HouseToSellListMessage : Message
{

public const ushort Id = 6140;
public override ushort MessageId
{
    get { return Id; }
}

public ushort pageIndex;
        public ushort totalPage;
        public Types.HouseInformationsForSell[] houseList;
        

public HouseToSellListMessage()
{
}

public HouseToSellListMessage(ushort pageIndex, ushort totalPage, Types.HouseInformationsForSell[] houseList)
        {
            this.pageIndex = pageIndex;
            this.totalPage = totalPage;
            this.houseList = houseList;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhShort(pageIndex);
            writer.WriteVarUhShort(totalPage);
            writer.WriteUShort((ushort)houseList.Length);
            foreach (var entry in houseList)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

pageIndex = reader.ReadVarUhShort();
            if (pageIndex < 0)
                throw new Exception("Forbidden value on pageIndex = " + pageIndex + ", it doesn't respect the following condition : pageIndex < 0");
            totalPage = reader.ReadVarUhShort();
            if (totalPage < 0)
                throw new Exception("Forbidden value on totalPage = " + totalPage + ", it doesn't respect the following condition : totalPage < 0");
            var limit = reader.ReadUShort();
            houseList = new Types.HouseInformationsForSell[limit];
            for (int i = 0; i < limit; i++)
            {
                 houseList[i] = new Types.HouseInformationsForSell();
                 houseList[i].Deserialize(reader);
            }
            

}


}


}