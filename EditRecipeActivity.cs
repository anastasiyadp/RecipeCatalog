using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RecipeCatalog.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace RecipeCatalog
{
    [Activity(Label = "EditRecipeActivity")]
    public class EditRecipeActivity: Activity
    {
        List<string> productsNames = new List<string>();
        List<string> categoryNames = new List<string>();
        List<ProductForList> products = new List<ProductForList>();
        Recipe newRecipe = new Recipe();

        ListView listEditedProducts;
        Product currentProduct;
        EditText editTextNameRecipe, editTextInstructionRecipe;
        Button addProductButton,saveButton; 
        AutoCompleteTextView autoComplete;
        Spinner spinner;
        string currentCategoryName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_editRecipe);

            editTextNameRecipe = FindViewById<EditText>(Resource.Id.editTextNameRecipe);
            editTextInstructionRecipe = FindViewById<EditText>(Resource.Id.editTextInstructionRecipe);
            listEditedProducts = FindViewById<ListView>(Resource.Id.listEditedProducts);

            saveButton = FindViewById<Button>(Resource.Id.saveData);
            addProductButton = FindViewById<Button>(Resource.Id.buttonAddProduct);
            autoComplete = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
            spinner = FindViewById<Spinner>(Resource.Id.spinner1);

            using (DataBase.db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DataBase.dbPath)))
            {
                var table = DataBase.db.Table<Product>();

                foreach (Product product in table)
                {
                    productsNames.Add(product.name);
                }

                var categories = DataBase.db.Table<Category>();
                foreach (Category category in categories)
                {
                    categoryNames.Add(category.name);
                }

                DataBase.db.Close();
            }
                       
            spinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, categoryNames);
            autoComplete.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, productsNames);
            listEditedProducts.Adapter = new AdapterProduct(this, products);

            addProductButton.Click += AddProductButtonClick;
            saveButton.Click += SaveButtonClick;
            listEditedProducts.ItemClick += ListEditedProductsClick;
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            currentCategoryName = categoryNames[e.Position];
            string toast = string.Format("Selected car is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void ListEditedProductsClick(object sender, AdapterView.ItemClickEventArgs arg)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View view = layoutInflater.Inflate(Resource.Layout.user_input_dialog_box, null);
            AlertDialog.Builder alertbuilder = new AlertDialog.Builder(this);
            alertbuilder.SetView(view);
            EditText userdata = view.FindViewById<EditText>(Resource.Id.editTextQuantity);
           
            alertbuilder.SetCancelable(false)
            .SetPositiveButton("Ok", delegate
            {
                ProductForList product = products[arg.Position];
                if (userdata.Text != "")
                {
                    product.quantity = Int32.Parse(userdata.Text);
                    ((BaseAdapter)listEditedProducts.Adapter).NotifyDataSetChanged();
                }
            });
            AlertDialog dialog = alertbuilder.Create();
            dialog.Show();
        }

        private void AddProductButtonClick(object sender, EventArgs arg)
        { 
            if (autoComplete.Text != "")
            {
                //using (DataBase.db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DataBase.dbPath)))
                //{
                    //currentProduct = DataBase.db.GetAllWithChildren<Product>().First(product => product.name == autoComplete.Text);
                    currentProduct = DataBase.GetProduct(autoComplete.Text);
                    products.Add(new ProductForList { name = currentProduct.name, quantity = 1, measure = currentProduct.unitMeasure });

                    ((BaseAdapter)listEditedProducts.Adapter).NotifyDataSetChanged();
                    DataBase.db.Close();
                //};
                
                Toast.MakeText(this, "Name Entered =" + autoComplete.Text, ToastLength.Short).Show();
                autoComplete.Text = "";
            }
            else
            {
                Toast.MakeText(this, "Please Enter Name!", ToastLength.Short).Show();
            }
        }
        private void SaveButtonClick(object sender, EventArgs arg)
        {
            using (DataBase.db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DataBase.dbPath)))
            {
                
                Product newProduct = new Product();
                List<Product> listproducts = new List<Product>();
                List<int> mera = new List<int>();

                newRecipe.name = editTextNameRecipe.Text;
                newRecipe.instruction = editTextInstructionRecipe.Text;
                newRecipe.products = new List<Product>();
                foreach (var s in products)
                {
                    //Product prod = DataBase.db.Table<Product>().ToList().First(y => y.name == s.name);
                    Product prod = DataBase.GetProduct(s.name);
                    newRecipe.products.Add(prod);
                    mera.Add(s.quantity);
                }

                DataBase.AddNewRecipe(newRecipe, currentCategoryName, mera);
            }
        }
    }
}