using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ProjWeb.Models
{
    [Serializable()]
    public class GroupTraining
    {
        public string name;
        [XmlIgnore]
        public trainingType trainingType;
        [XmlIgnore]
        public FitnessCenter fitnessCenter;
        public int trainingDuration;
        [XmlIgnore]
        public DateTime trainingTime;
        public int userCapacity;
        [XmlIgnore]
        public List<User> users;
        [XmlIgnore]
        public List<string> usersStr;
        public bool deleted;

        [XmlElement("trainingType")]
        public string TrainingType
        {
            get { return trainingType.ToString(); }
            set { trainingType = ParseEnum(value); }
        }
        [XmlElement("fitnessCenter")]
        public string FitnessCenter
        {
            get { return Database.fitnessCenters[fitnessCenter.name].name; }
            set { fitnessCenter = Database.fitnessCenters[value]; }
        }
        [XmlElement("trainingTime")]
        public string TrainingTime
        {
            get { return trainingTime.ToString("yyyy-MM-dd HH:mm:ss"); }
            set { trainingTime = DateTime.Parse(value); }
        }
        [XmlArray("UsersArray")]
        [XmlArrayItem("User")]
        public List<string> Users
        {
            get { return GetStringList(usersStr); }
            set { usersStr = GetStringList(value); }
        }
        public GroupTraining(string name, trainingType trainingType, FitnessCenter fitnessCenter, int trainingDuration, DateTime trainingTime, int userCapacity, List<User> users)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.trainingType = trainingType;
            this.fitnessCenter = fitnessCenter ?? throw new ArgumentNullException(nameof(fitnessCenter));
            this.trainingDuration = trainingDuration;
            this.trainingTime = trainingTime;
            this.userCapacity = userCapacity;
            this.users = users ?? throw new ArgumentNullException(nameof(users));
            this.deleted = false;
        }
        public trainingType ParseEnum(string word)
        {
            Enum.TryParse(word, out trainingType ret);
            return ret;
        }
        public List<string> GetStringList(List<string> lu)
        {
            List<User> ls = new List<User>();
            try
            {
                foreach (string u in lu)
                {
                    ls.Add(Database.users[u]);
                }
            }
            catch { }
            users = ls;
            return lu;
        }
        public GroupTraining()
        {
            users = new List<User>();
            Console.WriteLine(usersStr);
        }
    }
}