using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace ProjWeb.Models
{
    public static class FileHandler
    {
        public static void SaveData(object obj, string filename)
        {
            XmlSerializer sr = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(filename);
            sr.Serialize(writer, obj);
            writer.Close();
        }
        public static T ReadData<T>(string filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            T obj;
            using (XmlReader reader = XmlReader.Create(filename))
            {
                obj = (T)ser.Deserialize(reader);
            }
            return obj;
        }
        public static void LoadAll(List<User> u, List<Comment> c, List<FitnessCenter> f, List<GroupTraining> g)
        {
            UsersLoad(u);
            CommentLoad(c);
            CenterLoad(f);
            TrainingLoad(g);
        }
        public static void UsersLoad(List<User> lst)
        {
            foreach (User u in lst)
            {
                Database.users.Add(u.username, u);
                if (u.role.Equals(role.owner))
                {
                    foreach(FitnessCenter fc in u.fitnessCenter)
                    {
                        if (!u.fitnessCenter.Contains(fc))
                            continue;
                        Database.fitnessCenters[fc.name].owner = u;
                    }
                }
            }
        }
        public static void CommentLoad(List<Comment> lst)
        {
            foreach (Comment u in lst)
            {
                Database.comments.Add(u.id, u);
            }
        }
        public static void CenterLoad(List<FitnessCenter> lst)
        {
            foreach (FitnessCenter u in lst)
            {
                Database.fitnessCenters.Add(u.name, u);
            }
        }
        public static void TrainingLoad(List<GroupTraining> lst)
        {
            foreach (GroupTraining u in lst)
            {
                Database.groupTrainings.Add(u.name, u);
            }
        }
    }
}