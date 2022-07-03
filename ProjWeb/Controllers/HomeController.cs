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
                //User djura = new Models.User("djura", "a", "a", "a", gender.male, "a@a", DateTime.Now, role.owner, new List<GroupTraining>(), new List<FitnessCenter>());
                //Database.users.Add("djura", djura);

                //FitnessCenter fc = new FitnessCenter("asd", DateTime.Now.Year, "asd 99", djura, 1, 4, 4, 123, 123);
                //Database.fitnessCenters.Add("asd", fc);
                //Database.fitnessCenters.Add("qwe", new FitnessCenter("qwe", 2021, "qwe 11", djura, 2, 1, 3, 123, 123));
                //Database.fitnessCenters.Add("zxc", new FitnessCenter("zxc", 2020, "zxc 34", djura, 3, 2, 2, 123, 123));
                //Database.fitnessCenters.Add("fgh", new FitnessCenter("fgh", 2019, "fgh 81", djura, 4, 3, 1, 123, 123));

                //GroupTraining gt = new GroupTraining("asd", trainingType.bodyPump, Database.fitnessCenters["asd"], 3, DateTime.Now, 10, new List<User>());
                //Database.groupTrainings.Add("asd", gt);
                //Database.groupTrainings.Add("qwe", new GroupTraining("qwe", trainingType.lesMillsTone, Database.fitnessCenters["zxc"], 3, DateTime.Now, 15, new List<User>()));
                //Database.groupTrainings.Add("zxc", new GroupTraining("zxc", trainingType.bodyPump, Database.fitnessCenters["asd"], 3, DateTime.Now, 10, new List<User>()));
                //Database.groupTrainings.Add("fgh", new GroupTraining("fgh", trainingType.yoga, Database.fitnessCenters["fgh"], 3, DateTime.Now, 12, new List<User>()));

                //Database.comments.Add(0, new Comment(djura, Database.fitnessCenters["asd"], "PRE LO SE", 1, 0));
                //Database.comments.Add(1, new Comment(djura, Database.fitnessCenters["asd"], "KIDA", 5, 1));
                //Database.comments.Add(2, new Comment(djura, Database.fitnessCenters["asd"], "SMECE", 2, 2));
                //Database.comments.Add(3, new Comment(djura, Database.fitnessCenters["qwe"], "STA JE OVO", 2, 3));

                //djura.groupTrainings.Add(gt);
                //djura.fitnessCenter.Add(fc);
                //gt.users.Add(djura);

                //FileHandler.SaveData(Database.users.Values.ToList(), "users.xml");
                //FileHandler.SaveData(Database.comments.Values.ToList(), "comments.xml");
                //FileHandler.SaveData(Database.fitnessCenters.Values.ToList(), "centers.xml");
                //FileHandler.SaveData(Database.groupTrainings.Values.ToList(), "trainings.xml");
                List<FitnessCenter> f = FileHandler.ReadData<List<FitnessCenter>>("centers.xml");
                FileHandler.CenterLoad(f);
                List<User> u = FileHandler.ReadData<List<User>>("users.xml");
                FileHandler.UsersLoad(u);
                List<GroupTraining> g = FileHandler.ReadData<List<GroupTraining>>("trainings.xml");
                FileHandler.TrainingLoad(g);
                List<Comment> c = FileHandler.ReadData<List<Comment>>("comments.xml");
                FileHandler.CommentLoad(c);
            }
            catch(Exception e) {
                string m = e.Message;
            }
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
                FileHandler.SaveData(Database.fitnessCenters.Values.ToList(), "centers.xml");
                FileHandler.SaveData(Database.users.Values.ToList(), "users.xml");
            }
            catch
            {
                User owner = (User)Session["visitor"];
                if (Database.fitnessCenters[name].owner.name.Equals(owner.name))
                {
                    FitnessCenter fc = new FitnessCenter(name, int.Parse(year), address, (User)Session["visitor"], int.Parse(priceMonth), int.Parse(priceYear), int.Parse(priceTraining), int.Parse(priceGroupTraining), int.Parse(pricePersonalTraining));
                    Database.fitnessCenters[name] = fc;
                    owner.fitnessCenter[owner.fitnessCenter.FindIndex(x => x.name.Equals(fc.name))] = fc;
                    FileHandler.SaveData(Database.fitnessCenters.Values.ToList(), "centers.xml");
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