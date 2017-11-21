using SSync.IO;
using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.DLM
{
    public class DlmCell : INotifyPropertyChanged
    {
        private DlmLayer m_layer;

        public event PropertyChangedEventHandler PropertyChanged;

        public DlmLayer Layer
        {
            get
            {
                return this.m_layer;
            }
            set
            {
                if (this.m_layer == value)
                {
                    return;
                }
                this.m_layer = value;
                this.OnPropertyChanged("Layer");
            }
        }

        private short _Id;

        public short Id
        {
            get
            {
                return this._Id;
            }

            set
            {
                this._Id = value;
                this.OnPropertyChanged("Id");
            }
        }

        private DlmBasicElement[] _Elements;

        public DlmBasicElement[] Elements
        {
            get
            {
                return this._Elements;
            }

            set
            {
                if (this._Elements == value)
                {
                    return;
                }
                this._Elements = value;
                this.OnPropertyChanged("Elements");
            }
        }

        public DlmCell(DlmLayer layer)
        {
            this.Layer = layer;
        }

        public static DlmCell ReadFromStream(DlmLayer layer, BigEndianReader reader)
        {
            DlmCell cell = new DlmCell(layer);
            cell.Id = reader.ReadShort();
            cell.Elements = new DlmBasicElement[(int)reader.ReadShort()];
            for (int i = 0; i < cell.Elements.Length; i++)
            {
                DlmBasicElement element = DlmBasicElement.ReadFromStream(cell, reader);
                cell.Elements[i] = element;
            }
            return cell;
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