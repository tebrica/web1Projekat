using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjWeb.Models
{
    public class GroupTraining
    {
        public string name;
        public trainingType trainingType;
        public FitnessCenter fitnessCenter;
        public int trainingDuration;
        public DateTime trainingTime;
        public int userCapacity;
        public List<User> users;
        public bool deleted;
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
    }
}