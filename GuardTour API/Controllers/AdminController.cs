using GuardTour_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Design;
using System.Data;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
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

        public IActionResult ForgetPassword([FromForm] ForgetPassword obj)
        {
            try
            {
                //string ecnpwd = EncryptionHelper.Encrypt(obj.ConfirmPassword);
                //string oldpwden = EncryptionHelper.Encrypt(obj.OldPassword);


                var ds = util.Fill("exec Udp_Forgetpassword @username='" + obj.EmployeeCode.Trim() + "',@oldpwd='" + obj.OldPassword.Trim() + "',@confpwd='" + obj.ConfirmPassword.Trim() + "'");
                string errmsg = ds.Tables[0].Rows[0][1].ToString();
                if (errmsg == "Password Updated SuccessFully")
                {
                    return Content(JsonConvert.SerializeObject(new[] { new {
                        Message = errmsg,
                        Status = "Success"
                    } }), "application/json");


                }

                else

                {
                    return Content(JsonConvert.SerializeObject(new[] { new {
                        Message = errmsg,
                        Status = "Error"
                    } }), "application/json");


                }
            }



            catch (Exception ex)
            {


                return Content(JsonConvert.SerializeObject(new[] { new {
                        Message = "Failure",
                        Status = "Error"
                    } }), "application/json");


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

                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message
                    , Status = "Error" } }), "application/json");
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
                return Content(JsonConvert.SerializeObject(new[] { new { Message = "Task Not Found", Status = "Error" } }), "application/json");

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
                return Content(JsonConvert.SerializeObject(new[] { new { Message = "Task Not Found", Status = "Error" } }), "application/json");

            }

        }
        #endregion



        #region GetRoutePost
        [HttpGet("GetRoutePost")]
        public IActionResult GetRoutePost(string CompanyId, string BranchId,string EmployeeId,string CustomerId, string RouteCode,string SiteId, string ShiftId,string BeatId)
        {
            try
            {
                var ds = util.Fill("exec API_Usp_GetPost @EmpId='" + EmployeeId + "',@RouteCode='" + RouteCode + "',@CompanyId='" + CompanyId + "',@BranchId='" + BranchId + "',@BeatId='" + BeatId+ "',@CustomerId='"+CustomerId+ "',@SiteId='"+SiteId+ "',@ShiftId='"+ ShiftId + "'");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new[] { new { Message = "Post Not Found", Status = "Error" } }), "applicatin/json");

                }
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message
                    , Status = "Error" } }), "application/json");
            }

        }
        #endregion

        //#region ValidatePostId
        //[HttpGet("ValidatePostId")]
        //public IActionResult GetPost(string PostCode, string RouteId, string EmployeeId, string CompanyId, string BranchId)
        //{
        //	var ds = util.Fill("exec API_Usp_GetPost @Action='Single',@EmpId='" + EmployeeId + "',@RouteId='" + RouteId + "',@CompanyId='" + CompanyId + "',@BranchId='" + BranchId + "',@PostId='" + PostCode + "'");

        //	if (ds.Tables[0].Rows.Count != 0)
        //	{
        //		return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
        //	}
        //	else
        //	{
        //		return NotFound(new[]
        //		{

        //		new 
        //		{
        //			Status = HttpStatusCode.NotFound,
        //			Message = $"No post found with ID = {PostCode}"
        //		}});
        //	}
        //}

        //#endregion


        #region CheckGeoLocation

        [HttpGet("CheckGeoLocation")]
        public IActionResult CheckGeolocatoin(string PostId, float Latitude, float Longitude,string StartTime ,string EndTime,string BeatId)
        {
            try
            {


                var ds = util.Fill("exec App_Usp_CheckGeoLoaction @PostId='" + PostId + "',@Latitude='" + Latitude + "',@Longitude='" + Longitude + "',@StartTime='"+StartTime+ "',@EndTime='"+EndTime+ "',@BeatId='"+BeatId+"' ");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    return Content(JsonConvert.SerializeObject(ds.Tables[0]), "applicatin/json");
                }
                else
                {

                    return Content(JsonConvert.SerializeObject(new[] { new { Message = "Not In Validate GeoLocation", Status = "Error" } }), "applicatin/json");
                }
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message
                    , Status = "Error" } }), "application/json");
            }
        }
        #endregion

