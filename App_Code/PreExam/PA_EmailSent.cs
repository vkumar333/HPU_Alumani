using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using DataAccessLayer;
using System.Collections;
/// <summary>
/// Summary description for PA_EmailSent
/// </summary>
/// 
namespace PA_Email
{
    public class PA_EmailSent
    {
        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
        ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();
        int _ProgrammeId;

        long _PK_RegId;




        #region "Properties"
        public int ProgrammeId
        {
            get { return _ProgrammeId; }
            set { _ProgrammeId = value; }
        }
        public long PK_RegId
        {
            get { return _PK_RegId; }
            set { _PK_RegId = value; }
        }
        #endregion
        public PA_EmailSent()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds">passs here dataset of table[0] which contain sent from email dtls and table[1] should contain sent to email details</param>
        /// <param name="body">Message body</param>
        /// <param name="Pk_Regid">student Reg Id</param>
        /// <returns> 0 when no email sent else return 1 when email is sent</returns>
        public static int Send_Mails(DataSet ds, string subject, string body, long Pk_Regid)
        {
            int status = 0;
            try
            {
                #region "Select From Email Id and To Email Id details"

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {

                    //get the from email details here
                    DataTable dtFromEmail = ds.Tables[0];
                    DataTable dt_Tomail = ds.Tables[1];
                    string Body = "";
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    mail.IsBodyHtml = true;
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(dtFromEmail.Rows[0]["EmailId"].ToString());
                    foreach (DataRow dr in dt_Tomail.Rows)
                    {
                        if (dr["email"] != "")
                        {
                            mail.To.Add(new MailAddress(dr["email"].ToString()));
                        }
                    }
                    mail.Subject = subject.Trim();
                    mail.Body = body.Trim();
                    SmtpServer.Port = Convert.ToInt32(dtFromEmail.Rows[0]["port"].ToString());
                    SmtpServer.Credentials = new System.Net.NetworkCredential(dtFromEmail.Rows[0]["EmailId"].ToString(), dtFromEmail.Rows[0]["Pwd"].ToString());
                    SmtpServer.EnableSsl = true;
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain,
                        SslPolicyErrors sslPolicyErrors)
                    { return true; };
                    SmtpServer.Send(mail);
                    status = 1;
                    return status;
                }
                return status;
                #endregion
            }
            catch
            {
                status = 0;
                return status;
            }
        }

        #region Data Access Layer Code==================
        public DataSet SelectEmailToSent(long Pk_Regid)
        {

            Clear();
            names.Add("@Pk_Regid"); types.Add(SqlDbType.BigInt); values.Add(Pk_Regid);
            return DAobj.GetDataSet("HPU_EXAM_GetEmailIdAnd_Student_Dtl_sel", values, names, types);
        }
        /// <summary>
        /// To get the Short Name of programme name use 'DegreeCode' column and for full name of programme use 'Description' column
        /// </summary>
        /// <param name="ProgrammeId"></param>
        /// <returns></returns>
        public DataSet SelectProgrammeName(int ProgrammeId)
        {

            Clear();
            names.Add("@pk_ProgrammeId"); types.Add(SqlDbType.Int); values.Add(ProgrammeId);
            return DAobj.GetDataSet("HPU_EXAM_ProgrammeName_sel", values, names, types);
        }

        public int UpdateEmailSentDtl(int Pk_EmailId, long pk_Regid, string EmailSentFor, ref string Message)
        {
            Clear();

            names.Add("@Pk_EmailId"); values.Add(Pk_EmailId); types.Add(SqlDbType.VarChar);
            names.Add("@pk_Regid"); values.Add(pk_Regid); types.Add(SqlDbType.VarChar);
            names.Add("@EmailSentFor"); values.Add(EmailSentFor); types.Add(SqlDbType.VarChar);

            if (DAobj.ExecuteTransactionMsg("HPU_EXAM_EmailCounter_Ins", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }


        #region "Common Method"
        void Clear()
        {
            names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
        }
        #endregion

        #endregion
    }
}