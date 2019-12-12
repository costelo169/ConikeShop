using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactManager.Authorization;
using ContactManager.Pages.Contacts;
using ContactManager.Pages.Products;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactManager.Pages.Products
{

    public class DeleteModel : Product_BasePageModel
    {
        private readonly ConikeShopContext _context;
        public DeleteModel(
                ConikeShopContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager)
                : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.ID == id);

            if (Product == null)
            {
                return NotFound();
            }
           
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Product = await _context.Products.FindAsync(id);

            if (Product != null)
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }
            
            
            return RedirectToPage("./Index");
        }
    }
}
