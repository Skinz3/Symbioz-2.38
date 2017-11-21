


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class HumanOptionTitle : HumanOption
{

public const short Id = 408;
public override short TypeId
{
    get { return Id; }
}

public ushort titleId;
        public string titleParam;
        

public HumanOptionTitle()
{
}

public HumanOptionTitle(ushort titleId, string titleParam)
        {
            this.titleId = titleId;
            this.titleParam = titleParam;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteVarUhShort(titleId);
            writer.WriteUTF(titleParam);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            titleId = reader.ReadVarUhShort();
            if (titleId < 0)
                throw new Exception("Forbidden value on titleId = " + titleId + ", it doesn't respect the following condition : titleId < 0");
            titleParam = reader.ReadUTF();
            

}


}


}