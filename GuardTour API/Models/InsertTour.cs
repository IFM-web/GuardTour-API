namespace GuardTour_API.Models
{
    public class InsertTour
    {
        public string CompanyId { get; set; }
        public string BranchId { get; set; }
        public string CustomerId { get; set; }
        public string SiteId { get; set; }
        public string RouteCode { get; set; }    
        public string PostId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationName { get; set; }
        public string EmployeeId { get; set; }   
        public string ShiftId { get; set; }
        public string BeatId { get; set; }
        public string Remark { get; set; }

        public IFormFile  Image { get; set; }
    }
}
