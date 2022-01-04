using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data {
    public class InMemoryRestaurantData : IRestaurantData {

        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData() {
            restaurants = new List<Restaurant>() {
                new Restaurant {Id=1, Name="Domino's", Location="Leça da Palmeira", Cuisine=CuisineType.Italian},
                new Restaurant {Id=2, Name="Lado B", Location="Porto", Cuisine=CuisineType.Indian},
                new Restaurant {Id = 3, Name = "TexMex", Location = "Matosinhos", Cuisine=CuisineType.Mexican }
            };
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null) {
            return from r in restaurants 
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name 
                   select r;
        }
        public Restaurant GetById(int id) {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
        public Restaurant Add(Restaurant newRestaurant) {
            restaurants.Add(newRestaurant);
            //newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }
        public Restaurant Update(Restaurant restaurantToUpdate) {
            var updatedRestaurant = restaurants.SingleOrDefault(r => r.Id == restaurantToUpdate.Id);
            if (updatedRestaurant != null) {
                updatedRestaurant.Name = restaurantToUpdate.Name;
                updatedRestaurant.Location = restaurantToUpdate.Location;
                updatedRestaurant.Cuisine = restaurantToUpdate.Cuisine;
            }
            return updatedRestaurant;
        }

        public int Commit() {
            return 0;
        }

        public Restaurant Delete(int id) {
            var rest = restaurants.FirstOrDefault(r => r.Id == id);
            if (rest != null) restaurants.Remove(rest);
            return rest;
        }

        public int GetRestaurantCount() {
            return restaurants.Count();
        }
    }
}
