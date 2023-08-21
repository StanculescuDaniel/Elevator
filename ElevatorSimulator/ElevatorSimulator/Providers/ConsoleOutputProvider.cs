using ElevatorSimulator.Logic.Interface;
using ElevatorSimulator.Logic.Models;

namespace ElevatorSimulator.Providers
{
    public class ConsoleOutputProvider : IOutputProvider
    {
        private readonly object _lock = new();
        public void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            lock (_lock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }

        public void Write(string message, ConsoleColor color = ConsoleColor.White)
        {
            lock (_lock)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.ResetColor();
            }
        }

        public void WriteEnumerable(IEnumerable<object> list)
        {
            lock (_lock)
            {
                foreach (var item in list)
                {
                    WriteLine(item.ToString());
                }
            }
        }


    }
}
