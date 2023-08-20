using ElevatorSimulator.Builders;
using ElevatorSimulator.Models;

namespace ElevatorSimulator.Tests
{
    public class ElevatorsManagerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, TestCaseSource(nameof(GetTestCaseSource))]
        public void TestAssignBestElevatorToPerson(Floor[] floors, Elevator[] elevators, Person person, Elevator expectedAssignedElevator)
        {
            //Arrange
            var elevatorHandlers = ElevatorHandlerBuilder.Build(elevators);
            var elevatorsManager = new ElevatorsManager(elevatorHandlers, floors);

            //Act
            elevatorsManager.AssignBestElevatorToPerson(person);

            //Assert
            Assert.That(person.AssignedElevator, Is.EqualTo(expectedAssignedElevator));
        }

        #region TestCaseData
        private static IEnumerable<TestCaseData> GetTestCaseSource()
        {
            var floors = new Floor[]
            {
                new Floor()
                {
                    FloorNr = 0
                },
                new Floor()
                {
                    FloorNr = 1
                },
                new Floor()
                {
                    FloorNr = 2
                },
                new Floor()
                {
                    FloorNr = 3
                },
                new Floor()
                {
                    FloorNr = 4
                },
                new Floor()
                {
                    FloorNr = 5
                },
            };

            yield return PickStoppedElevatorAtCurrentFloor(floors);
            yield return PickClosestStoppedElevator(floors);
            yield return PickClosestMovingElevatorInTargetDirection(floors);
            yield return PickElevatorWithTheClosestFinalTargetFloor(floors);

        }
        private static TestCaseData PickStoppedElevatorAtCurrentFloor(Floor[] floors)
        {
            var elevators = new Elevator[]
            {
                new Elevator(floors)
                {
                    Id = 0,
                    CurrentFloorNr = 1,
                    State = ElevatorDirection.Stopped,
                    ConsoleColor = ConsoleColor.Green
                },
                new Elevator(floors)
                {
                    Id = 1,
                    CurrentFloorNr = 2,
                    State = ElevatorDirection.Stopped,
                    ConsoleColor = ConsoleColor.Blue
                },
                new Elevator(floors)
                {
                    Id = 2,
                    CurrentFloorNr = 2,
                    State = ElevatorDirection.Stopped,
                    ConsoleColor = ConsoleColor.Red
                }
            };

            var person = new Person
            {
                StartingFloor = floors[1],
                TargetFloor = floors[5]
            };

            var person2 = new Person
            {
                StartingFloor = floors[2],
                TargetFloor = floors[4]
            };

            var person3 = new Person
            {
                StartingFloor = floors[3],
                TargetFloor = floors[5]
            };

            floors[1].WaitingPeople.Add(person);
            floors[2].WaitingPeople.Add(person2);
            floors[3].WaitingPeople.Add(person3);

            return new TestCaseData(floors, elevators, person, elevators[0])
            {
                TestName = "When all elevators are stopped and one of them is at the same floor with the person then pick that one"
            };
        }
        private static TestCaseData PickClosestStoppedElevator(Floor[] floors)
        {
            var elevators = new Elevator[]
            {
                new Elevator(floors)
                {
                    Id = 0,
                    CurrentFloorNr = 4,
                    State = ElevatorDirection.Stopped,
                    ConsoleColor = ConsoleColor.Green
                },
                new Elevator(floors)
                {
                    Id = 1,
                    CurrentFloorNr = 3,
                    State = ElevatorDirection.Stopped,
                    ConsoleColor = ConsoleColor.Blue
                },
                new Elevator(floors)
                {
                    Id = 2,
                    CurrentFloorNr = 2,
                    State = ElevatorDirection.Stopped,
                    ConsoleColor = ConsoleColor.Red
                }
            };

            var person = new Person
            {
                StartingFloor = floors[1],
                TargetFloor = floors[5]
            };

            var person2 = new Person
            {
                StartingFloor = floors[2],
                TargetFloor = floors[4]
            };

            var person3 = new Person
            {
                StartingFloor = floors[3],
                TargetFloor = floors[5]
            };

            floors[1].WaitingPeople.Add(person);
            floors[2].WaitingPeople.Add(person2);
            floors[3].WaitingPeople.Add(person3);

            return new TestCaseData(floors, elevators, person, elevators[2])
            {
                TestName = "When all elevators are stopped then pick the closest one"
            };
        }
        private static TestCaseData PickClosestMovingElevatorInTargetDirection(Floor[] floors)
        {
            var elevators = new Elevator[]
            {
                new Elevator(floors)
                {
                    Id = 0,
                    CurrentFloorNr = 0,
                    State = ElevatorDirection.MovingUp,
                    ConsoleColor = ConsoleColor.Green
                },
                new Elevator(floors)
                {
                    Id = 1,
                    CurrentFloorNr = 1,
                    State = ElevatorDirection.MovingUp,
                    ConsoleColor = ConsoleColor.Blue
                },
                new Elevator(floors)
                {
                    Id = 2,
                    CurrentFloorNr = 2,
                    State = ElevatorDirection.MovingDown,
                    ConsoleColor = ConsoleColor.Red
                }
            };

            var person = new Person
            {
                StartingFloor = floors[1],
                TargetFloor = floors[5]
            };

            var person2 = new Person
            {
                StartingFloor = floors[2],
                TargetFloor = floors[4]
            };

            var person3 = new Person
            {
                StartingFloor = floors[3],
                TargetFloor = floors[5]
            };

            floors[1].WaitingPeople.Add(person);
            floors[2].WaitingPeople.Add(person2);
            floors[3].WaitingPeople.Add(person3);

            return new TestCaseData(floors, elevators, person2, elevators[1])
            {
                TestName = "When all elevators are moving then choose the closest one which is moving in the right target direction"
            };
        }
        private static TestCaseData PickElevatorWithTheClosestFinalTargetFloor(Floor[] floors)
        {
            var elevators = new Elevator[]
            {
                new Elevator(floors)
                {
                    Id = 0,
                    CurrentFloorNr = 1,
                    State = ElevatorDirection.MovingUp,
                    ConsoleColor = ConsoleColor.Green,
                    FloorsToVisit = new List<Floor>
                    {
                        floors[2], floors[5]
                    }

                },
                new Elevator(floors)
                {
                    Id = 1,
                    CurrentFloorNr = 2,
                    State = ElevatorDirection.MovingUp,
                    ConsoleColor = ConsoleColor.Blue,
                    FloorsToVisit = new List<Floor>
                    {
                        floors[3], floors[5]
                    }
                },
                new Elevator(floors)
                {
                    Id = 2,
                    CurrentFloorNr = 3,
                    State = ElevatorDirection.MovingUp,
                    ConsoleColor = ConsoleColor.Red,
                    FloorsToVisit = new List<Floor>
                    {
                        floors[4]
                    }
                }
            };

            var person = new Person
            {
                StartingFloor = floors[0],
                TargetFloor = floors[4]
            };

            var person2 = new Person
            {
                StartingFloor = floors[2],
                TargetFloor = floors[5]
            };

            var person3 = new Person
            {
                StartingFloor = floors[3],
                TargetFloor = floors[5]
            };

            floors[1].WaitingPeople.Add(person);
            floors[2].WaitingPeople.Add(person2);
            floors[3].WaitingPeople.Add(person3);

            return new TestCaseData(floors, elevators, person, elevators[2])
            {
                TestName = "When all elevators are away from starting floor then pick the one which has the closest final target floor"
            };
        }
        #endregion

    }
}