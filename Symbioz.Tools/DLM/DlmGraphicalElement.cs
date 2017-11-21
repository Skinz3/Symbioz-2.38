using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.DLM
{
    public class DlmGraphicalElement : DlmBasicElement
    {
        public const float CELL_HALF_WIDTH = 43f;
        public const float CELL_HALF_HEIGHT = 21.5f;
        private int m_altitude;
        private uint m_elementId;
        private ColorMultiplicator m_finalTeint;
        private ColorMultiplicator m_hue;
        private uint m_identifier;
        private System.Drawing.Point m_offset;
        private System.Drawing.Point m_pixelOffset;
        private ColorMultiplicator m_shadow;

        public event PropertyChangedEventHandler PropertyChanged;

        public DlmBasicElement.ElementTypesEnum ElementType
        {
            get
            {
                return DlmBasicElement.ElementTypesEnum.Graphical;
            }
        }

        public ColorMultiplicator ColorMultiplicator
        {
            get
            {
                return this.m_finalTeint;
            }
        }

        public int Altitude
        {
            get
            {
                return this.m_altitude;
            }
            set
            {
                if (this.m_altitude == value)
                {
                    return;
                }
                this.m_altitude = value;
                this.OnPropertyChanged("Altitude");
            }
        }

        public uint ElementId
        {
            get
            {
                return this.m_elementId;
            }
            set
            {
                if (this.m_elementId == value)
                {
                    return;
                }
                this.m_elementId = value;
                this.OnPropertyChanged("ElementId");
            }
        }

        public ColorMultiplicator FinalTeint
        {
            get
            {
                return this.m_finalTeint;
            }
            set
            {
                if (this.m_finalTeint == value)
                {
                    return;
                }
                this.m_finalTeint = value;
                this.OnPropertyChanged("FinalTeint");
            }
        }

        public ColorMultiplicator Hue
        {
            get
            {
                return this.m_hue;
            }
            set
            {
                if (this.m_hue == value)
                {
                    return;
                }
                this.m_hue = value;
                this.OnPropertyChanged("Hue");
            }
        }

        public uint Identifier
        {
            get
            {
                return this.m_identifier;
            }
            set
            {
                if (this.m_identifier == value)
                {
                    return;
                }
                this.m_identifier = value;
                this.OnPropertyChanged("Identifier");
            }
        }

        public System.Drawing.Point Offset
        {
            get
            {
                return this.m_offset;
            }
            set
            {
                if (this.m_offset == value)
                {
                    return;
                }
                this.m_offset = value;
                this.OnPropertyChanged("Offset");
            }
        }

        public System.Drawing.Point PixelOffset
        {
            get
            {
                return this.m_pixelOffset;
            }
            set
            {
                if (this.m_pixelOffset == value)
                {
                    return;
                }
                this.m_pixelOffset = value;
                this.OnPropertyChanged("PixelOffset");
            }
        }

        public ColorMultiplicator Shadow
        {
            get
            {
                return this.m_shadow;
            }
            set
            {
                if (this.m_shadow == value)
                {
                    return;
                }
                this.m_shadow = value;
                this.OnPropertyChanged("Shadow");
            }
        }

        public DlmGraphicalElement(DlmCell cell)
            : base(cell)
        {
        }

        public new static DlmGraphicalElement ReadFromStream(DlmCell cell, BigEndianReader reader)
        {
            DlmGraphicalElement element = new DlmGraphicalElement(cell);
            element.m_elementId = reader.ReadUInt();
            element.m_hue = new ColorMultiplicator((int)reader.ReadByte(), (int)reader.ReadByte(), (int)reader.ReadByte(), false);
            element.m_shadow = new ColorMultiplicator((int)reader.ReadByte(), (int)reader.ReadByte(), (int)reader.ReadByte(), false);
            if (cell.Layer.Map.Version <= 4)
            {
                element.m_offset.X = (int)reader.ReadByte();
                element.m_offset.Y = (int)reader.ReadByte();
                element.m_pixelOffset.X = (int)((float)element.m_offset.X * 43f);
                element.m_pixelOffset.Y = (int)((float)element.m_offset.Y * 21.5f);
            }
            else
            {
                element.m_pixelOffset.X = (int)reader.ReadShort();
                element.m_pixelOffset.Y = (int)reader.ReadShort();
                element.m_offset.X = (int)((float)element.m_pixelOffset.X / 43f);
                element.m_offset.Y = (int)((float)element.m_pixelOffset.Y / 21.5f);
            }
            element.m_altitude = (int)reader.ReadByte();
            element.m_identifier = reader.ReadUInt();
            element.CalculateFinalTeint();
            return element;
        }

        public void CalculateFinalTeint()
        {
            int loc = this.m_hue.Red + this.m_shadow.Red;
            int loc2 = this.m_hue.Green + this.m_shadow.Green;
            int loc3 = this.m_hue.Blue + this.m_shadow.Blue;
            loc = ColorMultiplicator.Clamp((loc + 128) * 2, 0, 512);
            loc2 = ColorMultiplicator.Clamp((loc2 + 128) * 2, 0, 512);
            loc3 = ColorMultiplicator.Clamp((loc3 + 128) * 2, 0, 512);
            this.m_finalTeint = new ColorMultiplicator(loc, loc2, loc3, true);
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}