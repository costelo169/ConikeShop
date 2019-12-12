using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Entities;

namespace ConikeShop
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(IEnumerable<T> source, int pageIndex, int totalPage)
        {
            PageIndex = pageIndex;
            TotalPage = totalPage;
            this.AddRange(source);
        }
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public bool HasNext
        {
            get { return PageIndex < TotalPage; }
        }
        public bool HasPrevious
        {
            get { return PageIndex > 1; }
        }

        public static PaginatedList<T> Create(IQueryable<T> query, int pageIndex, int pageSize)
        {
            int count = query.Count<T>();
            int totalPage = (int)Math.Ceiling(count / (double)pageSize);

            var items = query.Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize);


            return new PaginatedList<T>(items, pageIndex, totalPage);
        }

        internal IQueryable<Product> AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}