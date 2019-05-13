using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using SQLite;
using System;
using System.IO;
using System.Collections.Generic;

namespace RecipeCatalog
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        ListView listView;
        List<Stock> listRecipes;

        //SQLiteConnection db;
        DataBase dataBase;

        EditText nameText, instructionText;
        Button createButton, saveButton, deleteButton, addButton;
        int currentId;
        Stock recipe;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

             dataBase = new DataBase("demo.db3");
             dataBase.CreateTable();
            //string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "demo.db3");
            //db = new SQLiteConnection(dbPath);
            //CreateDB();

            addButton = FindViewById<Button>(Resource.Id.button5);
            createButton = FindViewById<Button>(Resource.Id.button4);
            saveButton = FindViewById<Button>(Resource.Id.button2);
            deleteButton = FindViewById<Button>(Resource.Id.button3);

            listView = FindViewById<ListView>(Resource.Id.listView1);
            nameText = FindViewById<EditText>(Resource.Id.editText1);
            instructionText = FindViewById<EditText>(Resource.Id.editText2);

            addButton.Visibility = Android.Views.ViewStates.Invisible;
            saveButton.Visibility = Android.Views.ViewStates.Invisible;
            deleteButton.Visibility = Android.Views.ViewStates.Invisible;
            nameText.Visibility = Android.Views.ViewStates.Invisible;
            instructionText.Visibility = Android.Views.ViewStates.Invisible;

            listRecipes = new List<Stock>();
            CreateListRecipes();

            listView.Adapter = new AdapterRecipe(this, listRecipes);
            listView.ItemClick += (sender, e) =>
            {
                recipe = listRecipes[e.Position];
                nameText.Text = recipe.name;
                instructionText.Text = recipe.instruction;
                currentId = recipe.Id;
                saveButton.Visibility = Android.Views.ViewStates.Visible;
                deleteButton.Visibility = Android.Views.ViewStates.Visible;
                nameText.Visibility = Android.Views.ViewStates.Visible;
                instructionText.Visibility = Android.Views.ViewStates.Visible;
                
            };

            saveButton.Click += (sender, e) => {
                SaveNote();
                saveButton.Visibility = Android.Views.ViewStates.Invisible;
                deleteButton.Visibility = Android.Views.ViewStates.Invisible;
                nameText.Visibility = Android.Views.ViewStates.Invisible;
                instructionText.Visibility = Android.Views.ViewStates.Invisible;
            };

            deleteButton.Click += (sender, e) =>
            {
                DeleteNote();
                saveButton.Visibility = Android.Views.ViewStates.Invisible;
                deleteButton.Visibility = Android.Views.ViewStates.Invisible;
                nameText.Visibility = Android.Views.ViewStates.Invisible;
                instructionText.Visibility = Android.Views.ViewStates.Invisible;
            };

            createButton.Click += (sender, e) => {
                CreateNote();
                addButton.Visibility = Android.Views.ViewStates.Visible;
            };

            addButton.Click += (sender, e) =>
            {
                AddNote();
                addButton.Visibility = Android.Views.ViewStates.Invisible;
            };
        }

        public void CreateDB()
        {
            //db.CreateTable<Stock>();
            //if (db.Table<Stock>().Count() == 0)
            //{
            //    var newStock = new Stock();
            //    newStock.name = "Салат";
            //    newStock.instruction = "Порезать кубиками, заправить майонезом";
            //    db.Insert(newStock);
            //    newStock = new Stock();
            //    newStock.name = "Суп";
            //    newStock.instruction = "Приготовить куриный бульон. Сварить картошку";
            //    db.Insert(newStock);
            //    newStock = new Stock();
            //    newStock.name = "Жаркое";
            //    newStock.instruction = "Порезать кусочками овощи, потушить";
            //    db.Insert(newStock);
            //}
            //Console.WriteLine("Reading data");
        }

        public void SaveNote()
        {
            recipe.name = nameText.Text;
            recipe.instruction = instructionText.Text;
            dataBase.SaveNote(recipe);
            ((BaseAdapter)listView.Adapter).NotifyDataSetChanged();
        }

        public void CreateNote()
        {
            nameText.Text = "";
            instructionText.Text = "";
            nameText.Visibility = Android.Views.ViewStates.Visible;
            instructionText.Visibility = Android.Views.ViewStates.Visible;
        }

        public void AddNote()
        {
            Stock newRecipe = new Stock();
            newRecipe.name = nameText.Text;
            newRecipe.instruction = instructionText.Text;
            listRecipes.Add(newRecipe);
            dataBase.AddNote(newRecipe);
            ((BaseAdapter)listView.Adapter).NotifyDataSetChanged();
            nameText.Visibility = Android.Views.ViewStates.Invisible;
            instructionText.Visibility = Android.Views.ViewStates.Invisible;
        }

        public void DeleteNote()
        {
            listRecipes.Remove(recipe);
            dataBase.DeleteNote(recipe);
            ((BaseAdapter)listView.Adapter).NotifyDataSetChanged();
        }


        public void CreateListRecipes()
        {
            var table = dataBase.db.Table<Stock>();
            foreach (var s in table)
            {
                listRecipes.Add(s);
                Console.WriteLine(s.Id + " " + s.name + " " + s.instruction);
            }
        }
    }

    [Table("Recipe")]
    class Stock
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string name { get; set; }
        public string instruction { get; set; }
    }
}