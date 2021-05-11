using System;

namespace Hahn.ApplicatonProcess.February2021.Models
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        private int _size = 10;

        public int Page { get; set; }
        public int Size 
        {
            get 
            {
                return _size;
            }
            set 
            {
                _size = Math.Min(_maxSize, value);
            } 
        }

        public string SortBy { get; set; } = "ID";
        private string _sortOrder = "asc";
        public string SortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                if(value == "asc" || value=="desc")
                {
                    _sortOrder = value;
                }
            }
        }
    }
}
