using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public class Logger
    {
        public Type ClassType { get; set; }

        public Logger()
        {
            StackFrame frame = new StackFrame(1, false);
            this.ClassType = frame.GetMethod().DeclaringType;
        }
        public void NewLine()
        {
            Console.WriteLine(Environment.NewLine);
        }
        public void Color1(object value, bool writeType = true)
        {
            Write(value, ConsoleColor.Green, writeType == false ? null : ClassType);
        }
        public void Color2(object value, bool writeType = true)
        {
            Write(value, ConsoleColor.DarkGreen, writeType == false ? null : ClassType);
        }
        public void Gray(object value)
        {
            Write(value, ConsoleColor.Gray, ClassType);
        }
        public void DarkGray(object value)
        {
            Write(value, ConsoleColor.DarkGray, ClassType);
        }
        public void White(object value)
        {
            Write(value, ConsoleColor.White, ClassType);
        }
        public void Error(object value)
        {
            Write(value, ConsoleColor.Red, ClassType);
        }
        public void Alert(object value)
        {
            Write(value, ConsoleColor.DarkRed, ClassType);
        }
        public static void Write(object value, ConsoleColor color, Type classType = null)
        {
            Console.ForegroundColor = color;
            if (classType != null)
                Console.WriteLine("[" + classType.Name + "] " + value);
            else
                Console.WriteLine(value);
        }
        public static void Write<T>(string value, ConsoleColor color)
        {
            Write(value, color, typeof(T));
        }
        private void Logo()
        {
            Color1(@"  _________            ___.   .__              ", false);
            Color2(@" /   _____/__.__. _____\_ |__ |__| ____________", false);
            Color1(@" \_____  <   |  |/     \| __ \|  |/  _ \___   /", false);
            Color2(@" /        \___  |  Y Y  \ \_\ \  (  <_> )    / ", false);
            Color1(@"/_______  / ____|__|_|  /___  /__|\____/_____ \", false);
            Color2(@"        \/\/          \/    \/               \/", false);
            Color1(@"Dofus 2.38.0.113902.1", false);

        }
        public void OnStartup()
        {
            Console.Title = Assembly.GetCallingAssembly().GetName().Name;
            Logo();
            NewLine();
            Color2("Written by Skinz", false);
            Color2("Check out my repo's!: https://github.com/Skinz3/", false);
            NewLine();
        }
    }
}
