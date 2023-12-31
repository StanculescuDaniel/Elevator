﻿using ElevatorSimulator.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Logic.Tests.TestCaseSources
{
    public static class ElevatorsManagerTestCaseSource
    {
        public static IEnumerable<TestCaseData> GetAssignBestElevatorToPersonTestCaseSource()
        {
            var floors = new Floor[]
            {
                new Floor(0),
                new Floor(1),
                new Floor(2),
                new Floor(3),
                new Floor(4),
                new Floor(5)
                
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
                new Elevator()
                {
                    Id = 0,
                    CurrentFloorNr = 1,
                    State = ElevatorState.Stopped,
                    ConsoleColor = ConsoleColor.Green
                },
                new Elevator()
                {
                    Id = 1,
                    CurrentFloorNr = 2,
                    State = ElevatorState.Stopped,
                    ConsoleColor = ConsoleColor.Blue
                },
                new Elevator()
                {
                    Id = 2,
                    CurrentFloorNr = 2,
                    State = ElevatorState.Stopped,
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
                new Elevator()
                {
                    Id = 0,
                    CurrentFloorNr = 4,
                    State = ElevatorState.Stopped,
                    ConsoleColor = ConsoleColor.Green
                },
                new Elevator()
                {
                    Id = 1,
                    CurrentFloorNr = 3,
                    State = ElevatorState.Stopped,
                    ConsoleColor = ConsoleColor.Blue
                },
                new Elevator()
                {
                    Id = 2,
                    CurrentFloorNr = 2,
                    State = ElevatorState.Stopped,
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
                new Elevator()
                {
                    Id = 0,
                    CurrentFloorNr = 0,
                    State = ElevatorState.MovingUp,
                    ConsoleColor = ConsoleColor.Green
                },
                new Elevator()
                {
                    Id = 1,
                    CurrentFloorNr = 1,
                    State = ElevatorState.MovingUp,
                    ConsoleColor = ConsoleColor.Blue
                },
                new Elevator()
                {
                    Id = 2,
                    CurrentFloorNr = 2,
                    State = ElevatorState.MovingDown,
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
                new Elevator()
                {
                    Id = 0,
                    CurrentFloorNr = 1,
                    State = ElevatorState.MovingUp,
                    ConsoleColor = ConsoleColor.Green,
                    FloorsToVisit = new List<Floor>
                    {
                        floors[2], floors[5]
                    }

                },
                new Elevator()
                {
                    Id = 1,
                    CurrentFloorNr = 2,
                    State = ElevatorState.MovingUp,
                    ConsoleColor = ConsoleColor.Blue,
                    FloorsToVisit = new List<Floor>
                    {
                        floors[3], floors[5]
                    }
                },
                new Elevator()
                {
                    Id = 2,
                    CurrentFloorNr = 3,
                    State = ElevatorState.MovingUp,
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
    }
}
