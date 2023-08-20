using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Interface
{
    public interface IOutputProvider
    {
        void WriteLine(string message, ConsoleColor color = ConsoleColor.White);
    }
}
