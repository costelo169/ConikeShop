using ContactManager.Data;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactManager.Pages.Contacts
{
    public abstract class Product_BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; }
         protected ConikeShopContext _Context {get;}
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        public Product_BasePageModel(
            ConikeShopContext _context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager) : base()
        {
            _Context = _context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
        
    }
}