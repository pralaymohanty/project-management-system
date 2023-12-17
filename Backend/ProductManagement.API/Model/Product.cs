using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.Model
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public string ProductId { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("product_code")]
        public string ProductCode { get; set; }

        [Column("product_price")]
        public decimal ProductPrice { get; set; }

        [Column("product_Image")]
        public string ProductImage { get; set; }

        [Column("product_desc")]
        public string ProductDescription { get; set; }
    }
}
