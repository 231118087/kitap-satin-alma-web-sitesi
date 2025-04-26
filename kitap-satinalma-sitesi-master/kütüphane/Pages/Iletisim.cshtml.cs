using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kütüphane.Pages
{
    public class IletisimModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Subject { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Burada formdan gelen verileri işleyebilirsiniz
            // Örneğin, veritabanına kaydetme, e-posta gönderme gibi işlemler
            TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi!";
            return RedirectToPage("Iletisim");
        }
    }
}
