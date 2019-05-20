﻿using System;
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
    class AdapterProduct : BaseAdapter<Recipe>
    {

       List<Recipe> recipes;
        Activity context;

        public AdapterProduct(Activity context, List<Recipe> recipes) : base()
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

            String namePro = view.FindViewById<TextView>(Resource.Id.textCategoryRecipe).Text = recipe.Category.name;
            String nameRecipe = view.FindViewById<TextView>(Resource.Id.textNameRecipe).Text = recipe.name;
            String instructionRecipe = view.FindViewById<TextView>(Resource.Id.textInstructionRecipe).Text = recipe.instruction;
            

            return view;
        }
    }
}