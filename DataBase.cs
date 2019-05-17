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

       
        //var storedValuation = db.GetWithChildren<Category>(1);
        //var x = db.GetWithChildren<Recipe>(1);
        // var t = db.Table<Category>().FirstOrDefault(y => y.Id == 1);
       

        static public void GetNote()
        {
            
        }

        static public void AddNote(Recipe recipe)
        {
            db.Insert(recipe);
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