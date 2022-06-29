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
    }
}