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

namespace RecipeCatalog.Models
{
    class ProductForList
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public string measure { get; set; }
    }
}