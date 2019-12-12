using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Infrastructure.Persistence;
using ApplicationCore.Entities;

namespace ConikeShop.Pages.Products
{
    public class OrderModel : PageModel
    {
        private readonly ConikeShopContext _context;

        public OrderModel(ConikeShopContext context)
        {
            _context = context;
        }
        public Product Product { get; set; }
        [BindProperty, EmailAddress, Required, Display(Name="Your Email Address")]
        public string OrderEmail { get; set; }
        [BindProperty, Required(ErrorMessage="Please supply a shipping address"), Display(Name="Shipping Address")]
        public string OrderShipping { get; set; } 
        [BindProperty, Display(Name="Quantity")]
        public int OrderQuantity { get; set; } = 1;

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
            Product = await _context.Products.FindAsync(id);
            if(ModelState.IsValid){
        
            var body = $@"<p>Thank you, we have received your order for {OrderQuantity} unit(s) of {Product.Title}!</p>
            <p>Your address is: <br/>{OrderShipping.Replace("\n", "<br/>")}</p>
            Your total is ${Product.Price * OrderQuantity}.<br/>
            We will contact you if we have questions about your order.  Thanks!<br/>";
            using(var smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = @"c:\mailpickup";
                var message = new MailMessage();
                message.To.Add(OrderEmail);
                message.Subject = "ConikeShop - New Order";
                message.Body = body;
                message.IsBodyHtml = true;
                message.From = new MailAddress("sales@ConikeShop.com");
                await smtp.SendMailAsync(message);
            }
            return RedirectToPage("OrderSuccess");
        }
        return Page();
}

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
       
    }
}
