using SSync.IO;
using System.ComponentModel;
using System.IO;

namespace Symbioz.Tools.D2P
{
    public class D2pIndexTable : INotifyPropertyChanged
    {
        public const int TableOffset = -24;
        public const SeekOrigin TableSeekOrigin = SeekOrigin.End;

        public event PropertyChangedEventHandler PropertyChanged;

        private D2pFile _Container;

        public D2pFile Container
        {
            get
            {
                return this._Container;
            }

            private set
            {
                if (this._Container == value)
                {
                    return;
                }
                this._Container = value;
                this.OnPropertyChanged("Container");
            }
        }

        private int _OffsetBase;

        public int OffsetBase
        {
            get
            {
                return this._OffsetBase;
            }

            set
            {
                if (this._OffsetBase == value)
                {
                    return;
                }
                this._OffsetBase = value;
                this.OnPropertyChanged("OffsetBase");
            }
        }

        private int _Size;

        public int Size
        {
            get
            {
                return this._Size;
            }

            set
            {
                if (this._Size == value)
                {
                    return;
                }
                this._Size = value;
                this.OnPropertyChanged("Size");
            }
        }

        private int _EntriesDefinitionOffset;

        public int EntriesDefinitionOffset
        {
            get
            {
                return this._EntriesDefinitionOffset;
            }

            set
            {
                if (this._EntriesDefinitionOffset == value)
                {
                    return;
                }
                this._EntriesDefinitionOffset = value;
                this.OnPropertyChanged("EntriesDefinitionOffset");
            }
        }

        private int _EntriesCount;

        public int EntriesCount
        {
            get
            {
                return this._EntriesCount;
            }

            set
            {
                if (this._EntriesCount == value)
                {
                    return;
                }
                this._EntriesCount = value;
                this.OnPropertyChanged("EntriesCount");
            }
        }

        private int _PropertiesOffset;

        public int PropertiesOffset
        {
            get
            {
                return this._PropertiesOffset;
            }

            set
            {
                if (this._PropertiesOffset == value)
                {
                    return;
                }
                this._PropertiesOffset = value;
                this.OnPropertyChanged("PropertiesOffset");
            }
        }

        private int _PropertiesCount;

        public int PropertiesCount
        {
            get
            {
                return this._PropertiesCount;
            }

            set
            {
                if (this._PropertiesCount == value)
                {
                    return;
                }
                this._PropertiesCount = value;
                this.OnPropertyChanged("PropertiesCount");
            }
        }

        public D2pIndexTable(D2pFile container)
        {
            this.Container = container;
        }

        public void ReadTable(IDataReader reader)
        {
            this.OffsetBase = reader.ReadInt();
            this.Size = reader.ReadInt();
            this.EntriesDefinitionOffset = reader.ReadInt();
            this.EntriesCount = reader.ReadInt();
            this.PropertiesOffset = reader.ReadInt();
            this.PropertiesCount = reader.ReadInt();
        }

        public void WriteTable(IDataWriter writer)
        {
            writer.WriteInt(this.OffsetBase);
            writer.WriteInt(this.Size);
            writer.WriteInt(this.EntriesDefinitionOffset);
            writer.WriteInt(this.EntriesCount);
            writer.WriteInt(this.PropertiesOffset);
            writer.WriteInt(this.PropertiesCount);
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