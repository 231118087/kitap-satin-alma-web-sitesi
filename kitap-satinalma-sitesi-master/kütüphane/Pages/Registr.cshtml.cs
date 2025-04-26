using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System;

namespace kütüphane.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string AdSoyad { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Telefon { get; set; }

        [BindProperty]
        public string Sifre { get; set; }

        public string BasariMesaji { get; set; }
        public string HataMesaji { get; set; }

        public IActionResult OnPost()
        {
            string connectionString = "Data Source=C:\\Users\\balki\\UYE_GIRISI.db";

            if (IsUserRegistered(Email))
            {
                HataMesaji = "Bu email ile zaten bir hesap oluşturulmuş.";
                return Page();  // Eğer kullanıcı zaten kayıtlıysa, aynı sayfada kalır
            }

            string sql = "INSERT INTO veriler ([Ad-Soyad], Email, Telefon, Sifre) VALUES (@AdSoyad, @Email, @Telefon, @Sifre)";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@AdSoyad", AdSoyad);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Telefon", Telefon);
                    command.Parameters.AddWithValue("@Sifre", Sifre);

                    connection.Open();
                    command.ExecuteNonQuery();  // Veriyi veritabanına ekler
                }
            }

            BasariMesaji = "Kayıt başarılı bir şekilde oluşturuldu!";
            
            // Başarıyla kayıt olduktan sonra ana sayfaya yönlendir
            return RedirectToPage("/Login");  // Ana sayfaya yönlendirme yapıyoruz
        }

        private bool IsUserRegistered(string email)
        {
            string connectionString = "Data Source=C:\\Users\\balki\\UYE_GIRISI.db";
            string sql = "SELECT COUNT(*) FROM veriler WHERE Email = @Email";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();

                    int userCount = Convert.ToInt32(command.ExecuteScalar());
                    return userCount > 0;
                }
            }
        }
    }
}
