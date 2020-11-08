using AutoMapper;
using SEDC.Lamazon.Homework.DataAccess.Interfaces;
using SEDC.Lamazon.Homework.Domain.Models;
using SEDC.Lamazon.Homework.Services.Interfaces;
using SEDC.Lamazon.Homework.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.Lamazon.Homework.Services.Services
{
    public class ProductService : IProductService
    {
        protected readonly IRepository<Product> _productRepository;
        protected readonly IMapper _mapper;
        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            List<Product> products = _productRepository.GetAll().ToList();
            return _mapper.Map<List<Product>, List<ProductViewModel>>(products);
        }

        public ProductViewModel GetProductById(int id)
        {
            Product product = _productRepository.GetById(id);
            ProductViewModel model = _mapper.Map<ProductViewModel>(product);
            
            if(model != null)
            {
                return model;
            }
            else
            {
                throw new Exception($"Product with id: {id} does not exist!");
            }
        }
    }
}
