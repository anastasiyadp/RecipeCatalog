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
    class AdapterProduct : BaseAdapter<ProductForList>
    {

       List<ProductForList> products;
        Activity context;

        public AdapterProduct(Activity context, List<ProductForList> products) : base()
        {
            this.context = context;
            this.products = products;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ProductForList this[int position]
        {
            get { return products[position]; }
        }

        public override int Count
        {
            get { return products.Count;}
            
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ProductForList product = products[position];
            string name = product.name;
            View view = convertView;
            if (view == null) view = context.LayoutInflater.Inflate(Resource.Layout.rowProduct, null);

            String nameProduct = view.FindViewById<TextView>(Resource.Id.nameProduct).Text = product.name;
            String nameQuantity = view.FindViewById<TextView>(Resource.Id.quantityProduct).Text = product.quantity.ToString();
            String measureProduct = view.FindViewById<TextView>(Resource.Id.unitMeasureProduct).Text = product.measure;
            

            return view;
        }
    }
}