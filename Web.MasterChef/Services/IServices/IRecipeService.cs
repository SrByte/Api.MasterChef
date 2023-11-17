using MasterChef.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterChef.Web.Services.IServices
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipetModel>> FindAllRecipes();
        Task<RecipetModel> FindRecipeById(long id);
        Task<RecipetModel> CreateRecipe(RecipetModel model);
        Task<RecipetModel> UpdateRecipe(RecipetModel model);
        Task<bool> DeleteRecipeById(long id);
    }
} 