using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data;
using System.Configuration;
/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail_class
{
    static string _smtpServer, _from, _smtpPass;
    static int _smtpPort;
    static bool _EnableSsl;

    static string _emailto, _subject, _body;

    public static string Emailto
    {
        get { return _emailto; }
        set { _emailto = value; }
    }
    public static string Subject
    {
        get { return _subject; }
        set { _subject = value; }
    }
    public static string Body
    {
        get { return _body; }
        set { _body = value; }
    }
    public SendMail_class()
	{
		
	}
    /// <summary>
    /// Send Mail to multiple user Function when passing Datatable as a parameter
    /// </summary>
    /// <param name="dt_Tomail"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static int Send_Multiple_Mails( DataTable dt_Tomail, string subject,string body)
    {
        int status = 0;
        try
        {
            string Body = "";
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("exponlineapp2@gmail.com");


            foreach (DataRow dr in dt_Tomail.Rows)
            {
                if (dr["email"] != "")
                {
                    mail.To.Add(new MailAddress(dr["email"].ToString()));
                }
            }
            mail.Subject = subject.Trim();
            mail.Body = body.Trim();
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("exponlineapp2@gmail.com", "expedien@123");
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
            SmtpServer.Send(mail);
            status = 1;
            return status;
        }
        catch
        {
            status = 0;
            return status;
        }
    }
    
    /// <summary>
    /// Statis Constructor to initialize the static member
    /// variables for getting the mail configuration 
    /// settings
    /// </summary>
    static SendMail_class()
    {
        _smtpServer = ConfigurationManager.AppSettings["smtpServer"].ToString();
        _from = ConfigurationManager.AppSettings["smtpUser"].ToString();
        _smtpPass = ConfigurationManager.AppSettings["smtpPass"].ToString();
        _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
        _EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
    }
    /// <summary>This function is used to send mail to multiple users </summary>
    /// <param name="emailTo"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static int Send_Multiple_Mail()
    {
        int status = 0;
        try
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient(_smtpServer);
            mail.From = new MailAddress(_from);
            string[] emailarr = _emailto.Split(',');
            foreach (string email in emailarr)
            {
                if (email != "")
                {
                    mail.To.Add(new MailAddress(email));
                }
            }
            mail.Subject = _subject.Trim();
            mail.Body = _body.Trim();
            SmtpServer.Port = _smtpPort;
            SmtpServer.Credentials = new System.Net.NetworkCredential(_from, _smtpPass);
            SmtpServer.EnableSsl = _EnableSsl;
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
            SmtpServer.Send(mail);
            status = 1;
            return status;
        }
        catch
        {
            status = 0;
            return status;
        }
    }
}