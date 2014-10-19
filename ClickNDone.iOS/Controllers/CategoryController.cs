// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ClickNDone.Core;

namespace ClickNDone.iOS
{
	public partial class CategoryController : MyViewController
	{
		readonly CategoriesModel categoriesModel = (CategoriesModel)DependencyInjectionWrapper.Instance.ServiceContainer ().GetService (typeof(CategoriesModel));

		public CategoryController(IntPtr handle) : base (handle)
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.LoadLeftbarButton ();
			btnHome.TouchDown += btnTouchDownHandler;
			btnBeauty.TouchDown += btnTouchDownHandler;
		}

		void btnTouchDownHandler (Object sender, EventArgs args)
		{
			UIButton pressedButton = (UIButton)sender;
			Category selectedCat = new Category();
			if (pressedButton.Tag == 0) 
			{
				selectedCat = categoriesModel.GetCategoryById ("BEA");
				PerformSegue("OnBEA", this);

			}
			else if(pressedButton.Tag == 1)
			{
				selectedCat = categoriesModel.GetCategoryById ("HOME");
				PerformSegue("OnHOME", this);

			}
			categoriesModel.SelectedCategory = selectedCat;

		}

	}
}
