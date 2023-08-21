namespace ElevatorSimulator.Logic.Models
{
    public class Floor
    {
        public Floor(int nr)
        {
            FloorNr = nr;
        }

        public int FloorNr { get; set; }
        public List<Person> WaitingPeople { get; set; } = new List<Person>();

        public override string ToString()
        {
            return $"FloorNr: {FloorNr}, WaitingPeople: {WaitingPeople.Count}";
        }
    }
}
