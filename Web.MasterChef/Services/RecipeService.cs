using MasterChef.Web.Models;
using MasterChef.Web.Services.IServices;
using MasterChef.Web.Utils;
using System.Net.Http.Headers;

namespace MasterChef.Web.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/recipes";

        public RecipeService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<RecipetModel>> FindAllRecipes()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<RecipetModel>>();
        }

        public async Task<RecipetModel> FindRecipeById(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<RecipetModel>();
        }

        public async Task<RecipetModel> CreateRecipe(RecipetModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<RecipetModel>();
            else throw new Exception("Something went wrong when calling API");
        }
        public async Task<RecipetModel> UpdateRecipe(RecipetModel model)
        {
            var response = await _client.PutAsJson(BasePath, model);
			//var response = await _client.GetAsync($"{BasePath}/{model.Id}");
			return await response.ReadContentAs<RecipetModel>();

			//if (response.IsSuccessStatusCode)
			//    return await response.ReadContentAs<RecipetModel>();
			//else throw new Exception("Something went wrong when calling API");
		}

        public async Task<bool> DeleteRecipeById(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else throw new Exception("Something went wrong when calling API");
        }
    }
}