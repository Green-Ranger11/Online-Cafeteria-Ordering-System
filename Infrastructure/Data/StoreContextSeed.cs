using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Restaurants.Any())
                {
                    var restaurantsData = File.ReadAllText("../Infrastructure/Data/SeedData/restaurants.json");

                    var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(restaurantsData);

                    foreach (var item in restaurants)
                    {
                        context.Restaurants.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Menus.Any())
                {
                    var menusData = File.ReadAllText("../Infrastructure/Data/SeedData/menus.json");

                    var menus = JsonSerializer.Deserialize<List<Menu>>(menusData);

                    foreach (var item in menus)
                    {
                        context.Menus.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.MealTypes.Any())
                {
                    var mealTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/menuTypes.json");

                    var mealTypes = JsonSerializer.Deserialize<List<MealType>>(mealTypesData);

                    foreach (var item in mealTypes)
                    {
                        context.MealTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Meals.Any())
                {
                    var mealsData = File.ReadAllText("../Infrastructure/Data/SeedData/meals.json");

                    var meals = JsonSerializer.Deserialize<List<Meal>>(mealsData);

                    foreach (var item in meals)
                    {
                        context.Meals.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}