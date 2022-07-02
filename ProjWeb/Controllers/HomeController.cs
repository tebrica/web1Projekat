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

                Database.groupTrainings.Add("asd", new GroupTraining("asd", trainingType.bodyPump, Database.fitnessCenters["asd"], 3, DateTime.Now, 10, new List<User>()));
                Database.groupTrainings.Add("qwe", new GroupTraining("qwe", trainingType.lesMillsTone, Database.fitnessCenters["zxc"], 3, DateTime.Now, 15, new List<User>()));
                Database.groupTrainings.Add("zxc", new GroupTraining("zxc", trainingType.bodyPump, Database.fitnessCenters["asd"], 3, DateTime.Now, 10, new List<User>()));
                Database.groupTrainings.Add("fgh", new GroupTraining("fgh", trainingType.yoga, Database.fitnessCenters["fgh"], 3, DateTime.Now, 12, new List<User>()));

                Database.comments.Add(0, new Comment(new Models.User(), Database.fitnessCenters["asd"], "PRE LO SE", 1, 0));
                Database.comments.Add(1, new Comment(new Models.User(), Database.fitnessCenters["asd"], "KIDA", 5, 1));
                Database.comments.Add(2, new Comment(new Models.User(), Database.fitnessCenters["asd"], "SMECE", 2, 2));
                Database.comments.Add(3, new Comment(new Models.User(), Database.fitnessCenters["qwe"], "STA JE OVO", 2, 3));
            }
            catch { }
            return View(Database.fitnessCenters.Values.OrderBy(x=>x.name));
        }
        [HttpPost]
        public ActionResult CreateCenter()
        {
            var name = Request["name"] != null ? Request["name"] : string.Empty;
            var address = Request["address"] != null ? Request["address"] : string.Empty;
            var year = Request["year"] != null ? Request["year"] : string.Empty;
            var priceMonth = Request["priceMonth"] != null ? Request["priceMonth"] : string.Empty;
            var priceYear = Request["priceYear"] != null ? Request["priceYear"] : string.Empty;
            var priceTraining = Request["priceTraining"] != null ? Request["priceTraining"] : string.Empty;
            var pricePersonalTraining = Request["pricePersonalTraining"] != null ? Request["pricePersonalTraining"] : string.Empty;
            var priceGroupTraining = Request["priceGroupTraining"] != null ? Request["priceGroupTraining"] : string.Empty;

            try
            {
                FitnessCenter fc = new FitnessCenter(name, int.Parse(year), address, (User)Session["visitor"], int.Parse(priceMonth), int.Parse(priceYear), int.Parse(priceTraining), int.Parse(priceGroupTraining), int.Parse(pricePersonalTraining));
                Database.fitnessCenters.Add(name, fc);
                User owner = (User)Session["visitor"];
                Database.users[owner.username].fitnessCenter.Add(fc);
            }
            catch
            {
                User owner = (User)Session["visitor"];
                if (Database.fitnessCenters[name].owner.name.Equals(owner.name))
                {
                    FitnessCenter fc = new FitnessCenter(name, int.Parse(year), address, (User)Session["visitor"], int.Parse(priceMonth), int.Parse(priceYear), int.Parse(priceTraining), int.Parse(priceGroupTraining), int.Parse(pricePersonalTraining));
                    Database.fitnessCenters[name] = fc;
                    owner.fitnessCenter[owner.fitnessCenter.FindIndex(x => x.name.Equals(fc.name))] = fc;
                }
                else
                {
                    ViewBag.message = "Center already exists";
                    return View("Index", Database.fitnessCenters.Values);
                }
            }
            return RedirectToAction("Index");
        }
    }
}