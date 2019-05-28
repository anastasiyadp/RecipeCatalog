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
using static Android.Support.V7.Widget.RecyclerView;

namespace RecipeCatalog
{
    class EditRecipeAdapter : BaseAdapter<ProductForList>
    {
        public List<EditModel> editModelList;
        List<ProductForList> products;
        Activity context;

        public EditRecipeAdapter(Activity context, List<ProductForList> products, List<EditModel> editModelList) : base()
        {
            this.context = context;
            this.products = products;
            this.editModelList = editModelList;
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
            get { return products.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ProductForList product = products[position];
            string name = product.name;
            View view = convertView;
            if (view == null) {
                view = context.LayoutInflater.Inflate(Resource.Layout.rowEditProduct, null);
            }

            String nameProduct = view.FindViewById<TextView>(Resource.Id.nameProduct).Text = product.name;
            EditText editText = view.FindViewById<EditText>(Resource.Id.editTextProduct);
           
            String measureProduct = view.FindViewById<TextView>(Resource.Id.unitMeasureProduct).Text = product.measure;


            return view;
        }
    }
}