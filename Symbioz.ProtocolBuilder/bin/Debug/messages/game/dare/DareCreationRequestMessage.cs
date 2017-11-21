


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class DareCreationRequestMessage : Message
{

public const ushort Id = 6665;
public override ushort MessageId
{
    get { return Id; }
}

public bool isPrivate;
        public bool isForGuild;
        public bool isForAlliance;
        public bool needNotifications;
        public int subscriptionFee;
        public int jackpot;
        public ushort maxCountWinners;
        public uint delayBeforeStart;
        public uint duration;
        public Types.DareCriteria[] criterions;
        

public DareCreationRequestMessage()
{
}

public DareCreationRequestMessage(bool isPrivate, bool isForGuild, bool isForAlliance, bool needNotifications, int subscriptionFee, int jackpot, ushort maxCountWinners, uint delayBeforeStart, uint duration, Types.DareCriteria[] criterions)
        {
            this.isPrivate = isPrivate;
            this.isForGuild = isForGuild;
            this.isForAlliance = isForAlliance;
            this.needNotifications = needNotifications;
            this.subscriptionFee = subscriptionFee;
            this.jackpot = jackpot;
            this.maxCountWinners = maxCountWinners;
            this.delayBeforeStart = delayBeforeStart;
            this.duration = duration;
            this.criterions = criterions;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, isPrivate);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, isForGuild);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 2, isForAlliance);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 3, needNotifications);
            writer.WriteByte(flag1);
            writer.WriteInt(subscriptionFee);
            writer.WriteInt(jackpot);
            writer.WriteUShort(maxCountWinners);
            writer.WriteUInt(delayBeforeStart);
            writer.WriteUInt(duration);
            writer.WriteUShort((ushort)criterions.Length);
            foreach (var entry in criterions)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

byte flag1 = reader.ReadByte();
            isPrivate = BooleanByteWrapper.GetFlag(flag1, 0);
            isForGuild = BooleanByteWrapper.GetFlag(flag1, 1);
            isForAlliance = BooleanByteWrapper.GetFlag(flag1, 2);
            needNotifications = BooleanByteWrapper.GetFlag(flag1, 3);
            subscriptionFee = reader.ReadInt();
            if (subscriptionFee < 0)
                throw new Exception("Forbidden value on subscriptionFee = " + subscriptionFee + ", it doesn't respect the following condition : subscriptionFee < 0");
            jackpot = reader.ReadInt();
            if (jackpot < 0)
                throw new Exception("Forbidden value on jackpot = " + jackpot + ", it doesn't respect the following condition : jackpot < 0");
            maxCountWinners = reader.ReadUShort();
            if (maxCountWinners < 0 || maxCountWinners > 65535)
                throw new Exception("Forbidden value on maxCountWinners = " + maxCountWinners + ", it doesn't respect the following condition : maxCountWinners < 0 || maxCountWinners > 65535");
            delayBeforeStart = reader.ReadUInt();
            if (delayBeforeStart < 0 || delayBeforeStart > 4294967295)
                throw new Exception("Forbidden value on delayBeforeStart = " + delayBeforeStart + ", it doesn't respect the following condition : delayBeforeStart < 0 || delayBeforeStart > 4294967295");
            duration = reader.ReadUInt();
            if (duration < 0 || duration > 4294967295)
                throw new Exception("Forbidden value on duration = " + duration + ", it doesn't respect the following condition : duration < 0 || duration > 4294967295");
            var limit = reader.ReadUShort();
            criterions = new Types.DareCriteria[limit];
            for (int i = 0; i < limit; i++)
            {
                 criterions[i] = new Types.DareCriteria();
                 criterions[i].Deserialize(reader);
            }
            

}


}


}