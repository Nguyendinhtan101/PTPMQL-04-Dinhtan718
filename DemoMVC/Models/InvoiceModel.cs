
using System.ComponentModel.DataAnnotations;

namespace BAITAP2102.Models
{
    public class InvoiceModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0")]
        public double UnitPrice { get; set; }

        public double CalculateTotal()
        {
            return Quantity * UnitPrice;
        }
    }
}
