namespace Utils.Extensions
{
    public static class SelectedItemExtensions
    {
        public static List<object> ToSelectListItem<T>(this IEnumerable<T> items, string itemText, string itemValue, string selectedVal = null)
        {
            List<dynamic> list = new List<dynamic>();
            foreach (var item in items)
            {
                dynamic _add = new System.Dynamic.ExpandoObject();
                _add.Text = item.GetStringValueFromObject(itemText);
                _add.Value = item.GetStringValueFromObject(itemValue);
                _add.Selected = (!string.IsNullOrEmpty(selectedVal)) ? _add.Text.Equals(selectedVal) : false;

                list.Add(_add);
            }
            return list;
        }


        public static List<SelectListItem> ToSelectListItemWithFormat<T,SelectListItem>(this List<T> items, string itemValue, string itemTextFormat) where SelectListItem: new()
        {
            items = items ?? new List<T>();
            List<SelectListItem> list = new List<SelectListItem>();
            List<string> listValueFormat = items.getStringValueFormat(itemTextFormat);

            for (int i = 0; i < items.Count; i++)
            {
                SelectListItem select = new SelectListItem();
                select.SetValueFromPropertyName("Text", listValueFormat[i]);
                select.SetValueFromPropertyName("Value", items[i].GetStringValueFromObject(itemValue));
                list.Add(select);
            }
            return list;
        }
    }

    public static class ArrayExtension
    {

    }
}
