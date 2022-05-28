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
                Database.users.Add(username, user);
            }
            catch
            {
                ViewBag.message = "USERNAME EXISTS";
                return View("Register");
            }
            if (user.role.Equals(role.visitor))
                Session["visitor"] = user;
            else if (user.role.Equals(role.owner))
                Session["owner"] = user;
            else if (user.role.Equals(role.trainer))
                Session["trainer"] = user;

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
                    if (Database.users[username].role.Equals(role.visitor))
                        Session["visitor"] = Database.users[username];
                    if (Database.users[username].role.Equals(role.owner))
                        Session["owner"] = Database.users[username];
                    if (Database.users[username].role.Equals(role.trainer))
                        Session["trainer"] = Database.users[username];
                }
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult logout()
        {
            Session["visitor"] = null;
            Session["owner"] = null;
            Session["trainer"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}