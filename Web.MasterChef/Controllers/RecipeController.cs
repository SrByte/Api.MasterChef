using MasterChef.Web.Models;
using MasterChef.Web.Services.IServices;
using MasterChef.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Web.Controllers
{
	public class RecipeController : Controller
	{
		private readonly IRecipeService _recipeService;

		public RecipeController(IRecipeService recipeService)
		{
			_recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
		}
		public async Task<IActionResult> RecipeIndex()
		{
			var recipes = await _recipeService.FindAllRecipes();
			return View(recipes);
		}

		public async Task<IActionResult> RecipeCreate()
		{
			return View();
		}

        //[Authorize]
        [HttpPost]
		public async Task<IActionResult> RecipeCreate(RecipetModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _recipeService.CreateRecipe(model);
				if (response != null) return RedirectToAction(
					 nameof(RecipeIndex));
			}
			return View(model);
		}
        public async Task<IActionResult> RecipeUpdate(int id)
		{
			var model = await _recipeService.FindRecipeById(id);
			if (model != null) return View(model);
			return NotFound();
		}

        //[Authorize]
        [HttpPost]
		public async Task<IActionResult> RecipeUpdate(RecipetModel model)
		{
			if (ModelState.IsValid)
			{
				var response = await _recipeService.UpdateRecipe(model);
				if (response != null) return RedirectToAction(
					 nameof(RecipeIndex));
			}
			return View(model);
		}

        //[Authorize]
        public async Task<IActionResult> RecipeDelete(int id)
		{
			var model = await _recipeService.FindRecipeById(id);
			if (model != null) return View(model);
			return NotFound();
		}

        [HttpPost]
		//[Authorize(Roles = Role.Admin)]
		public async Task<IActionResult> RecipeDelete(RecipetModel model)
		{
			var response = await _recipeService.DeleteRecipeById(model.Id);
			if (response) return RedirectToAction(
					nameof(RecipeIndex));
			return View(model);
		}
	}
}