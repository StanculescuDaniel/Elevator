using ElevatorSimulator.Logic.Builders;
using ElevatorSimulator.Logic.Handlers;
using ElevatorSimulator.Logic.Interface;
using ElevatorSimulator.Logic.Models;

namespace ElevatorSimulator.ConsoleApp
{
    public class ConsoleHandler
    {
        private readonly IOutputProvider outputProvider;
        public ConsoleHandler(IOutputProvider op)
        {
            outputProvider = op;
        }

        public bool TryGetFloorsFromFromUser(out Floor[] floors)
        {
            floors = Array.Empty<Floor>();
            outputProvider.Write("Enter nr of floors: ");
            var nrOFloorsStr = Console.ReadLine();
            if (!int.TryParse(nrOFloorsStr, out int nrOfFloors) && nrOfFloors <= 0)
            {
                outputProvider.WriteLine($"{nrOFloorsStr} is not valid");
                return false;
            }
            floors = FloorsBuilder.Build(nrOfFloors);
            return true;
        }

        public bool TryGetElevatorsFromUser(Floor[] floors, out ElevatorHandler[] elevatorHandlers)
        {
            elevatorHandlers = Array.Empty<ElevatorHandler>();
            outputProvider.WriteLine("Enter floor numbers where elevators are stopped separated by comma (eg: 2,3,3,6,7): ");
            var elevatorsStr = Console.ReadLine();
            if (string.IsNullOrEmpty(elevatorsStr))
            {
                outputProvider.WriteLine($"{elevatorsStr} is not valid");
                return false;
            }

            var floorNrsStr = elevatorsStr.Split(',');
            var floorNrs = floorNrsStr.Select(p => int.Parse(p)).ToArray();
            if (floorNrs.Max() >= floors.Length)
            {
                outputProvider.WriteLine($"{elevatorsStr} contains a floor which is higher than the entered nr of floors '{floors.Length}'");
                return false;
            }

            var elevatorBuilder = new ElevatorHandlerBuilder(outputProvider, floors);
            elevatorHandlers = elevatorBuilder.Build(floorNrs);

            outputProvider.WriteLine("The following elevators were created: ");

            foreach(var handler in elevatorHandlers)
            {
                handler.PrintState();
            }

            return true;
        }

        public bool TryGetWaitingPersonsFromUser(Floor[] floors, out List<Person> list)
        {
            list = new List<Person>();
            outputProvider.WriteLine("Enter waiting persons by entering their current and target floor separated by comma. Persons are separated by space. (Eg. 1,5 2,4 3,5). You can also enter these values while elevators start to move:");
            var consoleInput = Console.ReadLine();
            if (string.IsNullOrEmpty(consoleInput))
            {
                outputProvider.WriteLine($"{consoleInput} is not valid");
                return false;
            }

            var watingPersonsBuilder = new WaitingPersonBuilder(floors);
            var splittedPersonsStr = consoleInput.Split(' ');
            foreach (var personStr in splittedPersonsStr)
            {
                var splittedPersonStr = personStr.Split(',');

                if(splittedPersonStr.Length != 2)
                {
                    outputProvider.WriteLine($"{personStr} is not valid");
                    return false;
                }

                if(!int.TryParse(splittedPersonStr[0], out var startingFloor) ||
                   !int.TryParse(splittedPersonStr[1], out var targetFloor))
                {
                    outputProvider.WriteLine($"{splittedPersonStr} is not valid");
                    return false;
                }

                if(startingFloor >= floors.Length ||
                   targetFloor >= floors.Length)
                {
                    outputProvider.WriteLine($"Max floor number is {floors.Length - 1}");
                    return false;
                }
                

                var person = watingPersonsBuilder.Buid(startingFloor, targetFloor);
                list.Add(person);
            }

            return true;
        }
    }
}
