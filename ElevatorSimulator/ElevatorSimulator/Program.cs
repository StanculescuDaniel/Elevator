// See https://aka.ms/new-console-template for more information
using ElevatorSimulator;
using ElevatorSimulator.Builders;
using ElevatorSimulator.Interface;
using ElevatorSimulator.Models;

Console.WriteLine("Hello, World!");





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


var outputProvider = new ConsoleOutputProvider();
var elevatorsManager = new ElevatorsManager(ElevatorHandlerBuilder.Build(elevatorPool, floors), floors);

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
    StartingFloor = floors[3],
    TargetFloor = floors[5]
};

//add them to current flors
floors[1].WaitingPeople.Add(person);
floors[2].WaitingPeople.Add(person2);
floors[3].WaitingPeople.Add(person3);

//assign elevators
elevatorsManager.AssignBestElevatorToPerson(person);
elevatorsManager.AssignBestElevatorToPerson(person2);
elevatorsManager.AssignBestElevatorToPerson(person3);


Console.ReadKey();





//elevatorLogic.CallElevatorUnit(0, 2, 10);
//elevatorLogic.CallElevatorUnit(0, 2, 10);



//elevatorLogic.Run();



