using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GuardTour_API.Models
{
    public class AlertModel
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
        public string? Image { get; set; } = string.Empty;
       
    }
}
