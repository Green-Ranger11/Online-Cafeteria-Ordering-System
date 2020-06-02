namespace Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}