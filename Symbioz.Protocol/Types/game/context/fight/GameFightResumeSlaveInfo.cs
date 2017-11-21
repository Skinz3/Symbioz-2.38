


















// Generated on 04/27/2016 01:13:12
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameFightResumeSlaveInfo
{

public const short Id = 364;
public virtual short TypeId
{
    get { return Id; }
}

public double slaveId;
        public GameFightSpellCooldown[] spellCooldowns;
        public sbyte summonCount;
        public sbyte bombCount;
        

public GameFightResumeSlaveInfo()
{
}

public GameFightResumeSlaveInfo(double slaveId,GameFightSpellCooldown[] spellCooldowns, sbyte summonCount, sbyte bombCount)
        {
            this.slaveId = slaveId;
            this.spellCooldowns = spellCooldowns;
            this.summonCount = summonCount;
            this.bombCount = bombCount;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(slaveId);
            writer.WriteUShort((ushort)spellCooldowns.Length);
            foreach (var entry in spellCooldowns)
            {
                 entry.Serialize(writer);
            }
            writer.WriteSByte(summonCount);
            writer.WriteSByte(bombCount);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

slaveId = reader.ReadDouble();
            if (slaveId < -9007199254740990 || slaveId > 9007199254740990)
                throw new Exception("Forbidden value on slaveId = " + slaveId + ", it doesn't respect the following condition : slaveId < -9007199254740990 || slaveId > 9007199254740990");
            var limit = reader.ReadUShort();
            spellCooldowns = new GameFightSpellCooldown[limit];
            for (int i = 0; i < limit; i++)
            {
                 spellCooldowns[i] = new GameFightSpellCooldown();
                 spellCooldowns[i].Deserialize(reader);
            }
            summonCount = reader.ReadSByte();
            if (summonCount < 0)
                throw new Exception("Forbidden value on summonCount = " + summonCount + ", it doesn't respect the following condition : summonCount < 0");
            bombCount = reader.ReadSByte();
            if (bombCount < 0)
                throw new Exception("Forbidden value on bombCount = " + bombCount + ", it doesn't respect the following condition : bombCount < 0");
            

}


}


}