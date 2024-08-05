namespace Utils
{
    public class PaginatedItemsViewModel<T>
    {
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public ICollection<T> Data { get; set; }
        public string Error { get; set; }
    }

    public class DTParams
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Dictionary<string, string> Search { get; set; }
        //public string Search { get; set; }
        public bool SearchRegex { get; set; }
        public List<DTOrder> Order { get; set; }
        public List<DTColumn> Columns { get; set; }
        public bool IsAscendingOrder { get
            {
                bool isAscending = false;
                if (Order != null)
                    if (Order.Count > 0)
                        isAscending = Order.First().Dir == "asc";
                return isAscending;
            }
        }
        public string GetColumnNameToBeOrdered { get => Columns.Any() ? Columns[GetColumnIndexToBeOrdered].Data : ""; }
        public int GetColumnIndexToBeOrdered { get
            {
                int columnNumber = 0;
                if (Order != null)
                    if (Order.Count > 0)
                        columnNumber = Convert.ToInt32(Order.Last().Column);
                return columnNumber;
            }
        }

    }
    
    public class DTColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DTSearch DTSearch { get; set; }
    }

    public class DTOrder
    {
        public string Column { get; set; }
        public string Dir { get; set; }
    }

    public class DTSource<T> 
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public ICollection<T> Data { get; set; }
        public string Error { get; set; }
    }

    public class DTSearch
    {
        public string value { get; set; }
        public string regex { get; set; }
    }
}
