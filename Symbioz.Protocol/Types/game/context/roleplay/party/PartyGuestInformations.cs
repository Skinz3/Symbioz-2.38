


















// Generated on 04/27/2016 01:13:14
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class PartyGuestInformations
{

public const short Id = 374;
public virtual short TypeId
{
    get { return Id; }
}

public ulong guestId;
        public ulong hostId;
        public string name;
        public Types.EntityLook guestLook;
        public sbyte breed;
        public bool sex;
        public Types.PlayerStatus status;
        public Types.PartyCompanionBaseInformations[] companions;
        

public PartyGuestInformations()
{
}

public PartyGuestInformations(ulong guestId, ulong hostId, string name, Types.EntityLook guestLook, sbyte breed, bool sex, Types.PlayerStatus status, Types.PartyCompanionBaseInformations[] companions)
        {
            this.guestId = guestId;
            this.hostId = hostId;
            this.name = name;
            this.guestLook = guestLook;
            this.breed = breed;
            this.sex = sex;
            this.status = status;
            this.companions = companions;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteVarUhLong(guestId);
            writer.WriteVarUhLong(hostId);
            writer.WriteUTF(name);
            guestLook.Serialize(writer);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            writer.WriteShort(status.TypeId);
            status.Serialize(writer);
            writer.WriteUShort((ushort)companions.Length);
            foreach (var entry in companions)
            {
                 entry.Serialize(writer);
            }
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

guestId = reader.ReadVarUhLong();
            if (guestId < 0 || guestId > 9007199254740990)
                throw new Exception("Forbidden value on guestId = " + guestId + ", it doesn't respect the following condition : guestId < 0 || guestId > 9007199254740990");
            hostId = reader.ReadVarUhLong();
            if (hostId < 0 || hostId > 9007199254740990)
                throw new Exception("Forbidden value on hostId = " + hostId + ", it doesn't respect the following condition : hostId < 0 || hostId > 9007199254740990");
            name = reader.ReadUTF();
            guestLook = new Types.EntityLook();
            guestLook.Deserialize(reader);
            breed = reader.ReadSByte();
            sex = reader.ReadBoolean();
            status = Types.ProtocolTypeManager.GetInstance<Types.PlayerStatus>(reader.ReadShort());
            status.Deserialize(reader);
            var limit = reader.ReadUShort();
            companions = new Types.PartyCompanionBaseInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 companions[i] = new Types.PartyCompanionBaseInformations();
                 companions[i].Deserialize(reader);
            }
            

}


}


}