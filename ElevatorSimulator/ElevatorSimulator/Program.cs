// See https://aka.ms/new-console-template for more information
using ElevatorSimulator.Domain;
using ElevatorSimulator.Domain.Builders;
using ElevatorSimulator.Domain.Models;
using ElevatorSimulator.Domain.Providers;

var runWithCOnsoleInput = false;

if (runWithCOnsoleInput)
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

    outputProvider.Write("Enter nr of floors: ");
    var nrOFloorsStr = Console.ReadLine();
    if (!int.TryParse(nrOFloorsStr, out int nrOfFloors) && nrOfFloors <= 0)
    {
        outputProvider.WriteLine($"{nrOFloorsStr} is not valid");
    }
    var floors = FloorsBuilder.Build(nrOfFloors);

    outputProvider.Write("Enter floor numbers where elevators are stopped separated by comma (eg: 2,3,3,6,7): ");
    var elevatorsStr = Console.ReadLine();
    if (string.IsNullOrEmpty(elevatorsStr))
    {
        outputProvider.WriteLine($"{elevatorsStr} is not valid");
    }

    var floorNrsStr = elevatorsStr.Split(',');
    var floorNrs = floorNrsStr.Select(p => int.Parse(p)).ToArray();
    if (floorNrs.Max() > nrOfFloors)
    {
        outputProvider.WriteLine($"{elevatorsStr} contains a floor which is higher than the entered of floors '{nrOfFloors}'");
    }

    var elevatorBuilder = new ElevatorHandlerBuilder(outputProvider, floors);
    var elevatorHandlers = elevatorBuilder.Build(floorNrs);

    outputProvider.WriteLine("The following elevators were created: ");
    foreach (var handler in elevatorHandlers)
    {
        outputProvider.WriteLine(handler.Elevator.ToString());
    }


    outputProvider.Write("Enter waiting persons by entering their current and their target floor separated by comma. Persons are separated by space. (Eg. 1,5 2,4 3,5):");

    var personsStr = Console.ReadLine();
    if (string.IsNullOrEmpty(personsStr))
    {
        outputProvider.WriteLine($"{personsStr} is not valid");
    }

    var watingPersonsBuilder = new WaitingPersonBuilder(floors);
    var persons = watingPersonsBuilder.Buid(personsStr);

    outputProvider.WriteLine("The following persons were created:");
    outputProvider.WriteEnumerable(persons);

    var elevatorsManager = new ElevatorsManager(elevatorHandlers, floors);
    outputProvider.WriteLine("Assigning best elevators for persons:");
    foreach (var p in persons)
    {
        elevatorsManager.AssignBestElevatorToPerson(p);
    }
    outputProvider.WriteEnumerable(persons);
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




