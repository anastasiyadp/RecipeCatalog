using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLiteNetExtensions.Extensions;
using RecipeCatalog.Models;
using SQLite;
using System.IO;

namespace RecipeCatalog
{
    [Activity(Label = "RecipeActivity")]
    public class RecipeActivity : Activity
    {
        List<ProductForList> products = new List<ProductForList>();
        ListView listView;
        ArrayAdapter<string> adapter;
        Recipe currentRecipe;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_recipe);
          
            TextView textCategoryAndName = FindViewById<TextView>(Resource.Id.textCategoryAndName);
            TextView textInstruction = FindViewById<TextView>(Resource.Id.textInstruction);
            listView = FindViewById<ListView>(Resource.Id.listProducts);

            int recipeId = Intent.GetIntExtra("recipeId", 0);

            using (DataBase.db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DataBase.dbPath)))
            {
                currentRecipe = DataBase.db.GetAllWithChildren<Recipe>().First(recipe => recipe.Id == recipeId);

                 foreach (Product product in currentRecipe.products)
                {
                    Ingredients ingredient = DataBase.db.GetAllWithChildren<Ingredients>().ToList().First(y => (y.id_recipe == recipeId) && (y.id_product == product.Id));
                    products.Add(new ProductForList { name = product.name, quantity = ingredient.quantity, measure = product.unitMeasure });
                }

                DataBase.db.Close();
            };
            
            textCategoryAndName.Text = currentRecipe.name;
            textInstruction.Text = currentRecipe.instruction;

            listView.Adapter = new AdapterProduct(this, products);
           
        }
    }
}