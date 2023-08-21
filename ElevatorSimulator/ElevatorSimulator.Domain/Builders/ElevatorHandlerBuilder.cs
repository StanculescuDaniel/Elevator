using ElevatorSimulator.Domain.Handlers;
using ElevatorSimulator.Domain.Interface;
using ElevatorSimulator.Domain.Models;

namespace ElevatorSimulator.Domain.Builders
{
    public class ElevatorHandlerBuilder
    {
        private readonly IOutputProvider _output;
        private readonly Floor[] _floors;

        public ElevatorHandlerBuilder(IOutputProvider output, Floor[] floors)
        {
            _output = output;
            _floors = floors;
        }

        public ElevatorHandler[] Build(int[] stoppedFloors)
        {
            var handlers = new List<ElevatorHandler>();
            for (int i = 0; i < stoppedFloors.Length; i++)
            {
                var elevator = new Elevator
                {
                    Id = i,
                    CurrentFloorNr = stoppedFloors[i]
                };
                handlers.Add(new ElevatorHandler(elevator, _floors, _output));
            }

            return handlers.ToArray();
        }

        public ElevatorHandler[] Build(Elevator[] elevators)
        {
            var handlers = new List<ElevatorHandler>();

            foreach (var elevator in elevators)
            {
                handlers.Add(new ElevatorHandler(elevator, _floors, _output));
            }

            return handlers.ToArray();
        }

    }
}
