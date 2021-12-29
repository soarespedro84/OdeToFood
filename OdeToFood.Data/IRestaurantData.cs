using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OdeToFood.Data {
    public interface IRestaurantData {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
    }
    public class InMemoryRestaurantData : IRestaurantData {

        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData() {
            restaurants = new List<Restaurant>() {
                new Restaurant {Id=1, Name="Domino's", Location="Leça da Palmeira", Cuisine=CuisineType.Italian},
                new Restaurant {Id=2, Name="Lado B", Location="Porto", Cuisine=CuisineType.Indian},
                new Restaurant {Id = 3, Name = "TexMex", Location = "Matosinhos", Cuisine=CuisineType.Mexican }
            };
        }

        public Restaurant GetById(int id) {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null) {
            return from r in restaurants 
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name 
                   select r;
        }
    }
}
