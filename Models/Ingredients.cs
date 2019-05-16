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
    class Ingredients
    {
        [ForeignKey(typeof(Recipe))]
        public int id_recipe { get; set; }

        [ForeignKey(typeof(Product))]
        public int id_product { get; set; }

        public int quantity { get; set; }
    }
}