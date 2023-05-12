using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitServices.Models
{
    public class Fruit
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Family { get; set; }
        public string Order { get; set; }
        public string Genus { get; set; }
        public Nutritions Nutritions { get; set; }
    }
    public class Nutritions
    {
        public int Calories { get; set; }
        public double Fat { get; set; }
        public double Sugar { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
    }



}
