using EconomicShop.Data;
using EconomicShop.Model.Models;
using EconomicShop.ViewModel.Common.CartVM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EconomicShop.Service.Catalog.Carts
{
    public class CartService
    {
        private readonly QHuyShopDbContext _dbContext;

        public CartService()
        {
            _dbContext = new QHuyShopDbContext();
        }

        public int GetTotalProductCart()
        {
            var userId = HttpContext.Current.Session["UserId"]?.ToString();
            var total = _dbContext.Carts.Where(x => x.UserId == userId).Count();

            return total;
        }

        
        public async Task<List<CartViewModel>> GetAllCart(string userId)
        {
            var carts = await (from c in _dbContext.Carts
                         join p in _dbContext.Products on c.ProductId equals p.Id
                         join pc in _dbContext.ProductCategories on p.CategoryId equals pc.Id
                         select new {product = p, cart = c, productCategogy = pc}).Where(x => x.cart.UserId == userId).Select(x => new CartViewModel()
                {
                    Id = x.cart.Id,
                    UserId = x.cart.UserId,
                    DateCreated = x.cart.DateCreated,
                    Price = x.cart.Price,
                    ProductId = x.cart.ProductId,
                    ProductName = x.product.Name,
                    ProductImage = x.product.Image,
                    CategoryName = x.productCategogy.Name,
                    Quantity = x.cart.Quantity,
                }).ToListAsync();

            if (carts == null) throw new Exception("Không tìm thấy giỏ hàng.");

            return carts;
        }

        public async Task<Cart> AddToCart(AddToCartRequest request)
        {
            var cart = new Cart()
            {
                ProductId = request.ProductId,
                UserId = HttpContext.Current.Session["UserId"].ToString(),
                DateCreated = DateTime.Now,
                Price = request.Price,
                Quantity = request.Quantity,

            };

            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();

            return cart;
        }

        public short UpdateQuantity(int Id, short quantity)
        {
            var UserId = HttpContext.Current.Session["UserId"]?.ToString();
            var cart = _dbContext.Carts.FirstOrDefault(x => x.Id == Id && x.UserId == UserId);

            if (cart == null) throw new Exception("Không tìm thấy giỏ hàng");

            cart.Quantity = quantity;

            _dbContext.Entry(cart).Property(x => x.Quantity).IsModified = true;
            _dbContext.SaveChangesAsync();

            return cart.Quantity;
        }

        public async Task<Cart> DeleteCart(int Id, string? userId = null)
        {
            string? UserId = userId;
            if (UserId == null)
            {
                UserId = HttpContext.Current.Session["UserId"]?.ToString();

            }
            var cart = _dbContext.Carts.FirstOrDefault(c => c.Id == Id && c.UserId == userId);


            if (cart == null) throw new Exception("Không tìm thấy thông tin sản phẩm với cart id: " + Id);

            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();

            return cart;
        }
    }
}
