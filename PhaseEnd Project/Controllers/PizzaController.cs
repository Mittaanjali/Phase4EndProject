using Microsoft.AspNetCore.Mvc;
using PhaseEnd_Project.Models;

namespace PhaseEnd_Project.Controllers
{
    public class PizzaController : Controller

    {
        static public List<Pizza> pizzadetails = new List<Pizza>() {

                new Pizza { PizzaId = 1,Type = "Brick Oven Pizza", Price =350},
                new Pizza { PizzaId = 2,Type = "Italian Pizza",Price=280},
                new Pizza { PizzaId = 3,Type = "Greek Pizza",Price=260}
            };
        static public List<OrderInfo> orderdetails = new List<OrderInfo>();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cart(int id)
        {
            var found = (pizzadetails.Find(p => p.PizzaId == id));

            TempData["id"] = id;

            return View(found);

        }
        [HttpPost]
        public IActionResult Cart(IFormCollection f)
        {
            Random r = new Random();
            int id = Convert.ToInt32(TempData["id"]);
            OrderInfo o = new OrderInfo();
            var found = (pizzadetails.Find(p => p.PizzaId == id));
            o.OrderId = r.Next(100, 999);
            o.PizzaId = id;
            o.Price = found.Price;
            o.Type = found.Type;
            o.Quantity = Convert.ToInt32(Request.Form["qty"]);
            o.TotalPrice = o.Price * o.Quantity;

            orderdetails.Add(o);

            return RedirectToAction("Checkout");

        }
        public IActionResult Checkout()
        {

            return View(orderdetails);

        }
    }
}
