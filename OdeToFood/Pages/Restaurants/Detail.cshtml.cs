using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant{ get; set; }

        public DetailModel(IRestaurantData restaurantData) {
            this.restaurantData = restaurantData;
        }
        public IActionResult OnGet(int restId)
        {

            Restaurant = restaurantData.GetById(restId);
            if (Restaurant == null) {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}