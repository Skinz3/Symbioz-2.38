


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class KohUpdateMessage : Message
{

public const ushort Id = 6439;
public override ushort MessageId
{
    get { return Id; }
}

public Types.AllianceInformations[] alliances;
        public ushort[] allianceNbMembers;
        public uint[] allianceRoundWeigth;
        public sbyte[] allianceMatchScore;
        public Types.BasicAllianceInformations allianceMapWinner;
        public uint allianceMapWinnerScore;
        public uint allianceMapMyAllianceScore;
        public double nextTickTime;
        

public KohUpdateMessage()
{
}

public KohUpdateMessage(Types.AllianceInformations[] alliances, ushort[] allianceNbMembers, uint[] allianceRoundWeigth, sbyte[] allianceMatchScore, Types.BasicAllianceInformations allianceMapWinner, uint allianceMapWinnerScore, uint allianceMapMyAllianceScore, double nextTickTime)
        {
            this.alliances = alliances;
            this.allianceNbMembers = allianceNbMembers;
            this.allianceRoundWeigth = allianceRoundWeigth;
            this.allianceMatchScore = allianceMatchScore;
            this.allianceMapWinner = allianceMapWinner;
            this.allianceMapWinnerScore = allianceMapWinnerScore;
            this.allianceMapMyAllianceScore = allianceMapMyAllianceScore;
            this.nextTickTime = nextTickTime;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)alliances.Length);
            foreach (var entry in alliances)
            {
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)allianceNbMembers.Length);
            foreach (var entry in allianceNbMembers)
            {
                 writer.WriteVarUhShort(entry);
            }
            writer.WriteUShort((ushort)allianceRoundWeigth.Length);
            foreach (var entry in allianceRoundWeigth)
            {
                 writer.WriteVarUhInt(entry);
            }
            writer.WriteUShort((ushort)allianceMatchScore.Length);
            foreach (var entry in allianceMatchScore)
            {
                 writer.WriteSByte(entry);
            }
            allianceMapWinner.Serialize(writer);
            writer.WriteVarUhInt(allianceMapWinnerScore);
            writer.WriteVarUhInt(allianceMapMyAllianceScore);
            writer.WriteDouble(nextTickTime);
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            alliances = new Types.AllianceInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 alliances[i] = new Types.AllianceInformations();
                 alliances[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            allianceNbMembers = new ushort[limit];
            for (int i = 0; i < limit; i++)
            {
                 allianceNbMembers[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            allianceRoundWeigth = new uint[limit];
            for (int i = 0; i < limit; i++)
            {
                 allianceRoundWeigth[i] = reader.ReadVarUhInt();
            }
            limit = reader.ReadUShort();
            allianceMatchScore = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 allianceMatchScore[i] = reader.ReadSByte();
            }
            allianceMapWinner = new Types.BasicAllianceInformations();
            allianceMapWinner.Deserialize(reader);
            allianceMapWinnerScore = reader.ReadVarUhInt();
            if (allianceMapWinnerScore < 0)
                throw new Exception("Forbidden value on allianceMapWinnerScore = " + allianceMapWinnerScore + ", it doesn't respect the following condition : allianceMapWinnerScore < 0");
            allianceMapMyAllianceScore = reader.ReadVarUhInt();
            if (allianceMapMyAllianceScore < 0)
                throw new Exception("Forbidden value on allianceMapMyAllianceScore = " + allianceMapMyAllianceScore + ", it doesn't respect the following condition : allianceMapMyAllianceScore < 0");
            nextTickTime = reader.ReadDouble();
            if (nextTickTime < 0 || nextTickTime > 9007199254740990)
                throw new Exception("Forbidden value on nextTickTime = " + nextTickTime + ", it doesn't respect the following condition : nextTickTime < 0 || nextTickTime > 9007199254740990");
            

}


}


}