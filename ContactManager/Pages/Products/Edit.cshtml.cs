using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactManager.Authorization;
using ContactManager.Pages.Contacts;
using System.Linq;

namespace ContactManager.Pages.Products
{
    public class EditModel : Product_BasePageModel
    {
        private readonly ConikeShopContext _context;

        public EditModel(ConikeShopContext context,
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
             var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Product, ContactOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;
            var isAuthorized = await AuthorizationService
                                    .AuthorizeAsync(User, Product, ContactOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            
            

            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
