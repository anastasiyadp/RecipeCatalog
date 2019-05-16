using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using RecipeCatalog.Models;
using SQLiteNetExtensions.Extensions;
using Android.Content;
using Newtonsoft.Json;

namespace RecipeCatalog
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DataBase dataBase;
        ArrayAdapter<string> adapter;
        List<string> categories = new List<string>();


        ListView listView;
        List<Recipe> listRecipes;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            dataBase = new DataBase("demo.db3");
            
            listView = FindViewById<ListView>(Resource.Id.listCategories);

            var tableCategory = dataBase.db.Table<Category>();
            foreach (var category in tableCategory)
            {
                categories.Add(category.name);
            }
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, categories);
            listView.Adapter = adapter;
            listView.ItemClick += (sender, e) =>
            {
                var intent = new Intent(this, typeof(RecipesListActivity));
                string categoryName = categories[e.Position];
                intent.PutExtra("categoryName", categoryName);
                StartActivity(intent);
            };
    
        }

        //public void SaveNote()
        //{
        //    recipe.name = nameText.Text;
        //    recipe.instruction = instructionText.Text;
        //    dataBase.SaveNote(recipe);
        //    ((BaseAdapter)listView.Adapter).NotifyDataSetChanged();
        //}

        //public void CreateNote()
        //{
        //    nameText.Text = "";
        //    instructionText.Text = "";
        //    nameText.Visibility = Android.Views.ViewStates.Visible;
        //    instructionText.Visibility = Android.Views.ViewStates.Visible;
        //}

        //public void AddNote()
        //{
        //    Recipe newRecipe = new Recipe();
        //    newRecipe.name = nameText.Text;
        //    newRecipe.instruction = instructionText.Text;
        //    listRecipes.Add(newRecipe);
        //    dataBase.AddNote(newRecipe);
        //    ((BaseAdapter)listView.Adapter).NotifyDataSetChanged();
        //    nameText.Visibility = Android.Views.ViewStates.Invisible;
        //    instructionText.Visibility = Android.Views.ViewStates.Invisible;
        //}

        //public void DeleteNote()
        //{
        //    listRecipes.Remove(recipe);
        //    dataBase.DeleteNote(recipe);
        //    ((BaseAdapter)listView.Adapter).NotifyDataSetChanged();
        //}


        //public void CreateListRecipes()
        //{
        //    var table = dataBase.db.GetAllWithChildren<Recipe>();
        //    foreach (var s in table)
        //    {
        //        listRecipes.Add(s);
        //        Console.WriteLine(s.Id + " " + s.name + " " + s.instruction + " " + s.Category.name);
        //    }

        //}
    }
}