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
    class EditModel
    {
        private String editTextValue;

        public String getEditTextValue()
        {
            return editTextValue;
        }

        public void setEditTextValue(String editTextValue)
        {
            this.editTextValue = editTextValue;
        }
    }
}