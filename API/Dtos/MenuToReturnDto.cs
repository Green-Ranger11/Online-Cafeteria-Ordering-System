using Core.Entities;

namespace API.Dtos
{
    public class MenuToReturnDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Restaurant Restaurant { get; set; }

        public int RestaurantId { get; set; }
    }
}