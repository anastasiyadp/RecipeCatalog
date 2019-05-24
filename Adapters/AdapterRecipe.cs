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
using RecipeCatalog.Models;

namespace RecipeCatalog
{
    class AdapterRecipe: BaseAdapter<Recipe>
    {
        List<Recipe> recipes;
        Activity context;

        public AdapterRecipe(Activity context, List<Recipe> recipes) : base()
        {
            this.context = context;
            this.recipes = recipes;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Recipe this[int position]
        {
            get { return recipes[position]; }
        }

        public override int Count
        {
            get { return recipes.Count;}
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Recipe recipe = recipes[position];
            string name = recipe.name;
            View view = convertView;
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.rowRecipesList, null);

            String nameRecipe = view.FindViewById<TextView>(Resource.Id.textNameRecipe).Text = recipe.name;
            String allIngredients ="";
            foreach (Product product in recipe.products)
            {
                allIngredients += product.name + " ";
            }

            view.FindViewById<TextView>(Resource.Id.textAllIngredients).Text = allIngredients;
            

            return view;
        }
    }
}