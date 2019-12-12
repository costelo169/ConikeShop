using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.DTOs
{
    public class ProductDto
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string Genre { get; set; }

    }
}