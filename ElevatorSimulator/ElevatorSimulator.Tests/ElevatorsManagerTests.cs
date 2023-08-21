using ElevatorSimulator.Logic.Builders;
using ElevatorSimulator.Logic.Interface;
using ElevatorSimulator.Logic.Models;
using ElevatorSimulator.Logic.Tests.Mocks;
using ElevatorSimulator.Logic.Tests.TestCaseSources;

namespace ElevatorSimulator.Logic.Tests
{
    public class ElevatorsManagerTests
    {
        private IOutputProvider _output;
        [SetUp]
        public void Setup()
        {
            _output = new MockOutputProvider();
        }

        [Test, TestCaseSource(typeof(ElevatorsManagerTestCaseSource), nameof(ElevatorsManagerTestCaseSource.GetAssignBestElevatorToPersonTestCaseSource))]
        public void TestAssignBestElevatorToPerson(Floor[] floors, Elevator[] elevators, Person person, Elevator expectedAssignedElevator)
        {
            //Arrange
            var elevatorHandlers = new ElevatorHandlerBuilder(_output, floors).Build(elevators);
            var elevatorsManager = new ElevatorsManager(elevatorHandlers);

            //Act
            elevatorsManager.AssignBestElevatorToPerson(person);

            //Assert
            Assert.That(person.AssignedElevator, Is.EqualTo(expectedAssignedElevator));
        }
    }
}