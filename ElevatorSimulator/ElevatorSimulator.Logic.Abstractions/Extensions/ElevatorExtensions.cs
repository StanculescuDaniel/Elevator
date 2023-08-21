using ElevatorSimulator.Logic.Models;

namespace ElevatorSimulator.Logic.Abstractions.Extensions
{
    public static class ElevatorExtensions
    {
        private static ConsoleColor[] Colors = new ConsoleColor[]
        {
            ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Yellow
        };

        public static void AutoAssignColor(this Elevator elevator)
        {
            if (elevator.Id < Colors.Length)
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

        public static int GetFreeSpots(this Elevator elevator)
        {
            return elevator.MaxCapacity - elevator.GetNrOfPeopleInElevator();
        }

        public static bool IsFull(this Elevator elevator)
        {
            return elevator.GetFreeSpots() == 0;
        }

        public static Floor? GetNextFloorToVisit(this Elevator elevator)
        {
            return elevator.FloorsToVisit.FirstOrDefault();
        }

        public static string GetFloorsToVisitString(this Elevator elevator)
        {
            var floorsToVisit = elevator.FloorsToVisit.Select(p => p.FloorNr).ToArray();
            return string.Join(',', floorsToVisit);
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
