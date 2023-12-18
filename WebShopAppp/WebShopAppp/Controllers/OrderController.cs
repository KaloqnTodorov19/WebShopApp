using System.Globalization;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebShopAppp.Core.Contracts;
using WebShopAppp.Infrastructure.Data.Domain;
using WebShopAppp.Models.Order;

namespace WebShopAppp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public OrderController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        [Authorize(Roles = "Administrator")]
        // GET: OrderController
        public ActionResult Index()
        {
            List<OrderIndex> orders = _orderService.GetOrders()
                .Select(x => new OrderIndex 
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice,

                }).ToList();

            return View(orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create(int id)
        {
            Product product = _productService.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }

            OrderCreateVM order = new OrderCreateVM()
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                QuantityInStock = product.Quantity,
                Price = product.Price,
                Discount = product.Discount,
                Picture = product.Picture,
            };
            return View(order);

        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateVM bidingModel)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = this._productService.GetProductById(bidingModel.ProductId);
            if(currentUserId == null|| product == null|| product.Quantity < bidingModel.Quantity)
            {
                return RedirectToAction("Denied", "Order");
            }
            if (ModelState.IsValid)
            {
                _orderService.Create(bidingModel.ProductId, currentUserId, bidingModel.Quantity);
            }
            return this.RedirectToAction("Index", "Product");
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Denied()
        {
            return View();
        }

        public ActionResult MyOrders()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderIndex> orders = _orderService.GetOrders()
                .Select(x => new OrderIndex
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice,

                }).ToList();

            return View(orders);
        }
    }
}
