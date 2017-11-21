using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.D2P
{
    public class D2pProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Key;

        public string Key
        {
            get
            {
                return this._Key;
            }

            set
            {
                if (string.Equals(this._Key, value))
                {
                    return;
                }
                this._Key = value;
                this.OnPropertyChanged("Key");
            }
        }

        private string _Value;

        public string Value
        {
            get
            {
                return this._Value;
            }

            set
            {
                if (string.Equals(this._Value, value))
                {
                    return;
                }
                this._Value = value;
                this.OnPropertyChanged("Value");
            }
        }

        public D2pProperty()
        {
        }

        public D2pProperty(string key, string property)
        {
            this.Key = key;
            this.Value = property;
        }

        public void ReadProperty(IDataReader reader)
        {
            this.Key = reader.ReadUTF();
            this.Value = reader.ReadUTF();
        }

        public void WriteProperty(IDataWriter writer)
        {
            writer.WriteUTF(this.Key);
            writer.WriteUTF(this.Value);
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