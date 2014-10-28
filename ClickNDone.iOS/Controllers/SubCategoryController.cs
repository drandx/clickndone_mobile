// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using ClickNDone.Core;
using MonoTouch.ObjCRuntime;

namespace ClickNDone.iOS
{
	public partial class SubCategoryController : UIViewController
	{
		readonly CategoriesModel categoriesModel = (CategoriesModel)DependencyInjectionWrapper.Instance.ServiceContainer ().GetService (typeof(CategoriesModel));
		private float scrollBtnCursor = 0f;
		private float buttonHeight = 40.0f;
		private float scrollerHeigt = 0.0f;
		public SubCategoryController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			foreach(Category item in categoriesModel.SelectedCategory.Subcategories)
			{
				item.ParentId = categoriesModel.SelectedCategory.Id; //TODO - Get this id from the service
				UIButton btn = this.CreateButton (item.Id,item.Name);
				this.ScrollerButtons.Add (btn);
			}

			SizeF scrollerSize = new SizeF ();
			float heightSize = this.scrollerHeigt * 1.5f;
			scrollerSize.Height = heightSize;
			scrollerSize.Width = 300;
			this.ScrollerButtons.ContentSize = scrollerSize;

		}

		void handler (Object sender, EventArgs args)
		{
			UIButton selectedSubcategory = (UIButton)sender;
			categoriesModel.SelectedSubcategory = categoriesModel.GetSubCategory (selectedSubcategory.Tag);
			if(categoriesModel.SelectedCategory.Convention == "BEA")
				PerformSegue("OnBautyRequest", this);
			else
				PerformSegue("OnHomeRequest", this);
		}

		/**
		 *
		 *
		 * 
		 * */
		private UIButton CreateButton(int catId,string title)
		{
			var x = UIScreen.MainScreen.Bounds.Width;
			UIButton button = UIButton.FromType(UIButtonType.RoundedRect);

			RectangleF rect = new RectangleF(x*0.2f, this.scrollBtnCursor, x*0.6f, this.buttonHeight);
			button.Frame = rect;
			button.SetTitle(title, UIControlState.Normal);
			button.Tag = catId;
			UIColor borderColor = UIColor.FromRGB (0,167,229);
			UIColor ButtonBackgroundColor = UIColor.White;

			this.scrollBtnCursor = rect.Location.Y + 45f;
			this.scrollerHeigt = this.scrollerHeigt + this.buttonHeight;

			try
			{
				var path = UIBezierPath.FromRect(rect);
				ButtonBackgroundColor.SetFill();
				path.Fill();

				button.CurrentTitleColor.SetStroke();
				path.Stroke();

				button.Layer.BorderColor = borderColor.CGColor;
				button.Layer.BorderWidth = 2f;

				button.TouchDown += handler;

				return button;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error in SevenButton > Draw : {0}\n",ex.Message);
			}

			return null;

		}

	}
}
