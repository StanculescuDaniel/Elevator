using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Logic.Models
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
}
