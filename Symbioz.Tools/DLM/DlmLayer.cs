using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.DLM
{
    public class DlmLayer : INotifyPropertyChanged
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

        private int _LayerId;

        public int LayerId
        {
            get
            {
                return this._LayerId;
            }

            set
            {
                if (this._LayerId == value)
                {
                    return;
                }
                this._LayerId = value;
                this.OnPropertyChanged("LayerId");
            }
        }

        private DlmCell[] _Cells;

        public DlmCell[] Cells
        {
            get
            {
                return this._Cells;
            }

            set
            {
                if (this._Cells == value)
                {
                    return;
                }
                this._Cells = value;
                this.OnPropertyChanged("Cells");
            }
        }

        public DlmLayer(DlmMap map)
        {
            this.Map = map;
        }

        public static DlmLayer ReadFromStream(DlmMap map, BigEndianReader reader)
        {
            DlmLayer layer = new DlmLayer(map);
            if (map.Version >= 9)
            {
                layer.LayerId = reader.ReadByte();
            }
            else
            {
                layer.LayerId = reader.ReadInt();
            }

            layer.Cells = new DlmCell[(int)reader.ReadShort()];
            for (int i = 0; i < layer.Cells.Length; i++)
            {
                layer.Cells[i] = DlmCell.ReadFromStream(layer, reader);
            }
            return layer;
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