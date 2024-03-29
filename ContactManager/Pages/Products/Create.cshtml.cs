using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactManager.Authorization;
using ContactManager.Pages.Contacts;

namespace ContactManager.Pages.Products
{
    public class CreateModel : Product_BasePageModel
    {
        private readonly ConikeShopContext _context;

        public CreateModel(ConikeShopContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager)
                : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

          

            return RedirectToPage("./Index");
        }
    }
}
