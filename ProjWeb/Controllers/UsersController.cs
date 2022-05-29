using ProjWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjWeb.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult RegView()
        {
            return View("Register");
        }
        public ActionResult LogView()
        {
            return View("Login");
        }
        public ActionResult Profile()
        {
            return View("Profile");
        }
        [HttpPost]
        public ActionResult Register()
        {
            var username = Request["username"] != null ? Request["username"] :
           string.Empty;
            var password = Request["password"] != null ? Request["password"] :
           string.Empty;
            var name = Request["name"] != null ? Request["name"] :
           string.Empty;
            var lastName = Request["lastName"] != null ? Request["lastName"] :
           string.Empty;
            var email = Request["email"] != null ? Request["email"] :
           string.Empty;
            var dateOfBirthString = Request["dateOfBirth"] != null ? Request["dateOfBirth"] :
           string.Empty;
            var genderString = Request["gender"] != null ? Request["gender"] :
           string.Empty;
            var roleString = Request["role"] != null ? Request["role"] :
           string.Empty;

            User visitor = (User)Session["visitor"];
            if (visitor != null)
            {
                username = visitor.username;
                roleString = visitor.role.ToString();
            }

            Enum.TryParse(genderString, out gender gender);
            Enum.TryParse(roleString, out role role);
            DateTime.TryParse(dateOfBirthString, out DateTime dateOfBirth);

            User user = new User(
                username,
                password,
                name,
                lastName,
                gender,
                email,
                dateOfBirth,
                role,
                new List<GroupTraining>(),
                new List<FitnessCenter>()
                );
            try
            {
                if (visitor != null)
                {
                    Database.users[username] = user;
                }
                else
                {
                    Database.users.Add(username, user);
                }
            }
            catch
            {
                ViewBag.message = "USERNAME EXISTS";
                return View("Register");
            }
                Session["visitor"] = user;

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult login()
        {
            var username = Request["username"] != null ? Request["username"] :
           string.Empty;
            var password = Request["password"] != null ? Request["password"] :
           string.Empty;

            if (Database.users.ContainsKey(username))
            {
                if (Database.users[username].password.Equals(password))
                {
                    Session["visitor"] = Database.users[username];
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult logout()
        {
            Session["visitor"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}