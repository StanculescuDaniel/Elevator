using ElevatorSimulator.Models;
using System.Text;
using System.Timers;

namespace ElevatorSimulator.Handlers
{
    public class ElevatorHandler
    {
        private readonly object _lock = new object();
        private readonly Floor[] _floors;
        private readonly System.Timers.Timer _timer;
        public Elevator Elevator { get; private set; }
        public Floor CurrentFloor
        {
            get
            {
                return _floors.First(p => p.FloorNr == Elevator.CurrentFloorNr);
            }
        }

        public ElevatorHandler(Elevator elevator, Floor[] floors)
        {
            Elevator = elevator ?? throw new ArgumentNullException("elevator cannot be null");
            _floors = floors ?? throw new ArgumentNullException("floors cannot be null");

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }
        

        public void AddPersonToPick(Person person)
        {
            Elevator.PersonsToBePicker.Add(person);

            // elevator has no floors to visit (it's stopped)
            if (!Elevator.FloorsToVisit.Any())
            {
                Elevator.FloorsToVisit.Add(person.StartingFloor);
                Elevator.FloorsToVisit.Add(person.TargetFloor);
                Start();

                return;
            }

            var startFloorIndex = Elevator.FloorsToVisit.IndexOf(person.StartingFloor);
            // starting floor does not exist
            if (startFloorIndex == -1)
            {
                var insertIndexForStartFloor = FindInsertIndexForFloor(0, person.StartingFloor.FloorNr);
                startFloorIndex = insertIndexForStartFloor;
                Elevator.FloorsToVisit.Insert(insertIndexForStartFloor, person.StartingFloor);
            }

            if (!Elevator.FloorsToVisit.Contains(person.TargetFloor))
            {
                // target floor must always be after starting floor
                var insertIndexForTargetFloor = FindInsertIndexForFloor(startFloorIndex, person.TargetFloor.FloorNr);
                Elevator.FloorsToVisit.Insert(insertIndexForTargetFloor, person.TargetFloor);
            }

        }

        #region Private
        public void Start()
        {
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Enabled = false;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Stop();
            HandleCurrentFloor();
            DecideNextState();
        }

        private void DecideNextState()
        {
            var nextFloorToVisit = Elevator.FloorsToVisit.FirstOrDefault();
            if (nextFloorToVisit == null)
            {
                Elevator.State = ElevatorState.Stopped;
            }
            else
            {
                if (Elevator.CurrentFloorNr < nextFloorToVisit.FloorNr)
                {
                    Elevator.MoveUp();
                }
                else
                {
                    Elevator.MoveDown();
                }

                Start();
            }
        }

        private void HandleCurrentFloor()
        {
            var log = new StringBuilder();
            //take people from floor
            var personsToBePickerFromCurrentFloorByThisElevator = Elevator.PersonsToBePicker.Where(p => p.StartingFloor == CurrentFloor);
            if (personsToBePickerFromCurrentFloorByThisElevator.Any())
            {
                log.Append($"- Getting {personsToBePickerFromCurrentFloorByThisElevator.Count()} persons from floor {Elevator.CurrentFloorNr}");
                //insert them to elevator
                Elevator.PersonsInElevator.AddRange(personsToBePickerFromCurrentFloorByThisElevator);
                //remove persons from the floor
                CurrentFloor.WaitingPeople.RemoveAll(p => personsToBePickerFromCurrentFloorByThisElevator.Contains(p));
                Elevator.PersonsToBePicker.RemoveAll(p => personsToBePickerFromCurrentFloorByThisElevator.Contains(p));
            }

            //drop people at current floor
            var personsToBeDropedAtCurrentFloorByThisElevator = Elevator.PersonsInElevator.Where(p => p.TargetFloor == CurrentFloor);
            if (personsToBeDropedAtCurrentFloorByThisElevator.Any())
            {
                log.Append($"- Dropping {personsToBeDropedAtCurrentFloorByThisElevator.Count()} persons at floor {Elevator.CurrentFloorNr}");
                //remove persons from elevator
                Elevator.PersonsInElevator.RemoveAll(p => personsToBeDropedAtCurrentFloorByThisElevator.Contains(p));
            }


            // floor has been visited
            if (Elevator.FloorsToVisit.Any() && CurrentFloor == Elevator.FloorsToVisit.ElementAt(0))
            {
                Elevator.FloorsToVisit.RemoveAt(0);
            }

            this.PrintState(log.ToString());

        }
        private void PrintState(string message)
        {
            Console.ForegroundColor = Elevator.ConsoleColor;
            Console.WriteLine($"{Elevator} \t-> {message}");
            Console.ResetColor();
        }

        private int FindInsertIndexForFloor(int fromIndex,int floorNr)
        {
            var insertIndex = Elevator.FloorsToVisit.Count;
            for (var i = fromIndex; i < Elevator.FloorsToVisit.Count - 1; i++)
            {
                var element = Elevator.FloorsToVisit.ElementAt(i);
                var nextElement = Elevator.FloorsToVisit.ElementAt(i + 1);

                // elevator will be moving UP at this point in time
                if (element.FloorNr < nextElement.FloorNr)
                {
                    if (floorNr > element.FloorNr && floorNr < nextElement.FloorNr)
                    {
                        insertIndex = i + 1;
                        break;
                    }
                }
                // elevator will be moving DOWN at this point in time
                else if (element.FloorNr > nextElement.FloorNr)
                {
                    if (floorNr < element.FloorNr && floorNr > nextElement.FloorNr)
                    {
                        insertIndex = i + 1;
                        break;
                    }
                }
            }

            return insertIndex;
        }
        #endregion
    }
}
