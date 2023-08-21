using ElevatorSimulator.Logic.Handlers;
using ElevatorSimulator.Logic.Interface;
using ElevatorSimulator.Logic.Models;
using ElevatorSimulator.Logic.Tests.Mocks;
using ElevatorSimulator.Logic.Tests.TestCaseSources;

namespace ElevatorSimulator.Logic.Tests
{
    public class ElevatorHandlerTests
    {
        private IOutputProvider _output;

        [SetUp]
        public void Setup()
        {
            _output = new MockOutputProvider();
        }

        [Test, TestCaseSource(typeof(ElevatorHandlerTestCaseSource), nameof(ElevatorHandlerTestCaseSource.GetAddPersonToPickTestCaseSource))]
        public void TestAddPersonToPick(Floor[] floors, Elevator elevator, Person person, List<Floor> expetedFloorsToVisit)
        {
            //Arrage
            var handler = new ElevatorHandler(elevator, floors, _output);

            //Act
            handler.AddPersonToPick(person);

            //Assert
            Assert.That(elevator.FloorsToVisit.SequenceEqual(expetedFloorsToVisit), Is.True);
        }

        [Test, TestCaseSource(typeof(ElevatorHandlerTestCaseSource), nameof(ElevatorHandlerTestCaseSource.GetStartHandleingPersonsInElevator_TestCaseSource))]
        public void TestStartHandleingPersonsInElevator(Floor[] floors, Elevator elevator, List<Person> expectedPersonsInElevator)
        {
            //Arrages
            var handler = new ElevatorHandler(elevator, floors, _output);

            //Act
            handler.StartHandleing();

            //Assert
            Assert.That(elevator.PersonsInElevator.SequenceEqual(expectedPersonsInElevator), Is.True);
        }

        //[Test, TestCaseSource(typeof(ElevatorHandlerTestCaseSource), nameof(ElevatorHandlerTestCaseSource.GetStartHandleingTestCaseSource))]
        public void TestStartHandleingNextState(Floor[] floors, Elevator elevator, ElevatorState expectedElevatorState)
        {
            //Arrages
            var handler = new ElevatorHandler(elevator, floors, _output);

            //Act
            handler.StartHandleing();

            //Assert
            Assert.That(handler.Elevator.State, Is.EqualTo(expectedElevatorState));
        }

    }
}