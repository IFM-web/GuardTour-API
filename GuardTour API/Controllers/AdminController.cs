using GuardTour_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;
using System.Data;
using System.Net;
namespace GuardTour_API.Controllers
{
	[Route("API/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		db_Utility util = new db_Utility();
		ClsUtility csutil = new ClsUtility();
		ResponseMassage Response;

		#region ForgetPassword

		[HttpPost("ForgetPassword")]

		public ResponseMassage ForgetPassword([FromForm] ForgetPassword obj)
		{
			try
			{
				//string ecnpwd = EncryptionHelper.Encrypt(obj.ConfirmPassword);
				//string oldpwden = EncryptionHelper.Encrypt(obj.OldPassword);



				if (ModelState.IsValid)
				{




					var ds = util.Fill("exec Udp_Forgetpassword @username='" + obj.EmployeeCode.Trim() + "',@oldpwd='" + obj.OldPassword.Trim() + "',@confpwd='" + obj.ConfirmPassword.Trim() + "'");
					string errmsg = ds.Tables[0].Rows[0][1].ToString();
					if (errmsg == "Password Updated SuccessFully")
					{
						return Response = new ResponseMassage
						{
							Massage = errmsg,
							StatusCode = HttpStatusCode.OK
						};

					}
					else

					{
						return Response = new ResponseMassage
						{
							Massage = errmsg,
							StatusCode = HttpStatusCode.NotFound
						};

					}
				}
				else {
					return new ResponseMassage
					{
						Massage = "Invalid input data.",
						StatusCode = HttpStatusCode.BadRequest
					};

				}


			}
			catch (Exception ex)
			{
			
				return Response = new ResponseMassage
				{
					Massage = ex.Message,
					StatusCode = HttpStatusCode.ExpectationFailed
				};
			}
		}
		#endregion

		#region Employee Login     
		[HttpGet("EmployeeLogin")]
		public IActionResult Login(string EmployeeId, string EmployeePassword)
		{

			try
			{
				var ds = util.Fill("exec EmployeeLogin @EmpId='" + EmployeeId.Trim() + "',@password='" + EmployeePassword.Trim() + "'");
				var data = ds.Tables[0];

				return Content(JsonConvert.SerializeObject(data), "application/json");

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		#endregion

		

		#region GetTaskdetails

		[HttpGet("GetTaskdetailsUpcoming")]
		public IActionResult GetTaskdetails(string EmployeeId, string BranchId, string CompanyId)
		{
			var ds = util.Fill("exec API_Usp_GetTaskdetails @EmpId='" + EmployeeId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "'");
			if (ds.Tables[0].Rows.Count != 0)
			{
				return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
			}
			else
			{
				return NotFound(new { Massage = "Record Not Found", status = HttpStatusCode.NotFound });

			}

		}
		#endregion

		#region GetTaskdetailsCompleted

		[HttpGet("GetTaskdetailsCompleted")]
		public IActionResult GetTaskCompeleteddetails(string EmployeeId, string BranchId, string CompanyId)
		{
			var ds = util.Fill("exec [API_Usp_GetTaskdetailsCompleted] @EmpId='" + EmployeeId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "'");
			if (ds.Tables[0].Rows.Count != 0)
			{
				return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
			}
			else
			{
				return NotFound(new { Massage = "Record Not Found", status = HttpStatusCode.NotFound });

			}

		}
		#endregion

		//#region GetShift
		//[HttpGet("GetShift")]
		//public IActionResult GetShift(string CustomerId, string SiteId, string BranchId, string CompanyId)
		//{
		//	var ds = util.Fill("exec API_Usp_GetShift @CustomerId='" + CustomerId + "',@SiteId='" + SiteId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "'");
		//	if (ds.Tables[0].Rows.Count != 0)
		//	{
		//		return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
		//	}
		//	else
		//	{
		//		return NotFound(new { Massage = "Shift Not Found", status = HttpStatusCode.NotFound });

		//	}
		//}

		//#endregion


		//[HttpGet("GetBeat")]
		//public IActionResult GetBeat(string CustomerId, string SiteId, string ShiftId, string BranchId, string CompanyId)
		//{
		//	var ds = util.Fill("exec API_Usp_GetBeat @CustomerId='" + CustomerId + "',@ShiftId='" + ShiftId + "',@SiteId='" + SiteId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "'");
		//	if (ds.Tables[0].Rows.Count != 0)
		//	{
		//		return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
		//	}
		//	else
		//	{
		//		return NotFound(new { Massage = "Beat Not Found", status = HttpStatusCode.NotFound });

		//	}
		//}






		//#region GetCustomer

		//[HttpGet("GetCustomer")]
		//public IActionResult GetCustomer(string EmployeeId, string BranchId,string CompanyId)
		//{
		//    var ds = util.Fill("exec APP_Usp_GetCustomerByEmp  @EmpId='" + EmployeeId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "'");
		//    if (ds.Tables[0].Rows.Count != 0)
		//    {
		//        return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
		//    }
		//    else
		//    {
		//        return NotFound(new { Massage = "Not Found", status = HttpStatusCode.NotFound });             

		//    }

		//}

		//#endregion

		//#region GetSite

		//[HttpGet("GetSite")]
		//public IActionResult GetSite(string EmployeeId,string CustomerId, string BranchId, string CompanyId)
		//{
		//    var ds = util.Fill("exec APP_Usp_GetSiteByCustomer  @CustomerId='" + CustomerId+"',@EmpId='" + EmployeeId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "'");
		//    if (ds.Tables[0].Rows.Count != 0)
		//    {
		//        return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
		//    }
		//    else
		//    {
		//        return NotFound(new { Massage = "Not Found", status = HttpStatusCode.NotFound });

		//    }

		//}

		//#endregion

		#region GetRoutePost
		[HttpGet("GetRoutePost")]
		public IActionResult GetRoutePost(string EmployeeId, string RouteCode, string CompanyId, string BranchId)
		{
			var ds = util.Fill("exec API_Usp_GetPost @Action='All',@EmpId='" + EmployeeId + "',@RouteId='" + RouteCode + "',@CompanyId='" + CompanyId + "',@BranchId='" + BranchId + "'");
			if (ds.Tables[0].Rows.Count != 0)
			{
				return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
			}
			else
			{
				return NotFound(new { Massage = "Record Not Found", Status = 404, });

			}
		}
		#endregion

		#region ValidatePostId
		[HttpGet("ValidatePostId")]
		public IActionResult GetPost(string PostId, string RouteId, string EmployeeId, string CompanyId, string BranchId)
		{
			var ds = util.Fill("exec API_Usp_GetPost @Action='Single',@EmpId='" + EmployeeId + "',@RouteId='" + RouteId + "',@CompanyId='" + CompanyId + "',@BranchId='" + BranchId + "',@PostId='" + PostId + "'");

			if (ds.Tables[0].Rows.Count != 0)
			{
				return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
			}
			else
			{
				return NotFound(new
				{
					Status = HttpStatusCode.NotFound,
					Message = $"No post found with ID = {PostId}"
				});
			}
		}

		#endregion


		#region CheckGeoLocation

		[HttpGet("CheckGeoLocation")]
		public IActionResult CheckGeolocatoin(string PostId, float Latitude, float Longitude)
		{
			var ds = util.Fill("exec App_Usp_CheckGeoLoaction @PostId='" + PostId + "',@Latitude='" + Latitude + "',@Longitude='" + Longitude + "' ");
			if (ds.Tables[0].Rows.Count != 0)
			{
				return Ok(JsonConvert.SerializeObject(ds.Tables[0]));
			}
			else
			{
				return NotFound(new { Massage = "Not In Validate GeoLocation", StateCode = HttpStatusCode.NotFound });
			}
		}
		#endregion

		#region InsertTour

		[HttpPost("InsertTour")]
		public IActionResult InsertTour([FromForm] InsertTour obj)
		{
			try

			{
				Byte[] bs;
                using (var memoryStream = new MemoryStream())
                {
                     obj.Image.CopyTo(memoryStream);
                    bs= memoryStream.ToArray();
                }

				string bs64 = Convert.ToBase64String(bs);

                string msg = util.execQuery(@$"exec App_Usp_InsertTour @CompanyId='{obj.CompanyId}',@BranchId='{obj.BranchId}',@SiteId='{obj.SiteId}',@RouteCode='{obj.RouteCode}',@PostId='{obj.PostId}',@CustomerId='{obj.CustomerId}',@EmmployeeId='{obj.EmployeeId}',@remark='{obj.Remark}',@Image='{bs64}',@shiftid='{obj.ShiftId}',@beatid='{obj.BeatId}',@Latitude='{obj.Latitude}',@Longitude='{obj.Longitude}',@LocationName='{obj.LocationName}'");
				if (msg == "Successfull")
					return Ok(new { Message = msg, StatusCode = HttpStatusCode.Created });
				else
					return BadRequest(new { Message = msg, StatusCode = HttpStatusCode.ExpectationFailed });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Message = ex.Message, StatusCode = HttpStatusCode.InternalServerError });
			}


		}


		#endregion





	}
}
