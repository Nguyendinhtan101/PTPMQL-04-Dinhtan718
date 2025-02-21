
using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class HeThongPhanPhoi
    {
        [Key]
        public String MaHTPP { get; set;}
        public String TenHTPP { get;set;}
       
    }
}
