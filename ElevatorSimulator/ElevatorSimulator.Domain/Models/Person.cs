using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Domain.Models
{

    public class Person
    {

        public Floor StartingFloor { get; set; }
        public Floor TargetFloor { get; set; }
        public Elevator? AssignedElevator { get; set; }

        public override string ToString()
        {
            return $"[Person -> Start: {StartingFloor?.FloorNr}, Target: '{TargetFloor?.FloorNr}', AssignedElevator: '{AssignedElevator?.Id}']";
        }

    }

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

        public static PersonDirection GetDirection(this Person person)
        {
            return person.StartingFloor.FloorNr < person.TargetFloor.FloorNr ?
                PersonDirection.Up :
                PersonDirection.Down;
        }
    }

    public enum PersonDirection
    {
        Up,
        Down
    }
}
