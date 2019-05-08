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
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace RecipeCatalog.Models
{
    class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name;
        public string instruction;

      
        [ForeignKey(typeof(Category))]
        public int id_category { get; set; }

        [ManyToMany(typeof(Ingredients))]
        public List<Product> products { get; set; }
    }
}