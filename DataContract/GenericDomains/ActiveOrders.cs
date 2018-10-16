using BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.GenericDomains
{ 
    [CollectionName("ActiveOrders")]
    public class ActiveOrders : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public double TotalSum { get; set; }
        public Boolean IsOrdered { get; set; }
    }
}