//        #region EmergencyAlert

   
//        public string EmergencyAlert(DataTable dt)
//        {
//            try

//            {

                
//                string msg = "";



           



//                        ClsUtility clsUtility = new ClsUtility();
//                        var row = dt.Rows[0];
//                        string body = $@"


//<div style=""font-family: Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #e74c3c; border-radius: 8px; padding: 20px; background-color: #fff;"">
//    <h2 style=""color: #e74c3c; text-align: center;"">🚨 SOS Alert Triggered</h2>
    
//    <p style=""font-size: 16px; color: #333;"">
//        We have received an <strong>SOS</strong> with the following details:
//    </p>

//    <table style=""width: 100%; border-collapse: collapse; font-size: 15px; margin-top: 15px;"">
//        <tr style=""background-color: #f2f2f2;"">
//            <td style=""padding: 10px; font-weight: bold; width: 30%;"">Customer</td>
//            <td style=""padding: 10px;"">{row["CustomerName"]}</td>
//        </tr>
//        <tr>
//            <td style=""padding: 10px; font-weight: bold;"">Site</td>
//            <td style=""padding: 10px;"">{row["SitName"]}</td>
//        </tr>
//        <tr style=""background-color: #f2f2f2;"">
//            <td style=""padding: 10px; font-weight: bold;"">Route</td>
//            <td style=""padding: 10px;"">{row["RouteCode"]}</td>
//        </tr>
//        <tr>
//            <td style=""padding: 10px; font-weight: bold;"">Post</td>
//            <td style=""padding: 10px;"">{row["PostName"]}</td>
//        </tr>
//        <tr style=""background-color: #f2f2f2;"">
//            <td style=""padding: 10px; font-weight: bold;"">Shift</td>
//            <td style=""padding: 10px;"">{row["Shift"]}</td>
//        </tr>
// <tr>
//            <td style=""padding: 10px; font-weight: bold;"">Frequency Time</td>
//            <td style=""padding: 10px;"">{row["RoundTime"]}</td>
//        </tr>
// <tr style=""background-color: #f2f2f2;"">
//            <td style=""padding: 10px; font-weight: bold;"">Date</td>
//            <td style=""padding: 10px;"">{row["Date"]}</td>
//        </tr>
//    </table>

//    <p style=""margin-top: 20px; font-size: 14px; color: #666;"">
//        Please take immediate action as necessary. <br>
        
//    </p>
//</div>

//";
//                         msg = clsUtility.SendMailViaIIS_html("v19softech@gmail.com", "Nadeemali.bsd@gmail.com", "", "", "SOS Triggered: Urgent Help Requested", "", body, null, "hxezpeqrunircjhe", "smtp.gmail.com", "");
                    
//                return msg;

                 


                  
              
                 


//            }
//            catch (Exception ex)
//            {
//                return ex.Message ;
//            }


//        }


