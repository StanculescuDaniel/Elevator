using ElevatorSimulator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    public class ConsoleOutputProvider: IOutputProvider
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
