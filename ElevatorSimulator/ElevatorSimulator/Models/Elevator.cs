
using System.Text;
using System.Timers;

namespace ElevatorSimulator.Models
{
    public enum ElevatorDirection
    {
        MovingUp, MovingDown, Stopped
    }

    public class Elevator
    {
        public int Id { get; set; }
        public ConsoleColor ConsoleColor { get; set; } = ConsoleColor.White;
        public List<Person> PersonsInElevator { get; set; } = new List<Person>();
        public List<Person> PersonsToBePicker { get; set; } = new List<Person>();
        public List<Floor> FloorsToVisit { get; set; } = new List<Floor>();

        public Floor CurrentFloor {

            get
            {
                return _floors.First(p => p.FloorNr == CurrentFloorNr);
            } 
        }
        public int CurrentFloorNr { get; set; }
        public ElevatorDirection State { get; set; }

        public override string ToString()
        {
            return $"Elevator:{Id}    Floor: {CurrentFloorNr}    State:{State}   NrOfPeople:{this.GetNrOfPeopleInElevator()}";
        }

        private readonly System.Timers.Timer _timer;
        private readonly Floor[] _floors;

        public Elevator(Floor[] floors)
        {
            _floors = floors;
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public Elevator Start()
        {
            _timer.Enabled = true;
            return this;
        }

        public void Stop()
        {
            _timer.Enabled = false;
        }
       
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Stop();
            
            // handle current floor
            var log = new StringBuilder();
            HandleCurrentFloor(log);
            this.PrintState(log.ToString());

            var nextFloorToVisit = FloorsToVisit.FirstOrDefault();
            if (nextFloorToVisit == null)
            {
                State = ElevatorDirection.Stopped;
            }
            else
            {
                if (CurrentFloorNr < nextFloorToVisit.FloorNr)
                {
                    this.MoveUp();
                }
                else
                {
                    this.MoveDown();
                }
                
                Start();
            }
        }

        private void HandleCurrentFloor(StringBuilder log)
        {
            //take people from floor
            var personsToBePickerFromCurrentFloorByThisElevator = PersonsToBePicker.Where(p => p.StartingFloor == CurrentFloor);
            if (personsToBePickerFromCurrentFloorByThisElevator.Any())
            {
                log.Append($"- Getting {personsToBePickerFromCurrentFloorByThisElevator.Count()} persons from floor {CurrentFloorNr}");
                //insert them to elevator
                PersonsInElevator.AddRange(personsToBePickerFromCurrentFloorByThisElevator);
                //remove persons from the floor
                CurrentFloor.WaitingPeople.RemoveAll(p => personsToBePickerFromCurrentFloorByThisElevator.Contains(p));
                PersonsToBePicker.RemoveAll(p => personsToBePickerFromCurrentFloorByThisElevator.Contains(p));
            }

            //drop people at current floor
            var personsToBeDropedAtCurrentFloorByThisElevator = PersonsInElevator.Where(p => p.TargetFloor == CurrentFloor);
            if (personsToBeDropedAtCurrentFloorByThisElevator.Any())
            {
                log.Append($"- Dropping {personsToBeDropedAtCurrentFloorByThisElevator.Count()} persons at floor {CurrentFloorNr}");
                //remove persons from elevator
                PersonsInElevator.RemoveAll(p => personsToBeDropedAtCurrentFloorByThisElevator.Contains(p));
            }


            // floor has been visited
            if (FloorsToVisit.Any() && CurrentFloor == FloorsToVisit.ElementAt(0))
            {
                FloorsToVisit.RemoveAt(0);
            }

        }

        private string ListToString<T>(IEnumerable<T> list)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in list)
            {
                stringBuilder.Append(item);
                stringBuilder.Append(",");
            }

            return stringBuilder.ToString();
        }

    }


    public static class ElevatorExtensions
    {
        public static bool IsMoving(this Elevator elevator)
        {
            return elevator.State != ElevatorDirection.Stopped;
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

        public static int[] GetFloorNrsToVisit(this Elevator unit)
        {
            return unit.FloorsToVisit.Select(f => f.FloorNr).ToArray();
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
            unit.State = ElevatorDirection.MovingUp;
        }

        public static void MoveDown(this Elevator unit)
        {
            unit.CurrentFloorNr--;
            unit.State = ElevatorDirection.MovingDown;
        }

        public static void Stop(this Elevator unit)
        {
            unit.State = ElevatorDirection.Stopped;
        }

        public static void MoveToFloor(this Elevator unit, int targetFloor)
        {
            if(targetFloor > unit.CurrentFloorNr)
            {
                unit.State = ElevatorDirection.MovingUp;
            }
            else
            {
                unit.State = ElevatorDirection.MovingDown;
            }
            //unit.TargetFloor = targetFloor;
            unit.PrintState($"Moving to target floor {targetFloor}");
            Thread.Sleep(1000);
            unit.CurrentFloorNr = targetFloor;
            unit.State = ElevatorDirection.Stopped;
            //unit.TargetFloor = -1;
            unit.PrintState($"Arrived at target floor {targetFloor}");
        }
        
        public static void PrintState(this Elevator unit, string message)
        {
            Console.ForegroundColor = unit.ConsoleColor;
            Console.WriteLine($"{unit} \t-> {message}");
            Console.ResetColor();
        }

        public static void Print(this Elevator unit, string message)
        {
            Console.ForegroundColor = unit.ConsoleColor;
            Console.WriteLine($"{unit} \t-> {message}");
            Console.ResetColor();
        }

    }
}
