﻿using ElevatorSimulator.Logic.Models;

namespace ElevatorSimulator.Logic.Builders
{
    public class FloorsBuilder
    {
        public static Floor[] Build(int nrOfFloors)
        {
            var floors = new Floor[nrOfFloors];
            for (int i = 0; i < nrOfFloors; i++)
            {
                floors[i] = new Floor(i);
            }

            return floors;
        }
    }
}
