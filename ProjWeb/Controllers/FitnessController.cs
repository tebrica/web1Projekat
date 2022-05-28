using ProjWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjWeb.Controllers
{
    public class FitnessController : Controller
    {
        // GET: Fitness
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetFitnessCenter()
        {
            var centerName = Request["page"] != null ? Request["page"] :
           string.Empty;

            return View("Fitness", Database.fitnessCenters[centerName]);
        }
    }
}