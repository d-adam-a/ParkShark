using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkShark.Models
{
    public class Parking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TransportationType TransportationType { get; set; }
        public string LicensePlate { get; set; }
        public DateTime TimeEntry { get; set; }
    }
}
