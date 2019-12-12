using System.Collections.Generic;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using AutoMapper;

namespace ApplicationCore.Services
{
    public class ProductService : IProductService
    {
                private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ProductDto GetProduct(int id)
        {
            var movie = _unitOfWork.Products.GetBy(id);
            return _mapper.Map<Product, ProductDto>(movie);
        }

        public IEnumerable<ProductDto> GetProduct(string searchString, string genre, int pageIndex, int pageSize, out int count)
        {
            ProductSpecification productFilterPaginated = new ProductSpecification(searchString, genre, pageIndex, pageSize);
            ProductSpecification productFilter = new ProductSpecification(searchString, genre);

            var products = _unitOfWork.Products.Find(productFilterPaginated);
            count = _unitOfWork.Products.Count(productFilter);

            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public IEnumerable<string> GetGenres()
        {
            return _unitOfWork.Products.GetGenres();
        }

        public void CreateProduct(SaveProductDto saveProductDto)
        {
            var movie = _mapper.Map<SaveProductDto, Product>(saveProductDto);
            _unitOfWork.Products.Add(movie);

            _unitOfWork.Complete();
        }

        public void UpdateProduct(SaveProductDto saveProductDto)
        {
            var movie = _unitOfWork.Products.GetBy(saveProductDto.ID);
            if (movie == null) return;

            _mapper.Map<SaveProductDto, Product>(saveProductDto, movie);

            _unitOfWork.Complete();
        }

        public void DeleteProduct(int id)
        {
            var product = _unitOfWork.Products.GetBy(id);
            if (product != null)
            {
                _unitOfWork.Products.Remove(product);
                _unitOfWork.Complete();
            }
        }


        

    }
}