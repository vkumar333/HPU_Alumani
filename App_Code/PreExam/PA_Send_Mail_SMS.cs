using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using DataAccessLayer;
using System.Net;
using System.IO;
using System.Text;

namespace CommonForms
{
    public class PA_Send_Mail_SMS
    {
        DataAccess DAobj = new DataAccess();
        ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
      public  enum EmailSentFor
        {
            RegistrationConfirmation,
            OnlinePaymentConfirmation,
            ChallanRefNoApproval,
            ForgetUserIdOrPwd

        }
        #region Common Function
        void clearall()
        {
            names.Clear();
            values.Clear();
            types.Clear();
        }
        #endregion
        #region  Method

        //FUNCTION WILL BE CALLED WHEN THE PAYMENT WILL BE VERIFIED AND APPROVED
        public void SendSMS_Pymt(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToBoolean(dr["IsVerified"]) == false)
                {
                    string message = "Dear " + dr["s_name"].ToString().Split(' ')[0] + ", ";
                    message += "Reg No. : " + dr["regno"].ToString() + " ";
                    message += dr["reason"].ToString();
                    message += Environment.NewLine;
                    message += "HPU";
                    sendSingleSMS(dr["mobileno"].ToString(), message);
                }
            }
        }

        public void SendSMS(DataTable dt)//FUNCTION WILL BE CALLED WHEN THE STUDENT DOCUMENT WILL BE VERIFIED AND APPROVED
        {
            foreach (DataRow dr in dt.Rows)
            {
                string message = "Dear " + dr["s_name"].ToString().Split(' ')[0] + ", ";
                message += "Reg No. : " + dr["regno"].ToString() + " ";
                message += dr["reason"].ToString();
                message += Environment.NewLine;
                message += "HPU";

                sendSingleSMS(dr["mobileno"].ToString(), message);
            }
        }

        public string sendSingleSMS(String mobileNo, String message)
        {
            Stream dataStream;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://msdgweb.mgov.gov.in/esms/sendsmsrequest");
            request.ProtocolVersion = HttpVersion.Version10;
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
            request.Method = "POST";

            String smsservicetype = "bulkmsg";
            String query = "username=" + HttpUtility.UrlEncode("hpgovt-hpu") +
                "&password=" + HttpUtility.UrlEncode("shimlahpu@123") +
                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                "&content=" + HttpUtility.UrlEncode(message) +
                "&bulkmobno=" + HttpUtility.UrlEncode(mobileNo) +
                "&senderid=" + HttpUtility.UrlEncode("hpgovt");
            byte[] byteArray = Encoding.ASCII.GetBytes(query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            String Status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            return Status;
        }

        #endregion




    }
}