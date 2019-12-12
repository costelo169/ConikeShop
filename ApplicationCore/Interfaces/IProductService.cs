using System.Collections.Generic;
using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces
{
    public interface IProductService
    {
        ProductDto GetProduct(int id);
        IEnumerable<ProductDto> GetProduct(string searchString, string genre, int pageIndex, int pageSize, out int count);
        IEnumerable<string> GetGenres();
        void CreateProduct(SaveProductDto productDto);
        void UpdateProduct(SaveProductDto productDto);
        void DeleteProduct(int id);
    }
}