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

namespace RecipeCatalog
{
    class AdapterRecipe: BaseAdapter<Stock>
    {
        List<Stock> recipes;
        Activity context;

        public AdapterRecipe(Activity context, List<Stock> recipes) : base()
        {
            this.context = context;
            this.recipes = recipes;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Stock this[int position]
        {
            get { return recipes[position]; }
        }

        public override int Count
        {
            get { return recipes.Count;}
            
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Stock recipe = recipes[position];
            string name = recipe.name;
            View view = convertView;
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.rowRecipe, null);

            String nameDrink = view.FindViewById<TextView>(Resource.Id.textView1).Text = recipe.Id.ToString();
            String priceDrink = view.FindViewById<TextView>(Resource.Id.textView2).Text = recipe.name;
            String countDrink = view.FindViewById<TextView>(Resource.Id.textView3).Text = recipe.instruction;
            return view;
        }
    }
}