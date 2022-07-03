using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace ProjWeb.Models
{
    [Serializable()]
    public class Comment
    {
        public int id;
        [XmlIgnore]
        public User user;
        [XmlIgnore]
        public FitnessCenter fitnessCenter;
        public string text;
        public int mark;
        public bool approved;
        [XmlElement("user")]
        public string User
        {
            get { return user.username; }
            set { user = Database.users[value]; }
        }
        [XmlElement("fitnessCenter")]
        public string FitnessCenter
        {
            get { return fitnessCenter.name; }
            set { fitnessCenter = Database.fitnessCenters[value]; }
        }
        public Comment(User user, FitnessCenter fitnessCenter, string text, int mark, int id)
        {
            if(Database.comments.Count != 0)
            {
                this.id = Database.comments.Keys.ToList().Max() + 1;
            }
            else
            {
                id = 0;
            }
            this.user = user;
            this.fitnessCenter = fitnessCenter;
            this.text = text;
            this.mark = mark;
            this.approved = false;
        }
        public Comment()
        {
        }
    }
}