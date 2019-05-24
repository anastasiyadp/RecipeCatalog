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
    class EditRecipeActivity: Activity
    {
        List<string> products = new List<string>();
        List<ProductForList> products2 = new List<ProductForList>();
        ListView listEditedProducts;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_editRecipe);
            MultiSelectSpinner multiSelectSpinner = FindViewById<MultiSelectSpinner>(Resource.Id.multiSelectSpinner1);
            listEditedProducts = FindViewById<ListView>(Resource.Id.listEditedProducts);

            using (DataBase.db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DataBase.dbPath)))
            {
                var table = DataBase.db.Table<Product>();

                foreach (Product product in table)
                {
                    products.Add(product.name);
                }
                DataBase.db.Close();
            }



            multiSelectSpinner.SetItems(products);
            FindViewById<Button>(Resource.Id.button1).Click += MainActivity_Click;
        }

        Product currentProduct;

        
        public void MainActivity_Click(object sender, System.EventArgs e)
        {
            MultiSelectSpinner multiSelectSpinner = FindViewById<MultiSelectSpinner>(Resource.Id.multiSelectSpinner1);
            List<string> selected = multiSelectSpinner.GetSelectedStrings();
            
            foreach (var s in selected)
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! " + s);


            using (DataBase.db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DataBase.dbPath)))
            {
                selected = multiSelectSpinner.GetSelectedStrings();
                products2 = new List<ProductForList>();
                foreach (var s in selected) { 
                    currentProduct = DataBase.db.GetAllWithChildren<Product>().First(product => product.name == s);
                    products2.Add(new ProductForList { name = currentProduct.name, measure = currentProduct.unitMeasure });
                }

                DataBase.db.Close();
            };

            listEditedProducts.Adapter = new AdapterProduct(this, products2);
        }
        
    }
}