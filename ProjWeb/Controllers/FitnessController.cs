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
        public ActionResult Comment()
        {
            var comment = Request["comment"] != null ? Request["comment"] :
           string.Empty;
            var center = Request["center"] != null ? Request["center"] :
           string.Empty;
            var mark = Request["mark"] != null ? Request["mark"] :
           string.Empty;

            User visitor = (User)Session["visitor"];
            Comment c = new Comment(visitor, Database.fitnessCenters[center], comment, int.Parse(mark), 0);
            Database.comments.Add(c.id, c);
            FileHandler.SaveData(Database.comments.Values.ToList(), "comments.xml");
            return View("Fitness", Database.fitnessCenters[center]);
        }
        public ActionResult SignUp()
        {
            var training = Request["training"] != null ? Request["training"] :
           string.Empty;
            User visitor = (User)Session["visitor"];
            Database.groupTrainings[training].users.Add(visitor);
            Database.users[visitor.username].groupTrainings.Add(Database.groupTrainings[training]);
            FileHandler.SaveData(Database.users.Values.ToList(), "users.xml");
            FileHandler.SaveData(Database.groupTrainings.Values.ToList(), "trainings.xml");

            return RedirectToAction("Profile", "Users");
        }
        public ActionResult approveComment()
        {
            var comment = Request["comment"] != null ? Request["comment"] :
           string.Empty;
            try
            {
                int ind = int.Parse(comment);
                Database.comments[ind].approved = true;
                FileHandler.SaveData(Database.comments.Values.ToList(), "comments.xml");
            }
            catch{

            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Delete()
        {
            var training = Request["training"] != null ? Request["training"] :
           string.Empty;

            Database.groupTrainings[training].deleted = true;
            FileHandler.SaveData(Database.groupTrainings.Values.ToList(), "trainings.xml");


            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
        {
            try
            {
                var name = Request["name"] != null ? Request["name"] : string.Empty;
                var trainingType = Request["trainingType"] != null ? Request["trainingType"] : string.Empty;
                var trainingDuration = Request["trainingDuration"] != null ? Request["trainingDuration"] : string.Empty;
                var trainingDate = Request["trainingDate"] != null ? Request["trainingDate"] : string.Empty;
                var userCapacity = Request["userCapacity"] != null ? Request["userCapacity"] : string.Empty;
                var centerName = Request["centerName"] != null ? Request["centerName"] : string.Empty;
                var hours = Request["hours"] != null ? Request["hours"] : string.Empty;

                Enum.TryParse(trainingType, out trainingType tType);
                DateTime.TryParse(trainingDate, out DateTime tDate);
                tDate = tDate.AddHours(int.Parse(hours));
                if (tDate < DateTime.Now.AddDays(3))
                {
                    return RedirectToAction("Index", "Home");
                }
                GroupTraining gt = new GroupTraining(name, tType, Database.fitnessCenters[centerName], int.Parse(trainingDuration), tDate, int.Parse(userCapacity), new List<User>());
                Database.groupTrainings.Add(gt.name, gt);
                User visitor = (User)Session["visitor"];
                Database.users[visitor.username].groupTrainings.Add(gt);
                FileHandler.SaveData(Database.groupTrainings.Values.ToList(), "trainings.xml");
                FileHandler.SaveData(Database.users.Values.ToList(), "users.xml");
            }
            catch { }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit()
        {
            var name = Request["name"] != null ? Request["name"] : string.Empty;
            var trainingType = Request["trainingType"] != null ? Request["trainingType"] : string.Empty;
            var trainingDuration = Request["trainingDuration"] != null ? Request["trainingDuration"] : string.Empty;
            var trainingDate = Request["trainingDate"] != null ? Request["trainingDate"] : string.Empty;
            var userCapacity = Request["userCapacity"] != null ? Request["userCapacity"] : string.Empty;
            var centerName = Request["centerName"] != null ? Request["centerName"] : string.Empty;
            var hours = Request["hours"] != null ? Request["hours"] : string.Empty;

            Enum.TryParse(trainingType, out trainingType tType);
            DateTime.TryParse(trainingDate, out DateTime tDate);
            tDate = tDate.AddHours(int.Parse(hours));
            if (tDate < DateTime.Now.AddDays(3))
            {
                return RedirectToAction("Index", "Home");
            }
            GroupTraining gt = new GroupTraining(name, tType, Database.fitnessCenters[centerName], int.Parse(trainingDuration), tDate, int.Parse(userCapacity), new List<User>());
            Database.groupTrainings[gt.name] = gt;
            FileHandler.SaveData(Database.groupTrainings.Values.ToList(), "trainings.xml");
            User visitor = (User)Session["visitor"];
            visitor.groupTrainings[visitor.groupTrainings.FindIndex(x => x.name == gt.name)] = gt;

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Training()
        {
            var name = Request["training"] != null ? Request["training"] : string.Empty;
            return View("Training", Database.groupTrainings[name]);
        }
        public ActionResult DeleteFitnessCenter()
        {
            var name = Request["name"] != null ? Request["name"] : string.Empty;

            foreach(User u in Database.users.Values)
            {
                if(u.role.Equals(role.trainer) && u.fitnessCenter.Contains(Database.fitnessCenters[name]))
                {
                    Database.users[u.username].trainerBanned = true;
                    FileHandler.SaveData(Database.users.Values.ToList(), "users.xml");
                }
            }

            Database.fitnessCenters[name].deleted = true;
            FileHandler.SaveData(Database.fitnessCenters.Values.ToList(), "centers.xml");

            return RedirectToAction("Index", "Home");
        }
        public ActionResult CenterAddTrainer()
        {
            var name = Request["trainer"] != null ? Request["trainer"] : string.Empty;

            string trainer = name.Split('/')[0];
            string center = name.Split('/')[1];

            Database.users[trainer].fitnessCenter.Add(Database.fitnessCenters[center]);
            FileHandler.SaveData(Database.users.Values.ToList(), "users.xml");

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult BanTrainer()
        {
            var trainer = Request["trainer"] != null ? Request["trainer"] : string.Empty;
            Database.users[trainer].trainerBanned = true;
            FileHandler.SaveData(Database.users.Values.ToList(), "users.xml");

            return RedirectToAction("Index", "Home");
        }
    }
}