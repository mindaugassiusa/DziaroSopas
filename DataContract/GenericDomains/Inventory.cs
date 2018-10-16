using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;

namespace DataContract
{
    [CollectionName("Inventory")]
    public class Inventory : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public Boolean IsOrdered { get; set; }
        public int Quantity { get; set; }
        public int InStock { get; set; }
        public double FullPrice { get; set; }
    }
}
