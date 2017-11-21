


















// Generated on 04/27/2016 01:13:16
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class AbstractContactInformations
{

public const short Id = 380;
public virtual short TypeId
{
    get { return Id; }
}

public int accountId;
        public string accountName;
        

public AbstractContactInformations()
{
}

public AbstractContactInformations(int accountId, string accountName)
        {
            this.accountId = accountId;
            this.accountName = accountName;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteInt(accountId);
            writer.WriteUTF(accountName);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
            accountName = reader.ReadUTF();
            

}


}


}