//        #endregion


        #region InsertTour

        [HttpPost("InsertTour")]
        public IActionResult InsertTour([FromForm] InsertTour obj)
        {
            try

            {


                //Byte[] bs;
                //using (var memoryStream = new MemoryStream())
                //{
                //	obj.Image.CopyTo(memoryStream);
                //	bs = memoryStream.ToArray();
                //}

              

                    Byte[] bs;


                    if (obj.Image != null)
                    {
                        bs = Convert.FromBase64String(obj.Image);
                    }
                    else
                    {
                        bs = null;

                    }



                    var ds = util.Fill(@$"exec App_Usp_InsertTour @CompanyId='{obj.CompanyId}',@BranchId='{obj.BranchId}',@SiteId='{obj.SiteId}',@RouteCode='{obj.RouteCode}',@PostId='{obj.PostId}',@CustomerId='{obj.CustomerId}',@EmmployeeId='{obj.EmployeeId}',@remark='{obj.Remark}',@Image='{obj.Image}',@shiftid='{obj.ShiftId}',@BeatId='{obj.BeatId}',@Latitude='{obj.Latitude}',@Longitude='{obj.Longitude}',@LocationName='{obj.LocationName}',@AlertFlg='{obj.AlertFlg}'");

                if (ds.Tables.Count > 0)
                {
                    if (obj.AlertFlg == 1)
                    {
                        ClsUtility clsUtility = new ClsUtility();

                        var row = ds.Tables[0].Rows[0];
                        string body = $@"


<div style=""font-family: Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #e74c3c; border-radius: 8px; padding: 20px; background-color: #fff;"">
    <h2 style=""color: #e74c3c; text-align: center;"">🚨 SOS Alert Triggered</h2>
    
    <p style=""font-size: 16px; color: #333;"">
        We have received an <strong>SOS</strong> with the following details:
    </p>

    <table style=""width: 100%; border-collapse: collapse; font-size: 15px; margin-top: 15px;"">
        <tr style=""background-color: #f2f2f2;"">
            <td style=""padding: 10px; font-weight: bold; width: 30%;"">Customer</td>
            <td style=""padding: 10px;"">{row["CustomerName"]}</td>
        </tr>
        <tr>
            <td style=""padding: 10px; font-weight: bold;"">Site</td>
            <td style=""padding: 10px;"">{row["SitName"]}</td>
        </tr>
        <tr style=""background-color: #f2f2f2;"">
            <td style=""padding: 10px; font-weight: bold;"">Route</td>
            <td style=""padding: 10px;"">{row["RouteCode"]}</td>
        </tr>
        <tr>
            <td style=""padding: 10px; font-weight: bold;"">Post</td>
            <td style=""padding: 10px;"">{row["PostName"]}</td>
        </tr>
        <tr style=""background-color: #f2f2f2;"">
            <td style=""padding: 10px; font-weight: bold;"">Shift</td>
            <td style=""padding: 10px;"">{row["Shift"]}</td>
        </tr>
 <tr>
            <td style=""padding: 10px; font-weight: bold;"">Frequency Time</td>
            <td style=""padding: 10px;"">{row["RoundTime"]}</td>
        </tr>
 <tr style=""background-color: #f2f2f2;"">
            <td style=""padding: 10px; font-weight: bold;"">Date</td>
            <td style=""padding: 10px;"">{row["Date"]}</td>
        </tr>
 <tr style="""">
            <td style=""padding: 10px; font-weight: bold;"">Remark</td>
            <td style=""padding: 10px;"">{row["Remark"]}</td>
        </tr>
    </table>

    <p style=""margin-top: 20px; font-size: 14px; color: #666;"">
        Please take immediate action as necessary. <br>
        
    </p>
</div>

";
                       string msg = clsUtility.SendMailViaIIS_html("v19softech@gmail.com", "pradeep.yadav@ifm360.in", "", "", "SOS Triggered: Urgent Help Requested", "", body, null, "hxezpeqrunircjhe", "smtp.gmail.com", "");


                        if (msg == "Sent")
                        {


                            return Content(JsonConvert.SerializeObject(new[] { new { Message = "Alert " + msg + " SuccessFully", Status = "Success" } }), "application/json");
                        }
                      
                    }

                }

              
                    return Content(JsonConvert.SerializeObject(new[] { new { Message = "Submit SuccessFully", Status = "Success" } }), "application/json");

                

            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message, Status = "Error" } }), "application/json");
            }


        }


        #endregion





    }
}
