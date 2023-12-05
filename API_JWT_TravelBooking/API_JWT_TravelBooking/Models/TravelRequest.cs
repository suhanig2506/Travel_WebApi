using System.ComponentModel.DataAnnotations;

namespace API_JWT_TravelBooking.Models
{
    public class TravelRequest
    {
        [Key]
        public int RequestId { get; set; }
        public int? EmpId { get; set; }
        public string LocFrom { get; set; } = null!;
        public string LocTo { get; set; } = null!;
        public string? ApprovalStatus { get; set; }
        public string? BookingStatus { get; set; }
        public string? CurrentStatus { get; set; }
        public DateTime? ReqDate { get; set; }

        public virtual Employee? Emp { get; set; }
    }
}
