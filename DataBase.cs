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

        public DataBase(string nameDB)
        {
            db = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),nameDB));
        }

        public void CreateTable()
        {
            db.CreateTable<Stock>();
            if (db.Table<Stock>().Count() == 0)
            {
                var newStock = new Stock();
                newStock.name = "Салат";
                newStock.instruction = "Порезать кубиками, заправить майонезом";
                db.Insert(newStock);
                newStock = new Stock();
                newStock.name = "Суп";
                newStock.instruction = "Приготовить куриный бульон. Сварить картошку";
                db.Insert(newStock);
                newStock = new Stock();
                newStock.name = "Жаркое";
                newStock.instruction = "Порезать кусочками овощи, потушить";
                db.Insert(newStock);
            }
        }

        public void GetNote()
        {

        }

        public void AddNote(Stock recipe)
        {
            db.Insert(recipe);
        }

        public void SaveNote(Stock recipe)
        {
            db.Update(recipe);
        }

        public void DeleteNote(Stock recipe)
        {
            db.Delete(recipe);
        }
    }
}