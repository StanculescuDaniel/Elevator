
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

        public int CurrentFloorNr { get; set; }
        public ElevatorState State { get; set; } = ElevatorState.Stopped;

        public override string ToString()
        {
            return $"Elevator:{Id}    Floor: {CurrentFloorNr}   State:{State}   NrOfPeople:{this.GetNrOfPeopleInElevator()}";
        }

    }


    public static class ElevatorExtensions
    {

        private static ConsoleColor[] Colors = new ConsoleColor[]
        {
            ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Yellow
        };

        public static void AutoAssignColor(this Elevator elevator)
        {
            if(elevator.Id < Colors.Length)
            {
                elevator.ConsoleColor = Colors[elevator.Id];
            }
        }

        public static bool IsMoving(this Elevator elevator)
        {
            return elevator.State != ElevatorState.Stopped;
        }
        public static int GetNrOfPeopleInElevator(this Elevator elevator)
        {
            return elevator.PersonsInElevator.Count();
        }

        public static Floor? GetNextFloorToVisit(this Elevator elevator)
        {
            return elevator.FloorsToVisit.FirstOrDefault();
        }

        public static Floor? GetLastFloorToVisit(this Elevator elevator)
        {
            return elevator.FloorsToVisit.LastOrDefault();
        }

        public static void MoveUp(this Elevator elevator)
        {
            elevator.CurrentFloorNr++;
            elevator.State = ElevatorState.MovingUp;
        }

        public static void MoveDown(this Elevator elevator)
        {
            elevator.CurrentFloorNr--;
            elevator.State = ElevatorState.MovingDown;
        }
    }
}
