// See https://aka.ms/new-console-template for more information
using ElevatorSimulator;
using ElevatorSimulator.Builders;
using ElevatorSimulator.Models;

var outputProvider = new ConsoleOutputProvider();

outputProvider.Write("Enter nr of floors: ");
var nrOFloorsStr = Console.ReadLine();
if(!int.TryParse(nrOFloorsStr, out int nrOfFloors) && nrOfFloors <= 0)
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

Console.ReadKey();




