using System.Collections.Generic;
using System.Linq;

namespace ViewModels.Shared
{
    public class TypeAheadTagVM
    {
        public string propertyName { get; set; }
        public string model { get; set; }

        public string itemText { get; set; }
        public string itemValue { get; set; }
        public List<object> items { get; set; }
        public TypeAheadTagVM()
        {

        }
        public TypeAheadTagVM(string propertyName, string model, string itemText, string itemValue, IEnumerable<object> items)
        {
            this.propertyName = propertyName;
            this.model = model;
            this.itemText = itemText;
            this.itemValue = itemValue;
            this.items = items.ToList();
        }
    }
}
