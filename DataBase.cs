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
    static class DataBase
    {
        static public string dbPath;
        static public SQLiteConnection db; 

       // string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "demo.db3");

        static public void CreateDataBase(string dbName)
        {
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            using (db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbPath))) { 
                db.CreateTables<Recipe, Category, Product, Ingredients>();
                db.Close();
            }
            
            DataInTables.CreateNotesInTables(dbPath);
        }

        static public Product GetProduct(string name)
        {
            using (db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbPath)))
            {
                Product product = db.GetAllWithChildren<Product>().First(prod => prod.name == name);
                db.Close();
                return product;
            }
        }

        static public void AddNote(Recipe recipe)
        {
            db.Insert(recipe);
        }

        static public void AddNewRecipe(Recipe newRecipe, string currentCategoryName, List<int> mera)
        {
            using (db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbPath)))
            {
                AddNote(newRecipe);
                db.UpdateWithChildren(newRecipe);
                Recipe recipe = db.GetAllWithChildren<Recipe>().ToList().Last();
                Category category = db.GetAllWithChildren<Category>().ToList().First(y => y.name == currentCategoryName);
                category.recipes.Add(recipe);
                db.UpdateWithChildren(category);

                for (int i = 0; i < recipe.products.Count; i++)
                {
                    Ingredients ingredient = db.Table<Ingredients>().ToList().First(y => (y.id_recipe == recipe.Id) && (y.id_product == recipe.products[i].Id));
                    ingredient.quantity = mera[i];
                    db.UpdateWithChildren(ingredient);
                }
                db.Close();
            }
        }

        static public void SaveNote(Recipe recipe)
        {
            db.Update(recipe);
        }

        static public void DeleteNote(Recipe recipe)
        {
            db.Delete(recipe);
        }
    }
}