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

namespace RecipeCatalog
{
    [Activity(Label = "RecipeActivity")]
    public class RecipeActivity : Activity
    {
        DataBase dataBase;
        List<string> products = new List<string>();
        ListView listView;
        ArrayAdapter<string> adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_recipe);
            dataBase = new DataBase("demo.db3");

            TextView textCategoryAndName = FindViewById<TextView>(Resource.Id.textCategoryAndName);
            TextView textInstruction = FindViewById<TextView>(Resource.Id.textInstruction);
            listView = FindViewById<ListView>(Resource.Id.listProducts);

            int recipeId = Intent.GetIntExtra("recipeId", 0);

            Recipe recipe = dataBase.db.GetAllWithChildren<Recipe>().FirstOrDefault(y => y.Id == recipeId);

            textCategoryAndName.Text = recipe.name;
            textInstruction.Text = recipe.instruction;

            foreach (var product in recipe.products)
            {
                products.Add(product.name);
            }

            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, products);
            listView.Adapter = adapter;
        }
    }
}