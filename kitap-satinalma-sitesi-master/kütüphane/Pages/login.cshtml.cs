using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SQLite;

namespace kütüphane.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string AdSoyad { get; set; }

        [BindProperty]
        public string Sifre { get; set; }

        public IActionResult OnPost()
        {
            // SQLite veritabanı bağlantı dizgisi
            string connectionString = "Data Source=C:\\Users\\balki\\UYE_GIRISI.db;Version=3;";

            // SQL sorgusu (admin girişi kontrolü eklenmiştir)
            string sql = "SELECT COUNT(*) FROM veriler WHERE (\"Ad-Soyad\" = @AdSoyad AND Sifre = @Sifre) OR (\"Ad-Soyad\" = 'admin' AND Sifre = 'admin123')";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    // Parametrelerin eklenmesi
                    command.Parameters.AddWithValue("@AdSoyad", AdSoyad);
                    command.Parameters.AddWithValue("@Sifre", Sifre);

                    // Bağlantı açılır
                    connection.Open();

                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    if (userCount > 0)
                    {
                        if (AdSoyad == "admin" && Sifre == "admin123")
                        {
                            // Admin girişi yapıldıysa product sayfasına yönlendir
                            return RedirectToPage("/Product");
                        }
                        else
                        {
                            // Kullanıcı girişi yapıldıysa anasayfaya yönlendir
                            return RedirectToPage("/Index");
                        }
                    }
                    else
                    {
                        // Kullanıcı doğrulanamadıysa hata mesajını göster
                        ModelState.AddModelError(string.Empty, "Ad Soyad veya Şifre hatalı!");
                        return Page();
                    }
                }
            }
        }
    }
}
