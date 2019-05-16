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
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string name { get; set; }
        public string instruction { get; set; }


        [ForeignKey(typeof(Category))]
        public int id_category { get; set; }

        [ManyToOne]
        public Category Category { get; set; }

        [ManyToMany(typeof(Ingredients))]
        public List<Product> products { get; set; }
    }
}