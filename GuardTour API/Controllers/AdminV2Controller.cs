using GuardTour_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace GuardTour_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminV2Controller : ControllerBase
    {

       private readonly IServices _db;
        public AdminV2Controller(IServices Services)
        {
            this._db = Services;
        }


        #region Department      
        [HttpGet("Department")]
        public IActionResult Department(int CustomerId, int SiteId)
        {

            try
            {
                var ds = _db.Fill(@$"exec [APP_Usp_Department] @CustomerId='{CustomerId}',@SiteId='{SiteId}'");


                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Content(JsonConvert.SerializeObject(ds.Tables[0]), "application/json");
                }
                return Content(JsonConvert.SerializeObject(new[] { new { Message = "No Department Found", Status = "Error" } }), "application/json");


            }
            catch (Exception ex)
            {

                return Content(JsonConvert.SerializeObject(new[] { new { Message = ex.Message
                    , Status = "Error" } }), "application/json");
            }
        }

        #endregion


        #region InsertTour

        [HttpPost]
        public IActionResult InsertTour([FromForm] InsertTourV2 obj)
        {
            try
            {
                DataTable dtImages = new DataTable();
                dtImages.Columns.Add("ImagePath", typeof(string));

               

                    foreach (string img in obj.CheckPointImages)
                    {
                        dtImages.Rows.Add(img);
                    }
               
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(ConnectDB.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("App_Usp_InsertTournew", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                    cmd.Parameters.AddWithValue("@BranchId", obj.BranchId);
                    cmd.Parameters.AddWithValue("@SiteId", obj.SiteId);
                    cmd.Parameters.AddWithValue("@RouteCode", obj.RouteCode);
                    cmd.Parameters.AddWithValue("@PostId", obj.PostId);
                    cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
                    cmd.Parameters.AddWithValue("@EmmployeeId", obj.EmployeeId);
                    cmd.Parameters.AddWithValue("@Remark", obj.Remark);

                    SqlParameter imgParam = cmd.Parameters.AddWithValue("@Imagelist", SqlDbType.Structured);
                    imgParam.Value = dtImages;
                    imgParam.TypeName = "ImageList";

                    cmd.Parameters.AddWithValue("@Audio", obj.Audio);
                    cmd.Parameters.AddWithValue("@ShiftId", obj.ShiftId);
                    cmd.Parameters.AddWithValue("@BeatId", obj.BeatId);
                    cmd.Parameters.AddWithValue("@Latitude", obj.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", obj.Longitude);
                    cmd.Parameters.AddWithValue("@LocationName", obj.LocationName);
                    cmd.Parameters.AddWithValue("@AlertFlg", obj.AlertFlg);
                    cmd.Parameters.AddWithValue("@Image", obj.Image);
                    cmd.Parameters.AddWithValue("@Image2", obj.Image2);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(ds);
                }
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
                        string msg = _db.SendEmall("v19softech@gmail.com", "pradeep.yadav@ifm360.in", "", "", "Observation Triggered: Urgent Help Requested", "", body, null, "hxezpeqrunircjhe", "smtp.gmail.com", "");


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

    }
}
