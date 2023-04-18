using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkShark.Models
{
    public class DetailParking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Parking Parking { get; set; }
        public int HourlyRate { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public decimal ParkingFee { get; set; }
        public string Status { get; set; }
    }
}
