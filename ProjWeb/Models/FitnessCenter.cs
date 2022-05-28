using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjWeb.Models
{
    public class FitnessCenter
    {
        public string name;
        public int openingYear;
        public string address;
        public User owner;
        public int priceMonth;
        public int priceYear;
        public int priceTraining;
        public int priceGroupTraining;
        public int pricePersonalTraining;

        public FitnessCenter(string name, int openingYear, string address, User owner, int priceMonth, int priceYear, int priceTraining, int priceGroupTraining, int pricePersonalTraining)
        {
            this.name = name;
            this.openingYear = openingYear;
            this.address = address;
            this.owner = owner;
            this.priceMonth = priceMonth;
            this.priceYear = priceYear;
            this.priceTraining = priceTraining;
            this.priceGroupTraining = priceGroupTraining;
            this.pricePersonalTraining = pricePersonalTraining;
        }
    }
}