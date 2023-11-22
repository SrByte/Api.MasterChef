namespace MasterChef.Web.Models
{
	public class RecipeModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public CategoryModel Category { get; set; }
		public string Description { get; set; }
	}
}
