
using System.Text;
using System.Timers;

namespace ElevatorSimulator.Models
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

        public int CurrentFloorNr { get; set; }
        public ElevatorState State { get; set; } = ElevatorState.Stopped;

        public override string ToString()
        {
            return $"Elevator:{Id}    Floor: {CurrentFloorNr}   State:{State}   NrOfPeople:{this.GetNrOfPeopleInElevator()}";
        }

    }


    public static class ElevatorExtensions
    {
        public static bool IsMoving(this Elevator elevator)
        {
            return elevator.State != ElevatorState.Stopped;
        }
        public static int GetNrOfPeopleInElevator(this Elevator elevator)
        {
            return elevator.PersonsInElevator.Count();
        }

        public static Floor? GetNextFloorToVisit(this Elevator unit)
        {
            return unit.FloorsToVisit.FirstOrDefault();
        }

        public static Floor? GetLastFloorToVisit(this Elevator unit)
        {
            return unit.FloorsToVisit.LastOrDefault();
        }

        public static Floor? GetNextFloorToStop(this Elevator unit)
        {
            var floorsToStopToDropPersons = unit.PersonsInElevator.Select(p => p.TargetFloor);
            var floorsToStopToPickPersons = unit.PersonsToBePicker.Select(p => p.StartingFloor);

            var allFlorsToStop = floorsToStopToDropPersons.Concat(floorsToStopToPickPersons).OrderBy(p => p.FloorNr);
            return allFlorsToStop.FirstOrDefault();
        }

        public static void MoveUp(this Elevator unit)
        {
            unit.CurrentFloorNr++;
            unit.State = ElevatorState.MovingUp;
        }

        public static void MoveDown(this Elevator unit)
        {
            unit.CurrentFloorNr--;
            unit.State = ElevatorState.MovingDown;
        }

        public static void Stop(this Elevator unit)
        {
            unit.State = ElevatorState.Stopped;
        }

        public static void Print(this Elevator unit, string message)
        {
            Console.ForegroundColor = unit.ConsoleColor;
            Console.WriteLine($"{unit} \t-> {message}");
            Console.ResetColor();
        }

    }
}
