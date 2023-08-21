using ElevatorSimulator.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.ConsoleApp.Providers
{
    public class FileOutputProvider : IOutputProvider
    {
        private const string FileName = "elevatorSimulator.txt";
        private readonly object _lock = new();

        public void Write(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.Write(message);
        }

        public void WriteEnumerable(IEnumerable<object> list)
        {
            
        }

        public void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            lock (_lock)
            {
                try
                {
                    var temp = Path.GetTempPath();
                    using (var fs = File.Open(temp + FileName, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter writer = new(fs))
                        {
                            writer.WriteLine(message);
                        }
                        fs.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
