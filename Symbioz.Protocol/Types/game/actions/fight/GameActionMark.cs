


















// Generated on 04/27/2016 01:13:09
using System;
using System.Collections.Generic;
using System.Linq;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

public class GameActionMark
{

public const short Id = 351;
public virtual short TypeId
{
    get { return Id; }
}

public double markAuthorId;
        public sbyte markTeamId;
        public int markSpellId;
        public sbyte markSpellLevel;
        public short markId;
        public sbyte markType;
        public short markimpactCell;
        public GameActionMarkedCell[] cells;
        public bool active;
        

public GameActionMark()
{
}

public GameActionMark(double markAuthorId, sbyte markTeamId, int markSpellId, sbyte markSpellLevel, short markId, sbyte markType, short markimpactCell, GameActionMarkedCell[] cells, bool active)
        {
            this.markAuthorId = markAuthorId;
            this.markTeamId = markTeamId;
            this.markSpellId = markSpellId;
            this.markSpellLevel = markSpellLevel;
            this.markId = markId;
            this.markType = markType;
            this.markimpactCell = markimpactCell;
            this.cells = cells;
            this.active = active;
        }
        

public virtual void Serialize(ICustomDataOutput writer)
{

writer.WriteDouble(markAuthorId);
            writer.WriteSByte(markTeamId);
            writer.WriteInt(markSpellId);
            writer.WriteSByte(markSpellLevel);
            writer.WriteShort(markId);
            writer.WriteSByte(markType);
            writer.WriteShort(markimpactCell);
            writer.WriteUShort((ushort)cells.Length);
            foreach (var entry in cells)
            {
                 entry.Serialize(writer);
            }
            writer.WriteBoolean(active);
            

}

public virtual void Deserialize(ICustomDataInput reader)
{

markAuthorId = reader.ReadDouble();
            if (markAuthorId < -9007199254740990 || markAuthorId > 9007199254740990)
                throw new Exception("Forbidden value on markAuthorId = " + markAuthorId + ", it doesn't respect the following condition : markAuthorId < -9007199254740990 || markAuthorId > 9007199254740990");
            markTeamId = reader.ReadSByte();
            if (markTeamId < 0)
                throw new Exception("Forbidden value on markTeamId = " + markTeamId + ", it doesn't respect the following condition : markTeamId < 0");
            markSpellId = reader.ReadInt();
            if (markSpellId < 0)
                throw new Exception("Forbidden value on markSpellId = " + markSpellId + ", it doesn't respect the following condition : markSpellId < 0");
            markSpellLevel = reader.ReadSByte();
            if (markSpellLevel < 1 || markSpellLevel > 6)
                throw new Exception("Forbidden value on markSpellLevel = " + markSpellLevel + ", it doesn't respect the following condition : markSpellLevel < 1 || markSpellLevel > 6");
            markId = reader.ReadShort();
            markType = reader.ReadSByte();
            markimpactCell = reader.ReadShort();
            if (markimpactCell < -1 || markimpactCell > 559)
                throw new Exception("Forbidden value on markimpactCell = " + markimpactCell + ", it doesn't respect the following condition : markimpactCell < -1 || markimpactCell > 559");
            var limit = reader.ReadUShort();
            cells = new GameActionMarkedCell[limit];
            for (int i = 0; i < limit; i++)
            {
                 cells[i] = new GameActionMarkedCell();
                 cells[i].Deserialize(reader);
            }
            active = reader.ReadBoolean();
            

}


}


}