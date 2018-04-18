using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        private EcommerceContext _context;
 
        public HomeController(EcommerceContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("dashboard")]
        [Route("")]
        public IActionResult Dashboard()
        {
            ViewBag.allProducts = _context.products.OrderByDescending(q => q.created_at).Take(3);
            ViewBag.allOrders = _context.orders.Include(c => c.customer).Include(b => b.product).OrderByDescending(n => n.created_at).Take(3);
            ViewBag.allCustomers = _context.customers.OrderByDescending(w => w.created_at).Take(3);
            return View("dashboard");
        }
        [HttpPost]
        [Route("filter-dashboard")]
        public IActionResult FilterDashboard(string search)
        {
            ViewBag.allProducts = _context.products.Where(b => b.productname.Contains(search)).OrderByDescending(q => q.created_at).Take(3);

            ViewBag.allOrders = _context.orders.Include(c => c.customer).Include(b => b.product).Where(q => q.customer.name.Contains(search)).OrderByDescending(n => n.created_at).Take(3);

            ViewBag.allCustomers = _context.customers.Where(z => z.name.Contains(search)).OrderByDescending(w => w.created_at).Take(3);
            return View("dashboard");
        }

        [HttpGet]
        [Route("customers")]
        public IActionResult Customers()
        {
            ViewBag.all_customers = _context.customers.OrderByDescending(y => y.created_at);
            return View("customers");
        }
        [HttpPost]
        [Route("add-customer")]
        public IActionResult CreateCustomer(string name)
        {
            if(_context.customers.Any(n => n.name == name))
            {
                TempData["error"] = "Customer already exists";
            }
            else
            {
                Customer newCustomer = new Customer{
                    name = name
                };
                _context.customers.Add(newCustomer);
            }   
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }

        [HttpPost]
        [Route("remove-customer")]
        public IActionResult RemoveCustomer(int customer_id)
        {
            var del = _context.customers.SingleOrDefault(c => c.customerid == customer_id);
            _context.customers.Remove(del);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }

        [HttpPost]
        [Route("search-name")]
        public JsonResult SearchCustomer(string search)
        {
            if(String.IsNullOrEmpty(search))
            {
                return Json(_context.customers.ToList());
            }
            var cust = _context.customers.Where(v => v.name.ToLower().Contains(search.ToLower())).ToList();
            return Json(cust);
        }

// search: $("#search").val()
                // error: function (xhr, status, error){
                //     alert("Error");







        [HttpGet]
        [Route("orders")]
        public IActionResult Orders()
        {
            ViewBag.customers = _context.customers.ToList();

            ViewBag.products = _context.products.ToList();

            ViewBag.qqq = _context.orders.Include(v => v.product).Include(c => c.customer).OrderByDescending(j => j.created_at);
            // ods.OrderByDescending(s => s.created_at);
            return View("orders");

            
        }
        [HttpPost]
        [Route("new-order")]
        public IActionResult NewOrder(int customer, int quantity, int product)
        {
            // var check_quantity = _context.products.Where(f => f.quantity < quantity);
            var check_quantity = _context.products.SingleOrDefault(f => f.productid == product);
            // check_quantity.quantity < quantity
            if(check_quantity.quantity < quantity)
            {
                TempData["error"] = "You are trying to order more than seller have right now in stock";
                return RedirectToAction("Orders");
            }
            Order newOrder = new Order{
                customerid = customer,
                quantity_order = quantity,
                productid = product
            };
            _context.orders.Add(newOrder);
            Product change_q = _context.products.SingleOrDefault(f => f.productid == product);
            change_q.quantity -= quantity;
            _context.SaveChanges();
            return RedirectToAction("Orders");
        }



        [HttpGet]
        [Route("products")]
        public IActionResult Product()
        {
            ViewBag.all_products = _context.products.OrderByDescending(p => p.created_at);
            return View("products");
        }
        [HttpPost]
        [Route("create-product")]
        public IActionResult CreateProduct(NewProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                Product newProduct = new Product{
                    productname = model.productname,
                    image = model.image,
                    description = model.description,
                    quantity = model.quantity
                };
                _context.products.Add(newProduct);
                _context.SaveChanges();
                return RedirectToAction("Product");
            }
            ViewBag.all_products = _context.products.OrderByDescending(p => p.created_at);
            return View("products");
        }

        [HttpPost]
        [Route("search-product")]
        public IActionResult Search(string search)
        {
            ViewBag.all_products = _context.products.Where(k => k.productname.Contains(search)).OrderByDescending(p => p.created_at);
            return View("products");
        }
    }
}
