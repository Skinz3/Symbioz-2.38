


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class BasicWhoIsMessage : Message
{

public const ushort Id = 180;
public override ushort MessageId
{
    get { return Id; }
}

public bool self;
        public bool verbose;
        public sbyte position;
        public string accountNickname;
        public int accountId;
        public string playerName;
        public ulong playerId;
        public short areaId;
        public short serverId;
        public short originServerId;
        public Types.AbstractSocialGroupInfos[] socialGroups;
        public sbyte playerState;
        

public BasicWhoIsMessage()
{
}

public BasicWhoIsMessage(bool self, bool verbose, sbyte position, string accountNickname, int accountId, string playerName, ulong playerId, short areaId, short serverId, short originServerId, Types.AbstractSocialGroupInfos[] socialGroups, sbyte playerState)
        {
            this.self = self;
            this.verbose = verbose;
            this.position = position;
            this.accountNickname = accountNickname;
            this.accountId = accountId;
            this.playerName = playerName;
            this.playerId = playerId;
            this.areaId = areaId;
            this.serverId = serverId;
            this.originServerId = originServerId;
            this.socialGroups = socialGroups;
            this.playerState = playerState;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, self);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, verbose);
            writer.WriteByte(flag1);
            writer.WriteSByte(position);
            writer.WriteUTF(accountNickname);
            writer.WriteInt(accountId);
            writer.WriteUTF(playerName);
            writer.WriteVarUhLong(playerId);
            writer.WriteShort(areaId);
            writer.WriteShort(serverId);
            writer.WriteShort(originServerId);
            writer.WriteUShort((ushort)socialGroups.Length);
            foreach (var entry in socialGroups)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            writer.WriteSByte(playerState);
            

}

public override void Deserialize(ICustomDataInput reader)
{

byte flag1 = reader.ReadByte();
            self = BooleanByteWrapper.GetFlag(flag1, 0);
            verbose = BooleanByteWrapper.GetFlag(flag1, 1);
            position = reader.ReadSByte();
            accountNickname = reader.ReadUTF();
            accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
            playerName = reader.ReadUTF();
            playerId = reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740990)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0 || playerId > 9007199254740990");
            areaId = reader.ReadShort();
            serverId = reader.ReadShort();
            originServerId = reader.ReadShort();
            var limit = reader.ReadUShort();
            socialGroups = new Types.AbstractSocialGroupInfos[limit];
            for (int i = 0; i < limit; i++)
            {
                 socialGroups[i] = ProtocolTypeManager.GetInstance<Types.AbstractSocialGroupInfos>(reader.ReadShort());
                 socialGroups[i].Deserialize(reader);
            }
            playerState = reader.ReadSByte();
            if (playerState < 0)
                throw new Exception("Forbidden value on playerState = " + playerState + ", it doesn't respect the following condition : playerState < 0");
            

}


}


}