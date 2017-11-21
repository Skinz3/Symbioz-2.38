


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdolListMessage : Message
{

public const ushort Id = 6585;
public override ushort MessageId
{
    get { return Id; }
}

public ushort[] chosenIdols;
        public ushort[] partyChosenIdols;
        public Types.PartyIdol[] partyIdols;
        

public IdolListMessage()
{
}

public IdolListMessage(ushort[] chosenIdols, ushort[] partyChosenIdols, Types.PartyIdol[] partyIdols)
        {
            this.chosenIdols = chosenIdols;
            this.partyChosenIdols = partyChosenIdols;
            this.partyIdols = partyIdols;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)chosenIdols.Length);
            foreach (var entry in chosenIdols)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)partyChosenIdols.Length);
            foreach (var entry in partyChosenIdols)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)partyIdols.Length);
            foreach (var entry in partyIdols)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            chosenIdols = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 chosenIdols[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            partyChosenIdols = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 partyChosenIdols[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            partyIdols = new Types.PartyIdol[limit];
            for (int i = 0; i < limit; i++)
            {
                 partyIdols[i] = Types.ProtocolTypeManager.GetInstance<Types.PartyIdol>(reader.ReadShort());
                 partyIdols[i].Deserialize(reader);
            }
            

}


}


}