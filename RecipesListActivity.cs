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
    [Activity(Label = "RecipesListActivity")]
    public class RecipesListActivity : Activity
    {
        List<Recipe> recipesList;
        Recipe selectedRecipe;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_recipesList);

            TextView textCategory = FindViewById<TextView>(Resource.Id.textCategory);
            ListView listView = FindViewById<ListView>(Resource.Id.listRecipes);

            //textCategory.Visibility = ViewStates.Invisible;
            String txtName = Intent.GetStringExtra("categoryName"); ;

            using (DataBase.db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), DataBase.dbPath)))
            {
                Category selectedCategory = DataBase.db.Table<Category>().First(category => category.name == txtName);
                var recipes = DataBase.db.GetAllWithChildren<Recipe>().Where(recipe => recipe.id_category == selectedCategory.Id);
                recipesList = new List<Recipe>(recipes);
                DataBase.db.Close();
            };

            
            if (recipesList.Count == 0)
            {
                //textCategory.Visibility = ViewStates.Visible;
                textCategory.Text = "Рецептов в этой категории ещё нет";
                return;
            }
            textCategory.Text = txtName;

            listView.Adapter = new AdapterRecipe(this, recipesList);
            listView.ItemClick += (sender, arg) =>
            {
                Intent intent = new Intent(this, typeof(RecipeActivity));
                selectedRecipe = recipesList[arg.Position];
                intent.PutExtra("recipeId", selectedRecipe.Id);
                StartActivity(intent);
            };

        }
    }
}