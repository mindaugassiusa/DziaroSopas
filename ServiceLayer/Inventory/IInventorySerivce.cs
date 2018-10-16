using DataContract;
using System.Collections.Generic;

namespace ServiceLayer.Inventory
{
    public interface IInventorySerivce
    {
        void AddProducts(DataContract.Inventory model);
        List<CustomerModel> GetActiveOrders();
        List<CustomerModel> ShowInventoryToCustomer();
        List<CustomerModel> GetInventory();
        CustomerModel GetOrderById(string id);
        CustomerModel SaveEditedOrder(string id, CustomerModel model);
        CustomerModel AddNewProduct(CustomerModel model);
        CustomerModel CustomerMadeOrder(CustomerModel model);

    }
}
