
using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.DLM
{
    public class DlmFixture : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DlmMap _Map;

        public DlmMap Map
        {
            get
            {
                return this._Map;
            }

            set
            {
                if (this._Map == value)
                {
                    return;
                }
                this._Map = value;
                this.OnPropertyChanged("Map");
            }
        }

        private int _FixtureId;

        public int FixtureId
        {
            get
            {
                return this._FixtureId;
            }

            private set
            {
                if (this._FixtureId == value)
                {
                    return;
                }
                this._FixtureId = value;
                this.OnPropertyChanged("FixtureId");
            }
        }

        private System.Drawing.Point _Offset;

        public System.Drawing.Point Offset
        {
            get
            {
                return this._Offset;
            }

            set
            {
                if (this._Offset == value)
                {
                    return;
                }
                this._Offset = value;
                this.OnPropertyChanged("Offset");
            }
        }

        private short _Rotation;

        public short Rotation
        {
            get
            {
                return this._Rotation;
            }

            set
            {
                this._Rotation = value;
                this.OnPropertyChanged("Rotation");
            }
        }

        private short _ScaleX;

        public short ScaleX
        {
            get
            {
                return this._ScaleX;
            }

            set
            {
                this._ScaleX = value;
                this.OnPropertyChanged("ScaleX");
            }
        }

        private short _ScaleY;

        public short ScaleY
        {
            get
            {
                return this._ScaleY;
            }

            set
            {
                this._ScaleY = value;
                this.OnPropertyChanged("ScaleY");
            }
        }

        private int _Hue;

        public int Hue
        {
            get
            {
                return this._Hue;
            }

            set
            {
                if (this._Hue == value)
                {
                    return;
                }
                this._Hue = value;
                this.OnPropertyChanged("Hue");
            }
        }

        private byte _Alpha;

        public byte Alpha
        {
            get
            {
                return this._Alpha;
            }

            set
            {
                this._Alpha = value;
                this.OnPropertyChanged("Alpha");
            }
        }

        public DlmFixture(DlmMap map)
        {
            this.Map = map;
        }

        public static DlmFixture ReadFromStream(DlmMap map, BigEndianReader reader)
        {
            return new DlmFixture(map)
            {
                FixtureId = reader.ReadInt(),
                Offset = new System.Drawing.Point((int)reader.ReadShort(), (int)reader.ReadShort()),
                Rotation = reader.ReadShort(),
                ScaleX = reader.ReadShort(),
                ScaleY = reader.ReadShort(),
                Hue = (int)reader.ReadByte() << 16 | (int)reader.ReadByte() << 8 | (int)reader.ReadByte(),
                Alpha = (byte)reader.ReadSByte(),
            };
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