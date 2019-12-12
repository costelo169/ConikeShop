using System;
using System.Linq.Expressions;
using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification(string searchString, string genre)
            : base(MakeCriteria(searchString, genre))
        {
        }

        public ProductSpecification(string searchString, string genre, int pageIndex, int pageSize)
            : this(searchString, genre)
        {
            ApplyPaging(pageIndex, pageSize);
        }

        private static Expression<Func<Product, bool>> MakeCriteria(string searchString, string genre)
        {
            Expression<Func<Product, bool>> predicate = m => true;

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(genre))
            {
                predicate = m => m.Genre == genre && m.Title.Contains(searchString);
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                predicate = m => m.Title.Contains(searchString);
            }
            else if (!string.IsNullOrEmpty(genre))
            {
                predicate = m => m.Genre == genre;
            }

            return predicate;
        }
    }
}