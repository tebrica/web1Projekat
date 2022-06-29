using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjWeb.Models
{
    public enum gender
    {
        male,
        female
    };
    public enum role
    {
        visitor,
        trainer,
        owner
    };
    public enum trainingType
    {
        yoga,
        lesMillsTone,
        bodyPump
    }
    public static class Database
    {
        public static Dictionary<string, User> users = new Dictionary<string, User>();
        public static Dictionary<string, FitnessCenter> fitnessCenters = new Dictionary<string, FitnessCenter>();
        public static Dictionary<string, GroupTraining> groupTrainings = new Dictionary<string, GroupTraining>();
        public static Dictionary<int, Comment> comments = new Dictionary<int, Comment>();
    }
}