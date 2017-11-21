using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public class UpdateLogger
    {
        private object m_lastValue;

        public UpdateLogger()
        {
            PositionX = Console.CursorLeft;
            PositionY = Console.CursorTop;
        }

        public UpdateLogger(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public int PositionX
        {
            get;
            set;
        }

        public int PositionY
        {
            get;
            set;
        }

        public void Update(int value)
        {
            if (value.Equals(m_lastValue))
                return;

            m_lastValue = value;

            int oldX = Console.CursorLeft;
            int oldY = Console.CursorTop;

            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(value + "%");
            Console.SetCursorPosition(oldX, oldY);
        }

        public void Update(string value)
        {
            if (value.Equals(m_lastValue))
                return;

            int oldX = Console.CursorLeft;
            int oldY = Console.CursorTop;

            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(value);
            Console.SetCursorPosition(oldX, oldY);
        }

        public void End()
        {
            string cleaner = string.Empty;

            for (int i = 0; i < Console.BufferWidth - PositionX; i++)
            {
                cleaner += " ";
            }


            int oldX = Console.CursorLeft;
            int oldY = Console.CursorTop;

            Console.SetCursorPosition(PositionX, PositionY);
            Console.Write(cleaner);
            Console.SetCursorPosition(oldX, oldY);
        }
    }
}
