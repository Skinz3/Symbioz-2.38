using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.DLM
{
    public class DlmSoundElement : DlmBasicElement
    {
        private int m_baseVolume;
        private int m_fullVolumedistance;
        private int m_maxDelayBetweenloops;
        private int m_minDelayBetweenloops;
        private int m_nullVolumedistance;
        private int m_soundId;

        public event PropertyChangedEventHandler PropertyChanged;

        public int BaseVolume
        {
            get
            {
                return this.m_baseVolume;
            }
            set
            {
                if (this.m_baseVolume == value)
                {
                    return;
                }
                this.m_baseVolume = value;
                this.OnPropertyChanged("BaseVolume");
            }
        }

        public int FullVolumedistance
        {
            get
            {
                return this.m_fullVolumedistance;
            }
            set
            {
                if (this.m_fullVolumedistance == value)
                {
                    return;
                }
                this.m_fullVolumedistance = value;
                this.OnPropertyChanged("FullVolumedistance");
            }
        }

        public int MaxDelayBetweenloops
        {
            get
            {
                return this.m_maxDelayBetweenloops;
            }
            set
            {
                if (this.m_maxDelayBetweenloops == value)
                {
                    return;
                }
                this.m_maxDelayBetweenloops = value;
                this.OnPropertyChanged("MaxDelayBetweenloops");
            }
        }

        public int MinDelayBetweenloops
        {
            get
            {
                return this.m_minDelayBetweenloops;
            }
            set
            {
                if (this.m_minDelayBetweenloops == value)
                {
                    return;
                }
                this.m_minDelayBetweenloops = value;
                this.OnPropertyChanged("MinDelayBetweenloops");
            }
        }

        public int NullVolumedistance
        {
            get
            {
                return this.m_nullVolumedistance;
            }
            set
            {
                if (this.m_nullVolumedistance == value)
                {
                    return;
                }
                this.m_nullVolumedistance = value;
                this.OnPropertyChanged("NullVolumedistance");
            }
        }

        public int SoundId
        {
            get
            {
                return this.m_soundId;
            }
            set
            {
                if (this.m_soundId == value)
                {
                    return;
                }
                this.m_soundId = value;
                this.OnPropertyChanged("SoundId");
            }
        }

        public DlmSoundElement(DlmCell cell)
            : base(cell)
        {
        }

        public new static DlmSoundElement ReadFromStream(DlmCell cell, BigEndianReader reader)
        {
            return new DlmSoundElement(cell)
            {
                m_soundId = reader.ReadInt(),
                m_baseVolume = (int)reader.ReadShort(),
                m_fullVolumedistance = reader.ReadInt(),
                m_nullVolumedistance = reader.ReadInt(),
                m_minDelayBetweenloops = (int)reader.ReadShort(),
                m_maxDelayBetweenloops = (int)reader.ReadShort()
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