namespace GuardTour_API.Models
{
    public class ReportIncident
    {
        public string? CompanyId { get; set; } = string.Empty;
        public string? BranchId { get; set; } = string.Empty;
        public string? CustomerId { get; set; } = string.Empty;
        public string? SiteId { get; set; } = string.Empty;
        public string? EmployeeId { get; set; } = string.Empty;
        public string? Remark { get; set; } = string.Empty;
        public string? Image { get; set; } = "0";
        public string? Audio {  get; set; } = string.Empty;
      
    }
}
