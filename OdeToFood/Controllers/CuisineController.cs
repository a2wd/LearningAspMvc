using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class CuisineController : Controller
    {
        // GET: Cuisine
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost] <- an ActionSelector that would restrict the method to being called via a post request
        //Can be used to differentiate two different versions of an action

        //[Authorize] <- forces the user to be logged in to carry out an action
        [Authorize]
        public ActionResult Search(string name)
        {
            throw new Exception("Something went wrong ):");

            var message = Server.HtmlEncode(name);
            return Content(message);
        }
    }
}