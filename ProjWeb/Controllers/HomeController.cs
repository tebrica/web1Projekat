using ProjWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                Database.fitnessCenters.Add("asd", new FitnessCenter("asd", DateTime.Now.Year, "asd 99", new Models.User(), 1, 4, 4, 123, 123));
                Database.fitnessCenters.Add("qwe", new FitnessCenter("qwe", 2021, "qwe 11", new Models.User(), 2, 1, 3, 123, 123));
                Database.fitnessCenters.Add("zxc", new FitnessCenter("zxc", 2020, "zxc 34", new Models.User(), 3, 2, 2, 123, 123));
                Database.fitnessCenters.Add("fgh", new FitnessCenter("fgh", 2019, "fgh 81", new Models.User(), 4, 3, 1, 123, 123));
            }
            catch { }
            return View(Database.fitnessCenters.Values.OrderBy(x=>x.name));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}