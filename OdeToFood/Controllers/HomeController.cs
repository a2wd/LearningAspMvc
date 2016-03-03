using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var controller = RouteData.Values["controller"];
            var action = RouteData.Values["action"];
            var id = RouteData.Values["id"];

            var message = String.Format("Controller: {0}, action: {1}, id: {2}", controller, action, id);

            ViewBag.Message = message;
            return View();
        }

        public ActionResult About()
        {
            var model = new AboutModel();
            model.Location = "3chillies, Gilette Way";
            model.Name = "CSI Whitley";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}