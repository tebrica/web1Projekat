using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjWeb.Models
{
    public class Comment
    {
        public User user;
        public FitnessCenter fitnessCenter;
        public string text;
        public int mark;

        public Comment(User user, FitnessCenter fitnessCenter, string text, int mark)
        {
            this.user = user;
            this.fitnessCenter = fitnessCenter;
            this.text = text;
            this.mark = mark;
        }
    }
}