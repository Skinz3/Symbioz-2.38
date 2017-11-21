


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

    public class ExchangeCraftCountRequestMessage : Message
    {

        public const ushort Id = 6597;
        public override ushort MessageId
        {
            get { return Id; }
        }

        public int count;


        public ExchangeCraftCountRequestMessage()
        {
        }

        public ExchangeCraftCountRequestMessage(int count)
        {
            this.count = count;
        }


        public override void Serialize(ICustomDataOutput writer)
        {

            writer.WriteVarInt(count);


        }

        public override void Deserialize(ICustomDataInput reader)
        {

            count = reader.ReadVarInt();


        }


    }


}