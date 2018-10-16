using System.Collections.Generic;
using BackEnd.Base;
using DataContract;

namespace ServiceLayer.Inventory
{
    public class InventoryService : IInventorySerivce
    {
        private InventoryRepository _inventoryRepository = new InventoryRepository();

        private ActiveOrderRepository _activeOrderRepository = new ActiveOrderRepository();

        public void AddProducts(DataContract.Inventory model)
        {
            _inventoryRepository.AddEntity(model);
        }
        public List<CustomerModel> GetActiveOrders()
        {
            var result = new List<CustomerModel>();
            var orders = _activeOrderRepository.GetEntitiesByFilter(x => x.Quantity > 0);
            foreach (var order in orders)
            {
                var tempCustomerModel = new CustomerModel
                {
                    //Description = order.Description,
                    // InStock = order.InStock,
                    Id = order.Id,
                    IsOrdered = order.IsOrdered,
                    Quantity = order.Quantity,
                    Name = order.Name,
                    Price = order.Price,
                    TotalSum = order.Quantity * order.Price
                };
                result.Add(tempCustomerModel);
            }
            return result;
        }

        public List<CustomerModel> ShowInventoryToCustomer()
        {
            var result = new List<CustomerModel>();
            var orders = _inventoryRepository.GetEntitiesByFilter(x => x.Name != null);
            foreach (var order in orders)
            {
                var tempCustomerModel = new CustomerModel
                {
                    Description = order.Description,
                    InStock = order.InStock,
                    Id = order.Id,
                    IsOrdered = order.IsOrdered,
                    Quantity = order.Quantity,
                    Name = order.Name,
                    Price = order.Price,
                    TotalSum = order.Quantity * order.Price
                };
                result.Add(tempCustomerModel);
            }
            return result;
        }

        public CustomerModel GetOrderById(string Id)
        {
            var order = _activeOrderRepository.GetEntityById(Id);
            var tempCustomerModel = new CustomerModel
            {
                //Description = order.Description,
                //InStock = order.InStock,
                Id = order.Id,
                IsOrdered = order.IsOrdered,
                Quantity = order.Quantity,
                Name = order.Name,
                Price = order.Price,
                TotalSum = order.Quantity * order.Price
            };
            return tempCustomerModel;
        }

        public CustomerModel SaveEditedOrder(string Id, CustomerModel model)
        {
            var tempCustomerModel = new DataContract.Inventory
            {
                Description = model.Description,
                InStock = model.InStock,
                Id = model.Id,
                IsOrdered = model.Quantity > 0 ? true : false,
                Quantity = model.Quantity,
                Name = model.Name,
                Price = model.Price,
            };

            var order = _inventoryRepository.UpdateById(Id, tempCustomerModel);

            return model;
        }

        public CustomerModel AddNewProduct(CustomerModel model)
        {
            var tempCustomerModel = new DataContract.Inventory
            {
                Description = model.Description,
                InStock = model.InStock,
                Id = model.Id,
                IsOrdered = model.Quantity > 0 ? true : false,
                Quantity = model.Quantity,
                Name = model.Name,
                Price = model.Price,
            };

            var newProduct = _inventoryRepository.AddEntity(tempCustomerModel);

            return model;
        }
        //public CustomerModel CustomerMadeOrder(CustomerModel model)
        //{
        //    var tempCustomerModel = new DataContract.Inventory
        //    {
        //        Quantity = model.Quantity,
        //        IsOrdered = model.Quantity > 0 ? true : false,
        //        FullPrice = model.Price*model.Quantity, 
        //    };

        //    return model;
        //}

        public CustomerModel CustomerMadeOrder(CustomerModel model)
        {
            var tempCustomerModel = new DataContract.GenericDomains.ActiveOrders
            {
                Name = model.Name,
                Quantity = model.Quantity,
                Address = model.Address,
                Price = model.Price,
                TotalSum = model.Quantity * model.Price,
            };

            var newProduct = _activeOrderRepository.AddEntity(tempCustomerModel);

            return model;
        }
        public List<CustomerModel> GetInventory()
        {
            var result = new List<CustomerModel>();
            var orders = _inventoryRepository.GetEntitiesByFilter(x => x.Name != null);
            foreach (var order in orders)
            {
                var tempCustomerModel = new CustomerModel
                {
                    Description = order.Description,
                    InStock = order.InStock,
                    Id = order.Id,
                    IsOrdered = order.IsOrdered,
                    Quantity = order.Quantity,
                    Name = order.Name,
                    Price = order.Price,
                    //TotalSum = order.Quantity * order.Price,
                };
                result.Add(tempCustomerModel);
            }
            return result;
        }
    }
}
