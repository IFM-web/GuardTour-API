using Microsoft.OpenApi.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GuardTour_API.Models
{
    public class ForgetPassword
    {
        [Required]
        public string? EmployeeCode { get; set; }
		[Required]
		public string? OldPassword { get; set; }
		[Required]
		public string? ConfirmPassword { get; set; }

    }
}
