using EconomicShop.Service.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
using EconomicShop.ViewModel.Common.Product;

namespace EconomicShop.Areas.Admin.Controllers
{
    [RoutePrefix("Admin/ProductManager")]
    public class ProductManagerController : Controller
    {
        private readonly ProductService _productService;

        public ProductManagerController()
        {
            _productService = new ProductService();
        }

        // GET: Admin/ProductManager
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ProductManagerActive = "active";
            return View();
        }

        [HttpPost]
        public JsonResult LoadProduct()
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
                var customerData = _productService.GetAllProduct();

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name.Contains(searchValue));
                }

                //total number of rows count
                recordsTotal = customerData.Count();
                //Paging
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("add-new")]
        public JsonResult AddProduct(ProductCreateViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                }

                var product = _productService.AddProduct(request);
                if (product == null)
                {
                    return Json(new Dictionary<string, object> {
                        {"result", "Không thể thêm sản phẩm" },
                         {"status", "error" }
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new Dictionary<string, object> {
                        {"result", "Thêm sản phẩm thành công" },
                         {"status", "success" }
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Dictionary<string, object> {
                        {"result",  ex.Message},
                         {"status", "error" }
                    }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [Route("update-product")]
        public JsonResult UpdateProduct(ProductUpdateRequest request)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return Json(ModelState, JsonRequestBehavior.AllowGet);
                //}

                var product = _productService.UpdateProduct(request);
                if (product == null)
                {
                    return Json(new Dictionary<string, object> {
                        {"result", "Không thể sửa sản phẩm" },
                         {"status", "error" }
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new Dictionary<string, object> {
                        {"result", "Cập nhật sản phẩm thành công" },
                         {"status", "success" }
                    }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new Dictionary<string, object> {
                        {"result",  ex.Message},
                         {"status", "error" }
                    }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}