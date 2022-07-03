using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ProjWeb.Models
{
    [Serializable()]
    public class User
    {
        public string username;
        public string password;
        public string name;
        public string lastName;
        [XmlIgnore]
        public gender gender;
        public string email;
        public bool trainerBanned;
        [XmlIgnore]
        public DateTime dateOfBirth;
        [XmlIgnore]
        public role role;
        [XmlArray("TrainingsArray")]
        [XmlArrayItem("Training")]
        public List<GroupTraining> groupTrainings;
        [XmlArray("CentersArray")]
        [XmlArrayItem("Center")]
        public List<FitnessCenter> fitnessCenter;

        [XmlElement("dateOfBirth")]
        public string DateOfBirth
        {
            get { return dateOfBirth.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { dateOfBirth = DateTime.Parse(value); }
        }
        [XmlElement("role")]
        public string TrainingType
        {
            get { return role.ToString(); }
            set { role = ParseEnumR(value); }
        }
        [XmlElement("gender")]
        public string Gender
        {
            get { return gender.ToString(); }
            set { gender = ParseEnumG(value); }
        }

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
            groupTrainings = new List<GroupTraining>();
            fitnessCenter = new List<FitnessCenter>();
        }
        public gender ParseEnumG(string word)
        {
            Enum.TryParse(word, out gender ret);
            return ret;
        }
        public role ParseEnumR(string word)
        {
            Enum.TryParse(word, out role ret);
            return ret;
        }
    }
}