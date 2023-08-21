
using ElevatorSimulator.ConsoleApp;
using ElevatorSimulator.Logic;
using ElevatorSimulator.Logic.Builders;
using ElevatorSimulator.Logic.Handlers;
using ElevatorSimulator.Logic.Models;
using ElevatorSimulator.Providers;

var runWithConsoleInput = true;

if (runWithConsoleInput)
{
    RunWithConsoleInput();
}
else
{
    RunMock();
}

Console.ReadKey();


void RunWithConsoleInput()
{
    var outputProvider = new ConsoleOutputProvider();
    var consoleHandler = new ConsoleHandler(outputProvider);
    
    Floor[] floors;
    while(!consoleHandler.TryGetFloorsFromFromUser(out floors))
    {
        
    }

    ElevatorHandler[] elevatorHandlers;
    while (!consoleHandler.TryGetElevatorsFromUser(floors, out elevatorHandlers))
    {
        
    }

    var elevatorsManager = new ElevatorsManager(elevatorHandlers);
    while (true)
    {
        List<Person> waitingPersons;
        while (!consoleHandler.TryGetWaitingPersonsFromUser(floors, out waitingPersons))
        {
            
        }
        outputProvider.WriteLine("Assigning best elevators for persons:");
        foreach (var p in waitingPersons)
        {
            elevatorsManager.AssignBestElevatorToPerson(p);
        }
        outputProvider.WriteEnumerable(waitingPersons);
    }
}

#region Mock for testing
void RunMock()
{
    var outputProvider = new ConsoleOutputProvider();
    var floors = new Floor[]
    {
        new Floor(0),
        new Floor(1),
        new Floor(2),
        new Floor(3),
        new Floor(4),
        new Floor(5)
    };

    var elevatorPool = new Elevator[]
    {
        new Elevator()
        {
            Id = 0,
            CurrentFloorNr = 1,
            State = ElevatorState.Stopped,
            ConsoleColor = ConsoleColor.Green
        },
        new Elevator()
        {
            Id = 1,
            CurrentFloorNr = 2,
            State = ElevatorState.Stopped,
            ConsoleColor = ConsoleColor.Blue
        }
    };

    var elevatorsManager = new ElevatorsManager(new ElevatorHandlerBuilder(outputProvider, floors).Build(elevatorPool));

    var person = new Person
    {
        StartingFloor = floors[1],
        TargetFloor = floors[5]
    };

    var person2 = new Person
    {
        StartingFloor = floors[2],
        TargetFloor = floors[4]
    };

    var person3 = new Person
    {
        StartingFloor = floors[5],
        TargetFloor = floors[0]
    };

    //add them to current flors
    floors[1].WaitingPeople.Add(person);
    floors[2].WaitingPeople.Add(person2);
    floors[3].WaitingPeople.Add(person3);

    //assign elevators
    elevatorsManager.AssignBestElevatorToPerson(person);
    elevatorsManager.AssignBestElevatorToPerson(person2);
    elevatorsManager.AssignBestElevatorToPerson(person3);
}
#endregion




