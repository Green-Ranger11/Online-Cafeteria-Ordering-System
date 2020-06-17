using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class Meal : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public MealType MealType { get; set; }

        public int MealTypeId { get; set; }

        public Menu Menu { get; set; }

        public int MenuId { get; set; }

        public Restaurant Restaurant { get; set; }

        public int RestaurantId { get; set; }

        private readonly List<Ingrediant> _ingrediants = new List<Ingrediant>();
        public IReadOnlyList<Ingrediant> Ingrediants => _ingrediants.AsReadOnly();
        private readonly List<Photo> _photos = new List<Photo>();
        public IReadOnlyList<Photo> Photos => _photos.AsReadOnly();

        public void AddIngrediant(string name, decimal price, int quantity)
        {
            var ingrediant = new Ingrediant
            {
                Name = name,
                Price = price,
                Quantity = quantity
            };

            _ingrediants.Add(ingrediant);
        }
        public void UpdateIngrediant(string name, decimal price, int quantity, int id)
        {
            _ingrediants.FirstOrDefault(x => x.Id == id).Name = name;
            _ingrediants.FirstOrDefault(x => x.Id == id).Price = price;
            _ingrediants.FirstOrDefault(x => x.Id == id).Quantity = quantity;
        }

        public void RemoveIngrediant(int id)
        {
            var ingrediant = _ingrediants.Find(x => x.Id == id);
            _ingrediants.Remove(ingrediant);
        }

        public void AddPhoto(string pictureUrl, string fileName, bool isMain = false)
        {
            var photo = new Photo
            {
                FileName = fileName,
                PictureUrl = pictureUrl
            };

            if (_photos.Count == 0) photo.IsMain = true;

            _photos.Add(photo);
        }

        public void RemovePhoto(int id)
        {
            var photo = _photos.Find(x => x.Id == id);
            _photos.Remove(photo);
        }

        public void SetMainPhoto(int id)
        {
            var currentMain = _photos.SingleOrDefault(item => item.IsMain);
            foreach (var item in _photos.Where(item => item.IsMain))
            {
                item.IsMain = false;
            }

            var photo = _photos.Find(x => x.Id == id);
            if (photo != null)
            {
                photo.IsMain = true;
                if (currentMain != null) currentMain.IsMain = false;
            }
        }
    }
}