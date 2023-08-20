using ElevatorSimulator.Models;

namespace ElevatorSimulator.Builders
{
    public class WaitingPersonBuilder
    {
        private readonly Floor[] _floors;
        public WaitingPersonBuilder(Floor[] floors) 
        {
            _floors = floors;
        }

        public  List<Person> Buid(string consoleInput)
        {
            var splittedPersonsStr = consoleInput.Split(' ');
            var list = new List<Person>();

            foreach (var personStr in splittedPersonsStr)
            {
                var splittedPersonStr = personStr.Split(',');
                var startingFloor = int.Parse(splittedPersonStr[0]);
                var targetFloor = int.Parse(splittedPersonStr[1]);

                var person = new Person()
                {
                    StartingFloor = _floors[startingFloor],
                    TargetFloor = _floors[targetFloor]
                };
                _floors[startingFloor].WaitingPeople.Add(person);
                list.Add(person);
            }

            return list;
        }
    }
}
