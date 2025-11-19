using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using DataAccessLayer;
using System.Collections;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Configuration;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.Diagnostics;

public partial class Onlinepayment_ALM_Common_PaymentGateway : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();
    DataTable dsmain = new DataTable();
    private bool IspageReferesh = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["IsShowPayButton"] = null;
                BindDropDown();
                CheckStatusOfAllPreviousTransaction();
            }
            else
            {
                IspageReferesh = true;
            }
        }
        catch (Exception ex)
        {
            //  ClientMessaging(CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message));
            Response.Redirect("Alumin_Loginpage.aspx", false);
        }

    }

    private void BindDropDown()
    {
        try
        {
            PK_PaymentGatewayId = 0;
            DataSet ds = Get_PaymentGatwayList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem li = new ListItem();
                    string ImageUrl;
                    if (dr["Pk_PaymentGId"].ToString() == "1")
                    {
                        ImageUrl = " ../App_Themes/Darkgreen/online/Hdfc.jpg";
                    }
                    else
                        if (dr["Pk_PaymentGId"].ToString() == "2")
                    {
                        ImageUrl = "../App_Themes/Darkgreen/online/sbi.jpg";
                        ImageUrl = "../App_Themes/CCSBLUE/online/sbi.jpg";
                    }
                    else
                    {
                        ImageUrl = "../App_Themes/CCSBLUE/online/sbi.jpg";
                    }
                    li.Text = "<div style='padding:10px;'><img style='border:2px solid black; width:200px;height:150px;border-radius:20%!important;' src=\"" + ImageUrl + "\" alt=\"\" /> </div>&nbsp;<span style='font-size:15px;font-weight:bold'>" + dr["P_GatwayName"].ToString() + "</span>";
                    li.Value = dr["Pk_PaymentGId"].ToString();
                    ddlBankGatway.Items.Add(li);
                }
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }
    protected void ddlBankGatway_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBankGatway.SelectedIndex >= 0)
            {
                if (ViewState["IsShowPayButton"] != null)
                {
                    if (ViewState["IsShowPayButton"].ToString() == "0")
                    {
                        btnPay.Visible = false;
                    }
                    else
                    {
                        btnPay.Visible = true;
                    }
                }
                BindGatwayList();
            }
            else
            {
                btnPay.Visible = false;
                dlGatwayDtl.DataSource = null;
                dlGatwayDtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            string Msg = "";

        }
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        try
        {
            //btnPay.Visible = false;
            InsertPaymentTransactionDtl();
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }
    private void InsertPaymentTransactionDtl() // insert here purchase id and get unique trans id
    {
        try
        {
            ArrayList Result = new ArrayList();
            string Message = "";
            IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
            PK_PurchaseId = Convert.ToInt64(Session["Pk_PurchaseId"].ToString());
            PK_PaymentGatewayId = Convert.ToInt32(ddlBankGatway.SelectedValue);
            ProductInfo = "Alumni Registration";
            Session["ProductInfo"] = ProductInfo;
            try
            {
                if (InsertPaymentTranRecord(ref Message, ref Result) > 0)
                {
                    if (Result.Count > 0)
                    {
                        Session["Pk_TransId"] = Result[1].ToString().Trim();
                        SentToPaymentGatway();
                    }
                    else
                    {
                        lblPaymentMsg.Text = Message;
                        btnBack_Click(null, null);
                    }
                }
                else
                {
                    lblPaymentMsg.Text = Message;
                    btnBack_Click(null, null);
                }

            }
            catch (Exception ex)
            {

            }


        }
        catch (Exception ex)
        {
            //string Msg = "";
            //PA_Login_And_Exception_Log.Insert_ExceptionLog(Session["regid"].ToString(), Session["DegreeType"].ToString(),
            //    ex, HttpContext.Current.Request.UserHostAddress.ToString(), ref Msg);
        }
    }


    //  "Check here which payment gateway has been select and Sent to Gateway as per selection"

    private void SentToPaymentGatway() //check here bank id so that student can be sent to that gateway
    {
        try
        {
            if (ddlBankGatway.SelectedValue == "1")// as value 1 is for HDFC bank Gateway
            {
                if (Session["pk_TransId"] != null)
                {
                    Session["temp_BankGatwayId"] = "1";
                    Session["BankGatwayId"] = "1";
                    //  SentToHdfc();

                }
            }
            if (ddlBankGatway.SelectedValue == "2")// as value 2 is for Sbi Bank Gateway
            {
                if (Session["pk_TransId"] != null)
                {
                    Session["temp_BankGatwayId"] = "2";
                    Session["BankGatwayId"] = "2";
                    SentToSbi();

                }
            }

            if (ddlBankGatway.SelectedValue == "3")// as value 3 is for HPSCB Bank Gateway
            {
                if (Session["pk_TransId"] != null)
                {
                    Session["temp_BankGatwayId"] = "3";
                    Session["BankGatwayId"] = "3";
                    // SentToHPSCB();

                }
            }

        }
        catch (Exception ex)
        {
            string Msg = "";
            Insert_ExceptionLog(Session["regid"].ToString(), //Session["DegreeType"].ToString(),
                     ex, HttpContext.Current.Request.UserHostAddress.ToString(), ref Msg);
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    // "Code of different different Gateway To post on their URL"
    private void SentToSbi()
    {
        try
        {
            //long _fk_regid = Convert.ToInt64(Session["regid"].ToString());
            //// string _DegType = Session["DegreeType"].ToString();
            //string _TransactionID = Session["pk_TransId"].ToString();
            //DataSet ds = Get_Payer_StudentDetails(_fk_regid, _TransactionID);

            //int FK_Inter_Coll_Mig_Id = Convert.ToInt32(Session["FK_Inter_Coll_Mig_Id"]);
            long FK_RegId = Convert.ToInt32(Session["regid"].ToString()); //Convert.ToInt32(Session["AlumniID"].ToString()); 
            string Transactionid = Session["pk_TransId"].ToString();
            DataSet ds = Get_Payer_StudentDetails(FK_RegId, Transactionid);

            if (ds.Tables[0].Rows.Count > 0)
            {
                /*temprorary commented by ashish*/
                string returnurl = ConfigurationManager.AppSettings["s_sbi_url1"].ToString();
                string Pk_TranID = Session["pk_TransId"].ToString();
                string amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["RegFees"]).ToString();
                string ProductInfo = string.Empty;

                ProductInfo = "Alumni Registration";
                //**********************Below is the sequence of Parameters **************************************************
                string url_string = "";
                url_string = "transaction_id=" + Pk_TranID + "|" + "total_amt=" + amount + "|" + "productinfo=" + ProductInfo + "|" + "RU=" + returnurl;

                //********************End here Sequence and Calculated Check sum from below Code ******************************

                //byte[] byteArray = Encoding.UTF8.GetBytes(url_string);
                //MemoryStream stream = new MemoryStream(byteArray);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                string checkSum = "";
                checkSum = GetSHA256(url_string);
                string encdata = "";
                encdata = url_string + "|" + "checkSum=" + checkSum;// +"|" + "RU=" + returnurl; 
                encdata = EncryptWithKey(encdata, Server.MapPath("~/Key/HIMACHAL_UNIV.key"));//used here some key path

                //****************************CheckSum is calculated with Some Keys above ***************************************              

                string post_url = "";
                // post_url = "https://www.onlinesbi.sbi/merchant/merchantprelogin.htm";
                post_url = "https://merchant.onlinesbi.sbi/merchant/merchantprelogin.htm ";
                System.Collections.Hashtable data = new System.Collections.Hashtable();
                data.Add("encdata", encdata);
                // data.Add("merchant_code", "HPUNIV_RME"); 
                //data.Add("merchant_code", "HPUNIV_EVEN");
                data.Add("merchant_code", "HPUNIV_ALUMNI");
                //data.Add("merchant_code", "HPUNIV_ADMSN");

                string strForm = PreparePOSTFormSBI(post_url, data);
                Page.Controls.Add(new LiteralControl(strForm));



                //********************************End here posting of Data to sbi ***********************************************
            }
        }
        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");
        }
    }

    //  "Work for SBI payment Gateway"



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

    private string PreparePOSTFormSBI(string url, System.Collections.Hashtable data)
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
        foreach (System.Collections.DictionaryEntry key in data)
        {
            strForm.Append("<input type=\"hidden\" name=\"" + key.Key + "\" value=\"" + key.Value + "\">");
        }
        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." + formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }

    // "Work for HPSCB payment Gateway"

    public static string GenerateSHA512String(string inputString)
    {
        SHA512 sha512 = SHA512Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        byte[] hash = sha512.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i <= hash.Length - 1; i++)
        {
            stringBuilder.Append(hash[i].ToString("X2"));
        }

        return stringBuilder.ToString();
    }



    //  "Code to Check All gateway previous transaction status as Transaction may be attempted via all payment gateway"
    // to check all previous transaction attempted by a candidate and if there is any transaction is successs,pending  then stop user from making payment
    bool UpdatepaymentStatus = false;

    public void CheckStatusOfAllPreviousTransaction()
    {
        try
        {
            string Msg = "";
            long regid = Convert.ToInt64(Session["regid"]);

            DataSet ds = Get_All_PreviousTransaction(regid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtGetAllStatus = new DataTable();
                dtGetAllStatus = ds.Tables[0].Copy();
                dtGetAllStatus.Columns.Add("CurrentStatus", typeof(string));
                dtGetAllStatus.Columns.Add("Reference_No", typeof(string));
                dtGetAllStatus.TableName = "Status_Table";

                foreach (DataRow dr in dtGetAllStatus.Rows)
                {
                    //first check here for which gateway this transaction is initiated like if Fk_PaymentGId=1 then for HDFC gateway if Fk_PaymentGId =2 then for SBI gateway if Fk_PaymentGId=3 then for HPSCB Bank
                    string UniqueCode = "";

                    // "Get Responce From SBI"
                    if (dr["Fk_PaymentGId"].ToString() == "2")//SBI
                    {
                        if (dr["status"].ToString().ToUpper() != "FAILURE")
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //checked here the double verification responce from SBI bank
                                string StatusOfTran = "";
                                //Dictionary<string, string> DoubleVerification_Live_FromSBI_Gateway_Res = DoubleVerification_Live_FromSBI_Gateway(dr["RegFees"].ToString(), dr["TransactionID"].ToString(), ref StatusOfTran);
                                Dictionary<string, string> DoubleVerification_Live_FromSBI_Gateway_Res = DoubleVerification_Live_FromSBI_Gateway(dr["amount"].ToString(), dr["TransactionID"].ToString(), ref StatusOfTran);

                                //step 3 Pk_TranId is sent to SBI gateway for double verification
                                Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);

                                if (DoubleVerification_Live_FromSBI_Gateway_Res.Count > 0 && StatusOfTran.Length > 0) //status of tran will be greated than 0 if record exists in HPSCB bank
                                {
                                    //now put here all logic to update the db as per current status of transaction
                                    //check here transaction status and update to database 
                                    //string Msg = "";
                                    if (dr["status"].ToString() == "" || dr["status"].ToString().ToUpper().Contains("PENDING"))
                                    {
                                        Pk_TranId = dr["TransactionID"].ToString();
                                        //PK_PurchaseId = Convert.ToInt64(dr["fk_purchaseid"].ToString());
                                        IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                                        PK_PaymentGatewayId = 2; // as responce is coming from HPSCB gateway
                                        DataSet dsmain = GetSchema("ALM_eCoupon_Purchase_Payment_Trn");
                                        DataRow drMain = dsmain.Tables[0].NewRow();
                                        drMain["status"] = DoubleVerification_Live_FromSBI_Gateway_Res["status"].ToString();
                                        drMain["bank_ref_num"] = DoubleVerification_Live_FromSBI_Gateway_Res["sbi_ref_no"].ToString();
                                        drMain["status_description"] = DoubleVerification_Live_FromSBI_Gateway_Res["status_desc"].ToString();
                                        //drMain["fk_purchaseid"] = Convert.ToInt64(dr["Fk_purchaseid"].ToString());
                                        //drMain["sbi_chksum"] = DoubleVerification_Live_FromSBI_Gateway_Res["checkSum"].ToString();

                                        dsmain.Tables[0].Rows.Add(drMain);

                                        XmlDoc = dsmain.GetXml();

                                        if (UpdateTransactionRecord(ref Msg) > 0)
                                        {
                                            //step 4 got the latest update from SBI gateway and Transaction table is updated with Pk_tranId= dr["pk_TranId"].ToString() id
                                            Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                            Pk_TranId = dr["TransactionID"].ToString();
                                            Fk_regid = Session["FK_RegId"].ToString();
                                            //DataSet dsCode = Get_Payment_UniqueCode();
                                            //if (dsCode.Tables[0].Rows.Count > 0)
                                            //{
                                            //    UniqueCode = dsCode.Tables[0].Rows[0]["uniqueCode"].ToString();
                                            //    //step 5 reference no is generated as previous payment is confirmed
                                            //    Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 5 reference no is generated (SBI) as previous payment is confirmed", ref Msg);
                                            //}
                                        }
                                    }
                                    //now add here status and refno if created in db
                                    dr["CurrentStatus"] = DoubleVerification_Live_FromSBI_Gateway_Res["status"].ToString();
                                    dr["Reference_No"] = UniqueCode;
                                    if (dr["CurrentStatus"].ToString().ToUpper() == "SUCCESS")//&& UniqueCode.Length > 10 check here unique cod is generated or not
                                    {
                                        lblPaymentMsg.Text = "Your Previous payment is <B>Success</B>.Click on <B>BACK BUTTON ABOVE</B>";
                                        btnPay.Visible = false;
                                        ViewState["IsShowPayButton"] = "0";
                                        dgDetails.DataSource = dtGetAllStatus;
                                        dgDetails.DataBind();
                                        //step 6 current status of payment is success user is blocked from making new payment
                                        Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                        return;
                                    }
                                    else if (dr["CurrentStatus"].ToString().ToUpper() == "PENDING")
                                    {
                                        string Trndate = dr["transactiondate"].ToString();
                                        TimeSpan varTime = (DateTime)DateTime.Now - (DateTime)Convert.ToDateTime(Trndate);
                                        double fractionalMinutes = varTime.TotalMinutes;
                                        int wholeMinutes = (int)fractionalMinutes;
                                        if (wholeMinutes < 1440) //if 24 hours is completed then show button to make payment again
                                        {
                                            lblPaymentMsg.Text = "Wait for atleast 24 Hrs to get previous transaction Status. as Transaction status is  pending. Then try again!";
                                            btnPay.Visible = false;
                                            ViewState["IsShowPayButton"] = "0";
                                            //step 7 user is still waiting for 24 hrs as current status of this transaction is pending
                                            Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                        }
                                        else
                                        {
                                            //btnPay.Visible = true;
                                            ViewState["IsShowPayButton"] = "1";
                                            //step 8 user is allowed to make payment after 24 hours of waiting
                                            Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                        }
                                    }
                                    else if (dr["CurrentStatus"].ToString().ToUpper() == "FAILURE")
                                    {
                                        // btnPay.Visible = true;
                                        ViewState["IsShowPayButton"] = "1";
                                        //step 9 current status of this transaction is Failed
                                        Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                    }
                                    dr["CurrentStatus"] = dr["CurrentStatus"].ToString().Length == 0 ? "N/A" : dr["CurrentStatus"].ToString();
                                }
                                else
                                {
                                    //btnPay.Visible = true;// as there is no record of this txnid in bankdb so allow user to pay
                                    //step 10 user tried to go to payment gateway but disconnect so HPSCB gateway is not having any info so user is allowd to pay again
                                    Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                                    ViewState["IsShowPayButton"] = "1";
                                }
                                //ViewState["IsShowPayButton"] = "1";// remove it when code is written inside above method for double verification
                            }
                        }
                        else
                        {
                            //in case if status is failure  then 
                            dr["CurrentStatus"] = dr["status"].ToString();
                            ViewState["IsShowPayButton"] = "1";
                            //step2 alreay Transaction table is updated with Failure
                            Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
                        }
                    }

                }
                //now bind the grid with latest status of transaction
                dgDetails.DataSource = dtGetAllStatus;
                dgDetails.DataBind();
            }
            else
            {
                //setp 1 first time user is going to pay
                //btnPay.Visible = true;
                ViewState["IsShowPayButton"] = "1";
                Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), "0", 0, "setp 1 first time user is going to pay", ref Msg);
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }


    //=================================SBI DOUBLE VERIFICATION CODE============================================
    //  "Code of SBI payment Gateway For Double Verification"
    public Dictionary<string, string> DoubleVerification_Live_FromSBI_Gateway(string total_amt, string ref_no, ref string CurrentStatusOfTran)
    {
        //string merchant_code = "HIMACHAL_UNIV";
        //string merchant_code = "HIMA_UNIV";
        //string url_string = "total_amt=" + total_amt.ToString().Trim() + "|" + "transaction_id=" + ref_no;
        //byte[] byteArray = Encoding.UTF8.GetBytes(url_string);
        //MemoryStream stream = new MemoryStream(byteArray);
        //string checkSum = GetSHA256(url_string);//GetMD5Hash(stream);
        //string encdata = url_string + "|" + "checkSum=" + checkSum;
        //encdata = EncryptWithKey(encdata, Server.MapPath("~/Key/HIMACHAL_UNIV.key"));
        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //string post_url = "https://www.onlinesbi.sbi/thirdparties/doubleverification.htm";

        string merchant_code = "HPUNIV_ALUMNI";
        string url_string = "total_amt=" + total_amt.ToString().Trim() + "|" + "transaction_id=" + ref_no;
        // byte[] byteArray = Encoding.UTF8.GetBytes(url_string);
        // MemoryStream stream = new MemoryStream(byteArray);
        string checkSum = GetSHA256(url_string);//GetMD5Hash(stream);
        string encdata = url_string + "|" + "checkSum=" + checkSum;
        encdata = EncryptWithKey(encdata, Server.MapPath("~/Key/HIMACHAL_UNIV.key"));
        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //string post_url = "https://www.onlinesbi.sbi/thirdparties/doubleverification.htm";
        string post_url = "https://merchant.onlinesbi.sbi/thirdparties/doubleverification.htm";
        // "https://www.onlinesbi.com/thirdparties/doubleverification.htm";
        using (var client = new WebClient())
        {
            var values = new NameValueCollection();
            values["encdata"] = encdata;
            values["merchant_code"] = merchant_code;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var response = client.UploadValues(post_url, values);
            var responseString = Encoding.Default.GetString(response);
            string DecryptStr = "";
            //DecryptStr = Decrypt(Request.Form["encdata"], Server.MapPath("~/Key/TNAU.key"));
            DecryptStr = DecryptWithKey(responseString, Server.MapPath("~/Key/HIMACHAL_UNIV.key"));
            string sbi_ref_no1 = "", transaction_id = "", status = "", status_desc = "", checkSum1 = "";
            //Splitting The response code received from the SBI Bank Gateway
            string[] ArrStr1 = DecryptStr.Split('|');
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < ArrStr1.Length; i++)
            {
                if (i == 0)
                {
                    sbi_ref_no1 = Convert.ToString(ArrStr1[i].ToString()).Replace("sbi_ref_no=", "").Trim();
                    dic.Add("sbi_ref_no", sbi_ref_no1);
                }
                else if (i == 1)
                {
                    transaction_id = Convert.ToString(ArrStr1[i]).Replace("transaction_id=", "").Trim();
                    dic.Add("transaction_id", transaction_id);
                }
                else if (i == 2)
                {
                    total_amt = Convert.ToString(ArrStr1[i]).Replace("total_amt=", "").Trim();
                    dic.Add("total_amt", total_amt);
                }
                else if (i == 3)
                {
                    status = Convert.ToString(ArrStr1[i]).Replace("status=", "").Trim();
                    dic.Add("status", status);
                }
                else if (i == 4)

                {
                    status_desc = Convert.ToString(ArrStr1[i]).Replace("status_desc=", "").Trim();
                    dic.Add("status_desc", status_desc);
                }
                else if (i == 5)
                {
                    checkSum1 = Convert.ToString(ArrStr1[i]).Replace("checkSum=", "").Trim();
                    dic.Add("checkSum", checkSum1);
                }
            }
            CurrentStatusOfTran = dic["status"].ToString();
            return dic;

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


    private void BindGatwayList()
    {
        try
        {
            PK_PaymentGatewayId = Convert.ToInt32(ddlBankGatway.SelectedValue);
            DataSet ds = Get_PaymentGatwayList();
            if (ds.Tables[1].Rows.Count > 0)
            {
                if (ViewState["IsShowPayButton"] != null)
                {
                    if (ViewState["IsShowPayButton"].ToString() == "0")
                    {
                        btnPay.Visible = false;
                    }
                    else
                    {
                        btnPay.Visible = true;
                    }
                }
                btnPay.Visible = true;
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    // common code
    string _xmlDoc, _IpAddress, _Fee, _fk_regid, _ProductInfo, _Ret_Pk_Tranid, _Pk_TranId;
    int _MisId, _PK_PaymentGatewayId;
    long _PK_PurchaseId;
    public long PK_PurchaseId { get { return _PK_PurchaseId; } set { _PK_PurchaseId = value; } }
    public string ProductInfo { get { return _ProductInfo; } set { _ProductInfo = value; } }
    public int FK_Inter_Coll_Mig_Id { get; set; }
    public int FK_RegId { get; set; }

    public string DegreeType { get; set; }
    public string Sem_YearId { get; set; }
    public string Transactionid { get; set; }
    public string XmlDoc
    {
        get { return _xmlDoc; }
        set { _xmlDoc = value; }
    }
    public string Ret_Pk_Tranid
    {
        get { return _Ret_Pk_Tranid; }
        set { _Ret_Pk_Tranid = value; }
    }
    public string Fk_regid
    {
        get { return _fk_regid; }
        set { _fk_regid = value; }
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
    public int PK_PaymentGatewayId
    {
        get { return _PK_PaymentGatewayId; }
        set { _PK_PaymentGatewayId = value; }
    }
    public string Pk_TranId { get { return _Pk_TranId; } set { _Pk_TranId = value; } }
    public string RegNo { get; set; }
    public string RefNo { get; set; }


    void Clear()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }
    public DataSet Get_PaymentGatwayList()
    {
        Clear();
        names.Add("@Pk_PaymentGId"); types.Add(SqlDbType.Int); values.Add(_PK_PaymentGatewayId);
        return DAobj.GetDataSet("[HPU_ALM_SP_BankGateway]", values, names, types);
    }
    //public DataSet RME_ApplicationStatus_Get()
    //{
    //    Clear();
    //    names.Add("@RegNo"); values.Add(RegNo); types.Add(SqlDbType.Int);
    //    names.Add("@RefNo"); values.Add(RefNo); types.Add(SqlDbType.Int);
    //    return DAobj.GetDataSet("spu_RME_Migration_Status_Get", values, names, types);
    //}
    public int InsertPaymentTranRecord(ref string Message, ref ArrayList Result)
    {
        Clear();
        names.Add("@IpAddress"); values.Add(_IpAddress); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
        names.Add("@Fk_PaymentGid"); values.Add(_PK_PaymentGatewayId); types.Add(SqlDbType.Int); size.Add("MAX"); outtype.Add("false");
        names.Add("@pk_purchaseid"); values.Add(_PK_PurchaseId); types.Add(SqlDbType.VarChar); size.Add("10"); outtype.Add("false");
        names.Add("@ProductInfo"); values.Add(_ProductInfo); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
        names.Add("@pk_TransId"); values.Add(_Ret_Pk_Tranid); types.Add(SqlDbType.VarChar); size.Add("20"); outtype.Add("true");
        if (DAobj.ExecuteTransactionMsgIO("[HPU_ALM_Common_Payment_Trans_ins]", values, names, types, size, outtype, ref Message, ref Result) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public DataSet Get_Payer_StudentDetails(long _Pk_Regid, string TransationId)
    {
        Clear();
        names.Add("@pk_Regid"); types.Add(SqlDbType.BigInt); values.Add(_Pk_Regid);
        names.Add("@TransactionID"); types.Add(SqlDbType.VarChar); values.Add(TransationId);
        return DAobj.GetDataSet("[HPU_ALM_StudentDetails_Payer_Sel]", values, names, types);
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
    public DataSet Get_All_PreviousTransaction(long Pk_Regid)//,string Degreeyear,int Examtype
    {
        Clear();
        names.Add("@pk_regid"); values.Add(Pk_Regid); types.Add(SqlDbType.BigInt);
        // names.Add("@DegreeYear"); values.Add(Degreeyear); types.Add(SqlDbType.NVarChar);
        // names.Add("@ExamTypeid"); values.Add(Examtype); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("HPU_ALM_Select_AllTransactionOf_Alumni", values, names, types);
    }
    public int UpdateTransactionRecord(ref string Message)
    {
        Clear();
        names.Add("@xmlDoc"); values.Add(_xmlDoc); types.Add(SqlDbType.VarChar);
        names.Add("@IpAddress"); values.Add(_IpAddress); types.Add(SqlDbType.VarChar);
        names.Add("@pk_purchaseid"); values.Add(_PK_PurchaseId); types.Add(SqlDbType.BigInt);
        names.Add("@Pk_Tranid"); values.Add(_Pk_TranId); types.Add(SqlDbType.VarChar);
        names.Add("@Pk_GatewayId"); values.Add(_PK_PaymentGatewayId); types.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("[sp_HPU_ALM_eCoupon_Payment_Upd]", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("U", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public int Insert_DoubleVerification_log(long fk_regid, string fk_TranId, int fk_GatewayId, string Descriptions, ref string Message)
    {
        Clear();

        names.Add("@fk_regid"); values.Add(fk_regid); types.Add(SqlDbType.BigInt);
        names.Add("@fk_TranId"); values.Add(fk_TranId); types.Add(SqlDbType.VarChar);
        names.Add("@fk_GatewayId"); values.Add(fk_GatewayId); types.Add(SqlDbType.Int);
        names.Add("@Descriptions"); values.Add(Descriptions); types.Add(SqlDbType.VarChar);
        if (DAobj.ExecuteTransactionMsg("[HPU_ALM_DoubleVerification_log_ins]", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Pk_TransId"] = null;
            Response.Redirect("~/CommonPGandUG/Alumin_Loginpage.aspx");
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    public static int Insert_ExceptionLog(string StudentOrAdminRegId, Exception ex, string UserIP, ref string Message)//string DegreeType,
    {
        try
        {
            ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
            ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();
            names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
            //Getting here Exception Details
            string stackTraceDtl = ex.StackTrace.ToString();
            // path.Substring(path.LastIndexOf("/");
            string PageName = (ex.StackTrace.ToString()).Substring(ex.StackTrace.ToString().LastIndexOf('\\')).ToString();
            string ExcepMsg = ex.Message.ToString();
            string ExcepOccurInMethod = ex.TargetSite.ToString();
            StackTrace st = new StackTrace(ex);
            string ExcepOccurInClass = st.GetFrame(0).GetMethod().DeclaringType.ToString();
            //end

            DataAccess DAobj = new DataAccess();
            names.Add("@Pk_regid"); values.Add(StudentOrAdminRegId); types.Add(SqlDbType.VarChar);
            //   names.Add("@DegreeType"); values.Add(0); types.Add(SqlDbType.VarChar);
            names.Add("@IpAddress"); values.Add(UserIP); types.Add(SqlDbType.VarChar);
            names.Add("@stackTraceDtl"); values.Add(stackTraceDtl); types.Add(SqlDbType.VarChar);
            names.Add("@ExcepMsg"); values.Add(ExcepMsg); types.Add(SqlDbType.VarChar);
            names.Add("@ExcepOccurInMethod"); values.Add(ExcepOccurInMethod); types.Add(SqlDbType.VarChar);
            names.Add("@ExcepOccurInClass"); values.Add(ExcepOccurInClass + " Or " + PageName); types.Add(SqlDbType.VarChar);
            if (DAobj.ExecuteTransactionMsg("[`]", values, names, types, ref Message) > 0)
            {
                Message = DAobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch
        {
            return 0;
        }
    }
}