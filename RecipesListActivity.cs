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
using RecipeCatalog.Models;
using SQLiteNetExtensions.Extensions;

namespace RecipeCatalog
{
    [Activity(Label = "RecipesListActivity")]
    public class RecipesListActivity : Activity
    {
        DataBase dataBase;
        ListView listView;
        List<Recipe> listRecipes;
        Recipe recipe;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_recipesList);

            TextView textNoRecipes = FindViewById<TextView>(Resource.Id.textNoRecipes);
            listView = FindViewById<ListView>(Resource.Id.listRecipes);

            textNoRecipes.Visibility = ViewStates.Invisible;

            dataBase = new DataBase("demo.db3");

            String txtName = Intent.GetStringExtra("categoryName");
            listRecipes = new List<Recipe>();
            var table = dataBase.db.GetAllWithChildren<Recipe>();

            Category category = dataBase.db.Table<Category>().FirstOrDefault(y => y.name == txtName);
            var recipes = dataBase.db.GetAllWithChildren<Recipe>().Where(y => y.id_category == category.Id);

            List<Recipe> recipesList = new List<Recipe>(recipes);
            if (recipesList.Count == 0)
            {
                textNoRecipes.Visibility = ViewStates.Visible;
                return;
            }

            
            listView.Adapter = new AdapterRecipe(this, recipesList);
            listView.ItemClick += (sender, arg) =>
            {
                var intent = new Intent(this, typeof(RecipeActivity));
                recipe = listRecipes[arg.Position];
                intent.PutExtra("recipeId", recipe.Id);
                StartActivity(intent);
            };

        }
    }
}