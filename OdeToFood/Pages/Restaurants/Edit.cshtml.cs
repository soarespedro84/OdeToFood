using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, 
            IHtmlHelper htmlHelper) {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restId) 
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (restId.HasValue) {
                Restaurant = restaurantData.GetById(restId.Value);
            }
            else {
                Restaurant = new Restaurant();
            }
            if (Restaurant==null) {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }
            if (Restaurant.Id >0) {
                restaurantData.Update(Restaurant);
                TempData["TempMessage"] = "Restaurant updated!";
            }
            else {
                restaurantData.Add(Restaurant);
                TempData["TempMessage"] = "Restaurant added!";
            }
            restaurantData.Commit();
            return RedirectToPage("./Detail", new { restId = Restaurant.Id });


        }
    }
}