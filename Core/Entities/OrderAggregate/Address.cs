namespace Core.Entities.OrderAggregate
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string firstName, string lastName, string building, string room)
        {
            FirstName = firstName;
            LastName = lastName;
            Building = building;
            Room = room;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
    }
}