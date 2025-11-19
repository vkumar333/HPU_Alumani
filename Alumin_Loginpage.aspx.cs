using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Net.Security;
using DataAccessLayer;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using SubSonic;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.HtmlControls;
using AESEncdec;

public partial class Alumni_Alumin_Loginpage : System.Web.UI.Page
{
    crypto cpt = new crypto();
    string encType;

    protected void Page_PreInit()
    {
        Page.Theme = "CCSBLUE";
    }

    // protected void Page_PreRender(object sender, EventArgs e)
    // {
    // // Handle form submission here
    // if (IsPostBack)
    // {
    // string turnstileResponse = Request.Form["cf-turnstile-response"];
    // if (!ValidateTurnstile(turnstileResponse))
    // {
    // // Handle validation failure
    // ClientMessaging("Turnstile validation failed. Please try again.");
    // return;
    // }
    // }
    // }

    private bool ValidateTurnstile(string response)
    {
        string secretKey = "0x4AAAAAAAyYmtmJC3_OVN2FYIh5a7GjI5A";
        string url = "https://challenges.cloudflare.com/turnstile/v0/siteverify?secret={secretKey}&response={response}";

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                ClientMessaging(result);
                return result.success;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("RBValue");
            //Session["RBValue"] = "ExStu";
            //Session["RBValue"] = cpt.Encrypt("ExStu");

            FillCapctha();

            CheckStatusOfAllPreviousTransaction();

            R_txtLogin.Text = "";
            R_txtPass.Text = "";
            lblError.Text = "";
            pnlQR.Visible = false;
        }
    }

    protected void BindRadioBtnLists()
    {
        try
        {
            DataSet ds = FillRadioBtnList().GetDataSet();
            rdalumnitype.DataSource = ds.Tables[0];
            rdalumnitype.DataTextField = "Descriptions";
            rdalumnitype.DataValueField = "pk_feeid";
            rdalumnitype.DataBind();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Descriptions"].ToString() == "Ex-Student")
                {
                    rdalumnitype.Items[i].Selected = true;
                }
                else
                {
                    rdalumnitype.Items[i].Selected = false;
                }
            }
        }
        catch (Exception e)
        {
            lblMsg.Text = e.Message;
        }
    }

    protected void imgLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string toSum = CommonFunction.ReturnTextifNotBlank(R_txtCaptcha);

            if (toSum != Session["captcha"].ToString())
            {
                String script = String.Format("alert('{0}');", "Invalid captcha!");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                R_txtCaptcha.Text = "";
                R_txtCaptcha.Focus();

                //ViewState["txtCaptc"] = null;
                Session["captcha"] = null;
                // string Imsg = "";
                //Insert_Payment_Attempt_Log(ref Imsg, "100", "step 100 Invalid Captcha!");
                FillCapctha();
                CheckStatusOfAllPreviousTransaction();
                //GenerateCaptcha();
                return;
            }
            if (R_txtLogin.Text.Trim() == "")
            {
                ClientMessaging("Please Enter User ID");
                String script = String.Format("alert('{0}');", "Please Enter Login Name!");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                R_txtLogin.Focus();

                return;
            }
            if (R_txtPass.Text.Trim() == "")
            {
                ClientMessaging("Please Enter Password");
                String script = String.Format("alert('{0}');", "Please Enter Password!");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                R_txtPass.Focus();

                return;
            }
            if (R_txtCaptcha.Text.Trim() == "")
            {
                String script = String.Format("alert('{0}');", "Please Enter Captcha!");
                Anthem.Manager.IncludePageScripts = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
                R_txtPass.Focus();
                return;
            }

            //string str = cp.Encrypt(R_txtPass.Text.Trim());
            //DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_LoginAuthentication(R_txtLogin.Text.Trim(), cp.Encrypt(R_txtPass.Text.Trim())).GetDataSet();
            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_LoginAuthentication(R_txtLogin.Text.Trim(), R_txtPass.Text.Trim()).GetDataSet();
            if (ds.Tables[0].Rows[0]["cnt"].ToString() != "0")
            {

                Session["AlumniID"] = ds.Tables[1].Rows[0]["pk_alumniid"].ToString();
                Session["Alumnino"] = ds.Tables[1].Rows[0]["alumnino"].ToString();
                Session["AlumniName"] = ds.Tables[1].Rows[0]["alumni_name"].ToString();
                Session["ProfileId"] = ds.Tables[1].Rows[0]["fk_ProfileID"].ToString();

                GetImageDetail(ds.Tables[1]);

                if (ds.Tables[1].Rows[0]["isDisabled"].ToString() == "Y" && ds.Tables[1].Rows[0]["isDisabilityPercentage"].ToString() == "True")
                {
                    Response.Redirect("Alumni/ALM_Alumni_Home.aspx");
                }
                else if (ds.Tables[1].Rows[0]["isDisabled"].ToString() == "N")
                {
                    CheckStatusOfAllPreviousTransaction();
                    Response.Redirect("Alumni/ALM_Alumni_Home.aspx");
                }
            }
            else
            {
                lblError.Text = "Invalid User Name or Password! or Your Login Details is not approved yet. Please contact your admin for approval";
                Session["AlumniID"] = "";
                Session["Alumnino"] = "";
                Session["AlumniName"] = "";
                Session["Emailid"] = "";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Problem in Login! , Please Try again!";
            R_txtLogin.Text = "";
            R_txtPass.Text = "";
            R_txtLogin.Focus();
        }
    }

    protected void SendMail(string values, string splitMailCC)
    {
        try
        {
            lblMsg.Text = "";

            StringBuilder strBody = new StringBuilder();
            string emailTo = Session["Emailid"].ToString();
            strBody.Append("<Table border='0' cellpadding='0' cellspacing='0' style='width:75%;' align='left'>");

            strBody.Append("<Tr>");
            strBody.Append("<Td  style='font-size: 15px;font-family: Verdana;height: 14px;'>");
            strBody.Append("<B>" + "Dear " + ViewState["Alumni_Name"] + "," + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td>");
            strBody.Append("<B>" + "</B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td>");
            strBody.Append("<B> Please find your password below </B>");
            strBody.Append("</Td>");
            strBody.Append("</Tr>");

            strBody.Append("<Tr>");
            strBody.Append("<Td  style='font-size: 15px;font-family: Verdana;height: 14px;'>");
            strBody.Append("<B>" + "password is " + ViewState["Password"] + "" + "</B>");
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
            SendMail(splitMailCC, emailTo, strBody);
        }
        catch (Exception e)
        {

        }
    }

    /// <summary>
    /// Send Mail Function
    /// Created by aditya sharma
    /// </summary>
    /// <param name="splitMailCC"></param>
    /// <param name="email"></param>
    /// <param name="strBody"></param>
    private void SendMail(string splitMailCC, string email, StringBuilder strBody)
    {
        try
        {
            string Body = "";

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("alumni.hpushimla@gmail.com");

            mail.To.Add(email);
            mail.Subject = "Recovery of password";
            mail.Body = strBody.ToString();

            // if (splitMailCC != "")
            //   mail.CC.Add(splitMailCC);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("alumni.hpushimla@gmail.com", "hlhzhzanylwsdbza");
            SmtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            SmtpServer.Send(mail);
        }
        catch (Exception e)
        {

        }
    }

    //Sending Mail For Forgot Password

    private void GetImageDetail(DataTable dt)
    {
        DataTable dtimg = new DataTable();
        dtimg.Columns.Add("pk_alumniid", typeof(int));
        dtimg.Columns.Add("imgattach", typeof(byte));
        dtimg.Columns.Add("filename", typeof(string));
        dtimg.Columns.Add("Contenttype", typeof(string));
        DataView view = new DataView(dt);
        dtimg = view.ToTable("Selected", false, "pk_alumniid", "imgattach", "filename", "Contenttype");
        if (dtimg.Rows.Count > 0)
        {
            Session["AlumniImg"] = dtimg;
        }
    }
    /// <summary>
    /// send mail for forgot password
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        // lblMsg.Text = "";
        if (R_txtUsername.Text.ToString() == "")
        {
            String script = String.Format("alert('{0}');", "Please Enter User Name!");
            Anthem.Manager.IncludePageScripts = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
            R_txtUsername.Focus();
            return;
        }
        //if (E_txtEmail.Text.ToString() == "")
        //{
        //    String script = String.Format("alert('{0}');", "Please Enter Email id !");
        //    Anthem.Manager.IncludePageScripts = true;
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        //    R_txtUsername.Focus();
        //    return;
        //}
        try
        {
            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_Alumni_Forgotpassword(R_txtUsername.Text.ToString(), E_txtEmail.Text.ToString()).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Alumni_Name"] = ds.Tables[0].Rows[0]["Alumni_Name"].ToString();
                ViewState["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                Session["Emailid"] = ds.Tables[0].Rows[0]["email"].ToString();
                string splitMailCC = "";
                SendMail("a", splitMailCC);
                lblMsg.Text = "Mail Send Successfully";
                E_txtEmail.Text = string.Empty;
                R_txtUsername.Text = string.Empty;
            }
            else
            {
                lblMsg.Text = "Please enter valid credentials";
                return;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }
    protected void GenerateCaptcha()
    {
        Random r = new Random();
        int a = r.Next(10) + 5;
        int b = r.Next(10) + 10;
        //lblCaptch.Text = "";
        //lblCaptch.Text = "<B>What is ? " + a + " + " + b + "</B>";
        int X = a + b;
        ViewState["txtCaptc"] = X.ToString();
    }

    /// <summary>
    /// Fill Captcha
    /// </summary>
    protected void FillCapctha()
    {
        try
        {
            //   char[] identifier = new char[5];
            Random random = new Random();
            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 5; i++)
                //identifier[i] = (combination[random.Next(combination.Length)]);
                captcha.Append(combination[random.Next(combination.Length)]);
            // string s = string.Join("", identifier);
            Session["captcha"] = captcha.ToString();

            //Session["captcha"] = s.ToString();
            imgCaptcha.ImageUrl = "ImCaptch.aspx?" + DateTime.Now.Ticks.ToString();
        }
        catch
        {

            throw;
        }
    }

    protected void imgReferesh_Click(object sender, ImageClickEventArgs e)
    {
        FillCapctha();
    }
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();
    DataTable dsmain = new DataTable();
    string _xmlDoc, _IpAddress, _Fee, _fk_regid, _ProductInfo, _Ret_Pk_Tranid, _Pk_TranId;
    int _MisId, _PK_PaymentGatewayId;
    long _PK_PurchaseId;
    public long PK_PurchaseId { get { return _PK_PurchaseId; } set { _PK_PurchaseId = value; } }
    public string ProductInfo { get { return _ProductInfo; } set { _ProductInfo = value; } }
    public int FK_Inter_Coll_Mig_Id { get; set; }
    public int FK_RegId { get; set; }
    public string XmlDoc
    {
        get { return _xmlDoc; }
        set { _xmlDoc = value; }
    }
    public string IpAddress
    {
        get { return _IpAddress; }
        set { _IpAddress = value; }
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
    public DataSet GetSchema(string TableName)
    {
        try
        {
            Clear();
            return DAobj.GetSchema(TableName);
        }
        catch { throw; }
    }

    public string Pk_TranId { get { return _Pk_TranId; } set { _Pk_TranId = value; } }
    public string RegNo { get; set; }
    public string RefNo { get; set; }
    private bool IspageReferesh = false;
    //public int FK_RegId { get; set; }

    public string Fk_regid
    {
        get { return _fk_regid; }
        set { _fk_regid = value; }
    }

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
        //names.Add("@pk_purchaseid"); values.Add(_PK_PurchaseId); types.Add(SqlDbType.BigInt);
        names.Add("@Pk_Tranid"); values.Add(_Pk_TranId); types.Add(SqlDbType.VarChar);
        names.Add("@Pk_GatewayId"); values.Add(_PK_PaymentGatewayId); types.Add(SqlDbType.Int);
        if (DAobj.ExecuteTransactionMsg("ALM_Online_Payment_Upd_ForAlumni", values, names, types, ref Message) > 0)
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

    /// <summary>
    /// check previous transations
    /// </summary>
    public void CheckStatusOfAllPreviousTransaction()
    {
        try
        {
            string Msg = "";

            long regid = Convert.ToInt64(Session["AlumniID"]);

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
                                //Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);

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
                                        drMain["fk_purchaseid"] = Convert.ToInt64(dr["Fk_purchaseid"].ToString());
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
                                        lblPaymentMsg.Text = "";
                                        //lblPaymentMsg.Text = "Your Previous payment is <B>Success</B> Now visit to the status page.";
                                        //btnPay.Visible = false;
                                        ViewState["IsShowPayButton"] = "0";
                                        //dgDetails.DataSource = dtGetAllStatus;
                                        //dgDetails.DataBind();
                                        //step 6 current status of payment is success user is blocked from making new payment
                                        //Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), dr["TransactionID"].ToString(), Convert.ToInt32(dr["Fk_PaymentGId"]), "step 3 Pk_TranId is sent to SBI gateway for double verification", ref Msg);
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
                                            lblPaymentMsg.Text = "";
                                            //lblPaymentMsg.Text = "Wait for atleast 24 Hrs to get previous transaction Status. as Transaction status is  pending. Then try again!";
                                            //btnPay.Visible = false;
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
                //dgDetails.DataSource = dtGetAllStatus;
                //dgDetails.DataBind();
            }
            else
            {
                //setp 1 first time user is going to pay
                //btnPay.Visible = true;
                ViewState["IsShowPayButton"] = "1";
                //Insert_DoubleVerification_log(Convert.ToInt64(Session["regid"].ToString()), "0", 0, "setp 1 first time user is going to pay", ref Msg);
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

    protected void rdalumnitype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(rdalumnitype.SelectedValue))
        {
            ClientMessaging("Please select an alumni type before proceeding with the registration.");
            rdalumnitype.Focus();
            return;
        }
        else
        {
            //Session["RBValue"] = rdalumnitype.SelectedItem.Value;  
            //Session["RBValue"] = cpt.Encrypt(rdalumnitype.SelectedItem.Value.ToString());

            #region ""

            pnlQR.Visible = true;

            string selectedType = rdalumnitype.SelectedValue.Trim().ToString();
            //string registrationUrl = "https://alumni.hpushimla.in/Alumni/ALM_AlumniRegistration.aspx?type=";
            //string encSelectedAlumniType = cpt.Encrypt(selectedType);
            //string encSelectedAlumniType =  AesEnc.EncryptStringToBytes_Aes(selectedType);
            Session["RBValue"] = selectedType;

            //string qrScannerTypewiseFileName = "";

            string imgQRPath = ReturnPhysicalPathQRScanner() + "\\Alumni\\QR_Scanners\\";

            switch (selectedType)
            {
                case "F":
                    imgQR.ImageUrl = GetBase64Image(imgQRPath + "qr_faculty.png");
                    break;
                case "S":
                    imgQR.ImageUrl = GetBase64Image(imgQRPath + "qr_currentstudent.png");
                    break;
                case "ExStu":
                    imgQR.ImageUrl = GetBase64Image(imgQRPath + "qr_exstudent.png");
                    break;
                case "StuCh":
                    imgQR.ImageUrl = GetBase64Image(imgQRPath + "qr_studentchapter.png");
                    break;
                default:
                    imgQR.Visible = false;
                    qrPlaceholder.Style["display"] = "none";
                    break;
            }

            //switch (selectedType)
            //{
            //    case "F":
            //        registrationUrl = registrationUrl + "faculty";
            //        qrScannerTypewiseFileName = "qr_faculty";
            //        break;
            //    case "S":
            //        registrationUrl = registrationUrl + "currentstudent";
            //        qrScannerTypewiseFileName = "qr_currentstudent";
            //        break;
            //    case "ExStu":
            //        registrationUrl = registrationUrl + "exstudent";
            //        qrScannerTypewiseFileName = "qr_exstudent";
            //        break;
            //    case "StuCh":
            //        registrationUrl = registrationUrl + "studentchapter";
            //        qrScannerTypewiseFileName = "qr_studentchapter";
            //        break;
            //    default:
            //        imgQR.Visible = false;
            //        qrPlaceholder.Style["display"] = "none";
            //        break;
            //}

            //GenerateMyScannerQRCode(registrationUrl);

            //imgQR.Visible = true;
            //qrPlaceholder.Style["display"] = "block";

            //SaveQRCodeImage(registrationUrl, qrScannerTypewiseFileName);

            Anthem.Manager.IncludePageScripts = true;

            #endregion
        }
    }

    protected void btnSend_Click1(object sender, EventArgs e)
    {

    }

    protected void alm_registration_Click(object sender, EventArgs e)
    {
        //string almtype = "";
        //almtype = Session["RBValue"].ToString();
        //Response.Redirect("Alumni/ALM_AlumniRegistration.aspx?id="+ almtype);

        if (string.IsNullOrEmpty(rdalumnitype.SelectedValue))
        {
            ClientMessaging("Please select an alumni type before proceeding with the registration.");
            rdalumnitype.Focus();
            return;
        }

        string almtype = rdalumnitype.SelectedValue.Trim().ToString();
        //Session["RBValue"] = cpt.Encrypt(almtype);
        //Session["RBValue"] = almtype; //AesEnc.EncryptStringToBytes_Aes(almtype);

        string selectedAlumniType = string.Empty;

        switch (almtype)
        {
            case "F":
                selectedAlumniType = "faculty";
                break;
            case "S":
                selectedAlumniType = "currentstudent";
                break;
            case "ExStu":
                selectedAlumniType = "exstudent";
                break;
            case "StuCh":
                selectedAlumniType = "studentchapter";
                break;
            default:
                Response.Redirect("../Alumin_Loginpage.aspx");
                break;
        }

        Session["RBValue"] = selectedAlumniType;

        if (Session["RBValue"] == null)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
        else
        {
            Response.Redirect("~//Alumni//ALM_AlumniRegistration.aspx?type=" + selectedAlumniType);
        }
    }

    public static StoredProcedure FillRadioBtnList()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Registration_CategoryWise_Fee_Mst_SelActive", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    #region "Method to generate and display the QR code"

    //private void GenerateMyScannerQRCode(string qrCodeText)
    //{
    //    try
    //    {
    //        var QCwriter = new BarcodeWriter();
    //        QCwriter.Format = BarcodeFormat.QR_CODE;
    //        QCwriter.Options = new EncodingOptions
    //        {
    //            Width = 300,
    //            Height = 300,
    //            Margin = 1
    //        };
    //        var qrImage = QCwriter.Write(qrCodeText);
    //        imgQR.Src = "data:image/jpeg;base64," + ImageToBase64(qrImage, System.Drawing.Imaging.ImageFormat.Png);
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientMessaging("Error generating QR code: " + ex.Message);
    //    }
    //}

    protected void GenerateMyScannerQRCode(string qrCodeText)
    {
        try
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 1
                }
            };

            using (Bitmap qrImage = writer.Write(qrCodeText))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    qrImage.Save(ms, ImageFormat.Png); // keep PNG for base64
                    byte[] byteImage = ms.ToArray();
                    string base64Image = Convert.ToBase64String(byteImage);

                    //imgQR.Src = "data:image/png;base64," + base64Image;
                    imgQR.ImageUrl = "data:image/png;base64," + base64Image;
                }
            }

            qrPlaceholder.Style["display"] = "none";
            imgQR.Visible = true;
        }
        catch (Exception ex)
        {
            qrPlaceholder.Style["display"] = "block";
            imgQR.Visible = false;
            qrPlaceholder.InnerText = "Error generating QR: " + ex.Message;
        }
    }

    #endregion

    #region "Convert Image to Base64 string for displaying on the page"

    private string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
    {
        using (var ms = new System.IO.MemoryStream())
        {
            image.Save(ms, format);
            byte[] imageBytes = ms.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }

    #endregion

    #region "For restricted file url access"

    public string GetBase64Image(string imagePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        string mimeType = System.Web.MimeMapping.GetMimeMapping(imagePath);
        return string.Format("data:{0};base64,{1}", mimeType, base64String);
    }

    #endregion 

    #region "Method to save the generated QR code image"

    private void SaveQRCodeImage(string qrCodeText, string alumniType)
    {
        try
        {
            UploadQRScanners();

            string qrCodeImgFolderPath = this.upldPath;
            string QR = alumniType;

            if (!Directory.Exists(qrCodeImgFolderPath))
            {
                Directory.CreateDirectory(qrCodeImgFolderPath);
            }

            var QCwriter = new BarcodeWriter();
            QCwriter.Format = BarcodeFormat.QR_CODE;
            QCwriter.Options = new EncodingOptions
            {
                Width = 300,
                Height = 300,
                Margin = 1
            };
            var result = QCwriter.Write(qrCodeText);
            string path = qrCodeImgFolderPath + QR + ".png";
            var barcodeBitmap = new Bitmap(result);

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    bool IsExistPath = System.IO.Directory.Exists(qrCodeImgFolderPath);
                    if (!IsExistPath)
                        System.IO.Directory.CreateDirectory(qrCodeImgFolderPath);

                    barcodeBitmap.Save(memory, ImageFormat.Png);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            imgQR.Visible = true;
            string imgQRPath = ReturnPhysicalPathQRScanner() + "\\Alumni\\QR_Scanners\\" + QR + ".png";
            string base64imgQR = GetBase64Image(imgQRPath);
            imgQR.ImageUrl = base64imgQR;
        }
        catch (Exception ex)
        {
            //Console.WriteLine("Error saving QR code: " + ex.Message);
        }
    }

    #region "Global Variable"
    string upldPath = "";
    DataSet dsFile = null;

    #endregion

    public void UploadQRScanners()
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    upldPath = dr["Physical_Path"].ToString().Trim();
                    upldPath = upldPath + "\\Alumni\\QR_Scanners" + "\\";
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    #endregion

    public string ReturnPhysicalPathQRScanner()
    {
        try
        {
            string host = HttpContext.Current.Request.Url.Host;
            DataSet dsFilepath = new DataSet();
            dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
            foreach (DataRow dr in dsFilepath.Tables[0].Rows)
            {
                if (host == dr["Server_Ip"].ToString().Trim())
                {
                    return dr["Physical_Path"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}