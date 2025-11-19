using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using SubSonic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using IUMSNXG;
using DataAccessLayer;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Activities.Statements;
using System.Text.RegularExpressions;

public partial class Alumni_FundDonation : System.Web.UI.Page
{
    private Boolean IsPageRefresh = false;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    DataAccess Dobj = new DataAccess();
	
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    public ArrayList size = new ArrayList();
    public ArrayList outtype = new ArrayList();
    crypto cpt = new crypto();

    public string Contributor_Name { get; set; }
    public string Email { get; set; }
    public string Mobile_No { get; set; }
    public string Donation_amount { get; set; }
    public string Countrycode { get; set; }
    public bool isCheckedananoymus { get; set; }
    public int @Fk_contribution_ID { get; set; }
    public int FkCountyID { get; set; }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    private DataTable FundDetails(int pk_contribution_ID)
    {
        ClearArrayLists();
        names.Add("@pk_contribution_ID"); types.Add(SqlDbType.NVarChar); values.Add(pk_contribution_ID);
        return Dobj.GetDataTable("Alm_Show_Crowd_Fund_Details_by_id", values, names, types);
    }

    private DataTable FundDetailsCat(int pk_contribution_ID)
    {
        ClearArrayLists();
        names.Add("@pk_contribution_ID"); types.Add(SqlDbType.NVarChar); values.Add(pk_contribution_ID);
        return Dobj.GetDataTable("Almm_Crowd_Fund_Details", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                //Page.Form.Attributes.Add("enctype", "multipart/form-data");
                int pk_contribution_ID = Convert.ToInt32(cpt.DecodeString(Request.QueryString["ID"].ToString()));
                ViewState["funddetails"] = pk_contribution_ID;
                if (pk_contribution_ID != 0)
                {
                    ALLRepeter(pk_contribution_ID);
                    RepeterWithcategories(pk_contribution_ID);
                    //repTopContributors(pk_contribution_ID);
                }
            }
            //Details_Repeter();
            FillCountry();
            FillGrid();
        }
    }

    protected void FillCountry()
    {
        DataSet ds = BindCountry();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCountry.DataValueField = "PK_Country_ID";
            ddlCountry.DataTextField = "Country_Name";
            ddlCountry.DataSource = ds;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("-- Select County --", "0"));
            ddlCountry.SelectedIndex = 98;
        }
        else
        {
            ddlCountry.DataSource = null;
            ddlCountry.DataBind();
        }
    }

    private DataSet BindCountry()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_FillCountry", values, names, types);
    }

    private void FillGrid()
    {
        try
        {
            DataSet ds;
            ds = Contributors_Lists_for_Portal();
            //gvConPayHistry.DataSource = ds.Tables[0];
            //gvConPayHistry.DataBind();
            //gvConPayHistry.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    private DataSet Contributors_Lists_for_Portal()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_Contribution_Payment_History_SP", values, names, types);
    }
	
    /// <summary>
    /// Bind Repeater
    /// </summary>
    /// <param name="pk_contribution_ID"></param>
    private void ALLRepeter(int pk_contribution_ID)
    {
        DataTable dt = new DataTable();
        dt = FundDetails(pk_contribution_ID);
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
            lblHeading.Text = dt.Rows[0]["heading"].ToString();
            Session["heading"] = lblHeading.Text;
        }
    }
	
    private void RepeterWithcategories(int pk_contribution_ID)
    {
        DataTable dt = new DataTable();
        dt = FundDetailsCat(pk_contribution_ID);
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            RepOnlydetails.DataSource = dt;
            RepOnlydetails.DataBind();
            coordinatorsDetails.DataSource = dt;
            coordinatorsDetails.DataBind();
        }
    }

    public DataSet Getmain()
    {
        DataSet ds_master = Dobj.GetSchema("ALM_Contributors_Details");
        DataRow dr = ds_master.Tables[0].NewRow();
        dr["Donation_amount"] = Txt_Amount.Text.ToString();
        dr["Contributor_Name"] = txtname.Text.ToString();
        dr["Email"] = Textemail.Text.ToString();
        dr["Mobile_No"] = txtMobile.Text.ToString();
        //dr["fk_cat_id"] = D_ddlCategories.SelectedValue;
        //dr["fk_userid"] = Session["UserID"].ToString();
        ds_master.Tables[0].Rows.Add(dr);
        return ds_master;
    }

    //private void Details_Repeter()
    //{
    //    DataTable dt = new DataTable();
    //    dt = FundDetails();
    //    //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");       
    //    if (dt.Rows.Count > 0)
    //    {
    //        string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
    //        //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
    //        rep.DataSource = dt;
    //        rep.DataBind();
    //    }
    //}

    #region "Other common Events"

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #endregion

    public int Insfunddata(ref string Message)
    {
        try
        {
            names.Add("@Contributor_Name"); values.Add(Contributor_Name); types.Add(SqlDbType.VarChar);
            names.Add("@Email"); values.Add(Email); types.Add(SqlDbType.VarChar);
            names.Add("@Mobile_No"); values.Add(Mobile_No); types.Add(SqlDbType.VarChar);
            names.Add("@Donation_amount"); values.Add(Donation_amount); types.Add(SqlDbType.VarChar);
            names.Add("@Fk_contribution_ID"); values.Add(ViewState["funddetails"]); types.Add(SqlDbType.Int);

            if (Dobj.ExecuteTransactionMsg("Alm_Fund_Data_Ins", values, names, types, ref Message) > 0)
            {
                Message = Dobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public int insertContributorFundData(ref string Message, ref ArrayList Result)
    {
        try
        {
            ClearArrayLists();
            names.Add("@Contributor_Name"); values.Add(Contributor_Name); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
            names.Add("@Email"); values.Add(Email); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
            names.Add("@Mobile_No"); values.Add(Mobile_No); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
            names.Add("@Donation_amount"); values.Add(Donation_amount); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
            names.Add("@Fk_contribution_ID"); values.Add(ViewState["funddetails"]); types.Add(SqlDbType.Int); size.Add("MAX"); outtype.Add("false");
            names.Add("@pk_FundId"); values.Add(regId); types.Add(SqlDbType.VarChar); size.Add("10"); outtype.Add("true");
            names.Add("@Code"); values.Add(Countrycode); types.Add(SqlDbType.VarChar); size.Add("10"); outtype.Add("False");
            names.Add("@Anonymous"); values.Add(isCheckedananoymus); types.Add(SqlDbType.Bit); size.Add("10"); outtype.Add("false");
            names.Add("@FK_countryID"); values.Add(Countrycode); types.Add(SqlDbType.Int); size.Add("10"); outtype.Add("False");
            if (Dobj.ExecuteTransactionMsgIO("ALM_Contributor_Fund_Data_Ins", values, names, types, size, outtype, ref Message, ref Result) > 0)
            {
                //Message = Dobj.ShowMessage("S", "");
                Message = "Record Saved Successfully !";
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    #region "Email Validator"

    protected bool IsEmailIdValid(string EmailId)
    {
        string strEmailReg = @"^([\w-\.]+)@((\[[0-9]{2,3}\.[0-9]{2,3}\.[0-9]{2,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{2,3})(\]?)$";
        if (Regex.IsMatch(EmailId, strEmailReg))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    public bool Validation()
    {
        //if (D_ddlCategories.SelectedIndex == 0)
        //{
        //    ClientMessaging("Categories cannot be blank!");
        //    D_ddlCategories.Focus();
        //    return false;
        //}

        if (string.IsNullOrEmpty(Txt_Amount.Text.ToString()))
        {
            ClientMessaging("Amount cannot be blank!.");
            Txt_Amount.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtname.Text.ToString()))
        {
            ClientMessaging("Name cannot be blank!.");
            txtname.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(Textemail.Text.ToString()))
        {
            ClientMessaging("Email can not be blank!.");
            Textemail.Focus();
            return false;
        }
        else if (Textemail.Text.Trim() != "" && Textemail.Text.Trim() != null && !IsEmailIdValid(Textemail.Text.Trim()))
        {
            ClientMessaging("Invalid Email-Id!.");
            Textemail.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtMobile.Text.ToString()))
        {
            ClientMessaging("Mobile can not be blank!.");
            Anthem.Manager.IncludePageScripts = true;
            txtMobile.Focus();
            return false;
        }
        else if (txtMobile.Text.Trim() != "" && txtMobile.Text.Length < 10)
        {
            ClientMessaging("Invalid Mobile Number!.");
            txtMobile.Focus();
            return false;
        }
        return true;
    }

    public void Save()
    {
        if (Validation())
        {
            try
            {
                string Message = "";
                //    DataSet ds_details = new DataSet();
                //    ds_details = Getmain();
                ////string doc = "";
                ////doc = ds_details.GetXml();
                ////xmldoc = doc;
                ////mode = 1;
                //int a= Convert.ToInt32(mobile.Text);
                //string b= cars.

                Session["ContributorAmt"] = Txt_Amount.Text.ToString();

                @Donation_amount = Txt_Amount.Text.ToString();
                @Contributor_Name = txtname.Text.ToString();
                @Email = Textemail.Text.ToString();
                @Mobile_No = txtMobile.Text.ToString();
                @Fk_contribution_ID = Convert.ToInt32(ViewState["funddetails"]);
                Countrycode = ddlCountry.SelectedValue;
                FkCountyID = Convert.ToInt32(ddlCountry.SelectedIndex);
                isCheckedananoymus = exampleCheck1.Checked;
                //if (Insfunddata(ref Message) > 0)
                //{
                //    ClientMessaging("Record Save Successfully.");
                //}

                ArrayList Result = new ArrayList();
                ipAddress = HttpContext.Current.Request.UserHostAddress.ToString();

                if (insertContributorFundData(ref Message, ref Result) > 0)
                {
                    // ViewState["Result"] = Result.ToString();
                    if (Result.Count > 0)
                    {
                        // ClientMessaging("Record Save Successfully.");
                        Session["pk_fundId"] = Result[1].ToString().Trim();
                        //lblMsg.Text = "Contributor Detail has Been Saved! Please Payment For the Further prosess";
                    }
                }
                else
                {
                    lblMsg.Text = "Retry!";
                }
            }
            catch (Exception Ex)
            {
                Throw E;
            }
        }
    }

    //protected void donate_Click(object sender, EventArgs e)
    //{
    //    if (donate.CommandName.ToString().ToUpper() == "SAVE")
    //    {
    //        Save();
    //        //Clear();
    //        //GrdFile.Visible = false;

    //    }
    //    //else
    //    //{
    //    //    Update();
    //    //    GrdFile.Visible = false;
    //    //    //Fill_grid();
    //    //}
    //}

    protected void donate_Click(object sender, EventArgs e)
    {
        Save();
        ForPayment();
    }

    protected void ForPayment()
    {
        if (!IsPageRefresh)
        {
            if (!Validation())
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Load", "javascript:showPopUpDiv();", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Load", "javascript:hidePopUpDiv();", true);
                btnPay();
            }
        }
    }

    protected void btnPay()
    {
        //SAVE HERE DETAILS IN PURCHASE MST AS USER IS GOING TO CHOOSE PAYMENT GATEWAY OPTIONS OPTIONS
        try
        {
            if (Session["pk_fundId"] != null)
            {
                string Message = "";
                ArrayList Result = new ArrayList();

                DataSet dsMain = new DataSet();
                dsMain = GetMainP();
                string dsXml = dsMain.GetXml();
                //string Location = Session["LocationID"].ToString().Trim();
                DataSet ds = InsPublic_Contribution_Record(dsXml);
                string Pk_Trandtl_Id = Convert.ToString(ds.Tables[1].Rows[0]["Pk_Trandtl_Id"]);
                Session["pk_TransId"] = Pk_Trandtl_Id;

                SentToSbi();
            }
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// Insert Data on Btnpay Click
    /// </summary>
    /// <returns></returns>

    protected DataSet GetMain()
    {
        DataSet ds = null;
        try
        {
            contributorId = Convert.ToInt32(Session["pk_fundId"].ToString());

            DataSet dsDetails = Get_All_Details_Of_Contributor();

            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                ds = GetSchema("HPU_Alumni_eCoupon_Purchase_Mst");
                DataRow dr = ds.Tables[0].NewRow();
                dr["pk_purchaseid"] = "0";
                dr["fk_fundId"] = Session["pk_fundId"].ToString();
                dr["Entrydate"] = DateTime.Now;
                dr["RegFees"] = Session["ContributorAmt"];
                dr["S_Name"] = dsDetails.Tables[0].Rows[0]["Contributor_Name"].ToString();
                dr["Email"] = dsDetails.Tables[0].Rows[0]["Email"].ToString();
                dr["Mobileno"] = dsDetails.Tables[0].Rows[0]["Mobile_No"].ToString();
                ds.Tables[0].Rows.Add(dr);
                dr = null;
                return ds;
            }
            else
                return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }
    }

    int _contributorId;
    string _xmlDoc, _ipAddress, _Code;
    long _Ret_Pk_FundId, regId;

    public int contributorId
    {
        get { return _contributorId; }
        set { _contributorId = value; }
    }

    public string ipAddress
    {
        get { return _ipAddress; }
        set { _ipAddress = value; }
    }

    public string Code
    {
        get { return _Code; }
        set { _Code = value; }
    }

    public string xmlDoc
    {
        get { return _xmlDoc; }
        set { _xmlDoc = value; }
    }

    public int insertContributionPaymentRecord(ref string Message, ref ArrayList Result)
    {
        ClearArrayLists();
        names.Add("@xmlDoc"); values.Add(_xmlDoc); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
        names.Add("@IpAddress"); values.Add(_ipAddress); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
        names.Add("@pk_purchaseid"); values.Add(_Ret_Pk_FundId); types.Add(SqlDbType.VarChar); size.Add("10"); outtype.Add("true");

        if (Dobj.ExecuteTransactionMsgIO("[ALM_Alumni_eCoupon_Purchase_Mst_Ins_SP]", values, names, types, size, outtype, ref Message, ref Result) > 0)
        {
            Message = Dobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public DataSet GetSchema(string TableName)
    {
        try
        {
            ClearArrayLists();
            return Dobj.GetSchema(TableName);
        }
        catch { throw; }
    }

    public DataSet Get_All_Details_Of_Contributor()
    {
        ClearArrayLists();
        names.Add("@contributorId");
        types.Add(SqlDbType.Int);
        values.Add(_contributorId);
        return Dobj.GetDataSet("ALM_Alumni_Contributor_Details", values, names, types);
    }

    public DataSet InsPublic_Contribution_Record(string dsXml)
    {
        DataSet ds = new DataSet();
        DataAccess obj = new DataAccess();
        try
        {
            this.ClearArrayLists();
            names.Add("@doc"); values.Add(dsXml); types.Add(SqlDbType.VarChar);
            ds = obj.GetDataSet("ALM_Contribution_PaymentTransaction_Dtl_Ins", values, names, types);
        }
        catch (Exception ex)
        {
            return null;
        }
        return ds;
    }

    protected DataSet GetMainP()
    {
        DataSet dsMain;
        dsMain = Dobj.GetSchema("Common_PaymentTransaction_Dtl");
        DataRow dr_main = dsMain.Tables[0].NewRow();
        dr_main["Pk_Trandtl_Id"] = "0";

        if (Session["pk_fundId"] != null)
        {
            dr_main["Fk_Personid"] = Session["pk_fundId"].ToString();
        }

        dr_main["Fk_Contributionid"] = ViewState["funddetails"].ToString();
        dr_main["Amount"] = Session["ContributorAmt"];
        dr_main["Product_Info"] = "CONTRIBUTION FUND";
        dr_main["IsActive"] = false;
        dr_main["Insert_Date"] = DateTime.Now;
        dr_main["Update_Date"] = DateTime.Now;

        if (Session["pk_fundId"] != null)
        {
            dr_main["Insert_Userid"] = Session["pk_fundId"].ToString();
            dr_main["Update_Userid"] = Session["pk_fundId"].ToString();
        }

        //dr_main["CompanyName"] = R_txtemailid.Text.Trim();
        // DateTime now = DateTime.Now;
        dsMain.Tables[0].Rows.Add(dr_main);
        return dsMain;
    }

    private void SentToSbi()
    {
        try
        {
            //long _fk_regid = Convert.ToInt64(Session["regid"].ToString());
            //// string _DegType = Session["DegreeType"].ToString();
            //string _TransactionID = Session["pk_TransId"].ToString();
            //DataSet ds = Get_Payer_StudentDetails(_fk_regid, _TransactionID);

            //int FK_Inter_Coll_Mig_Id = Convert.ToInt32(Session["FK_Inter_Coll_Mig_Id"]);
            // long FK_RegId = Convert.ToInt32(Session["regid"].ToString()); //Convert.ToInt32(Session["AlumniID"].ToString()); 
            string Transactionid = Session["pk_TransId"].ToString();
            // DataSet ds = Get_Payer_StudentDetails(FK_RegId, Transactionid);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            /*temprorary commented by ashish*/
            string returnurl = ConfigurationManager.AppSettings["s_sbi_url_con1"].ToString();
            string Pk_TranID = Session["pk_TransId"].ToString();
            string amount = Convert.ToDecimal(Session["ContributorAmt"]).ToString();
            string ProductInfo = string.Empty;

            ProductInfo = "CONTRIBUTION FUND";
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
            //}
        }
        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");
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

    #region DAL

    DataAccess DAobj = new DataAccess();

    #endregion

    private void repTopContributors(int pk_contribution_ID)
    {
        DataTable dt = new DataTable();
        dt = GetTopContributors(pk_contribution_ID);
        if (dt.Rows.Count > 0)
        {
            //repTopContributor.DataSource = dt;
            //repTopContributor.DataBind();
        }
    }

    private DataTable GetTopContributors(int pk_contribution_ID)
    {
        ClearArrayLists();
        names.Add("@pk_contribution_ID"); types.Add(SqlDbType.NVarChar); values.Add(pk_contribution_ID);
        return Dobj.GetDataTable("ALM_Get_Top_Contributors_Shows", values, names, types);
    }
}