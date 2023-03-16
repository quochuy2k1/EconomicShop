using EconomicShop.Data;
using EconomicShop.Model.Models;
using EconomicShop.Service.Enums;
using EconomicShop.ViewModel.Common.Product;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web;

namespace EconomicShop.Service.Catalog.Products
{
    public class ProductService
    {
        private readonly QHuyShopDbContext _dbContext;

        public ProductService()
        {
            _dbContext = new QHuyShopDbContext();
        }

        public IQueryable<ProductViewModel> GetAllProduct()
        {
            var query = _dbContext.Products
                .GroupJoin(_dbContext.ProductCategories,
                (p) => p.CategoryId,
                (pc) => pc.Id,
                (p, pc) => new { product = p, productCategory = pc }).Select(x => new ProductViewModel()
                {
                    Id = x.product.Id,
                    CategoryName = x.productCategory.FirstOrDefault().Name,
                    CategoryId = x.productCategory.FirstOrDefault().Id,
                    Image = x.product.Image,
                    Name = x.product.Name,
                    OriginalPrice = x.product.OriginalPrice,
                    Price = x.product.Price,
                    Promotion = x.product.Promotion,
                    Quantity = x.product.Quantity,
                    Content = x.product.Content,
                    Description = x.product.Description,
                    HomeFlag = x.product.HomeFlag,
                    HotFlag = x.product.HotFlag,
                    Status = x.product.Status,
                }).AsQueryable();

            return query;
        }

        public async Task<EconomicShop.ViewModel.Paging.PagedResult<ProductViewModel>> GetAllPagingProduct(ProductPagingRequest request)
        {
            var query = _dbContext.Products
                .GroupJoin(_dbContext.ProductCategories,
                (p) => p.CategoryId,
                (pc) => pc.Id,
                (p, pc) => new { product = p, productCategory = pc }).AsQueryable();

            // 2. Filter
            if (!String.IsNullOrEmpty(request.Keyword))
                query = query.Where(p => p.product.Name.Contains(request.Keyword));

            if (request.CategoryId != null)
                query = query.Where(p => request.CategoryId == p.product.CategoryId);

            // 3. Paging

            var TotalRow = await query.CountAsync();

            var data = await query.OrderBy(a => a.product.Name).Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.product.Id,
                    CategoryName = x.productCategory.FirstOrDefault().Name,
                    CategoryId = x.productCategory.FirstOrDefault().Id,
                    Image = x.product.Image,
                    Content = x.product.Content,
                    Name = x.product.Name,
                    OriginalPrice = x.product.OriginalPrice,
                    Price = x.product.Price,
                    Promotion = x.product.Promotion,
                    Quantity = x.product.Quantity,
                    Description = x.product.Description,
                    HomeFlag = x.product.HomeFlag,
                    HotFlag = x.product.HotFlag,
                    Status = x.product.Status
                }).ToListAsync();

            // 4. Select and Projection

            var PageResult = new EconomicShop.ViewModel.Paging.PagedResult<ProductViewModel>()
            {
                TotalRecord = TotalRow,
                Items = data
            };

            return PageResult;
        }

        public Task<ProductDetailViewModel> GetProductDetail(int Id)
        {
            var product = _dbContext.Products.Select(x => new ProductDetailViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Content = x.Content,
                Description = x.Description,
                Image = x.Image,
                MoreImages = x.MoreImages,
                OriginalPrice = x.OriginalPrice,
                Price = x.Price,
                Promotion = x.Promotion,
                Quantity = x.Quantity,
                Tags = x.Tags,
                ViewCount = x.ViewCount,
                Warranty = x.Warranty
            }).FirstOrDefaultAsync(p => p.Id == Id);

            if (product == null) throw new Exception("Không tìm thấy sản phẩm với Id: " + Id);

            return product;
        }

        public Product AddProduct(ProductCreateViewModel request)
        {
            var product = new Product()
            {
                Alias = "Alias",
                CategoryId = request.CategoryId,
                Content = request.Content,
                Description = request.Description,
                Name = request.Name,
                OriginalPrice = request.OriginalPrice,
                Price = request.Price,
                Quantity = request.Quantity,
                ViewCount = 0,
                Tags = request.Tags,
                CreatedDate = DateTime.Now,
                Status = request.Status,
                HomeFlag = request.HomeFlag,
                MoreImages = request.MoreImages,
                UpdatedDate = DateTime.Now,
                HotFlag = request.HotFlag,
                Promotion = request.Promotion,
                Warranty = request.Warranty,
            };

            if (request.Image != null)
            {
                string fileName = Path.GetFileName(request.Image.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Products"), fileName);
                if (!File.Exists(path))
                {
                    request.Image.SaveAs(path);
                }
                product.Image = fileName;
            }

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return product;
        }


        public async Task<Product> UpdateProduct(ProductUpdateRequest request)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync();
                if (product == null) throw new Exception("Không tìm thấy sản phẩm với Id: " + request.Id);

                if (product.CategoryId != null) product.CategoryId = request.CategoryId.Value;
                if (product.Content != null) product.Content = request.Content;
                //if (product.Description != null) product.Description = request.Description;
                if (product.Name != null) product.Name = request.Name;
                if (product.OriginalPrice != null) product.OriginalPrice = request.OriginalPrice.Value;
                if (product.Price != null) product.Price = request.Price.Value;
                if (product.Quantity != null) product.Quantity = request.Quantity.Value;
                if (product.ViewCount != null) product.ViewCount = 0;
                //if (product.Tags != null) product.Tags = request.Tags;
                if (product.Status != null) product.Status = request.Status;
                if (product.HomeFlag != null) product.HomeFlag = request.HomeFlag;
                //if (product.MoreImages != null) product.MoreImages = request.MoreImages;

                if (product.HotFlag != null) product.HotFlag = request.HotFlag;
                if (product.Promotion != null) product.Promotion = request.Promotion;
                if (product.Warranty != null) product.Warranty = request.Warranty;


                product.UpdatedDate = DateTime.Now;

                if (request.Image != null)
                {
                    string fileName = Path.GetFileName(request.Image.FileName);
                    string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Products"), fileName);
                    if (!File.Exists(path))
                    {
                        request.Image.SaveAs(path);
                    }
                    product.Image = fileName;
                }

                _dbContext.Products.Attach(product);
               //await _dbContext.SaveChangesAsync();

                return product;
            }
            catch (Exception)
            {

                throw;
            }

           
        }

        public async Task<bool> UpdateQuantity(int Id, int quantity, UpdateQuantityAction action)
        {
            var product = await _dbContext.Products.FindAsync(Id);
            if (product == null) return false;
            switch (action)
            {
                case UpdateQuantityAction.Increment:

                    product.Quantity += quantity;
                    _dbContext.Entry(product).Property(x => x.Quantity).IsModified = true;
                    return true;
                case UpdateQuantityAction.Decrement:

                    product.Quantity -= quantity;
                    _dbContext.Entry(product).Property(x => x.Quantity).IsModified = true;
                    return true;
                default:
                    return false;
            }
        }
    }
}