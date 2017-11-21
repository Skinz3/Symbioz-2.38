


















// Generated on 04/27/2016 01:13:15
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class Item
{

public const short Id = 7;
public virtual short TypeId
{
    get { return Id; }
}



public Item()
{
}



public virtual void Serialize(ICustomDataOutput writer)
{



}

public virtual void Deserialize(ICustomDataInput reader)
{



}


}


}