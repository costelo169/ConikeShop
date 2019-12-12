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
using System.Threading.Tasks;
using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ContactManager.Authorization;
using ContactManager.Pages.Contacts;


namespace ContactManager.Pages.Products
{
    public class IndexModel : Product_BasePageModel
    {
        private readonly ConikeShopContext _context;

        public IndexModel(ConikeShopContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager)
                : base(context, authorizationService, userManager)
        {
            _context = context;
        }

      
        public PaginatedList<Product> Products { get;set; }
        public IList<Contact> Contact { get; set; }
        
        public void OnGetAsync(string searchString,int pageIndex = 1)
        
        {
             
            ViewData["searchString"] = searchString;

            var isAuthorized = User.IsInRole(Constants.ContactManagersRole) ||
                               User.IsInRole(Constants.ContactAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

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
