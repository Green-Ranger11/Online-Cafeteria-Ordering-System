using System.Collections.Generic;

namespace API.Dtos
{
    public class MealToReturnDto
    {
        public int Id {get; set;}
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public string MealType { get; set; }

        public string Menu { get; set; }

        public string Restaurant {get; set;}
        public int Stock { get; set; }

        public IEnumerable<PhotoToReturnDto> Photos { get; set; }

        public IEnumerable<IngrediantToReturnDto> Ingrediants { get; set; }

    }
}