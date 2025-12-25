using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace GuardTour_API.Models
{
    public class ServicesOpration : IServices
    {
        public DataSet Fill(string Query)
        {
            DataSet ds = new DataSet();
            
            using (SqlConnection sqcon = new SqlConnection(ConnectDB.ConnectionString))
            {
                try
                {
                    SqlCommand sqcmd = new SqlCommand(Query, sqcon);
                    sqcmd.CommandTimeout = 0;
                    SqlDataAdapter SqlDa = new SqlDataAdapter(sqcmd);
                    SqlDa.Fill(ds);
                }
                catch (Exception exce)
                {
                    DataSet dset = new DataSet();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Message");
                    dt.Columns.Add("Status");

                    DataRow dr = dt.NewRow();

                    dr["Message"] = "Failure";
                    dr["Status"] = "Error";



                    dt.Rows.Add(dr);
                    dset.Tables.Add(dt);


                    return dset;

                }
            }
            return ds;
        }




        public string SendEmall(string from, string to, string cc, string bcc, string subject, string attach, string _body, IConfiguration iConfig, string MAIL_PASSWORD, string Host, string attachPath = "")
        {
            //create the mail message
            string functionReturnValue = null;
            string _from = from, _to = to, _cc = cc, _bcc = bcc, _subject = subject;
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                //set the addresses

                mail.From = new System.Net.Mail.MailAddress(_from);

                if (_to.Trim().Length > 0)
                {
                    mail.To.Add(new System.Net.Mail.MailAddress(_to));
                }
                if (_cc.Trim().Length > 0)
                {
                    mail.CC.Add(new System.Net.Mail.MailAddress(_cc));
                }
                if (bcc.Trim().Length > 0 & bcc.Trim() != "none")
                {
                    mail.Bcc.Add(new System.Net.Mail.MailAddress(_bcc));
                }
                else if (bcc.Trim().Length == 0 & bcc.Trim() != "none")
                {
                    //mail.Bcc.Add(New system.net.mail.mailaddress("support@indiastat.com"))
                    //mail.Bcc.Add(New system.net.mail.mailaddress("diplnd07@gmail.com"))
                }

                if (!string.IsNullOrEmpty(attachPath))
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachPath);

                    mail.Attachments.Add(attachment);

                }
                mail.Subject = _subject;
                mail.Body = _body;
                mail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient SmtpClient = new System.Net.Mail.SmtpClient();

                SmtpClient.Host = Host;
                SmtpClient.Credentials = new NetworkCredential(_from, MAIL_PASSWORD);
                SmtpClient.Port = 587;
                SmtpClient.EnableSsl = true;
                SmtpClient.Send(mail);

                functionReturnValue = "Sent";
                mail.Dispose();
                SmtpClient = null;
            }
            catch (System.FormatException ex)
            {
                functionReturnValue = ex.Message;
            }
            catch (SmtpException ex)
            {
                functionReturnValue = ex.Message;
            }
            catch (System.Exception ex)
            {
                functionReturnValue = ex.Message;
            }
            return functionReturnValue;
        }

    


       public void WriteLogFile(string LogPath, string Query, string Button, string Page, string IP, string BrowserName, string BrowerVersion, string javascript, string function)
        {
            try
            {

                if (!string.IsNullOrEmpty(Query))
                {
                    string path = Path.Combine("APILOG/" + LogPath + "/" + System.DateTime.UtcNow.ToString("dd-MM-yyyy") + ".txt");

                    if (!File.Exists(path))
                    {
                        File.Create(path).Dispose();

                        using (System.IO.FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write))
                        {

                            StreamWriter streamWriter = new StreamWriter(file);

                            streamWriter.WriteLine((((((((System.DateTime.Now + " - ") + Query + " - ") + Button + " - ") + Page + " - ") + IP + " - ") + BrowserName + " - ") + BrowerVersion + " - ") + javascript + function);

                            streamWriter.Close();

                        }
                    }
                    else
                    {
                        using (System.IO.FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write))
                        {

                            StreamWriter streamWriter = new StreamWriter(file);

                            streamWriter.WriteLine((((((((System.DateTime.Now + " - ") + Query + " - ") + Button + " - ") + Page + " - ") + IP + " - ") + BrowserName + " - ") + BrowerVersion + " - ") + javascript + function);

                            streamWriter.Close();

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
