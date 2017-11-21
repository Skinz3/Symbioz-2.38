using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.ELE.Repertory
{
    public class AnimatedGraphicalElementData : NormalGraphicalElementData
    {
        

        public override EleGraphicalElementTypes Type
        {
            get
            {
                return EleGraphicalElementTypes.ANIMATED;
            }
        }

        private uint _MinDelay;

        public uint MinDelay
        {
            get
            {
                return this._MinDelay;
            }

            set
            {
                if (this._MinDelay == value)
                {
                    return;
                }
                this._MinDelay = value;
            }
        }

        private uint _MaxDelay;

        public uint MaxDelay
        {
            get
            {
                return this._MaxDelay;
            }

            set
            {
                if (this._MaxDelay == value)
                {
                    return;
                }
                this._MaxDelay = value;
            }
        }

        public AnimatedGraphicalElementData(Elements instance, int id)
            : base(instance, id)
        {
        }

        public new static AnimatedGraphicalElementData ReadFromStream(Elements instance, int id, BigEndianReader reader)
        {
            AnimatedGraphicalElementData data = new AnimatedGraphicalElementData(instance, id);
            data.Gfx = reader.ReadInt();
            data.Height = reader.ReadByte();
            data.HorizontalSymmetry = reader.ReadBoolean();
            data.Origin = new System.Drawing.Point((int)reader.ReadShort(), (int)reader.ReadShort());
            data.Size = new System.Drawing.Size((int)reader.ReadShort(), (int)reader.ReadShort());
            if (instance.Version == 4)
            {
                data.MinDelay = reader.ReadUInt();
                data.MaxDelay = reader.ReadUInt();
            }
            return data;
        }
    }
}