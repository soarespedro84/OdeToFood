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
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurantData restaurantData ) {
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

        public IActionResult OnPost(int restId) {
            var rest = restaurantData.Delete(restId);
            restaurantData.Commit();

            if (rest == null) {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{rest.Name} deleted";
            return RedirectToPage("./List");
        }
    }
}