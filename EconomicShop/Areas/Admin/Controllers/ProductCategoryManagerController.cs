using EconomicShop.Service.Catalog.ProductCategories;
using EconomicShop.ViewModel.Common.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EconomicShop.Areas.Admin.Controllers
{
    [RoutePrefix("Admin/ProductCategoryManager")]
    public class ProductCategoryManagerController : Controller
    {
        private readonly ProductCategoryService _productCategoryService;

        public ProductCategoryManagerController()
        {
            _productCategoryService = new ProductCategoryService();
        }

        // GET: Admin/ProductCateroryManager
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ProductCategoryManagerActive = "active";
            return View();
        }

        [HttpPost]
        [Route("load-product-category")]
        public JsonResult LoadProductCategory()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                //Paging Size (10,20,50,100)
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data
                var customerData = _productCategoryService.GetProductCateroryByDataTable();

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name == searchValue);
                }

                //total number of rows count
                recordsTotal = customerData.Count();
                //Paging
                var data = customerData.Select(x=> new ProductCategoryViewModel()
                {
                    Description = x.Description,
                    DisplayOrder = x.DisplayOrder,
                    HomeFlag = x.HomeFlag,
                    Id = x.Id,
                    Image = x.Image,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Status = x.Status
                }).Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<JsonResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                }
                var productCategories = await _productCategoryService.GetAllProductCategory();

                if (productCategories == null) return Json(new Dictionary<string, object> {
                        {"result", "Danh mục sản phẩm trống" }
                    }, JsonRequestBehavior.AllowGet);

                return Json(productCategories, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Dictionary<string, object> {
                        {"result",  ex.Message}
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("get-all-by-select2")]
        public async Task<JsonResult> GetAllBySelect2(string q)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                }
                var productCategories = await _productCategoryService.GetAllProductCategoryBySelect2(q);

                if (productCategories == null) return Json(new Dictionary<string, object> {
                        {"result", "Danh mục sản phẩm trống" }
                    }, JsonRequestBehavior.AllowGet);

                return Json(new
                {
                    results = productCategories,
                    pagination = new
                    {
                        more = true
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Dictionary<string, object> {
                        {"result",  ex.Message}
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("add-new")]
        public async Task<JsonResult> AddNewProductCategory(ProductCategoryCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                }
                var productCategory = await _productCategoryService.AddNewProductCategory(request);

                if (productCategory == null) return Json(new Dictionary<string, object> {
                        {"result", "Không thể thêm danh mục sản phẩm" },
                         {"status", "error" }
                    }, JsonRequestBehavior.AllowGet);

                return Json(new Dictionary<string, object> {
                        {"result", "Thêm danh mục sản phẩm thành công" },
                    {"status", "success" }
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Dictionary<string, object> {
                        {"result",  ex.Message},  {"status", "error" }
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<JsonResult> UpdateProductCategory(ProductCategoryUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                }
                var productCategory = await _productCategoryService.UpdateProductCategory(request);

                if (productCategory == null) return Json(new Dictionary<string, object> {
                        {"result", "Không thể cập nhật danh mục sản phẩm" },
                         {"status", "error" }
                    }, JsonRequestBehavior.AllowGet);

                return Json(new Dictionary<string, object> {
                        {"result", "Cập nhật danh mục sản phẩm thành công" },
                    {"status", "success" }
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Dictionary<string, object> {
                        {"result",  ex.Message},  {"status", "error" }
                    }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}