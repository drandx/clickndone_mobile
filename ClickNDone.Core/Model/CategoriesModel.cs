using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


namespace ClickNDone.Core
{
	public class CategoriesModel : BaseViewModel
	{
		public List<Category> Categories{ get; set; }
		public Category SelectedCategory;
		public Category SelectedSubcategory;
		public static bool Loaded { get; set;}

		public async Task GetCategories()
		{
			IsBusy = true;
			try
			{
				Categories = await service.GetCategoriesAsync(settings.User.sessionToken, settings.DeviceToken);
				SortCategoriesAsc();
			}
			finally {
				IsBusy = false;
				Loaded = true;
			}

		}

		public Category GetCategoryByConvention(string categoryConvention)
		{
			Category result = Categories.Where (c => c.Convention == categoryConvention).First();
			return result;
		}

		public Category GetCategoryById(int catId)
		{
			Category result = Categories.Where (c => c.Id == catId).First();
			return result;
		}


		public Category GetSubCategoryById(int subCatId)
		{
			if (SelectedCategory != null) {
				var result = SelectedCategory.Subcategories.Where (c => c.Id == subCatId);
				Category catResult = new Category();
				if (result.Count()>0)
					catResult = result.First ();
				return catResult;
			}
			return new Category ();
		}

		private void SortCategoriesAsc()
		{
			foreach(var catItem in Categories)
			{
				var subcategories = catItem.Subcategories;
				var sortedSubcategories = from sc in subcategories
				                          orderby sc.Name
				                          select sc;

				catItem.Subcategories = sortedSubcategories.ToList();
			}

		}

		public class CaseInsensitiveComparer : IComparer<string> 
		{ 
			public int Compare(string x, string y) 
			{ 
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase); 
			} 
		}
	}
}

