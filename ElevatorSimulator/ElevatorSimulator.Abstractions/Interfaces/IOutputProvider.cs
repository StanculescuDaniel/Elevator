using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Logic.Interface
{
    public interface IOutputProvider
    {
        void WriteLine(string message, ConsoleColor color = ConsoleColor.White);
        void Write(string message, ConsoleColor color = ConsoleColor.White);
        void WriteEnumerable(IEnumerable<object> list);

    }
}
