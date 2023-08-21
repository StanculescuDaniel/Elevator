// See https://aka.ms/new-console-template for more information
using ElevatorSimulator.ConsoleApp;
using ElevatorSimulator.Logic;
using ElevatorSimulator.Logic.Builders;
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
    
    if(!consoleHandler.TryGetFloorsFromFromUser(out var floors))
    {
        return;
    }

    if(!consoleHandler.TryGetElevatorsFromUser(floors, out var elevatorHandlers))
    {
        return;
    }

    var elevatorsManager = new ElevatorsManager(elevatorHandlers, floors);
    while (true)
    {
        if(!consoleHandler.TryGetWaitingPersonsFromUser(floors, out var waitingPersons))
        {
            return;
        }
        outputProvider.WriteLine("Assigning best elevators for persons:");
        foreach (var p in waitingPersons)
        {
            elevatorsManager.AssignBestElevatorToPerson(p);
        }
        outputProvider.WriteEnumerable(waitingPersons);
    }
}

void RunMock()
{
    var outputProvider = new ConsoleOutputProvider();
    var floors = new Floor[]
    {
        new Floor()
        {
            FloorNr = 0
        },
        new Floor()
        {
            FloorNr = 1
        },
        new Floor()
        {
            FloorNr = 2
        },
        new Floor()
        {
            FloorNr = 3
        },
        new Floor()
        {
            FloorNr = 4
        },
        new Floor()
        {
            FloorNr = 5
        },
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

    var elevatorsManager = new ElevatorsManager(new ElevatorHandlerBuilder(outputProvider, floors).Build(elevatorPool), floors);

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




