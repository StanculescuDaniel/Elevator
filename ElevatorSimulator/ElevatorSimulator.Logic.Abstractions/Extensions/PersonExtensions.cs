using ElevatorSimulator.Logic.Models;

namespace ElevatorSimulator.Logic.Abstractions.Extensions
{
    public static class PersonExtensions
    {
        public static int GetTargetFloorNumber(this Person person)
        {
            return person.TargetFloor.FloorNr;
        }

        public static int GetStartingFloorNumber(this Person person)
        {
            return person.StartingFloor.FloorNr;
        }

        public static bool WantsToGoUp(this Person person)
        {
            return person.StartingFloor.FloorNr < person.TargetFloor.FloorNr;
        }

        public static bool WantsToGoDown(this Person person)
        {
            return person.StartingFloor.FloorNr > person.TargetFloor.FloorNr;
        }
    }
}
