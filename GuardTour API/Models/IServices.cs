using System.Data;

namespace GuardTour_API.Models
{
    public interface IServices
    {

       public DataSet Fill(string Query);

        string SendEmall(string from, string to, string cc, string bcc, string subject, string attach, string _body, IConfiguration iConfig, string MAIL_PASSWORD, string Host, string attachPath = "");

        public void WriteLogFile(string LogPath, string Query, string Button, string Page, string IP, string BrowserName, string BrowerVersion, string javascript, string function);

    }
}
