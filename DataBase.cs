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
using SQLite;
using SQLiteNetExtensions;
using RecipeCatalog.Models;
using SQLiteNetExtensions.Extensions;

namespace RecipeCatalog
{
    class DataBase
    {
        private SQLiteConnection _db;
        public SQLiteConnection db {
            get => _db;
            set => _db = value;
        }

       // string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "demo.db3");

        public DataBase(string dbName)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            
            db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbPath));
                    
            db.CreateTable<Recipe>();
            db.CreateTable<Category>();
            db.CreateTable<Product>();
            db.CreateTable<Ingredients>();
            CreateNotesInTables();
           
        }

        public void CreateNotesInTables()
        {
           
            var newRecipe1 = new Recipe
            {
                name = "Ромашка",
                instruction = "Порубить кубиками мясо, вареную морковь и лук. Заправить майонезом."
            };
            var newRecipe2 = new Recipe
            {
                name = "Борщ",
                instruction = "Подготовить бульон. Порубить кубиками катошку. Добавить свежую зелень."
            };
            var newRecipe3 = new Recipe
            {
                name = "Бульон",
                instruction = "Сварить курицу. Вытащить её"
            };
            var newRecipe4 = new Recipe
            {
                name = "Оливье",
                instruction = "Сварить картошку и яйца. Порезать их кубиками. Также порезать колбасу, лук, сол. огурцы. " +
                "Высыпать зеленый горошек. Заправить майонезом"
            };
            

            var newCategory1 = new Category { name = "Суп" };
            var newCategory2 = new Category { name = "Салат" };
            var newCategory3 = new Category { name = "Второе"};
            var newCategory4 = new Category { name = "Выпечка"};


            var newProduct1 = new Product
            {
                name = "Морковь",
                unitMeasure = "шт."
            };
            var newProduct2 = new Product
            {
                name = "Картошка",
                unitMeasure = "шт."
            };
            var newProduct3 = new Product
            {
                name = "Мясо",
                unitMeasure = "г."
            };
            var newProduct4 = new Product
            {
                name = "Колбаса",
                unitMeasure = "г."
            };
            var newProduct5 = new Product
            {
                name = "Лук",
                unitMeasure = "шт."
            };
            var newProduct6 = new Product
            {
                name = "Соленый огурец",
                unitMeasure = "г."
            };
            var newProduct7 = new Product
            {
                name = "Яйцо",
                unitMeasure = "шт."
            };
            var newProduct8 = new Product
            {
                name = "Зелёный горошек",
                unitMeasure = "банка"
            };

            db.Insert(newRecipe1);
            db.Insert(newRecipe2);
            db.Insert(newRecipe3);
            db.Insert(newRecipe4);
            db.Insert(newCategory1);
            db.Insert(newCategory2);
            db.Insert(newCategory3);
            db.Insert(newCategory4);
            db.Insert(newProduct1);
            db.Insert(newProduct2);
            db.Insert(newProduct3);
            db.Insert(newProduct4);
            db.Insert(newProduct5);
            db.Insert(newProduct6);
            db.Insert(newProduct7);
            db.Insert(newProduct8);

            newRecipe1.products = new List<Product> { newProduct1, newProduct3, newProduct5};
            newRecipe2.products = new List<Product> { newProduct2, newProduct3, newProduct5 };
            newRecipe3.products = new List<Product> { newProduct2, newProduct3, };
            newRecipe4.products = new List<Product> { newProduct2, newProduct4, newProduct5, newProduct6, newProduct8 };
            db.UpdateWithChildren(newRecipe1);
            db.UpdateWithChildren(newRecipe2);
            db.UpdateWithChildren(newRecipe3);
            db.UpdateWithChildren(newRecipe4);

            newCategory1.recipes = new List<Recipe> { newRecipe2, newRecipe3 };
            newCategory2.recipes = new List<Recipe> { newRecipe1, newRecipe4 };
            db.UpdateWithChildren(newCategory1);
            db.UpdateWithChildren(newCategory2);

            //var storedValuation = db.GetWithChildren<Category>(1);
            //var x = db.GetWithChildren<Recipe>(1);
           // var t = db.Table<Category>().FirstOrDefault(y => y.Id == 1);
        }

        public void GetNote()
        {
            
        }

        public void AddNote(Recipe recipe)
        {
            db.Insert(recipe);
        }

        public void SaveNote(Recipe recipe)
        {
            db.Update(recipe);
        }

        public void DeleteNote(Recipe recipe)
        {
            db.Delete(recipe);
        }
    }
}