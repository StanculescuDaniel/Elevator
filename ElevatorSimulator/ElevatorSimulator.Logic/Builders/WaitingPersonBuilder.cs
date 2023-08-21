using ElevatorSimulator.Logic.Models;

namespace ElevatorSimulator.Logic.Builders
{
    public class WaitingPersonBuilder
    {
        private readonly Floor[] _floors;
        public WaitingPersonBuilder(Floor[] floors) 
        {
            _floors = floors;
        }

        public Person Buid(int startingFloor, int targetFloor)
        {
            var person = new Person()
            {
                StartingFloor = _floors[startingFloor],
                TargetFloor = _floors[targetFloor]
            };
            _floors[startingFloor].WaitingPeople.Add(person);

            return person;
        }
    }
}
