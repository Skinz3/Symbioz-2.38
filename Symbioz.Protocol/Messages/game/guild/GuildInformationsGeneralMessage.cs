


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class GuildInformationsGeneralMessage : Message
{

public const ushort Id = 5557;
public override ushort MessageId
{
    get { return Id; }
}

public bool enabled;
        public bool abandonnedPaddock;
        public byte level;
        public ulong expLevelFloor;
        public ulong experience;
        public ulong expNextLevelFloor;
        public int creationDate;
        public ushort nbTotalMembers;
        public ushort nbConnectedMembers;
        

public GuildInformationsGeneralMessage()
{
}

public GuildInformationsGeneralMessage(bool enabled, bool abandonnedPaddock, byte level, ulong expLevelFloor, ulong experience, ulong expNextLevelFloor, int creationDate, ushort nbTotalMembers, ushort nbConnectedMembers)
        {
            this.enabled = enabled;
            this.abandonnedPaddock = abandonnedPaddock;
            this.level = level;
            this.expLevelFloor = expLevelFloor;
            this.experience = experience;
            this.expNextLevelFloor = expNextLevelFloor;
            this.creationDate = creationDate;
            this.nbTotalMembers = nbTotalMembers;
            this.nbConnectedMembers = nbConnectedMembers;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, enabled);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, abandonnedPaddock);
            writer.WriteByte(flag1);
            writer.WriteByte(level);
            writer.WriteVarUhLong(expLevelFloor);
            writer.WriteVarUhLong(experience);
            writer.WriteVarUhLong(expNextLevelFloor);
            writer.WriteInt(creationDate);
            writer.WriteVarUhShort(nbTotalMembers);
            writer.WriteVarUhShort(nbConnectedMembers);
            

}

public override void Deserialize(ICustomDataInput reader)
{

byte flag1 = reader.ReadByte();
            enabled = BooleanByteWrapper.GetFlag(flag1, 0);
            abandonnedPaddock = BooleanByteWrapper.GetFlag(flag1, 1);
            level = reader.ReadByte();
            if (level < 0 || level > 255)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0 || level > 255");
            expLevelFloor = reader.ReadVarUhLong();
            if (expLevelFloor < 0 || expLevelFloor > 9007199254740990)
                throw new Exception("Forbidden value on expLevelFloor = " + expLevelFloor + ", it doesn't respect the following condition : expLevelFloor < 0 || expLevelFloor > 9007199254740990");
            experience = reader.ReadVarUhLong();
            if (experience < 0 || experience > 9007199254740990)
                throw new Exception("Forbidden value on experience = " + experience + ", it doesn't respect the following condition : experience < 0 || experience > 9007199254740990");
            expNextLevelFloor = reader.ReadVarUhLong();
            if (expNextLevelFloor < 0 || expNextLevelFloor > 9007199254740990)
                throw new Exception("Forbidden value on expNextLevelFloor = " + expNextLevelFloor + ", it doesn't respect the following condition : expNextLevelFloor < 0 || expNextLevelFloor > 9007199254740990");
            creationDate = reader.ReadInt();
            if (creationDate < 0)
                throw new Exception("Forbidden value on creationDate = " + creationDate + ", it doesn't respect the following condition : creationDate < 0");
            nbTotalMembers = reader.ReadVarUhShort();
            if (nbTotalMembers < 0)
                throw new Exception("Forbidden value on nbTotalMembers = " + nbTotalMembers + ", it doesn't respect the following condition : nbTotalMembers < 0");
            nbConnectedMembers = reader.ReadVarUhShort();
            if (nbConnectedMembers < 0)
                throw new Exception("Forbidden value on nbConnectedMembers = " + nbConnectedMembers + ", it doesn't respect the following condition : nbConnectedMembers < 0");
            

}


}


}