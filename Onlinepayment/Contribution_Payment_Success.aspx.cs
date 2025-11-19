using System;
using System.Web;
using System.Web.UI;
using System.Data;
using PA_Log;
using System.Security.Cryptography;
using System.Text;
using DataAccessLayer;
using System.Collections;
using System.IO;
using System.Configuration;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.Net.Mail;
using System.Net;
using System.Net.Security;

public partial class Onlinepayment_Contribution_Payment_Success : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    static string rgn;
    string IsAnnual;
    static int Collegeid;
    string examtype = "";
    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();
    DataTable dsmain = new DataTable();

    private void clear()
    {
        names.Clear(); values.Clear(); types.Clear();
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Get_Responce_CommingFrom_Gateway();
                //  string encdata= Request.Form.encdata;
                CheckTransactionStatusSBI();
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Alumni/Alm_FundDonationlist.aspx");
        }
        catch (Exception ex)
        {
            string Msg = "";
            //Insert_ExceptionLog(Session["regid"].ToString(), Session["DegreeType"].ToString(),
            //        ex, HttpContext.Current.Request.UserHostAddress.ToString(), ref Msg);
        }
    }

    //protected void bntSuccess_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        // Response.Redirect("~/CommonPGandUG/RME_Status_Page.aspx");
    //        Response.Redirect("~/CommonPGandUG/RME_Status_Page.aspx");
    //    }
    //    catch (Exception ex)
    //    {
    //        string Msg = "";
    //        //    PA_Login_And_Exception_Log.Insert_ExceptionLog(Session["regid"].ToString(), Session["DegreeType"].ToString(),
    //        //        ex, HttpContext.Current.Request.UserHostAddress.ToString(), ref Msg);
    //    }
    //}

    protected void btnPending_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Alumni/Alm_FundDonationlist.aspx");
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    protected void btnFailed_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Alumni/Alm_FundDonationlist.aspx");//("../Onlinepayment/ALM_Common_PaymentGateway.aspx");
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    #region "Check Responce coming from Which Gateway"

    //private void Get_Responce_CommingFrom_Gateway()// this will handle only responce of HPSCB bank
    //{
    //    try
    //    {
    //        CheckTransactionStatusHDFC();
    //    }
    //    catch (Exception ex)
    //    {
    //        // Response.Redirect("~/CommonPGandUG/RME_Status_Page.aspx");
    //        string Msg = "";
    //        return;
    //    }
    //}

    #endregion

    #region "Check The Responce from Gateway and Update the Info as per Gateway like HDFC,HPSCB and SBI gateway"

    private void CheckTransactionStatusHDFC()
    {
        try
        {
            string AllMsg = string.Empty;
            StringBuilder str = new StringBuilder();
            string[] merc_hash_vars_seq;
            string merc_hash_string = string.Empty;
            string merc_hash = string.Empty;
            string order_id = string.Empty;
            string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";

            merc_hash_vars_seq = hash_seq.Split('|');
            Array.Reverse(merc_hash_vars_seq);
            merc_hash_string = ConfigurationManager.AppSettings["SALT"] + "|" + Request.Form["status"];

            foreach (string merc_hash_var in merc_hash_vars_seq)
            {
                merc_hash_string += "|";
                merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");
            }

            //generate here hash to match the record
            merc_hash = Generatehash512(merc_hash_string).ToLower();
            if (merc_hash != Request.Form["hash"])
            {
                // Response.Write("Hash value did not matched");
                //here logout the user as responce is not correct..
                Response.Redirect("~/CommonPGandUG/RME_Status_Page.aspx");
                return;
            }
            else
            {
                StringBuilder strMsg = new StringBuilder();
                string HeaderMsg = string.Empty;
                string UniqueCode = string.Empty;
                string Msg = string.Empty;
                if (Request.Form["status"].ToString().ToUpper() == "SUCCESS")
                {
                    divSuccessPayment.Visible = true;
                    divPendingPayment.Visible = false;
                    divFailedPayment.Visible = false;
                    HeaderMsg = "Your Payment has been processed Successfully. <br />";
                }

                if (Request.Form["status"].ToString().ToUpper() == "FAILURE")
                {
                    divSuccessPayment.Visible = false;
                    divPendingPayment.Visible = false;
                    divFailedPayment.Visible = true;
                    HeaderMsg = "Your Payment has been Failed. <br />";
                }

                if (Request.Form["status"].ToString().ToUpper() == "PENDING")
                {
                    divSuccessPayment.Visible = false;
                    divPendingPayment.Visible = true;
                    divFailedPayment.Visible = false;
                    HeaderMsg = "Your Payment is Pending... <br />";
                }

                Session["pk_TransId"] = Request.Form["txnid"].ToString();

                if (Session["pk_TransId"].ToString() == Request.Form["txnid"].ToString())
                {
                    Pk_TranId = Session["pk_TransId"].ToString();
                    //PK_PurchaseId = Convert.ToInt64(Session["Pk_purchaseid_mls"].ToString());

                    IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                    PK_PaymentGatewayId = 1; // as responce is coming from hdfc gateway
                    DataSet dsmain = GetSchema("ALM_eCoupon_Purchase_Payment_Trn");
                    DataRow dr = dsmain.Tables[0].NewRow();
                    dr["mihpayid"] = Request.Form["mihpayid"].ToString();
                    dr["mode"] = Request.Form["mode"].ToString();
                    dr["status"] = Request.Form["status"].ToString();
                    dr["PG_TYPE"] = Request.Form["PG_TYPE"].ToString();
                    dr["bank_ref_num"] = Request.Form["bank_ref_num"].ToString();
                    dr["unmappedstatus"] = Request.Form["unmappedstatus"].ToString();
                    dr["Error"] = Request.Form["Error"].ToString();
                    dr["bankcode"] = Request.Form["bankcode"].ToString();
                    dr["StatusUpdatedDate"] = DateTime.Now.ToString();
                    //dr["fk_purchaseid"] = Convert.ToInt64(Session["Pk_purchaseid_mls"].ToString());
                    dsmain.Tables[0].Rows.Add(dr);
                    XmlDoc = dsmain.GetXml();
                    try
                    {
                        if (UpdateTransactionRecord(ref Msg) > 0)
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                strMsg.Append(HeaderMsg);
                strMsg.Append("Details are given below:<br />");
                strMsg.Append("Transcation id :-  " + Request.Form["txnid"].ToString() + "  <br />");
                strMsg.Append("Amount :-" + Request.Form["amount"].ToString() + "<br />");
                strMsg.Append("Date Time :- " + DateTime.Now.ToString() + "<br />");
                strMsg.Append("Transaction Status :-" + Request.Form["status"].ToString() + "<br />");

                //-----------------------To Show Message on Div------------------
                lblFailedMsg.Text = strMsg.ToString();
                lblPendingMsg.Text = strMsg.ToString();
                lblSuccessMsg.Text = strMsg.ToString();
                //----------------------End Here--------------------------------

                order_id = Request.Form["txnid"];
                str.Append("<b>" + "value matched --" + Request.Form["txnid"] + "</b>  <br />");
                str.Append("<b>" + "mihpayid --" + Request.Form["mihpayid"] + "</b>  <br />");
                str.Append("<b>" + "mode --" + Request.Form["mode"] + "</b>  <br />");
                str.Append("<b>" + "status--" + Request.Form["status"] + "</b>  <br />");
                str.Append("<b>" + "txnid--" + Request.Form["txnid"] + "</b>  <br />");
                str.Append("<b>" + "Firstname--" + Request.Form["Firstname"] + "</b>  <br />");
                str.Append("<b>" + "bankcode--" + Request.Form["bankcode"] + "</b>  <br />");
                str.Append("<b>" + "PG_TYPE--" + Request.Form["PG_TYPE"] + "</b>  <br />");
                str.Append("<b>" + "bank_ref_num--" + Request.Form["bank_ref_num"] + "</b>  <br />");
                str.Append("<b>" + "unmappedstatus--" + Request.Form["unmappedstatus"] + "</b>  <br />");
                str.Append("<b>" + "Error---" + Request.Form["Error"] + "</b>  <br />");
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
            PA_Login_And_Exception_Log.Insert_ExceptionLog(Session["regid"].ToString(), Session["DegreeType"].ToString(),
                ex, HttpContext.Current.Request.UserHostAddress.ToString(), ref Msg);
        }
    }

    #endregion

    #region "Work for HDFC payment Gateway"

    public string Generatehash512(string text)
    {
        byte[] message = Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    #endregion

    #region "Check The Responce from Gateway and Update the Info as per Gateway SBI  gateway"

    public void writemsg(string msg)
    {
        using (StreamWriter sw = new StreamWriter(Server.MapPath("LogList.txt"), true, Encoding.ASCII))
        {
            // string msg = "msg" + Msg + "line no - 1292";
            sw.WriteLine(msg);
        }
    }

    public void CheckTransactionStatusSBI()
    {
        try
        {
            string DecryptStr = "";
            DecryptStr = DecryptWithKey(Request.Form["encdata"], Server.MapPath("~/Key/HIMACHAL_UNIV.key"));

            string sbi_ref_no = "";
            string total_amt = "";
            string transaction_id = ""; //our pk_TranId is this
            string status = "";
            string status_desc = "";
            string checkSum = "";
            string[] ArrStr = DecryptStr.Split('|');

            #region "Get and Set here all Responce value of SBI"

            for (int i = 0; i < ArrStr.Length; i++)
            {
                if (i == 0) sbi_ref_no = Convert.ToString(ArrStr[i].ToString()).Replace("sbi_ref_no=", "").Trim();
                if (i == 1) total_amt = Convert.ToString(ArrStr[i]).Replace("total_amt=", "").Trim();
                if (i == 2) transaction_id = Convert.ToString(ArrStr[i]).Replace("transaction_id=", "").Trim();
                if (i == 3) status = Convert.ToString(ArrStr[i]).Replace("status=", "").Trim();
                if (i == 4) status_desc = Convert.ToString(ArrStr[i]).Replace("status_desc=", "").Trim();
                if (i == 5) checkSum = Convert.ToString(ArrStr[i]).Replace("checkSum=", "").Trim();
            }

            #endregion

            #region "Now Calculate checksum of Response and match with Responce Checksum"

            //Code to Check CheckSum
            string url_string = "";
            url_string = DecryptStr.Substring(0, DecryptStr.IndexOf("|checkSum"));
            //Response.Write("url_string = " + url_string);
            //byte[] byteArray = Encoding.UTF8.GetBytes(url_string);
            //MemoryStream stream = new MemoryStream(byteArray);
            string CalCulatedCheckSum = "";
            CalCulatedCheckSum = GetSHA256(url_string);

            #endregion

            #region "Match here Checksum and Update the DB"

            string[] checksumarray = checkSum.Split('\0');
            checkSum = checksumarray[0].ToString();

            if (CalCulatedCheckSum != checkSum)
            {
                Response.Redirect("~/Alumni/Alm_FundDonationlist.aspx", false);
                return;
            }
            else
            {
                //  basis of this id we need to set all session as we need to logged into the system to update the payment and keep user login to site.
                //////////////////////////////////////SET HERE ALL THE SESSION ///////////////////////////////////////////////////////////
                if (transaction_id.Length > 0)
                {
                    DataSet ds = Get_All_Details_of_Current_Trasaction(transaction_id);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //Session["FK_Inter_Coll_Mig_Id"] = ds.Tables[0].Rows[0]["FK_Inter_Coll_Mig_Id"].ToString();
                        Session["Pk_Trandtl_Id"] = ds.Tables[0].Rows[0]["Pk_Trandtl_Id"].ToString();
                        Session["Amount"] = ds.Tables[0].Rows[0]["Amount"].ToString();
                        // Session["BankGatwayId"] = ds.Tables[0].Rows[0]["Fk_PaymentGId"].ToString();
                        // Session["temp_BankGatwayId"] = ds.Tables[0].Rows[0]["Fk_PaymentGId"].ToString();
                        Session["pk_TransId"] = ds.Tables[0].Rows[0]["Pk_Trandtl_Id"].ToString();
                        Session["Fk_Contributionid"] = ds.Tables[0].Rows[0]["Fk_Contributionid"].ToString();
                        Session["Fk_Personid"] = ds.Tables[0].Rows[0]["Fk_Personid"].ToString();
                    }
                    else
                    {
                        // Response.Redirect("~/CommonPGandUG/RME_Status_Page.aspx", false);
                        return;
                    }
                }
                else
                {
                    //Response.Redirect("~/CommonPGandUG/RME_Status_Page.aspx", false);
                    return;
                }
                ////////////////////////////////////////END HERE////////////////////////////////////////////////////////////////////////////
                StringBuilder strMsg = new StringBuilder();
                string HeaderMsg = string.Empty;
                string UniqueCode = string.Empty;
                string Msg = string.Empty;
                string msg = "";

                if (status.ToUpper() == "SUCCESS")
                {
                    //string splitMailCC = "";

                    //SendMail("a", splitMailCC);

                    divSuccessPayment.Visible = true;
                    divPendingPayment.Visible = false;
                    divFailedPayment.Visible = false;
                    HeaderMsg = "Your Payment has been processed Successfully. <br/> ";
                    // HeaderMsg = "Your Payment has been processed Successfully. <br />Kindly note Your Login Id is:-" + Session["EmailID"] + "<br /> & Your Password is:-" + Session["password"] + "<br />";
                }

                if (status.ToUpper() == "FAILURE")
                {
                    divSuccessPayment.Visible = false;
                    divPendingPayment.Visible = false;
                    divFailedPayment.Visible = true;
                    HeaderMsg = "Your Payment has been Failed. <br />";
                }

                if (status.ToUpper().Contains("PENDING"))
                {
                    divSuccessPayment.Visible = false;
                    divPendingPayment.Visible = true;
                    divFailedPayment.Visible = false;
                    HeaderMsg = "Your Payment is Pending... <br />";
                }

                if (Session["Pk_Trandtl_Id"].ToString() == transaction_id)  //check here our unique pk_transid with responce trans id as refno
                {
                    Pk_TranId = Session["Pk_Trandtl_Id"].ToString();
                    IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                    PK_PaymentGatewayId = 2;// as responce is coming from SBI gateway
                    DataSet dsmain = GetSchema("Common_PaymentTransaction_Dtl");
                    DataRow dr_main = dsmain.Tables[0].NewRow();
                    //dr["status"] = status;
                    //dr["bank_ref_num"] = sbi_ref_no;
                    //dr["status_description"] = status_desc;
                    //dr["StatusUpdatedDate"] = DateTime.Now.ToString();
                    //dr["fk_purchaseid"] = 0; //Convert.ToInt64(Session["Pk_purchaseid_mls"].ToString());
                    //dsmain.Tables[0].Rows.Add(dr);
                    // Fee =  total_amt;

                    dr_main["Tranid"] = Pk_TranId;
                    dr_main["Bank_Tansactionid"] = sbi_ref_no;
                    dr_main["Status_Update_Date"] = DateTime.Now;
                    dr_main["IsActive"] = true;
                    dr_main["Status"] = status;
                    dr_main["Update_Date"] = DateTime.Now;
                    dr_main["Update_Userid"] = Session["Fk_Personid"].ToString();
                    dr_main["Satus_Description"] = status_desc;
                    dsmain.Tables[0].Rows.Add(dr_main);
                    XmlDoc = dsmain.GetXml();
                    UpdateContributor_Record(XmlDoc, Pk_TranId);
                    //try
                    //{
                    //    if (UpdateContributor_Record(ref Msg) > 0)
                    //    {

                    //    }
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    //if (UpdateTransactionRecord(ref Msg) > 0)
                    //{
                    //    Pk_TranId = Session["pk_TransId"].ToString();
                    //     = Convert.ToInt32(Session["regid"].ToString());

                    //    DataSet ds = obj.Get_Payment_UniqueCodeOtherThenFirst();
                    //    if (ds.Tables[0].Rows.Count > 0)
                    //    {
                    //        UniqueCode = ds.Tables[0].Rows[0]["uniqueCode"].ToString();
                    //        Response.Write(UniqueCode);
                    //        Send_Details_of_RefNo_Via_Email(UniqueCode, Session["RegFees"].ToString());
                    //    }
                    //}
                }
                strMsg.Append(HeaderMsg);
                strMsg.Append(" Details are given below:<br />");
                strMsg.Append("Transcation id :-  " + Session["Pk_Trandtl_Id"].ToString() + "  <br />");
                strMsg.Append("Amount :-" + total_amt + "<br />");
                strMsg.Append("Date Time :- " + DateTime.Now.ToString() + "<br />");
                strMsg.Append("Transaction Status :-" + status + "<br />");
                //if (UniqueCode.Length > 1)
                //{
                //    strMsg.Append("Transaction Reference No :- " + "<B>" + UniqueCode + "</B><br />");
                //    strMsg.Append("This Reference number will be used To submit the Application Form.");
                //}
                //-----------------------To Show Message on Div------------------
                lblFailedMsg.Text = strMsg.ToString();
                lblPendingMsg.Text = strMsg.ToString();
                lblSuccessMsg.Text = strMsg.ToString();
                //----------------------End Here--------------------------------
            }
            #endregion
        }
        catch (Exception ex)
        {
            string Msg = "";

            //PA_Login_And_Exception_Log.Insert_ExceptionLog(Session["regid"].ToString(), Session["DegreeType"].ToString(),
            //ex, HttpContext.Current.Request.UserHostAddress.ToString(), ref Msg);
        }

    }
    #endregion
    public DataSet UpdateContributor_Record(string dsXml, string Pk_Trandtl_Id)
    {
        DataSet ds = new DataSet();
        DataAccess obj = new DataAccess();
        try
        {
            this.clear();
            names.Add("@Pk_Trandtl_Id"); values.Add(Pk_Trandtl_Id); types.Add(SqlDbType.NVarChar);
            names.Add("@doc"); values.Add(dsXml); types.Add(SqlDbType.VarChar);
            ds = obj.GetDataSet("ALM_Contribution_PaymentTransaction_Dtl_Upd", values, names, types);
        }
        catch (Exception ex)
        {
            return null;
        }
        return ds;
    }

    #region "Work for SBI payment Gateway"

    protected string GetSHA256(string name)
    {
        SHA256 SHA256 = new SHA256CryptoServiceProvider();
        byte[] ba = SHA256.ComputeHash(Encoding.Default.GetBytes(name));
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    public string EncryptWithKey(String messageToEncrypt, string FilePath, byte[] nonSecretPayload = null)
    {
        byte[] key = File.ReadAllBytes(FilePath);
        if (string.IsNullOrEmpty(messageToEncrypt))
        {
            throw new ArgumentException("Secret Message Required!", "messageToEncrypt");
        }
        byte[] msgToEncryptByte = Encoding.UTF8.GetBytes(messageToEncrypt);
        //Non-secret Payload Optional
        nonSecretPayload = nonSecretPayload ?? new byte[] { };
        //Using random nonce large enough not to repeat
        byte[] cipherText = null;
        byte[] nonce = null;
        var cipher = new GcmBlockCipher(new AesEngine());
        try
        {
            Random rnd = new Random();
            nonce = new byte[16];
            rnd.NextBytes(nonce);
            var parameters = new AeadParameters(new KeyParameter(key), 128, nonce, nonSecretPayload);
            cipher.Init(true, parameters);
            cipherText = new byte[cipher.GetOutputSize(msgToEncryptByte.Length)];
            var len = cipher.ProcessBytes(msgToEncryptByte, 0, msgToEncryptByte.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error {0}", ex.Message);
            Console.ReadKey();
        }
        //Assemble Message
        using (var combinedStream = new MemoryStream())
        {
            using (var binaryWriter = new BinaryWriter(combinedStream))
            {
                //Prepend Nonce
                binaryWriter.Write(nonce);
                //Write Cipher Text
                binaryWriter.Write(cipherText);
            }
            return Convert.ToBase64String(combinedStream.ToArray());
        }
    }

    public string DecryptWithKey(string encrypteddata, string FilePath, int nonSecretPayloadLength = 0)
    {
        byte[] key = File.ReadAllBytes(FilePath);
        byte[] encryptedMessage = Convert.FromBase64String(encrypteddata);

        if (encryptedMessage == null || encryptedMessage.Length == 0)
        {
            throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");
        }

        using (var cipherStream = new MemoryStream(encryptedMessage))

        using (var cipherReader = new BinaryReader(cipherStream))
        {
            //Grab Payload    

            //Grab Nonce
            var nonce = cipherReader.ReadBytes(16);

            //Decrypt Cipher Text 
            var cipherText = cipherReader.ReadBytes(encryptedMessage.Length);
            var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);
            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), 128, nonce, nonSecretPayload);
            cipher.Init(false, parameters);
            var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

            var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
            try
            {
                cipher.DoFinal(plainText, len);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //  return plainText;
            return Encoding.UTF8.GetString(plainText);
        }
    }

    //public string DecryptWithKey(string encrypteddata, string FilePath, int nonSecretPayloadLength = 0)
    //{
    //    byte[] key = File.ReadAllBytes(FilePath);
    //    byte[] encryptedMessage = Convert.FromBase64String(encrypteddata);
    //    if (encryptedMessage == null || encryptedMessage.Length == 0)
    //    {
    //        throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");
    //    }
    //    using (var cipherStream = new MemoryStream(encryptedMessage))
    //    using (var cipherReader = new BinaryReader(cipherStream))
    //    {
    //        //Grab Payload   
    //        //Grab Nonce
    //        var nonce = cipherReader.ReadBytes(16);
    //        //Decrypt Cipher Text
    //        var cipherText = cipherReader.ReadBytes(encryptedMessage.Length);
    //        var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);
    //        var cipher = new GcmBlockCipher(new AesEngine());
    //        var parameters = new AeadParameters(new KeyParameter(key), 128, nonce, nonSecretPayload);
    //        cipher.Init(false, parameters);
    //        var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];
    //        var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
    //        try
    //        {
    //            cipher.DoFinal(plainText, len);
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e.Message);
    //        }
    //        //  return plainText;
    //        return Encoding.UTF8.GetString(plainText);
    //    }
    //}

    Byte[] GetFileBytes(String filePath)
    {
        byte[] buffer;
        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        try
        {
            int length = (int)fileStream.Length;
            buffer = new byte[length];
            int count;
            int sum = 0;
            while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                sum += count;
        }
        finally
        {
            fileStream.Close();
        }
        return buffer;
    }

    #endregion

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #region common code

    string _xmlDoc, _IpAddress, _Fee, _Pk_TranId;

    int _MisId, _PK_PaymentGatewayId;

    long _Ret_Pk_PurchaseId, _PK_PurchaseId;

    public long Ret_Pk_PurchaseId { get { return _Ret_Pk_PurchaseId; } set { _Ret_Pk_PurchaseId = value; } }

    public long PK_PurchaseId { get { return _PK_PurchaseId; } set { _PK_PurchaseId = value; } }

    public string Pk_TranId { get { return _Pk_TranId; } set { _Pk_TranId = value; } }

    public string XmlDoc
    {
        get { return _xmlDoc; }
        set { _xmlDoc = value; }
    }

    public int PK_PaymentGatewayId
    {
        get { return _PK_PaymentGatewayId; }
        set { _PK_PaymentGatewayId = value; }
    }

    public string IpAddress
    {
        get { return _IpAddress; }
        set { _IpAddress = value; }
    }

    public string Fee
    {
        get { return _Fee; }
        set { _Fee = value; }
    }

    public int MisId
    {
        get { return _MisId; }
        set { _MisId = value; }
    }

    void Clear()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    public int UpdateTransactionRecord(ref string Message)
    {
        Clear();
        names.Add("@xmlDoc"); values.Add(_xmlDoc); types.Add(SqlDbType.VarChar);
        names.Add("@IpAddress"); values.Add(_IpAddress); types.Add(SqlDbType.VarChar);
        //names.Add("@pk_purchaseid"); values.Add(_PK_PurchaseId); types.Add(SqlDbType.BigInt);
        names.Add("@Pk_Tranid"); values.Add(_Pk_TranId); types.Add(SqlDbType.VarChar);
        names.Add("@Pk_GatewayId"); values.Add(_PK_PaymentGatewayId); types.Add(SqlDbType.Int);
        //  names.Add("@fee"); values.Add(_Fee); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("[ALM_Online_Payment_Upd_ForAlumni]", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("U", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public DataSet Get_All_Details_of_Current_Trasaction(string PK_TranId)
    {
        Clear();
        names.Add("@PK_TranId"); values.Add(PK_TranId); types.Add(SqlDbType.VarChar);
        return DAobj.GetDataSet("ALM_ContributerData_After_SBI_Responce", values, names, types);
    }

    public DataSet GetSchema(string TableName)
    {
        try
        {
            Clear();
            return DAobj.GetSchema(TableName);
        }
        catch { throw; }
    }

    #endregion

    protected void bntlogin_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Alumni/Alm_FundDonationlist.aspx");
        }
        catch (Exception ex)
        {
            string Msg = "";
            //    PA_Login_And_Exception_Log.Insert_ExceptionLog(Session["regid"].ToString(), Session["DegreeType"].ToString(),
            //        ex, HttpContext.Current.Request.UserHostAddress.ToString(), ref Msg);
        }
    }

    #region SendEmail

    protected void SendMail(string values, string splitMailCC)
    {
        try
        {
            // Get_username_Pass();
            // lblMsg.Text = "";

            StringBuilder strBody = new StringBuilder();
            string emailTo = Session["EmailID"].ToString().Trim();
            // Session["AlumniName"]= txtAlumniName.Text.Trim();
            strBody.Append("<Table border='0' cellpadding='0' cellspacing='0' style='width:75%;' align='left'>");

            strBody.Append("<Tr>");
            strBody.Append("<Td  style='font-size: 15px;font-family: Verdana;height: 14px;'>");
            strBody.Append("<B>" + "Dear " + Session["Alumni_Name"] + "," + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td>");
            strBody.Append("<B>" + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td>");
            strBody.Append("<B>Your Alumni Registration has been done successfully,Please find your User Name & Password below  </B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td  style='font-size: 15px;font-family: Verdana;height: 14px;'>");
            strBody.Append("<B>" + "User Name - " + Session["EmailID"] + "" + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td  style='font-size: 15px;font-family: Verdana;height: 14px;'>");
            strBody.Append("<B>" + " & Password - " + Session["password"] + "" + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td colspan='4'>");
            strBody.Append("<B>" + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td>");
            strBody.Append("<B> In case of any query please contact college </B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td align=left style='font-size: 12px;font-family: Verdana;height: 14px;'>");
            strBody.Append("<B>" + "Thanks and Regards ," + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");
            strBody.Append("<Tr>");
            strBody.Append("<Td align=left style='font-size: 12px;font-family: Verdana;height: 14px;'>");
            strBody.Append("<B>" + "HPU" + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("</Table>");

            //SendMail(splitMailCC, emailTo, strBody);

        }
        catch (Exception ex)
        {

        }
    }

    //private void SendMail(string splitMailCC, string email, StringBuilder strBody)
    //{
    //    try
    //    {
    //        string Body = "";
    //        //Body = strBody.ToString();

    //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
    //        mail.IsBodyHtml = true;
    //        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
    //        mail.From = new MailAddress("alumni.hpushimla@gmail.com");

    //        mail.To.Add(email);
    //        mail.Subject = "Registration to HPU Alumni Cell";
    //        mail.Body = strBody.ToString();

    //        // if (splitMailCC != "")
    //        //   mail.CC.Add(splitMailCC);

    //        SmtpServer.Port = 587;
    //        SmtpServer.Credentials = new System.Net.NetworkCredential("alumni.hpushimla@gmail.com", "hlhzhzanylwsdbza");
    //        SmtpServer.EnableSsl = true;
    //        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //        { return true; };

    //        SmtpServer.Send(mail);

    //    }
    //    catch (Exception e)
    //    {

    //    }
    //}

    #endregion
}