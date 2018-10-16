using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract;
using DataContract.GenericDomains;

namespace BackEnd.Base
{
    public class InventoryRepository : MongoRepository<Inventory>
    {

    }

    public class ActiveOrderRepository : MongoRepository<ActiveOrders>
    {

    }
}
