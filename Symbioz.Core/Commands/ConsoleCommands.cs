using Symbioz.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core.Commands
{
    public class ConsoleCommands : Singleton<ConsoleCommands>
    {
        private delegate void ConsoleCommandDelegate(string input);

        private readonly Dictionary<string, ConsoleCommandDelegate> m_commands = new Dictionary<string, ConsoleCommandDelegate>();

        static Logger logger = new Logger();

        public void Initialize(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var attribute = method.GetCustomAttribute(typeof(ConsoleCommandAttribute)) as ConsoleCommandAttribute;

                    if (attribute != null)
                    {
                        m_commands.Add(attribute.Name, (ConsoleCommandDelegate)method.CreateDelegate(typeof(ConsoleCommandDelegate)));
                    }

                }
            }
            m_commands.Add("help", HelpCommand);
            logger.Gray(m_commands.Count + " command(s) registered");
        }

        private void HelpCommand(string input)
        {
            logger.Color1("Commands :");
            foreach (var item in m_commands)
            {
                logger.Color2("-" + item.Key);
            }
        }
        public void WaitHandle()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input != string.Empty)
                {
                    try
                    {
                        Handle(input);
                    }
                    catch (Exception ex)
                    {
                        logger.Alert(ex.ToString());
                    }
                }
            }
        }
        private void Handle(string input)
        {

            string commandName = input.Split(null).First().ToLower();

            var command = m_commands.FirstOrDefault(x => x.Key == commandName);

            if (command.Value != null)
            {
                command.Value.DynamicInvoke(new string(input.Skip(commandName.Length + 1).ToArray()));
            }
            else
            {
                logger.Color2(string.Format("{0} is not a valid command. (type 'help' for a list of commands)", commandName));
            }
        }
    }
}
