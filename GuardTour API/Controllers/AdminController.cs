using GuardTour_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
namespace GuardTour_API.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
       readonly IServices _db;
        public AdminController(IServices Services)
        {
            this._db = Services;
        }

        ResponseMassage Response;

        #region ForgetPassword

        [HttpPost("ForgetPassword")]

        public IActionResult ForgetPassword([FromForm] ForgetPassword obj)
        {
            try
            {
                //string ecnpwd = EncryptionHelper.Encrypt(obj.ConfirmPassword);
                //string oldpwden = EncryptionHelper.Encrypt(obj.OldPassword);


                var ds = _db.Fill("exec Udp_Forgetpassword @username='" + obj.EmployeeCode.Trim() + "',@oldpwd='" + obj.OldPassword.Trim() + "',@confpwd='" + obj.ConfirmPassword.Trim() + "'");
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
                var ds = _db.Fill("exec EmployeeLogin @EmpId='" + EmployeeId.Trim() + "',@password='" + EmployeePassword.Trim() + "'");
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

        #region Customer      
        [HttpGet("Customer")]
        public IActionResult Customer(string EmployeeId, string CompanyId, string BranchId)
        {

            try
            {
                var ds = _db.Fill(@$"exec APP_Usp_GetCustomerByEmp @EmpId='{EmployeeId.Trim()}',@CompanyId='{CompanyId}',@BranchId='{BranchId}'");
                var data = ds.Tables[0];
                if (data.Rows.Count == 0)
                {
                    return Content(JsonConvert.SerializeObject(new[] { new { Message = "No Customer Found", Status = "Error" } }), "application/json");
                }

                return Content(JsonConvert.SerializeObject(data), "application/json");

            }
            catch (Exception ex)
            {

                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message
                    , Status = "Error" } }), "application/json");
            }
        }
        #endregion

        #region CustomerFLG      
        [HttpGet("CustomerFLG")]
        public IActionResult CustomerFLG(string CustomerId)
        {

            try
            {
                var ds = _db.Fill(@$"exec [APP_Usp_CustomerFlg] @CustomerId='{CustomerId}'");
                var data = ds.Tables[0];
                if (data.Rows.Count == 0)
                {
                    return Content(JsonConvert.SerializeObject(new[] { new { Message = "No CustomerFLG  Found", Status = "Error" } }), "application/json");
                }

                return Content(JsonConvert.SerializeObject(data), "application/json");

            }
            catch (Exception ex)
            {

                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message
                    , Status = "Error" } }), "application/json");
            }
        }

        #endregion

        #region Site      
        [HttpGet("Site")]
        public IActionResult Site(string EmployeeId, string CustomerId, string CompanyId, string BranchId)
        {

            try
            {
                var ds = _db.Fill(@$"exec APP_Usp_GetSiteByCustomer @EmpId='{EmployeeId}',@CustomerId='{CustomerId}',@CompanyId='{CompanyId}',@BranchId='{BranchId}'");
                var data = ds.Tables[0];
                if (data.Rows.Count == 0)
                {
                    return Content(JsonConvert.SerializeObject(new[] { new { Message = "No Site Found", Status = "Error" } }), "application/json");
                }

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
        public IActionResult GetTaskdetails(string EmployeeId, string CustomerId, string SiteId, string BranchId, string CompanyId)
        {
            var ds = _db.Fill("exec API_Usp_GetTaskdetails @EmpId='" + EmployeeId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "',@CustomerId='" + CustomerId + "',@SiteId='" + SiteId + "'");
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
        public IActionResult GetTaskdetailsCompleted(string EmployeeId, string CustomerId, string SiteId, string BranchId, string CompanyId)
        {
            var ds = _db.Fill("exec [API_Usp_GetTaskdetailsCompleted] @EmpId='" + EmployeeId + "',@BranchId='" + BranchId + "',@CompanyId='" + CompanyId + "',@CustomerId='" + CustomerId + "',@SiteId='" + SiteId + "'");
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
        public IActionResult GetRoutePost(string CompanyId, string BranchId, string EmployeeId, string CustomerId, string RouteCode, string SiteId, string ShiftId, string BeatId)
        {
            try
            {
                var ds = _db.Fill("exec API_Usp_GetPost @EmpId='" + EmployeeId + "',@RouteCode='" + RouteCode + "',@CompanyId='" + CompanyId + "',@BranchId='" + BranchId + "',@BeatId='" + BeatId + "',@CustomerId='" + CustomerId + "',@SiteId='" + SiteId + "',@ShiftId='" + ShiftId + "'");
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

        #region CheckGeoLocation

        [HttpGet("CheckGeoLocation")]
        public IActionResult CheckGeolocatoin(string PostId,string QRValue, float Latitude, float Longitude, string StartTime, string EndTime, string BeatId)
        {
            try
            {


                var ds = _db.Fill("exec App_Usp_CheckGeoLoaction @PostId='" + PostId + "',@Latitude='" + Latitude + "',@Longitude='" + Longitude + "',@StartTime='" + StartTime + "',@EndTime='" + EndTime + "',@BeatId='" + BeatId + "',@QRValue='"+ QRValue + "' ");
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

        #region InsertTour

        [HttpPost("InsertTour")]
        public IActionResult InsertTour([FromForm] InsertTour obj)
        {
            try
            {



                var ds = _db.Fill(@$"exec App_Usp_InsertTour @CompanyId='{obj.CompanyId}',@BranchId='{obj.BranchId}',@SiteId='{obj.SiteId}',@RouteCode='{obj.RouteCode}',@PostId='{obj.PostId}',@CustomerId='{obj.CustomerId}',@EmmployeeId='{obj.EmployeeId}',@remark='{obj.Remark}',@Image='{obj.Image}',@Image2='{obj.Image2}',@Audio='{obj.Audio}',@shiftid='{obj.ShiftId}',@BeatId='{obj.BeatId}',@Latitude='{obj.Latitude}',@Longitude='{obj.Longitude}',@LocationName='{obj.LocationName}',@AlertFlg='{obj.AlertFlg}'");
                var obj1 = obj;
                obj1.Image = "";
                if (obj.AlertFlg == 0)
                {
                    var jsondata = JsonConvert.SerializeObject(obj1);
                    var data1 = JsonConvert.SerializeObject(ds.Tables[0]);
                    _db.WriteLogFile("Apilog", jsondata, "", "", "", "", "", "", data1);
                }

                if (obj.AlertFlg == 1)
                {
                    if (ds.Tables.Count > 0)
                    {

                        var cometodb = JsonConvert.SerializeObject(ds.Tables[0]);
                        _db.WriteLogFile("Apilog", cometodb, "", "", "", "", "", "", "Alert Flag");
                        var row = ds.Tables[0].Rows[0];
                        string body = $@"


                                    <div style=""font-family: Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #e74c3c; border-radius: 8px; padding: 20px; background-color: #fff;"">
                                        <h2 style=""color: #e74c3c; text-align: center;""> Observation Report </h2>
    
                                        <p style=""font-size: 16px; color: #333;"">
                                            We have received an <strong>Observation</strong> with the following details:
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
                        string msg = _db.SendEmall("v19softech@gmail.com", "chanchalk@luluindia.com", "pradeep.yadav@ifm360.in", "", "Observation Triggered: Urgent Help Requested", "", body, null, "hxezpeqrunircjhe", "smtp.gmail.com", "");


                        if (msg == "Sent")
                        {
                            obj.Image = "";
                            string datacome = JsonConvert.SerializeObject(obj);
                            _db.WriteLogFile("Apilog", datacome, "", "", "", "", "", "", "Email Sent SuccessFully");

                            return Content(JsonConvert.SerializeObject(new[] { new { Message = "Report Observation SuccessFully", Status = "Success" } }), "application/json");
                        }

                    }


                }

                return Content(JsonConvert.SerializeObject(new[] { new { Message = "Submit SuccessFully", Status = "Success" } }), "application/json");







            }
            catch (Exception ex)
            {
                _db.WriteLogFile("Errorlog", ex.Message, "", "", "", "", "", "", "Error");
                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message, Status = "Error" } }), "application/json");
            }


        }


        #endregion


        #region SOS

        [HttpGet("SOS")]
        public IActionResult SOS(string EmployeeId, string CompanyId, string BranchId)
        {


            var ds = _db.Fill(@$"exec App_Usp_EmergencyAlert @EmployeeId='{EmployeeId}',@CompanyId='{CompanyId}',@BranchId='{BranchId}'");

            return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");




        }
        #endregion

        #region ReportIncident

        [HttpPost("ReportIncident")]
        public IActionResult ReportIncident([FromForm] ReportIncident obj)
        {
            try
            {
                var ds = _db.Fill(@$"exec [App_Usp_ReportIncident] @EmployeeId='{obj.EmployeeId}',@CompanyId='{obj.CompanyId}',@BranchId='{obj.BranchId}',@CustomerId='{obj.CustomerId}',@SiteId='{obj.SiteId}',@Remark=N'{obj.Remark}',@ImageFile=N'{obj.Image}',@Audio='{obj.Audio}'
");
                return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");

            }
            catch (Exception ex)
            {
                _db.WriteLogFile("Errorlog", ex.Message, "", "", "", "", "", "", "Error");
                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message, Status = "Error" } }), "application/json");
            }


        }
        #endregion
    }
}
