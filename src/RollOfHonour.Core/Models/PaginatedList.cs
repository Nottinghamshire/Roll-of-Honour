using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RollOfHonour.Core.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            // Maybe this should be calculated in case the list changes?
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public PaginatedList()
        {

        }
    }
}