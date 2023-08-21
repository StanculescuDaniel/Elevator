using ElevatorSimulator.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Logic.Tests.Mocks
{
    public class MockOutputProvider : IOutputProvider
    {
        public void Write(string message, ConsoleColor color = ConsoleColor.White)
        {
            
        }

        public void WriteEnumerable(IEnumerable<object> list)
        {
            
        }

        public void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
        {
            
        }
    }
}
