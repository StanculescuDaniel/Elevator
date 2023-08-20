using ElevatorSimulator.Models;

namespace ElevatorSimulator.Handlers
{
    public class ElevatorHandler
    {
        private readonly object _lock = new object();
        public Elevator Elevator { get; private set; }

        public ElevatorHandler(Elevator elevator)
        {
            Elevator = elevator ?? throw new ArgumentNullException("elevator cannot be null");
        }
        

        public void AddPersonToPick(Person person)
        {
            Elevator.PersonsToBePicker.Add(person);

            // elevator has no floors to visit (it's stopped)
            if (!Elevator.FloorsToVisit.Any())
            {
                Elevator.FloorsToVisit.Add(person.StartingFloor);
                Elevator.FloorsToVisit.Add(person.TargetFloor);
                Elevator.Start();

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

        public bool IsMoving()
        {
            return Elevator.State != ElevatorDirection.Stopped;
        }

        #region Private
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
