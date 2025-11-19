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
using SubSonic;
using DataAccessLayer;

public partial class Alumni_ALM_AlumniPayLater : System.Web.UI.Page
{
    private Boolean IsPageRefresh = false;
    crypto cpt = new crypto();

    public StoredProcedure ALM_Detils_grid(int Pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Detils_grid", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_alumniid", Pk_alumniid, DbType.Int32);
        return sp;
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridAluminidetails();
        }
    }

    #region DATA ACCESS LAYER CODE 

    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();

    int _ProgrammeId, _RetRegId, _RegId, _fk_ChallanBankId, _Pk_ExpId, _FreshOrReAppear, _DegreeTypeId, _Fee, _Mode, _UserId, _StudentId;

    double _RegFees;
    string _xmlDoc, _Imgtype, _IpAddress, _Details, _DisQorSuspended, _PaymentOption, _step, _IsSmsOrEmail, _MobileNo, _EmailId, _Regno, _Semister, _DegreeId, _InstituteId, _DegreeYear, _fk_collegeid;
    string _DegreeType, _ChallanRefNo, _SubOrNonSubs, _Option, _CategoryId, _Degreetyid, _SubjectMasterId, _CourseMasterId, _CourseId1, _CourseId2, _StudentSubjectDetailsId;
    long _Ret_Pk_PurchaseId;
    long regid;

    public int RegId
    {
        get { return _RegId; }
        set { _RegId = value; }
    }

    public string IpAddress
    {
        get { return _IpAddress; }
        set { _IpAddress = value; }
    }

    public string XmlDoc
    {
        get { return _xmlDoc; }
        set { _xmlDoc = value; }
    }

    #endregion

    private void FeeDetails(string membertype)
    {
        DataSet Ds = Alm_GetMembershipFee(membertype).GetDataSet();
        if (Ds.Tables[0].Rows.Count > 0)
        {
            //DataRow dr = Ds.Tables[0].NewRow();
            //lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
            Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
        }
    }

    protected void GridAluminidetails()
    {
        if (Session["AlumniID"] != null)
        {
            //if (Session["AlumniID"].ToString() != "")
            //{
            //    int loginId = Convert.ToInt32(Session["AlumniID"].ToString());
            //    DataSet ds = ALM_Detils_grid(loginId).GetDataSet();
            //    dgv.DataSource = ds;
            //    dgv.DataBind();
            //}

            if (Session["AlumniID"].ToString() != "")
            {
                int loginId = Convert.ToInt32(Session["AlumniID"].ToString());
                DataSet ds = ALM_Detils_grid(loginId).GetDataSet();
                ds.Tables[0].Columns.Add("encId");
                int rnum = (ds.Tables[0].Rows.Count) + 1;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string pkid = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();
                    string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                    // ds.Tables[0].Rows[0]["encId"] = encId;

                    ViewState["encId"] = encId;

                    lblAlumni.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();
                    lblMobileNo.Text = ds.Tables[0].Rows[0]["contactno"].ToString();
                    lblEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                    lblAlumniType.Text = ds.Tables[0].Rows[0]["alumnitype"].ToString();
                    lblMembership.Text = ds.Tables[0].Rows[0]["Membership_Type"].ToString();

                    //dgv.DataSource = ds;
                    //dgv.DataBind();
                }
                else
                {
                    ViewState["encId"] = "";
                    Response.Redirect("../Alumin_Loginpage.aspx");
                }
            }
        }
    }

    protected void ForPayment()
    {
        if (!IsPageRefresh)
        {
            //if (rdPaymentOption.SelectedValue == "O")//for online payment
            //{
            //    pnlOnlinePayment.Visible = true;
            //}
            //else// for payment via challan
            //{
            //    pnlOnlinePayment.Visible = false;
            //}
            //btnPay();
        }
    }

    protected void btnPay()
    {
        //SAVE HERE DETAILS IN PURCHASE MST AS USER IS GOING TO CHOOSE PAYMENT GATEWAY OPTIONS OPTIONS
        try
        {
            if (Session["RegFees"] == null)
            {
                //lblPaymentMsg.Text = "Fee is not available. Please Try Again or contact to University!";
                return;
            }

            string Message = "";
            ArrayList Result = new ArrayList();
            DataSet dsMain = GetMain();

            IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
            XmlDoc = dsMain.GetXml();

            if (InsertpaymentRecord(ref Message, ref Result) > 0)
            {
                if (Result.Count > 0)
                {
                    Session["Pk_purchaseid"] = Result[1].ToString().Trim();
                    Session["temp_Pk_purchaseid"] = Result[1].ToString().Trim();
                    Response.Redirect("../Onlinepayment/ALM_Common_PaymentGateway.aspx", false);
                }
            }
            else
            {
                // lblPaymentMsg.Text = Message;
            }
        }
        catch (Exception ex)
        {

        }
    }

    void Clear()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    public int InsertpaymentRecord(ref string Message, ref ArrayList Result)
    {
        Clear();
        names.Add("@xmlDoc"); values.Add(_xmlDoc); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
        names.Add("@IpAddress"); values.Add(_IpAddress); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
        names.Add("@pk_purchaseid"); values.Add(_Ret_Pk_PurchaseId); types.Add(SqlDbType.VarChar); size.Add("10"); outtype.Add("true");

        if (DAobj.ExecuteTransactionMsgIO("[HPU_Alumni_eCoupon_Purchase_Mst_ins]", values, names, types, size, outtype, ref Message, ref Result) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public DataSet Get_All_Details_Of_Alumni()
    {
        Clear();
        names.Add("@Pk_Regid");
        types.Add(SqlDbType.Int);
        values.Add(_RegId);
        return DAobj.GetDataSet("ALM_AlumniRegistration_Details", values, names, types);
    }

    public StoredProcedure Alm_GetMembershipFee(string membertype)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_FeeCollection", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@membershipType", membertype, DbType.String);
        return sp;
    }


    public static StoredProcedure Get_All_Details_Of_Alumni_New(int Pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_AlumniRegistration_Details", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Pk_Regid", Pk_alumniid, DbType.Int32);
        return sp;
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

    protected DataSet GetMain()
    {
        DataSet ds = null;
        try
        {
            Session["regid"] = Session["AlumniID"];

            DataSet dsDetails = Get_All_Details_Of_Alumni_New(Convert.ToInt32(Session["AlumniID"].ToString())).GetDataSet();

            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                ds = GetSchema("HPU_Alumni_eCoupon_Purchase_Mst");
                DataRow dr = ds.Tables[0].NewRow();
                dr["pk_purchaseid"] = "0";
                dr["fk_regid"] = Session["regid"].ToString();//Session["AlumniID"].ToString();
                dr["Entrydate"] = DateTime.Now;
                dr["RegFees"] = Session["RegFees"];
                dr["S_Name"] = dsDetails.Tables[0].Rows[0]["alumni_name"].ToString();
                dr["Email"] = dsDetails.Tables[0].Rows[0]["email"].ToString();
                dr["Mobileno"] = dsDetails.Tables[0].Rows[0]["contactno"].ToString();
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

    protected void btnpayment_Click1(object sender, EventArgs e)
    {
        ForPayment();
    }

    protected void rdPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsDetails = Get_All_Details_Of_Alumni_New(Convert.ToInt32(Session["AlumniID"].ToString())).GetDataSet();
        string membershiptype = dsDetails.Tables[0].Rows[0]["Membership_Type"].ToString();

        FeeDetails(membershiptype);
        //pnlOnlinePayment.Visible = true;
    }

    protected void dgv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void lnkPay_Click(object sender, EventArgs e)
    {
        string ecrptID = ViewState["encId"].ToString();
        Response.Redirect("~/Alumni/ALM_AlumniProfileDetails.aspx?ID=" + ecrptID);
    }
}