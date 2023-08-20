﻿using System;


namespace ElevatorSimulator.Models
{
    public class Floor
    {
        public Floor()
        {
            
        }
        public Floor(int nr)
        {
            FloorNr = nr;
        }

        public int FloorNr { get; set; }
        public List<Person> WaitingPeople { get; set; } = new List<Person>();
    }
}