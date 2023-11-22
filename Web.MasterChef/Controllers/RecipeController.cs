using MasterChef.Web.Models;
using MasterChef.Web.Services.IServices;
using MasterChef.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MasterChef.Web.Controllers
{
	public class RecipeController : Controller
	{
		private readonly IRecipeService _recipeService;
		private readonly ICategoryService _categoryService;
		private readonly IIngredientService _ingredientService;
		private readonly IMemoryCache _cache;



		public RecipeController(IRecipeService recipeService, ICategoryService categoryService, IIngredientService ingredientService)
		{
			_recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
			_ingredientService = ingredientService ?? throw new ArgumentNullException(nameof(ingredientService));
		}
		public async Task<IActionResult> RecipeIndex()
		{

			var recipes = await _recipeService.FindAllRecipes();
			return View(recipes);
		}
		private async Task<ActionResult> popularCategoriaAsync()
		{

			ViewBag.Lista = await _categoryService.FindAllCategories();

			return View();
		}
		public async Task<IActionResult> RecipeCreate()
		{
			ViewBag.CategoryId = new SelectList(	await _categoryService.FindAllCategories(),
				"Id",
				"Name"
				);
			return View();
		}

		//[Authorize]
		[HttpPost]
		public async Task<IActionResult> RecipeCreate(RecipeModel model, string CategoryId)
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
		public async Task<IActionResult> RecipeUpdate(RecipeModel model)
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
		public async Task<IActionResult> RecipeDelete(RecipeModel model)
		{
			var response = await _recipeService.DeleteRecipeById(model.Id);
			if (response) return RedirectToAction(
					nameof(RecipeIndex));
			return View(model);
		}
	}
}