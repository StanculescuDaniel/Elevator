using ElevatorSimulator.Models;
using System;
using System.Timers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ElevatorSimulator.Tasks
{
    internal class ElevatorTask
    {
        private readonly Elevator _elevatorUnit;
        private readonly System.Timers.Timer _timer = new System.Timers.Timer(1000);

        private readonly List<Floor> _floorsToStop = new List<Floor>();

        public ElevatorTask(Elevator elevatorUnit)
        {
            _elevatorUnit = elevatorUnit;
        }

        public void AddTask(ElevatorTaskRequest request)
        {
            
        }

        public void Run()
        {
            Task.Run(() => {
                //while (true)
                //{
                //    var nextFloor = _elevatorUnit.GetNextFloorToStop();
                //    if(nextFloor != null)
                //    {
                //        if (nextFloor.FloorNr > _elevatorUnit.CurrentFloor)
                //        {
                //            _elevatorUnit.MoveUp();
                //            _elevatorUnit.PersonsInElevator.RemoveAll(p => p.TargetFloor == nextFloor);
                //            var personsToBePicked = _elevatorUnit.PersonsToBePicker.Where(p => p.StartingFloor == nextFloor);
                //            _elevatorUnit.PersonsInElevator.AddRange(personsToBePicked);
                //            _elevatorUnit.PersonsToBePicker.RemoveAll(p => personsToBePicked.Contains(p));
                //        }
                //        else
                //        {
                //            _elevatorUnit.MoveDown();
                //        }
                //    }
                //    else
                //    {
                //        _elevatorUnit.Stop();
                //    }
                //}
            });
        }

        private void SetupTimer()
        {
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
        }

        private void HandleRequest(ElevatorTaskRequest request)
        {

        }
    }
}
