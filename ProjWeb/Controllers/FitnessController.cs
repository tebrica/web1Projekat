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
            int newId = Database.comments.Keys.Max() + 1;
            Database.comments.Add(newId, new Comment(visitor, Database.fitnessCenters[center], comment, int.Parse(mark), newId));
            return View("Fitness", Database.fitnessCenters[center]);
        }
        public ActionResult SignUp()
        {
            var training = Request["training"] != null ? Request["training"] :
           string.Empty;
            User visitor = (User)Session["visitor"];
            Database.groupTrainings[training].users.Add(visitor);
            Database.users[visitor.username].groupTrainings.Add(Database.groupTrainings[training]);

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
            }catch{

            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Delete()
        {
            var training = Request["training"] != null ? Request["training"] :
           string.Empty;

            Database.groupTrainings[training].deleted = true;


            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
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
            
            return RedirectToAction("Index", "Home");
        }
    }
}