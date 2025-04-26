using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace kütüphane.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; }

        public void OnGet()
        {
            CartItems = GetCartItems();
        }

        public IActionResult OnPostRemoveFromCart(int bookId)
        {
            var cartItems = GetCartItems();
            var itemToRemove = cartItems.Find(item => item.Book.Id == bookId);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                SaveCartItems(cartItems);
            }
            return RedirectToPage("Cart");
        }

        private List<CartItem> GetCartItems()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
        }

        private void SaveCartItems(List<CartItem> cartItems)
        {
            var cartJson = JsonConvert.SerializeObject(cartItems);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }

    public class CartItem
    {
        public BookModel Book { get; set; }
        public int Quantity { get; set; }
    }

    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public string ImageFileName { get; set; }
    }
}
