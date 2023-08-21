using ElevatorSimulator.Logic.Handlers;
using ElevatorSimulator.Logic.Models;

namespace ElevatorSimulator.Logic
{
    public class ElevatorsManager
    {
        private readonly ElevatorHandler[] _elevatorHandlers;
        private readonly Floor[] _floors;
        private readonly int _nrOfFloors;

        public ElevatorsManager(
            ElevatorHandler[] elevatorHandlers,
            Floor[] floors)
        {
            _elevatorHandlers = elevatorHandlers;
            _floors = floors;
            _nrOfFloors = floors.Count();

        }

        public void AssignBestElevatorToPerson(Person person)
        {
            if(person.TargetFloor == null || person.StartingFloor ==null)
            {
                throw new ArgumentException("TargetFloor or StartingFloor are null");
            }

            var bestElevatorHandler = GetBestElevatorHandler(person);
            bestElevatorHandler.AddPersonToPick(person);
            bestElevatorHandler.StartHandleing();
            
        }

        private ElevatorHandler GetBestElevatorHandler(Person person)
        {
            var startingFloorNr = person.StartingFloor.FloorNr;
            var targetFloorNr = person.TargetFloor.FloorNr; 

            var stoppedElevators = _elevatorHandlers.Where(u => !u.Elevator.IsMoving());
            // stopped elevators case
            if (stoppedElevators.Any())
            {
                return GetClosestElevatorUnit(stoppedElevators, startingFloorNr);
            }

            var movingElevators = _elevatorHandlers.Where(u => u.Elevator.IsMoving());

            // elevators which are moving in the correct direction - choose the closest one
            var eligibleMovingElevators = movingElevators
                .Where(e =>
                           (person.WantsToGoUp() && e.Elevator.CurrentFloorNr < startingFloorNr && e.Elevator.State == ElevatorState.MovingUp) ||
                           (person.WantsToGoDown() && e.Elevator.CurrentFloorNr > startingFloorNr && e.Elevator.State == ElevatorState.MovingDown));

            
            if (eligibleMovingElevators.Any())
            {
                return GetClosestElevatorUnit(eligibleMovingElevators, startingFloorNr);
            }

            // all elevators move away from floor - choose the one which has the final target floor the closest
            return GetElevatorHandlerWithClosestTargerFloor(movingElevators, startingFloorNr);

        }

        private ElevatorHandler GetClosestElevatorUnit(IEnumerable<ElevatorHandler> elevatorUnits, int requestedFloorNr)
        {
            var minDistance = int.MaxValue;
            ElevatorHandler? bestElevatorHandler = elevatorUnits.FirstOrDefault();
            foreach (var unit in elevatorUnits)
            {
                var currentDistance = Math.Abs(requestedFloorNr - unit.Elevator.CurrentFloorNr);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    bestElevatorHandler = unit;
                }
            }

            return bestElevatorHandler;
        }

        private ElevatorHandler GetElevatorHandlerWithClosestTargerFloor(IEnumerable<ElevatorHandler> elevatorHandlers, int requestedFloorNr)
        {
            var minDistance = int.MaxValue;
            ElevatorHandler? bestElevatorHandler = elevatorHandlers.FirstOrDefault();
            foreach (var handler in elevatorHandlers)
            {
                var currentDistance = Math.Abs(requestedFloorNr - handler.Elevator.GetLastFloorToVisit().FloorNr);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    bestElevatorHandler = handler;
                }
            }

            return bestElevatorHandler;
        }
    }
}
