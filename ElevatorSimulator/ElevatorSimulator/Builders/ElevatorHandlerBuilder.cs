using ElevatorSimulator.Handlers;
using ElevatorSimulator.Models;

namespace ElevatorSimulator.Builders
{
    public class ElevatorHandlerBuilder
    {
        public static ElevatorHandler[] Build(Elevator[] elevators)
        {
            var handlers = new List<ElevatorHandler>();

            foreach (var elevator in elevators)
            {
                handlers.Add(new ElevatorHandler(elevator));
            }

            return handlers.ToArray();
        }

    }
}
