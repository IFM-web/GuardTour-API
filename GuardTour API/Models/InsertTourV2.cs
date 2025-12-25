using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GuardTour_API.Models
{
    public class InsertTourV2
    {
        public string? CompanyId { get; set; } = string.Empty;
        public string? BranchId { get; set; } = string.Empty;
        public string? CustomerId { get; set; } = string.Empty;
        public string? SiteId { get; set; } =string.Empty;
        public string? RouteCode { get; set; } = string.Empty;
        public string? PostId { get; set; } = string.Empty;
        public string? Latitude { get; set; } = string.Empty;
        public string? Longitude { get; set; } = string.Empty;
        public string? LocationName { get; set; } = string.Empty;
        public string? EmployeeId { get; set; } = string.Empty;
        public string? ShiftId { get; set; } = string.Empty;
        public string? BeatId { get; set; } = string.Empty;
        public string? Remark { get; set; } = string.Empty;
        public string? Image { get; set; } = "0";
        public string? Image2 { get; set; } = "0";
        public string? Audio { get; set; } = "0";

        public string[] CheckPointImages { get; set; } = null;
        public int AlertFlg { get; set;}
    }
}
