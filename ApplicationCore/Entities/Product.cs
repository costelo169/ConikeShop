using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class Product : IAggregateRoot
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
    }
}