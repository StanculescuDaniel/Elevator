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

        [Test, TestCaseSource(typeof(AddPersonToPickTestCaseSource), nameof(AddPersonToPickTestCaseSource.GetTestCaseSource))]
        public void TestAddPersonToPick(Floor[] floors, Elevator elevator, Person person, List<Floor> expetedFloorsToVisit)
        {
            //Arrage
            var handler = new ElevatorHandler(elevator, floors, _output);

            //Act
            handler.AddPersonToPick(person);

            //Assert
            Assert.That(elevator.FloorsToVisit.SequenceEqual(expetedFloorsToVisit), Is.True);
        }

    }
}