using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ConikeShop;
using Microsoft.AspNetCore.Mvc.Rendering;
using Infrastructure.Persistence;
using ApplicationCore.Entities;

namespace ContactManager.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ConikeShopContext _context;

        public IndexModel(ConikeShopContext context)
        {
            _context = context;
        }

        public PaginatedList<Product> Products { get;set; }

        public void OnGet(string searchString,int pageIndex = 1)
        {
             ViewData["searchString"] = searchString;

            var genres = from m in _context.Products
                         orderby m.Genre
                         select m.Genre;
                         
            var products = from m in _context.Products
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(m => m.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(ProductGenre))
            {
                products = products.Where(m => m.Genre == ProductGenre);
            }
            int pageSize = 3;

            Genres = new SelectList(genres.Distinct().ToList());
            Products = PaginatedList<Product>.Create(products.AsNoTracking(), pageIndex, pageSize);
        }
        public IList<Product> product { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ProductGenre { get; set; }
    }
}
