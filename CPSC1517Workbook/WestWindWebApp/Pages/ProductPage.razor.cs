using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindWebApp.Pages
{
	public partial class ProductPage
	{
		[Inject]
		ProductServices ProductServices { get; set; }

		[Inject]
		CategoryServices CategoryServices { get; set; }

		[Inject]
		SupplierServices SupplierServices { get; set; }

		[Inject]
		NavigationManager NavigationManager { get; set; }

		[Parameter]
		public int ProductId { get; set; }//to show in the navigation bar for the ProductId to be displayed

		private List<Supplier>? Suppliers { get; set; } = new();//this is so we can display the suppliers and categories in the drop down list 
		private List<Category>? Categories { get; set; } = new();//this is so we can display the suppliers and categories in the drop down list 

		private Product? Product { get; set; }//creating a single product instead of using the attributes for each of these products 

		private Dictionary<string, string> Errors { get; set; } = new();
		private string? FeedbackMessage { get; set; }

		/// <summary>
		/// Validate the form controls before processing
		/// </summary>
		/// <returns>True if no errors were found, false otherwise</returns>
		private bool ValidateForm()
		{
			Errors.Clear();

			if (string.IsNullOrWhiteSpace(Product!.ProductName))
			{
				Errors.Add("product-name", "Product name cannot be empty.");
			}

			if (string.IsNullOrWhiteSpace(Product.QuantityPerUnit))
			{
				Errors.Add("quantity", "Quantity per unit cannot be empty.");
			}

			if (Product.UnitPrice <= 0)
			{
				Errors.Add("unit-price", "Unit price must be greater than zero.");
			}

			if (Product.UnitsOnOrder < 0)
			{
				Errors.Add("units-order", "Units on order cannot be negative.");
			}

			if (Product.CategoryId == 0)
			{
				Errors.Add("category", "Must choose a category.");
			}

			if (Product.SupplierId == 0)
			{
				Errors.Add("supplier", "Must choose a supplier.");
			}

			return Errors.Count == 0;
		}

		protected override void OnInitialized()//When the page is first loaded
		{
			Categories = CategoryServices.GetAllCategories();//to display in the drop down... This is from the Category Services
			Suppliers = SupplierServices.GetAllSuppliers();//To display in the drop down... This is from the Supplier Services
			Errors = new Dictionary<string, string>();

			if(ProductId != 0)//This will check if there is a Product selected... If so it will display the product with information
			{
				//View/Edit
				Product = ProductServices.GetProductById(ProductId);//this is a method from the Product Services 

				if(Product == null)//if there is no product Id selected then it will go to the add page this is specific with the ID in the URl
				{
					Errors.Add("init-product", $"No product was found for id {ProductId}.");
					Product = new Product();
				}
			}
            else
            {
				//Add a new product
				Product = new Product();
            }

            base.OnInitialized();
		}

		/// <summary>
		/// Process form submission and update or create a new product
		/// </summary>
		private void HandleSaveProduct()
		{
			if (ValidateForm())//call the validate method here and if its true then we can do the rest
			{
				if(ProductId == 0) //the product is 0 which means we want to create a new product 
				{
					try
					{
						ProductServices.AddProduct(Product!);//get the add product method
						FeedbackMessage = "Product successfully Added.";//this is the feedback msg where the msg will come back letting them know no errors were found
						NavigationManager.NavigateTo($"/product/{Product!.ProductId}");//this will navigate the url with the right product ID and update it
					}
					catch(Exception ex) //If there is an exception (something went wrong display the error msg
					{
						Errors.Add("product-add", ex.Message);
					}
				}
			}
			else// if the product id is not a 0 which means there is a Id so we update the product.
			{
				try
				{
					ProductServices.UpdateProduct(Product!);// here we are telling them to update the product using the product services in which the method is 
					FeedbackMessage = "Successfully Updated Product";// the message once the product is updated in the DB
				}
				catch (Exception ex)//If there is an exception (something went wrong give the error
				{
					Errors.Add("product-add", ex.Message);
				}
			}
		}

		/// <summary>
		/// Handle form submission and discontinue a product
		/// </summary>
		private void HandleDiscontinue()//method for the discontinued product section
		{
			if(Product!.ProductId != 0)//if the product is not not equal to 0 then we discontinue the product
			{
				try
				{
					ProductServices.DiscontinueProduct(Product!);
					FeedbackMessage = "Successfully Discontinued Product";
				}
				catch (Exception ex)
				{
					Errors.Add("product-discontinued", ex.Message);
				}
			}
		}
	}
}
