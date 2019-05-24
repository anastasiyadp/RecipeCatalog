using Android.App;
using Android.Content;
using Android.Util;
using Android.Widget;
using Java.Lang;
using Java.Util;
using System;
using System.Collections.Generic;

public class MultiSelectSpinner : Spinner, IDialogInterfaceOnMultiChoiceClickListener
{
    string[] _items = null;
    bool[] _selection = null;
    ArrayAdapter<string> _proxyAdapter;

    public MultiSelectSpinner(Context context) : base(context)
    {
        _proxyAdapter = new ArrayAdapter<string>(context, Android.Resource.Layout.SimpleSpinnerItem);
        base.Adapter = _proxyAdapter;
    }

    public MultiSelectSpinner(Context context, IAttributeSet attrs) : base(context, attrs)
    {
        _proxyAdapter = new ArrayAdapter<string>(context, Android.Resource.Layout.SimpleSpinnerItem);
        base.Adapter = _proxyAdapter;
    }

    public void OnClick(IDialogInterface dialog, int which, bool isChecked)
    {
        if (_selection != null && which < _selection.Length)
        {
            _selection[which] = isChecked;
            _proxyAdapter.Clear();
            _proxyAdapter.Add(BuildSelectedItemString());
            SetSelection(0);
        }
        else
        {
            throw new IllegalArgumentException("Argument 'which' is out of bounds");
        }
    }

    IDialogInterfaceOnClickListener listener;

    public override bool PerformClick()
    {
        
        AlertDialog.Builder builder = new AlertDialog.Builder(Context);
        builder.SetMultiChoiceItems(_items, _selection, this);
        builder.SetTitle("Добавьте продукты в рецепт");
        //builder.SetPositiveButton("OK", this);
        builder.SetPositiveButton("OK", delegate 
        {
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
        });

        //builder.SetNegativeButton("Cancel", this);
        builder.Show();
        return true;
    }

    public void OkAction(object sender, DialogClickEventArgs e)
    {
        var myButton = sender as Button; //this will give you the OK button on the dialog but you're already in here so you don't really need it - just perform the action you want to do directly unless I'm missing something..
        if (myButton != null)
        {
            //do something on ok selected
        }
    }
   
    public override ISpinnerAdapter Adapter
    {
        set { throw new RuntimeException("SetAdapter is not supported by MultiSelectionSpinner."); }
    }

    public void SetItems(string[] items)
    {
        _items = items;
        _selection = new bool[_items.Length];

        Arrays.Fill(_selection, false);
    }

    public void SetItems(List<string> items)
    {
        _items = items.ToArray();
        _selection = new bool[_items.Length];

        Arrays.Fill(_selection, false);
    }

    public void SetSelection(string[] selection)
    {
        foreach (string sel in selection)
        {
            for (int j = 0; j < _items.Length; ++j)
            {
                if (_items[j].Equals(sel))
                {
                    _selection[j] = true;
                }
            }
        }
    }

    public void SetSelection(List<string> selection)
    {
        foreach (string sel in selection)
        {
            for (int j = 0; j < _items.Length; ++j)
            {
                if (_items[j].Equals(sel))
                {
                    _selection[j] = true;
                }
            }
        }
    }

    public void SetSelection(int[] selectedIndicies)
    {
        foreach (int index in selectedIndicies)
        {
            if (index >= 0 && index < _selection.Length)
            {
                _selection[index] = true;
            }
            else
            {
                throw new IllegalArgumentException("Index " + index + " is out of bounds.");
            }
        }
    }

    public List<string> GetSelectedStrings()
    {
        List<string> selection = new List<string>();
        for (int i = 0; i < _items.Length; ++i)
        {
            if (_selection[i])
            {
                selection.Add(_items[i]);
            }
        }
        return selection;
    }

    public List<int> GetSelectedIndicies()
    {
        List<int> selection = new List<int>();
        for (int i = 0; i < _items.Length; ++i)
        {
            if (_selection[i])
            {
                selection.Add(i);
            }
        }
        return selection;
    }

    private string BuildSelectedItemString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        bool foundOne = false;

        for (int i = 0; i < _items.Length; ++i)
        {
            if (_selection[i])
            {
                if (foundOne)
                {
                    sb.Append(", ");
                }
                foundOne = true;

                sb.Append(_items[i]);
            }
        }
        return sb.ToString();
    }
}