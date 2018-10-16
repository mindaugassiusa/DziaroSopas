
namespace DataContract
{
    public class CustomerModel
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsOrdered { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int InStock { get; set; }

        public double TotalSum { get; set; }

        public string Address { get; set; }
    }
}