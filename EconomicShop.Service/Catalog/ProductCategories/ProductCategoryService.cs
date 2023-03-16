using EconomicShop.Data;
using EconomicShop.Model.Models;
using EconomicShop.ViewModel.Common.ProductCategory;
using EconomicShop.ViewModel.Common.Select2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EconomicShop.Service.Catalog.ProductCategories
{
    public class ProductCategoryService
    {
        private readonly QHuyShopDbContext _dbContext;

        public ProductCategoryService()
        {
            _dbContext = new QHuyShopDbContext();
        }

        public IQueryable<ProductCategory> GetProductCateroryByDataTable()
        {
            var query = _dbContext.ProductCategories.AsQueryable();

            return query;
        }

        public async Task<List<ProductCategoryViewModel>?> GetAllProductCategory()
        {
            var productCategories = await _dbContext.ProductCategories
                .Select(x => new ProductCategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    DisplayOrder = x.DisplayOrder,
                    Description = x.Description,
                    HomeFlag = x.HomeFlag,
                    Image = x.Image,
                    ParentId = x.ParentId
                }).ToListAsync();

            if (productCategories == null) return null;

            return productCategories;
        }

        public async Task<List<Select2ViewModel>?> GetAllProductCategoryBySelect2(string q)
        {
            var productCategories = _dbContext.ProductCategories
                .Select(x => new Select2ViewModel()
                {
                    id = x.Id.ToString(),
                    text = x.Name
                }).AsQueryable();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                productCategories = productCategories.Where(x => x.text.ToLower().Contains(q.ToLower()));
            }

            if (productCategories == null) return null;

            return await productCategories.ToListAsync();
        }

        public async Task<ProductCategory?> AddNewProductCategory(ProductCategoryCreateRequest request)
        {
            var productCategory = new ProductCategory()
            {
                Alias = "Alias",
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                DisplayOrder = request.DisplayOrder,
                HomeFlag = request.HomeFlag,
                ParentId = request.ParentId,
                UpdatedDate = DateTime.Now,

                Status = request.Status,
            };

            if (request.Image != null)
            {
                string fileName = Path.GetFileName(request.Image.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/ProductCategories"), fileName);
                if (!File.Exists(path))
                {
                    request.Image.SaveAs(path);
                }
                productCategory.Image = fileName;
            }

            _dbContext.ProductCategories.Add(productCategory);
            await _dbContext.SaveChangesAsync();

            return productCategory;
        }

        public async Task<ProductCategory?> UpdateProductCategory(ProductCategoryUpdateRequest request)
        {
            string? path = null;
            var productCategory = await _dbContext.ProductCategories.FindAsync(request.Id);

            if (productCategory == null) throw new Exception("Không tìm thấy danh mục sản phẩm với id: " + request.Id);

            if(request.Name != null) productCategory.Name = request.Name;
            if(request.Description != null) productCategory.Description = request.Description;
            if(request.ParentId != null) productCategory.ParentId = request.ParentId;
            if(request.HomeFlag != null) productCategory.HomeFlag = request.HomeFlag!.Value;
            if(request.DisplayOrder != null) productCategory.DisplayOrder = request.DisplayOrder!.Value;
            if(request.Status != null) productCategory.Status = request.Status!.Value;


            if(request.Image != null)
            {
                string fileName = Path.GetFileName(request.Image.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/ProductCategories"), fileName);
               
               
                productCategory.Image = fileName;
            }

            _dbContext.ProductCategories.Attach(productCategory);
            _dbContext.Entry(productCategory).State = EntityState.Modified;
            if( await _dbContext.SaveChangesAsync() > 0)
            {
                if (!File.Exists(path) && request.Image != null)
                {
                    request.Image.SaveAs(path);
                }

                return productCategory;
            }
            return null;
        }
    }
}