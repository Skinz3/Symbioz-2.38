using System.ComponentModel;

namespace Symbioz.Tools.DLM
{
    public class ColorMultiplicator : INotifyPropertyChanged
    {
        private int m_blue;
        private int m_green;
        private int m_red;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _IsOne;

        public bool IsOne
        {
            get
            {
                return this._IsOne;
            }

            private set
            {
                if (this._IsOne == value)
                {
                    return;
                }
                this._IsOne = value;
                this.OnPropertyChanged("IsOne");
            }
        }

        public int Blue
        {
            get
            {
                return this.m_blue;
            }
            set
            {
                if (this.m_blue == value)
                {
                    return;
                }
                this.m_blue = value;
                this.OnPropertyChanged("Blue");
            }
        }

        public int Green
        {
            get
            {
                return this.m_green;
            }
            set
            {
                if (this.m_green == value)
                {
                    return;
                }
                this.m_green = value;
                this.OnPropertyChanged("Green");
            }
        }

        public int Red
        {
            get
            {
                return this.m_red;
            }
            set
            {
                if (this.m_red == value)
                {
                    return;
                }
                this.m_red = value;
                this.OnPropertyChanged("Red");
            }
        }

        public ColorMultiplicator(int p1, int p2, int p3, bool p4)
        {
            this.m_red = p1;
            this.m_green = p2;
            this.m_blue = p3;
            if (!p4 && p1 + p2 + p3 == 0)
            {
                this.IsOne = true;
            }
        }

        public ColorMultiplicator Multiply(ColorMultiplicator p1)
        {
            ColorMultiplicator result;
            if (this.IsOne)
            {
                result = p1;
            }
            else
            {
                if (p1.IsOne)
                {
                    result = this;
                }
                else
                {
                    ColorMultiplicator multiplicator = new ColorMultiplicator(0, 0, 0, false)
                    {
                        m_red = this.m_red + p1.m_red,
                        m_green = this.m_green + p1.m_green,
                        m_blue = this.m_blue + p1.m_blue
                    };
                    multiplicator.m_red = ColorMultiplicator.Clamp(multiplicator.m_red, -128, 127);
                    multiplicator.m_green = ColorMultiplicator.Clamp(multiplicator.m_green, -128, -127);
                    multiplicator.m_blue = ColorMultiplicator.Clamp(multiplicator.m_blue, -128, 127);
                    multiplicator.IsOne = false;
                    result = multiplicator;
                }
            }
            return result;
        }

        public static int Clamp(int p1, int p2, int p3)
        {
            int result;
            if (p1 > p3)
            {
                result = p3;
            }
            else
            {
                result = ((p1 < p2) ? p2 : p1);
            }
            return result;
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