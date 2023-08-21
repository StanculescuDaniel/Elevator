
using ElevatorSimulator.Logic.Abstractions.Extensions;
using System.Text;
using System.Timers;

namespace ElevatorSimulator.Logic.Models
{
    public enum ElevatorState
    {
        MovingUp, 
        MovingDown, 
        Stopped
    }

    public class Elevator
    {
        public int Id { get; set; }
        public ConsoleColor ConsoleColor { get; set; } = ConsoleColor.White;
        public List<Person> PersonsInElevator { get; set; } = new List<Person>();
        public List<Person> PersonsToBePicker { get; set; } = new List<Person>();
        public List<Floor> FloorsToVisit { get; set; } = new List<Floor>();
        public int MaxCapacity { get; set; } = 10;
        public int CurrentFloorNr { get; set; }
        public ElevatorState State { get; set; } = ElevatorState.Stopped;

        public override string ToString()
        {
            return $"Elevator:{Id}    Floor: {CurrentFloorNr}   State:{State}   NrOfPeople:'{this.GetNrOfPeopleInElevator()}' FreeSpots:'{this.GetFreeSpots()}' FloorsToVisit:{this.GetFloorsToVisitString()}";
        }
    }
}
