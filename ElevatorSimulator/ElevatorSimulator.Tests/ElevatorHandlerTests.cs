using ElevatorSimulator.Handlers;
using ElevatorSimulator.Models;

namespace ElevatorSimulator.Tests
{
    public class ElevatorHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, TestCaseSource(nameof(GetTestCaseSource))]
        public void TestAddPersonToPick(Floor[] floors, Elevator elevator, Person person, List<Floor> expetedFloorsToVisit)
        {
            //Arrage
            var handler = new ElevatorHandler(elevator, floors);

            //Act
            handler.AddPersonToPick(person);

            //Assert
            Assert.IsTrue(elevator.FloorsToVisit.SequenceEqual(expetedFloorsToVisit));
        }

        #region TestCaseData
        private static IEnumerable<TestCaseData> GetTestCaseSource()
        {
            var floors = new Floor[]
            {
                new Floor(0),
                new Floor(1),
                new Floor(2),
                new Floor(3),
                new Floor(4),
                new Floor(5),
                new Floor(6)
            };

            yield return ElevatorStoppedTestCase(floors);
            yield return ElevatorMovingUpTestCase(floors);
            yield return ElevatorMovingUpStartingFloorAlreadyExistsTestCase(floors);
            yield return ElevatorMovingUpTargetFloorAlreadyExistsTestCase(floors);

            yield return ElevatorMovingDownTestCase(floors);
            yield return ElevatorMovingDownTargetFloorAlreadyExistsTestCase(floors);
            yield return ElevatorMovingDownStartingFloorAlreadyExistsTestCase(floors);

            yield return ElevatorMovingUp_PersonWantsToGoDown_StartingAndTargetFloorBellowElevator_TestCase(floors);
            yield return ElevatorMovingUp_PersonWantsToGoUp_StartingAndTargetFloorBellowElevator_TestCase(floors);

        }

        private static TestCaseData ElevatorStoppedTestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 1,
                State = ElevatorState.Stopped,
            };

            var person = new Person
            {
                StartingFloor = floors[1],
                TargetFloor = floors[5],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>() 
            {
                floors[1],
                floors[5],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is stopped add starting and target floors in FloorsToVisit list"
            };
        }

        private static TestCaseData ElevatorMovingUpTestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 1,
                State = ElevatorState.MovingUp,
                FloorsToVisit = new List<Floor>()
                {
                    floors[2],
                    floors[5],
                }
            };

            var person = new Person
            {
                StartingFloor = floors[3],
                TargetFloor = floors[4],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>()
            {
                floors[2],
                floors[3],
                floors[4],
                floors[5],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is moving up add the starting and target floor at the correct index in FloorsToVisit list"
            };
        }

        private static TestCaseData ElevatorMovingUpStartingFloorAlreadyExistsTestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 0,
                State = ElevatorState.MovingUp,
                FloorsToVisit = new List<Floor>()
                {
                    floors[1],
                    floors[2],
                    floors[4]
                }
            };

            var person = new Person
            {
                StartingFloor = floors[3],
                TargetFloor = floors[4],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>()
            {
                floors[1],
                floors[2],
                floors[3],
                floors[4],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is moving up and starting floor already exists then add only the target floor at the correct index in FloorsToVisit list"
            };
        }

        private static TestCaseData ElevatorMovingUpTargetFloorAlreadyExistsTestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 0,
                State = ElevatorState.MovingDown,
                FloorsToVisit = new List<Floor>()
                {
                    floors[2],
                    floors[3],
                    floors[5],
                }
            };

            var person = new Person
            {
                StartingFloor = floors[3],
                TargetFloor = floors[4],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>()
            {
                floors[2],
                floors[3],
                floors[4],
                floors[5],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is moving up and target floor already exists then add only the starting floor at the correct index in FloorsToVisit list"
            };
        }

        private static TestCaseData ElevatorMovingDownTestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 5,
                State = ElevatorState.MovingDown,
                FloorsToVisit = new List<Floor>()
                {
                    floors[5],
                    floors[2],
                }
            };

            var person = new Person
            {
                StartingFloor = floors[4],
                TargetFloor = floors[3],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>()
            {
                floors[5],
                floors[4],
                floors[3],
                floors[2],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is moving down add the starting and target floor at the correct index in FloorsToVisit list"
            };
        }

        private static TestCaseData ElevatorMovingDownTargetFloorAlreadyExistsTestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 6,
                State = ElevatorState.MovingDown,
                FloorsToVisit = new List<Floor>()
                {
                    floors[5],
                    floors[3],
                    floors[2],
                }
            };

            var person = new Person
            {
                StartingFloor = floors[4],
                TargetFloor = floors[3],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>()
            {
                floors[5],
                floors[4],
                floors[3],
                floors[2],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is moving down and target floor already exists then add only the starting floor at the correct index in FloorsToVisit list"
            };
        }

        private static TestCaseData ElevatorMovingDownStartingFloorAlreadyExistsTestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 5,
                State = ElevatorState.MovingDown,
                FloorsToVisit = new List<Floor>()
                {
                    floors[4],
                    floors[2],
                    floors[1]
                }
            };

            var person = new Person
            {
                StartingFloor = floors[4],
                TargetFloor = floors[3],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>()
            {
                floors[4],
                floors[3],
                floors[2],
                floors[1],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is moving down and starting floor already exists then add only the target floor at the correct index in FloorsToVisit list"
            };
        }

        private static TestCaseData ElevatorMovingUp_PersonWantsToGoDown_StartingAndTargetFloorBellowElevator_TestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 2,
                State = ElevatorState.MovingUp,
                FloorsToVisit = new List<Floor>()
                {
                    floors[3],
                    floors[4]
                }
            };

            var person = new Person
            {
                StartingFloor = floors[1],
                TargetFloor = floors[0],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>()
            {
                floors[3],
                floors[4],
                floors[1],
                floors[0],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is moving up, person wants to go down but starting and target floor are below the elevator then after the elevator finish with the highest target floor iw will come back to pick person"
            };
        }

        private static TestCaseData ElevatorMovingUp_PersonWantsToGoUp_StartingAndTargetFloorBellowElevator_TestCase(Floor[] floors)
        {
            var elevator = new Elevator()
            {
                CurrentFloorNr = 2,
                State = ElevatorState.MovingUp,
                FloorsToVisit = new List<Floor>()
                {
                    floors[3],
                    floors[4]
                }
            };

            var person = new Person
            {
                StartingFloor = floors[0],
                TargetFloor = floors[1],
                AssignedElevator = elevator
            };
            var expected = new List<Floor>()
            {
                floors[3],
                floors[4],
                floors[0],
                floors[1],
            };

            return new TestCaseData(floors, elevator, person, expected)
            {
                TestName = "When elevator is moving up, person wants to go up but starting and target floor are below the elevator then after the elevator finish with the highest target floor iw will come back to pick person"
            };
        }


        #endregion 

    }
}