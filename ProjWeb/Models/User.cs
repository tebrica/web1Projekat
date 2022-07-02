using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjWeb.Models
{
    public class User
    {
        public string username;
        public string password;
        public string name;
        public string lastName;
        public gender gender;
        public string email;
        public bool trainerBanned;
        public DateTime dateOfBirth;
        public role role;
        public List<GroupTraining> groupTrainings;
        public List<FitnessCenter> fitnessCenter;

        public User(string username, string password, string name, string lastName, gender gender, string email, DateTime dateOfBirth, role role, List<GroupTraining> groupTrainings, List<FitnessCenter> fitnessCenter)
        {
            this.trainerBanned = false;
            this.username = username;
            this.password = password;
            this.name = name;
            this.lastName = lastName;
            this.gender = gender;
            this.email = email;
            this.dateOfBirth = dateOfBirth;
            this.role = role;
            this.groupTrainings = groupTrainings;
            this.fitnessCenter = fitnessCenter;
        }
        public User()
        {
            this.name = "djura";
        }
    }
}