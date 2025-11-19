using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.IO;
using DataAccessLayer;
using System.Linq;
using SubSonic;
using System.Text.RegularExpressions;
using CuteWebUI;
using System.Web.UI.WebControls;

public partial class Alumni_ALM_AlumniProfileDetails : System.Web.UI.Page
{
    #region Data_Work

    DataAccess Dobj = new DataAccess(); crypto cpt = new crypto();
    string upldPath = "";
    DataSet dsFile = null;

    public string alumni_Type { get; private set; }
    public string Membershiptype { get; private set; }

    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();

    void Clear()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    public int pk_degreeid { get; private set; }

    int _RegId;

    string _xmlDoc, _Imgtype, _IpAddress;

    long _Ret_Pk_PurchaseId;

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

    public string alumniType { get; private set; }

    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure Bind_subject()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_Bind_subject", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_degreeid", pk_degreeid, DbType.Int32);
        return sp;
    }

    public DataSet ALM_ACD_Degree_Mst_sel()
    {
        Clear();
        return DAobj.GetDataSet("ALM_ACD_Degree_Mst_sel", values, names, types);
    }

    public DataSet ALM_Department_sel()
    {
        Clear();
        return DAobj.GetDataSet("ALM_Department_sel", values, names, types);
    }

    #endregion

    // crypto crp = new crypto();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    //protected void Page_PreInit()
    //{
    //    if ((Session["DDOID"] != null || Session["LocationID"] != null) && (Session["UserID"] != null && Session["UserID"].ToString() != "") && (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != ""))
    //    {
    //        Page.MasterPageFile = "~/UMM/MasterPage.master";
    //        int check = int.Parse(alm_alumni_check(int.Parse(Request.QueryString["id"])).GetDataSet().Tables[0].Rows[0][0].ToString());
    //        if (check < 1)
    //        {
    //            btnRegister_Click(null, null);
    //            Response.Redirect("../Alumin_Loginpage.aspx");
    //        }

    //       Page.Theme = "CCSBLUE";
    //    }
    //    if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
    //    {
    //        int check = int.Parse(alm_alumni_check(int.Parse(Session["AlumniID"].ToString())).GetDataSet().Tables[0].Rows[0][0].ToString());
    //        if (check < 1)
    //        {
    //            Response.Redirect("../Alumin_Loginpage.aspx");
    //        }
    //        // Page.MasterPageFile = "~/Alumni/Alm_registration_master.master";
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    string decId = cpt.DecodeString(Request.QueryString["ID"].ToString());
                    int pk_alumniid = Convert.ToInt32(decId);
                    ViewState["Pic"] = ""; ViewState["Doc"] = "";
                    if (pk_alumniid != 0)
                    {
                        FillDegree();
                        pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
                        getsubject(pk_degreeid);
                        Anthem.Manager.IncludePageScripts = true;
                        FillForUpdate(pk_alumniid);
                        ControlReadonly();
                    }
                }

                //if (Request.QueryString["u"] != null && Request.QueryString["u"] == "1")
                //{
                //    lblMsg.Text = "Record Updated Successfully!";
                //}

                //if (Request.QueryString["id"] != null)
                //{
                //    Session["EmpView_AlumniID"] = Request.QueryString["id"].ToString();
                //}

                if (Session["AlumniID"] == null)
                {
                    Response.Redirect("../Alumin_Loginpage.aspx");

                }
                // FillCollege();
                //if (Session["AlumniID"] != null)
                //{
                //    FillDegree();
                //    pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
                //    getsubject(pk_degreeid);
                //    Anthem.Manager.IncludePageScripts = true;
                //    txtAlumniName.Focus();
                //    FillForUpdate();
                //    ControlReadonly();
                //}
            }
            catch (Exception ex)
            {
                string Error = ex.Message.ToString();
                ClientMessaging(Error);
            }
        }
    }

    protected void FillSalution()
    {
        DataSet ds = ALM_ALM_ACD_Salutation_fill().GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            Drp_Alumni_Name.DataValueField = "PK_Salutation_ID";
            Drp_Alumni_Name.DataTextField = "Salutation_Name";
            Drp_Alumni_Name.DataSource = ds;
            Drp_Alumni_Name.DataBind();
            Drp_Alumni_Name.Items.Insert(0, new ListItem("- Title -", "0"));

            Drp_FatherName.DataValueField = "PK_Salutation_ID";
            Drp_FatherName.DataTextField = "Salutation_Name";
            Drp_FatherName.DataSource = ds;
            Drp_FatherName.DataBind();
            Drp_FatherName.Items.Insert(0, new ListItem("- Title -", "0"));

            Drp_MotherName.DataTextField = "Salutation_Name";
            Drp_MotherName.DataValueField = "PK_Salutation_ID";
            Drp_MotherName.DataSource = ds;
            Drp_MotherName.DataBind();
            Drp_MotherName.Items.Insert(0, new ListItem("- Title -", "0"));
        }
    }

    private void FillDegree()
    {
        DataSet ds = new DataSet();
        ds = ALM_ACD_Degree_Mst_sel();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldegree.DataValueField = "pk_degreeid";
            ddldegree.DataTextField = "degree";
            ddldegree.DataSource = ds;
            ddldegree.DataBind();
            ddldegree.Items.Insert(0, new ListItem("-- Select Degree --", "0"));
        }
        else
        {
            ddldegree.DataSource = null;
            ddldegree.DataBind();
        }
    }

    private void getsubject(int pk_degreeid)
    {
        DataSet ds = Bind_subject().GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            subjectlist.DataValueField = "pk_subjectid";
            subjectlist.DataTextField = "subject";
            subjectlist.DataSource = ds;
            subjectlist.DataBind();
            subjectlist.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        }
        else
        {
            subjectlist.DataSource = null;
            subjectlist.DataBind();
        }
    }

    private void FillDepartment()
    {
        DataSet ds = new DataSet();
        ds = ALM_Department_sel();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepartment.DataValueField = "Pk_deptId";
            ddlDepartment.DataTextField = "description";
            ddlDepartment.DataSource = ds;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        }
        else
        {
            ddlDepartment.DataSource = null;
            ddlDepartment.DataBind();
        }
    }

    //protected void Fill_PassingYear()
    //{
    //    D_ddlYeofPass.Items.Clear();

    //    DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();

    //    if (ds.Tables[1].Rows.Count > 0)
    //    {
    //        D_ddlYeofPass.DataSource = ds.Tables[1];
    //        D_ddlYeofPass.DataTextField = "PassingYear";
    //        D_ddlYeofPass.DataValueField = "PassingYear";
    //        D_ddlYeofPass.DataBind();
    //        D_ddlYeofPass.Items.Insert(0, "-- Select Year of Passing -- ");
    //    }
    //    else
    //    {
    //        D_ddlYeofPass.Items.Insert(0, "-- Select Year of Passing -- ");
    //    }
    //}

    protected void Fill_PassingYear()
    {
        D_ddlYeofPass.Items.Clear();
        DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();
        if (ds.Tables[2].Rows.Count > 0)
        {
            D_ddlYeofPass.DataSource = ds.Tables[2];
            D_ddlYeofPass.DataTextField = "PassYear_Name";
            D_ddlYeofPass.DataValueField = "Pk_pass_id";
            D_ddlYeofPass.DataBind();
            D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
        }
        else
        {
            D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
        }
    }

    protected void FillForUpdate(int alumniid)
    {
        try
        {
            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(alumniid).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["alumnitype"].ToString() != null)
                {
                    alumni_Type = ds.Tables[0].Rows[0]["alumnitype"].ToString();
                    rbdAlumniMemType_SelectedIndexChanged(null, null);
                }

                // GetImageDetail(ds.Tables[0]); //change home page image as per latest image

                R_txtAlumnino.Text = ds.Tables[0].Rows[0]["alumnino"].ToString();

                FillSalution();

                if (ds.Tables[0].Rows[0]["Alumni_Sal"].ToString() != null || ds.Tables[0].Rows[0]["Alumni_Sal"].ToString() != "")
                {
                    Drp_Alumni_Name.SelectedValue = ds.Tables[0].Rows[0]["Alumni_Sal"].ToString();
                }
                else
                {
                    Drp_Alumni_Name.SelectedIndex = 0;
                }
                txtAlumniName.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();

                ViewState["id"] = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();

                if (ds.Tables[0].Rows[0]["Father_Sal"].ToString() != null || ds.Tables[0].Rows[0]["Father_Sal"].ToString() != "")
                {
                    Drp_FatherName.SelectedValue = ds.Tables[0].Rows[0]["Father_Sal"].ToString();
                }
                else
                {
                    Drp_FatherName.SelectedIndex = 0;
                }

                R_txtFatherName.Text = ds.Tables[0].Rows[0]["fathername"].ToString();

                if (ds.Tables[0].Rows[0]["Mother_Sal"].ToString() != null || ds.Tables[0].Rows[0]["Mother_Sal"].ToString() != "")
                {
                    Drp_MotherName.SelectedValue = ds.Tables[0].Rows[0]["Mother_Sal"].ToString();
                }
                else
                {
                    Drp_MotherName.SelectedIndex = 0;
                }

                R_txtMotherName.Text = ds.Tables[0].Rows[0]["mothername"].ToString();

                R_txtPostedDate.Text = ds.Tables[0].Rows[0]["dob"].ToString();
                E_txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                R_txtContactno.Text = ds.Tables[0].Rows[0]["contactno"].ToString();
                txtremarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                chkmentor.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]);
                txtCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["designation"].ToString();
                txtperadd.Text = ds.Tables[0].Rows[0]["per_address"].ToString();
                txtCurrentAddress.Text = ds.Tables[0].Rows[0]["currentaddress"].ToString();
                txtsplinterest.Text = ds.Tables[0].Rows[0]["special_interest"].ToString();
                txtachievement.Text = ds.Tables[0].Rows[0]["Achievement"].ToString();

                if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
                {
                    rdbGender.SelectedValue = "M";
                }
                else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
                {
                    rdbGender.SelectedValue = "F";
                }
                else if (ds.Tables[0].Rows[0]["gender"].ToString() == "O")
                {
                    rdbGender.SelectedValue = "O";
                }

                if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "Y")
                {
                    rdbIsPersonDisability.SelectedValue = "Y";
                }
                else
                {
                    rdbIsPersonDisability.SelectedValue = "N";
                }

                if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "Y" && Convert.ToBoolean(ds.Tables[0].Rows[0]["isDisabilityPercentage"].ToString()) == true)
                {
                    isChkDisability.Checked = true;

                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        if (ds.Tables[5].Rows[0]["Files_Unique_Name"] != null && ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString() != "")
                        {
                            ViewState["File3"] = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
                            Session["DDFile"] = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
                            Session["filePath"] = ds.Tables[5].Rows[0]["file_Path"].ToString();
                            lnkDisabilityDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                            lnkDisabilityDoc.CommandName = ds.Tables[5].Rows[0]["Files_Unique_Name"].ToString();
                            lnkDisabilityDoc.Visible = true;
                            getDFileName();
                        }
                    }
                    else
                    {
                        Session["DDFile"] = null;
                        Session["filePath"] = null;
                        lnkDisabilityDoc.Text = "";
                        lnkDisabilityDoc.Visible = false;
                    }
                }
                else
                {
                    rdbIsPersonDisability.SelectedValue = "N";
                    Session["DDFile"] = null;
                    Session["filePath"] = null;
                    lnkDisabilityDoc.Text = "";
                    lnkDisabilityDoc.Visible = false;
                }

                rdbIsPersonDisability_SelectedIndexChanged(null, null);

                if (ds.Tables[0].Rows[0]["isDisabled"].ToString() != "" || ds.Tables[0].Rows[0]["isDisabilityPercentage"].ToString() == "true")
                {
                    isChkDisability.Checked = true;
                    txtDisabilityRemark.Text = ds.Tables[0].Rows[0]["disabiltyRemarks"].ToString();
                }
                else
                {
                    isChkDisability.Checked = false;
                    txtDisabilityRemark.Text = "";
                }

                ddldegree.SelectedValue = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
                ddldegree.CssClass = "ChosenSelector";
                ddldegree_SelectedIndexChanged(null, null);
                subjectlist.SelectedValue = ds.Tables[0].Rows[0]["Fk_subjectid"].ToString();
                FillDepartment();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();

                ViewState["id"] = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();

                if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
                {
                    rdbGender.SelectedValue = "M";
                }
                else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
                {
                    rdbGender.SelectedValue = "F";
                }
                else if (ds.Tables[0].Rows[0]["gender"].ToString() == "O")
                {
                    rdbGender.SelectedValue = "O";
                }

                Fill_PassingYear();
                D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["fk_pyearid"].ToString();
                E_txtEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
                txtCurrentAddress.Text = ds.Tables[0].Rows[0]["currentaddress"].ToString();
                txtperadd.Text = ds.Tables[0].Rows[0]["per_address"].ToString();
                txtCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
                R_txtContactno.Text = ds.Tables[0].Rows[0]["contactno"].ToString();
                txtsplinterest.Text = ds.Tables[0].Rows[0]["special_interest"].ToString();
                txtachievement.Text = ds.Tables[0].Rows[0]["Achievement"].ToString();
                R_txtPostedDate.Text = ds.Tables[0].Rows[0]["dob"].ToString();
                txtremarks.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                // Added by manoj
                txtDesignation.Text = ds.Tables[0].Rows[0]["designation"].ToString();
                chkmentor.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["isMentor"]);
                // txttelephoneno.Text = ds.Tables[0].Rows[0]["telephoneno"].ToString();
                txtCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
                R_txtLoginName1.Text = ds.Tables[0].Rows[0]["email"].ToString();

                if (ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    R_txtPassword.Attributes.Add("value", ds.Tables[0].Rows[0]["password"].ToString());
                    R_txtPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
                }
                string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
                fileName = ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim();

                if (ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString().Trim() != "")
                {
                    Session["IsProfilec"] = "1";
                    imgProfileimg.ImageUrl = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                }
                else
                {
                    Session["IsProfilec"] = "0";
                    imgProfileimg.ImageUrl = "https://alumni.hpushimla.in/Alumni/StuImage/default-user.jpg";
                    hdPath.Text = "";
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["Files_Unique_Name"] != null && ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString() != "")
                    {
                        ViewState["Pic"] = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                        lnkprofile.CommandArgument = (ViewState["id"]) == null ? "1" : (ViewState["id"].ToString());
                        lnkprofile.CommandName = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                        lnkprofile.Text = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                        lnkprofile.Visible = true;
                        getProfileName();
                    }
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    if (ds.Tables[2].Rows[0]["Files_Unique_Name"] != null && ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString() != "")
                    {
                        ViewState["Doc"] = ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                        lnkDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                        lnkDoc.CommandName = ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                        lnkDoc.Text = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
                        lnkDoc.Visible = true;
                        getFileName();
                    }
                }

                //if (ds.Tables[0].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0)
                //{
                //    lblMsgPMode.Text = ds.Tables[4].Rows[0]["PaymentMode"].ToString();
                //    lblTransID.Text = ds.Tables[4].Rows[0]["TransactionID"].ToString();
                //    lblMsgPAmount.Text = ds.Tables[4].Rows[0]["amount"].ToString();
                //    lblTransStatus.Text = ds.Tables[4].Rows[0]["status"].ToString();
                //}
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    protected void getProfileName()
    {
        string FileName = lnkprofile.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
        //if (FileName.Contains("/"))
        //{
        FileDisplayName = FileName;
        FileRealName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        //FileUrl = FileUrl + FileName;
        lnkprofile.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }
    protected void getFileName()
    {
        string FileName = lnkDoc.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
        //if (FileName.Contains("/"))
        //{
        FileDisplayName = FileName;
        FileRealName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        // FileUrl = FileUrl + FileName;
        lnkDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }

    protected void getDFileName()
    {
        string FileName = lnkDisabilityDoc.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
        //if (FileName.Contains("/"))
        //{
        FileDisplayName = FileName;
        FileRealName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        // FileUrl = FileUrl + FileName;
        lnkDisabilityDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }

    public string ReturnPath()
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
                    return dr["http_Add"].ToString().Trim();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void setimageonedit(byte[] fileBytes, string contenttype, string filename)
    {
        string host = HttpContext.Current.Request.Url.Host;
        string upldPath = "";
        string showimgPath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                upldPath = dr["Physical_Path"].ToString().Trim();
                upldPath = upldPath + "\\" + "Alumni\\StuImage" + "\\" + filename.ToString().Trim();
                showimgPath = dr["http_Add"].ToString().Trim() + "\\" + "Alumni\\StuImage" + "\\" + filename.ToString().Trim();
                //filepath = upldPath;
            }
        }
    }

    /// <summary>
    /// For Pop-up Message
    /// </summary>
    /// <param name="msg"></param>
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        // Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");

    }
    /// <summary>
    /// Get Unique  No.
    /// </summary>
    /// <returns></returns>
    private string getUniqueNo()
    {
        string sId = "";
        try
        {
            string sGuid = System.Guid.NewGuid().ToString();
            int idx = sGuid.LastIndexOf('-');
            sId = (sGuid.Substring(idx + 1)).Substring(0, 6).ToUpper().ToString();
        }
        catch (Exception)
        {
            sId = "";
        }

        return sId;
    }

    /// <summary>
    /// Update Onclick Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAlumniName.Text == "")
            {
                ClientMessaging("Please enter Alumni Name");
                txtAlumniName.Focus();
                return;
            }
            if (R_txtFatherName.Text == "")
            {
                ClientMessaging("Please enter  Father Name");
                R_txtFatherName.Focus();
                return;
            }

            if (R_txtMotherName.Text == "")
            {
                ClientMessaging("Please enter  Mother Name");
                R_txtMotherName.Focus();
                return;
            }

            if (R_txtPostedDate.Text.Trim() == "")
            {
                ClientMessaging("Please enter Date of Birth");
                R_txtPostedDate.Focus();
                return;
            }

            if (E_txtEmail.Text == "")
            {
                ClientMessaging("Please enter Email Id");
                E_txtEmail.Focus();
                return;
            }

            E_txtEmail_TextChanged(null, null); //to check email id format

            if (R_txtContactno.Text.Trim().Length < 10)
            {
                ClientMessaging("Please enter valid Mobile No.");
                R_txtContactno.Focus();
                return;
            }

            if (R_txtPostedDate.Text.Trim() != "")
            {
                DateTime dtDob = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txtPostedDate.Text.Trim()));
                int DobYear = dtDob.Year;
                int PassYear = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text.ToString());
                PassYear = PassYear - 18;
                if (DobYear > PassYear)
                {
                    ClientMessaging("Alumni age should be greater than 18 years as per Passing year!");
                    R_txtPostedDate.Focus();
                    return;
                }
            }
            if (ddldegree.SelectedIndex == 0)
            {
                ClientMessaging("Please Select Degree");
                ddldegree.Focus();
                return;
            }
            if (D_ddlYeofPass.SelectedIndex == 0)
            {
                ClientMessaging("Please select Year of Passing");
                D_ddlYeofPass.Focus();
                return;
            }
            DataSet ds = new DataSet();
            ds = Dobj.GetSchema("ALM_AlumniRegistration");
            DataRow dr = ds.Tables[0].NewRow();
            dr["alumnino"] = R_txtAlumnino.Text.Trim();

            if (Drp_Alumni_Name.SelectedIndex > 0)
            {
                dr["Alumni_Sal"] = Drp_Alumni_Name.SelectedValue.ToString().Trim();
            }
            else
            {
                dr["Alumni_Sal"] = DBNull.Value;
            }

            dr["alumni_name"] = txtAlumniName.Text.Trim();

            if (Drp_FatherName.SelectedIndex > 0)
            {
                dr["Father_Sal"] = Drp_FatherName.SelectedValue.ToString().Trim();
            }
            else
            {
                dr["Father_Sal"] = DBNull.Value;
            }

            dr["fathername"] = R_txtFatherName.Text.Trim();

            if (Drp_MotherName.SelectedIndex > 0)
            {
                dr["Mother_Sal"] = Drp_MotherName.SelectedValue.ToString().Trim();
            }
            else
            {
                dr["Mother_Sal"] = DBNull.Value;
            }

            dr["mothername"] = R_txtMotherName.Text.Trim();

            //  dr["regno"] = 0;// R_txtReg.Text.Trim();
            dr["fk_collegeid"] = 0;
            dr["fk_degreeid"] = Convert.ToInt32(ddldegree.SelectedValue);
            dr["Fk_subjectid"] = Convert.ToInt32(subjectlist.SelectedValue);//txtdegree.Text.Trim()
            dr["yearofpassing"] = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text);
            dr["fk_pyearid"] = Convert.ToInt32(D_ddlYeofPass.SelectedValue);
            dr["email"] = E_txtEmail.Text.Trim();
            dr["currentaddress"] = txtCurrentAddress.Text.Trim();
            dr["per_address"] = txtperadd.Text.Trim();
            dr["currentoccupation"] = txtCurrentOccupation.Text.Trim();
            dr["contactno"] = R_txtContactno.Text.Trim();
            dr["special_interest"] = txtsplinterest.Text.Trim();
            dr["Achievement"] = txtachievement.Text.Trim();
            dr["remarks"] = txtremarks.Text.Trim();
            dr["dob"] = R_txtPostedDate.Text.Trim();
            //CommonCode.DateFormats.Date_FrontToDB_R(R_txtPostedDate.Text.Trim());
            // Added By Manoj
            dr["fk_Deptid"] = Convert.ToInt32(ddlDepartment.SelectedValue);
            dr["designation"] = txtDesignation.Text.Trim();
            dr["mothername"] = R_txtMotherName.Text.Trim();
            dr["fathername"] = R_txtFatherName.Text.Trim();
            dr["loginname"] = R_txtAlumnino.Text.Trim();
            dr["password"] = R_txtPostedDate.Text.Trim(); //crp.Encrypt(R_txtPassword.Text.Trim());

            if (rdbGender.SelectedValue.ToString() == "M")
            {
                dr["gender"] = "M";
            }
            else if (rdbGender.SelectedValue.ToString() == "F")
            {
                dr["gender"] = "F";
            }

            dr["isMentor"] = chkmentor.Checked;
            //Code Added By Santosh sah
            string filepath = ""; string fileName = ""; string contenttype = ""; byte[] fileBytes = null;

            #region" "For Profile Pic"

            Random randomNo = new Random();
            string FileType = "";
            double filesize = 0;
            DataRow drimg = null;
            string Fileuniquename = string.Empty;

            //  dsImg = null;

            DataSet dsImg = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
            dsImg.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";

            if (flUpload.HasFile == true && checkValidPhotoOrNot())
            {
                //filesize = flUpload.PostedFile.ContentLength;
                string Name = flUpload.PostedFile.FileName;

                //filesize = Math.Round((filesize / 1024), 0);
                //if (Name.Length > 100)
                //{
                //    ClientMessaging("Photo File Name Should not be more than 100 characters!");
                //    return;
                //}
                //if (filesize > 1024)
                //{
                //    ClientMessaging("Photo size should not be more than 1 MB !");
                //    return;
                //}

                FileType = Path.GetExtension(flUpload.PostedFile.FileName);

                //if (FileType != null)
                //{
                //if (FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
                //&& FileType != ".pjpeg" && FileType != ".PJPEG" && FileType != ".bmp" && FileType != ".BMP"
                //&& FileType != ".gif" && FileType != ".GIF" && FileType != ".png" && FileType != ".PNG")
                //{
                //    ClientMessaging("Photo is not in proper format!");
                //    return;
                //}

                UploadFiles();//to get the physical path of server 
                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;

                drimg = dsImg.Tables[0].NewRow();

                //drimg["Fk_Regid"] = Convert.ToInt64(Session["regid"].ToString());

                drimg["IsProfilePicOrDoc"] = 1;
                //string filetype = aItem.FileName.Substring(aItem.FileName.LastIndexOf("."));
                drimg["Files_Name"] = Name;
                drimg["Files_Unique_Name"] = "HPU_Alumni_PP_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
                Fileuniquename = "HPU_Alumni_PP_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                   DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;

                // save only file unique name so that we can upload this seperately
                dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds

                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                flUpload.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            //  }
            else
            {
                //get pic details from viewstate
                //if (ViewState["StudentPicDtls"] != null)
                //{
                //    DataTable dt = ViewState["StudentPicDtls"] as DataTable;
                //    if (dt.Rows.Count > 0)
                //    {
                //        var PP = dt.AsEnumerable()
                //                .Where(r => r.Field<long>("Fk_Regid") == Convert.ToInt64(Session["regid"].ToString()) && r.Field<string>("Img_Type") == "PP").SingleOrDefault();
                //        if (PP != null)
                //        {
                //            drimg = dsImg.Tables[0].NewRow();
                //            drimg["Fk_Regid"] = Convert.ToInt64(Session["regid"].ToString());
                //            drimg["Img_Type"] = "PP";
                //            drimg["Files_Name"] = PP[4].ToString();
                //            drimg["Files_Unique_Name"] = PP[5].ToString(); ;
                //            drimg["FileExtension"] = PP[6].ToString(); ;
                //            drimg["File_Path"] = PP[7].ToString();
                //            dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                //        }
                //    }
                //}

                if (ViewState["id"] != null)
                {
                    DataSet dsS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsS != null && dsS.Tables[1].Rows.Count > 0)
                    {
                        //var PP = dsS.Tables[1].AsEnumerable()
                        //        .Where(r => r.Field<long>("Fk_alumniid") == Convert.ToInt64(ViewState["id"].ToString()) && r.Field<string>("IsProfilePicOrDoc") == "1").SingleOrDefault();
                        //if (PP != null)
                        //{
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsS.Tables[1].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsS.Tables[1].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsS.Tables[1].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsS.Tables[1].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsS.Tables[1].Rows[0]["File_Path"].ToString();
                        dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                        //}
                    }
                }

            }
            //}
            //else
            //{
            //    dr["Files_Name"] = (string)ViewState["File1"].ToString();
            //}
            #endregion

            #region "Document Upload"

            if (uploadDocuments.HasFile == true && checkValidDocOrNot())
            {
                //filesize = uploadDocuments.PostedFile.ContentLength;
                string DocumentName = uploadDocuments.PostedFile.FileName;
                ///filesize = Math.Round((filesize / 1024), 0);

                ////if (DocumentName.Length > 100)
                ////{
                ////    ClientMessaging("HPU Document File Name Should not be more than 100 characters!");
                ////    return;
                ////}
                ////if (filesize > 2048)
                ////{
                ////    ClientMessaging(" Document size should not be more than 2MB !");
                ////    return;
                ////}

                FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);

                ////if (FileType != null && FileType != "")
                ////{
                ////    if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
                ////&& FileType != ".pjpeg" && FileType != ".PJPEG" && FileType != ".bmp" && FileType != ".BMP"
                ////&& FileType != ".gif" && FileType != ".GIF" && FileType != ".png" && FileType != ".PNG")
                ////    {
                ////        ClientMessaging("HPU Document Should be in PDF and Image and  format!");
                ////        lblMsg.Text = "Document Should be in pdf format!";
                ////        return;
                ////    }
                ////}

                drimg = dsImg.Tables[0].NewRow();
                //drFile = dtFileDtl.NewRow();
                // drimg["Fk_Regid"] = Convert.ToInt64(Session["regid"].ToString());
                // drimg["Img_Type"] = "DOC";
                drimg["IsProfilePicOrDoc"] = 0;
                UploadFiles();//to get the physical path of server 

                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                Random randomNo1 = new Random();
                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = "HPU_Alumni_DOC_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PH" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;
                //drFile["Files_Unique_Name"] = drimg["Files_Unique_Name"].ToString();// to upload on server seperately
                dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                uploadDocuments.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            else
            {
                // dr["Files_Name"] = (string)ViewState["File2"].ToString();
                //if (ViewState["StudentPicDtls"] != null)
                //{
                //    DataTable dt = ViewState["StudentPicDtls"] as DataTable;
                //    if (dt.Rows.Count > 0)
                //    {
                //        var PH = dt.AsEnumerable().Where(r => r.Field<long>("Fk_Regid") == Convert.ToInt64(Session["regid"].ToString()) && r.Field<string>("Img_Type") == "PH").SingleOrDefault();
                //        if (PH != null)
                //        {
                //            drimg = dsImg.Tables[0].NewRow();
                //            drimg["Fk_Regid"] = Convert.ToInt64(Session["regid"].ToString());
                //            drimg["Img_Type"] = "PH";
                //            drimg["Files_Name"] = PH[4].ToString();
                //            drimg["Files_Unique_Name"] = PH[5].ToString(); ;
                //            drimg["FileExtension"] = PH[6].ToString(); ;
                //            drimg["File_Path"] = PH[7].ToString();
                //            dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                //        }
                //    }
                //}

                if (ViewState["id"] != null)
                {
                    DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsSS != null && dsSS.Tables[2].Rows.Count > 0)
                    {
                        //var PH = dsSS.Tables[2].AsEnumerable()
                        //        .Where(r => r.Field<long>("Fk_alumniid") == Convert.ToInt64(ViewState["id"].ToString()) && r.Field<string>("IsProfilePicOrDoc") == "0").SingleOrDefault();
                        //if (PH != null)
                        //{
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsSS.Tables[2].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsSS.Tables[2].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsSS.Tables[2].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsSS.Tables[2].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsSS.Tables[2].Rows[0]["File_Path"].ToString();
                        dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
                        //}
                    }
                }
            }

            #endregion

            ds.Tables[0].Rows.Add(dr);
            if (dsImg != null)
            {
                ds.Merge(dsImg);
            }

            //string Message = "";
            ArrayList Result = new ArrayList();
            //IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
            string XmlDoc1 = ds.GetXml();

            //string xmlstr = ds.GetXml();
            int AlumniI = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Upd(Convert.ToInt32(ViewState["id"]), XmlDoc1, fileBytes).Execute();

            if (AlumniI > 0)
            {
                lblMsg.Text = "Record Updated Successfully!";
                ClientMessaging("Record Update Successfully");
            }
            else
            {
                lblMsg.Text = "Retry!";
            }
        }
        catch (Exception ex)
        {
            //   lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }
    /// <summary>
    /// End Update Click
    /// </summary>

    protected void E_txtEmail_TextChanged(object sender, EventArgs e)
    {
        lblEmailMsg.Text = "";
        if (E_txtEmail.Text.Trim() != "")
        {
            DataSet ds = IUMSNXG.SP.ALM_SP_Check_Duplicate_Email_AtUpdate(Convert.ToInt32(Session["AlumniId"].ToString()), E_txtEmail.Text.Trim()).GetDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsExists"].ToString() == "1")
                {
                    lblEmailMsg.Text = "Email Id Already Exists!";
                    E_txtEmail.Text = "";
                    ClientMessaging("Email Id Already Exists!");
                    Anthem.Manager.IncludePageScripts = true;
                    E_txtEmail.Focus();
                    return;
                }
            }
        }
    }

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

    protected void Assign_Uploaded_File_Dts(CuteWebUI.UploadAttachments Fl_UploadFiles, bool IsProfilePicOrDoc)
    {
        dsFile = null;
        dsFile = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
        dsFile.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";

        DataRow drf;
        int J = Fl_UploadFiles.Items.Count;
        if (Fl_UploadFiles.Items.Count > 0)
        {
            for (int i = 0; i < Fl_UploadFiles.Items.Count; i++)
            {
                J--;
                UploadFiles();
                drf = dsFile.Tables[0].NewRow();
                AttachmentItem aItem = Fl_UploadFiles.Items[i];

                string filetype = aItem.FileName.Substring(aItem.FileName.LastIndexOf("."));

                drf["Fk_alumniid"] = 0;
                drf["IsProfilePicOrDoc"] = IsProfilePicOrDoc == true ? 1 : 0;
                drf["Files_Name"] = aItem.FileName;
                drf["Files_Unique_Name"] = "File_Alumni" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + i + "_" + J + "_" + filetype;
                drf["FileExtension"] = filetype;
                drf["File_Path"] = upldPath + drf["Files_Unique_Name"].ToString();
                dsFile.Tables[0].Rows.Add(drf);
            }
            ViewState["FileDtl"] = null;
            ViewState["FileDtl"] = dsFile;
        }
    }

    protected void UploadFiles()
    {
        string host = HttpContext.Current.Request.Url.Host;
        // string upldPath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                upldPath = dr["Physical_Path"].ToString().Trim();
                upldPath = upldPath + "Alumni\\StuImage" + "\\";// +"\\" + FileName;
                break;
            }
        }
    }

    void UploadDocumentFiles(DataTable dt, string upldPath, CuteWebUI.UploadAttachments Fl_UploadFiles)
    {
        string currDir = System.IO.Directory.GetCurrentDirectory();
        upldPath = this.upldPath;
        bool IsExistPath = System.IO.Directory.Exists(upldPath);
        if (!IsExistPath)
            System.IO.Directory.CreateDirectory(upldPath);
        string filename = "";

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            filename = dt.Rows[i]["Files_Unique_Name"].ToString();
            AttachmentItem aItem = Fl_UploadFiles.Items[i];
            aItem.CopyTo(upldPath + filename);
        }
    }

    //it will return file based on file unique name
    public string SetServiceDoc(string FileName)
    {
        string FolderName = @"/Alumni";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                if (host != "localhost")
                {
                    FilePath = dr["http_Add"].ToString().Trim();
                    FilePath = @FilePath + FolderName + @"/" + FileName;
                    // FilePath = FolderName + FileName;
                }
                else
                {
                    FilePath = dr["Physical_Path"].ToString().Trim();
                    FilePath = @FilePath + FolderName + @"/" + FileName;
                    //  FilePath = FolderName  + FileName;
                }
                //return FolderName+FileName;
                return FilePath;
            }
        }
        return FilePath;
    }

    /// <summary>
    /// Unique Mobile No.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void R_txtContactno_TextChanged(object sender, EventArgs e)
    {

        lblMobleNoMsg.Text = "";
        if (R_txtContactno.Text.Trim() != "")
        {
            DataSet ds = IUMSNXG.SP.ALM_SP_GetDuplicate_Email_or_MobileNo("", R_txtContactno.Text.Trim()).GetDataSet();
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblMobleNoMsg.Text = "Mobile No. Already Exists!";
                //R_txtContactno.Text = "";
                Anthem.Manager.IncludePageScripts = true;
                R_txtContactno.Focus();
                return;
            }
        }
    }

    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
        getsubject(pk_degreeid);
    }

    protected void permanentaddchk_CheckedChanged(object sender, EventArgs e)
    {
        if (permanentaddchk.Checked)
        {
            txtCurrentAddress.Text = txtperadd.Text.ToString();
        }
        else
        {
            txtCurrentAddress.Text = "";
        }
    }

    protected void txtCurrentAddress_TextChanged(object sender, EventArgs e)
    {
        if (permanentaddchk.Checked)
        {
            txtCurrentAddress.Text = txtperadd.Text.ToString();
        }
    }

    public StoredProcedure ALM_ALM_ACD_Salutation_fill()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ACD_Salutation_Sel", DataService.GetInstance("IUMSNXG"), "");
        //sp.Command.AddParameter("@C_id", C_id, DbType.Int32);
        return sp;
    }

    protected void rdbGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbGender.SelectedValue.ToString() == "M")
        {
            rdbGender.SelectedValue = "M";
        }
        else if (rdbGender.SelectedValue.ToString() == "F")
        {
            rdbGender.SelectedValue = "F";
        }
        else if (rdbGender.SelectedValue.ToString() == "O")
        {
            rdbGender.SelectedValue = "O";
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_AlumniPayLater.aspx");
    }

    private bool checkValidPhotoOrNot()
    {
        if (!flUpload.HasFile)
        {
            ClientMessaging("Please upload the required photo.!!!");
            flUpload.Focus();
            return false;
        }

        double filesize = flUpload.PostedFile.ContentLength;
        string Name = flUpload.PostedFile.FileName;

        if (Name.Length > 100)
        {
            ClientMessaging("Photo File Name Should not be more than 100 characters!");
            flUpload.Focus();
            return false;
        }

        if (filesize > (1 * 1024 * 1024))
        {
            ClientMessaging("Photo size should not be more than 1 MB !");
            flUpload.Focus();
            return false;
        }

        string FileType = flUpload.PostedFile.FileName != "" ? Path.GetExtension(flUpload.PostedFile.FileName) : System.IO.Path.GetExtension(ViewState["File2"].ToString());

        switch (FileType.ToLower())
        {
            case ".jpg":
                return true;
            case ".jpeg":
                return true;
            case ".png":
                return true;
            default:
                ClientMessaging("Only files .jpg, .jpeg and .png extension are allowed.");
                return false;
        }
        return true;
    }

    private bool checkValidDocOrNot()
    {
        if (!uploadDocuments.HasFile)
        {
            ClientMessaging("Please upload the required HPU documents.!!!");
            uploadDocuments.Focus();
            return false;
        }

        double filesizes = uploadDocuments.PostedFile.ContentLength;
        string Names = uploadDocuments.PostedFile.FileName;
        //filesizes = Math.Round((filesizes / 1024), 0);

        if (Names.Length > 100)
        {
            ClientMessaging("Documents File Name Should not be more than 100 characters!");
            uploadDocuments.Focus();
            return false;
        }
        if (filesizes > (2 * 1024 * 1024))
        {
            ClientMessaging("HPU Document size should not be more than 2 MB !");
            uploadDocuments.Focus();
            return false;
        }

        string FileTypes = uploadDocuments.PostedFile.FileName != "" ? Path.GetExtension(uploadDocuments.PostedFile.FileName) : System.IO.Path.GetExtension(ViewState["File2"].ToString());

        switch (FileTypes.ToLower())
        {
            case ".jpg":
                return true;
            case ".jpeg":
                return true;
            case ".png":
                return true;
            case ".pdf":
                return true;
            case ".bmp":
                return true;
            case ".gif":
                return true;
            default:
                ClientMessaging("Only files .jpg, .jpeg, .png, .pdf, .bmp, and .gif extension are allowed.");
                return false;
        }
        return true;
    }

    public void ControlReadonly()
    {
        Drp_Alumni_Name.Enabled = true;
        txtAlumniName.Enabled = true;
        Drp_FatherName.Enabled = true;
        R_txtFatherName.Enabled = true;
        Drp_MotherName.Enabled = true;
        R_txtMotherName.Enabled = true;
        R_txtPostedDate.Enabled = true;
        E_txtEmail.Enabled = true;
        R_txtContactno.Enabled = true;
        lblMobleNoMsg.Enabled = true;
        txtremarks.Enabled = true;
        txtCurrentOccupation.Enabled = true;
        txtDesignation.Enabled = true;
        txtperadd.Enabled = true;
        txtCurrentAddress.Enabled = true;
        permanentaddchk.Enabled = true;
        txtsplinterest.Enabled = true;
        txtachievement.Enabled = true;
        rdbGender.Enabled = true;
        chkmentor.Enabled = false;
        ddldegree.Enabled = true;
        subjectlist.Enabled = true;
        D_ddlYeofPass.Enabled = true;
        ddlDepartment.Enabled = true;
    }

    private bool validateDetails()
    {
        try
        {
            Random randomNo = new Random();
            string FileTypes = "";
            string FileType = "";
            double filesize = 0;
            double filesizes = 0;
            string Name = "";
            string Names = "";
            DataRow drimg = null;
            string Fileuniquename = string.Empty;

            if (rbdAlumniMemType.SelectedValue != "LM" && rbdAlumniMemType.SelectedValue != "SM")
            {
                ClientMessaging("Please Choose Membership Type.!");
                rbdAlumniMemType.Focus();
                return false;
            }
            if (txtAlumniName.Text == "")
            {
                ClientMessaging("Please enter Alumni Name.!");
                txtAlumniName.Focus();
                return false;
            }
            if (R_txtFatherName.Text == "")
            {
                ClientMessaging("Please Enter Father Name.!");
                R_txtFatherName.Focus();
                return false;
            }
            if (R_txtMotherName.Text == "")
            {
                ClientMessaging("Please Enter Mother Name.!");
                R_txtMotherName.Focus();
                return false;
            }
            if (R_txtPostedDate.Text.Trim() == "")
            {
                ClientMessaging("Please Enter Date of Birth.!");
                R_txtPostedDate.Focus();
                return false;
            }
            if (E_txtEmail.Text == "")
            {
                ClientMessaging("Please Enter Email.!");
                E_txtEmail.Focus();
                return false;
            }

            E_txtEmail_TextChanged(null, null);

            if (R_txtContactno.Text.Trim().Length < 10)
            {
                ClientMessaging("Please Enter Valid Mobile No.!");
                R_txtContactno.Focus();
                return false;
            }

            if (rdbIsPersonDisability.SelectedValue == "Y" && !isChkDisability.Checked)
            {
                ClientMessaging("Please Check Special Ability More than 40% ?.");
                Anthem.Manager.IncludePageScripts = true;
                isChkDisability.Focus();
                return false;
            }

            if (rdbIsPersonDisability.SelectedValue == "Y" && isChkDisability.Checked && txtDisabilityRemark.Text == "")
            {
                ClientMessaging("Please Enter Special Ability Description.");
                Anthem.Manager.IncludePageScripts = true;
                txtDisabilityRemark.Focus();
                return false;
            }

            if (ddldegree.SelectedIndex == 0)
            {
                ClientMessaging("Please Enter Degree.!");
                Anthem.Manager.IncludePageScripts = true;
                ddldegree.Focus();
                return false;
            }
            if (subjectlist.SelectedIndex == 0)
            {
                ClientMessaging("Please Enter Subject.!");
                Anthem.Manager.IncludePageScripts = true;
                subjectlist.Focus();
                return false;
            }
            if (D_ddlYeofPass.SelectedIndex == 0)
            {
                ClientMessaging("Please Select Passing Year.!");
                Anthem.Manager.IncludePageScripts = true;
                D_ddlYeofPass.Focus();
                return false;
            }

            if (R_txtPostedDate.Text.Trim() != "")
            {
                DateTime dtDob = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txtPostedDate.Text.Trim()));
                int DobYear = dtDob.Year;
                int PassYear = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text.ToString());
                PassYear = PassYear - 18;
                if (DobYear > PassYear)
                {
                    ClientMessaging("Alumni age should be greater than 18 years as per Passing year.!");
                    R_txtPostedDate.Focus();
                    return false;
                }
            }

            if (ddlDepartment.SelectedIndex == 0)
            {
                ClientMessaging("Please Select Department.!");
                ddlDepartment.Focus();
                return false;
            }

            if (!flUpload.HasFile && string.IsNullOrEmpty(lnkprofile.Text))
            {
                ClientMessaging("Please Upload the Required Photo.!");
                flUpload.Focus();
                return false;
            }

            FileType = Path.GetExtension(flUpload.PostedFile.FileName);

            if (FileType != null && FileType != "")
            {
                if (FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
            && FileType != ".bmp" && FileType != ".BMP" && FileType != ".gif" && FileType != ".GIF" && FileType != ".png" && FileType != ".PNG")
                {
                    ClientMessaging("Photo Should Be in Required Format.!");
                    lblMsg.Text = "Photo Should Be in Required Format.!";
                    flUpload.Focus();
                    return false;
                }
            }

            filesize = flUpload.PostedFile.ContentLength;
            Name = flUpload.PostedFile.FileName;
            // filesize = Math.Round((filesize / 1024), 0);

            if (Name.Length > 500)
            {
                ClientMessaging("Photo File Name Should not be more than 500 characters.!");
                flUpload.Focus();
                return false;
            }
            if (filesize > (1024 * 1024))
            {
                ClientMessaging("Photo size should not be more than 1 MB.!");
                flUpload.Focus();
                return false;
            }

            if (uploadDocuments.HasFile)
            {
                FileTypes = Path.GetExtension(uploadDocuments.PostedFile.FileName);

                if (FileTypes != null && FileTypes != "")
                {
                    if (FileTypes != ".pdf" && FileTypes != ".PDF" && FileTypes != ".jpg" && FileTypes != ".JPG" && FileTypes != ".jpeg" && FileTypes != ".JPEG"
                && FileTypes != ".bmp" && FileTypes != ".BMP" && FileTypes != ".gif" && FileTypes != ".GIF" && FileTypes != ".png" && FileTypes != ".PNG")
                    {
                        ClientMessaging("HPU Document Should be in required format.!");
                        lblMsg.Text = "HPU Document Should be in required format.!";
                        uploadDocuments.Focus();
                        return false;
                    }
                }

                filesizes = uploadDocuments.PostedFile.ContentLength;
                Names = uploadDocuments.PostedFile.FileName;
                // filesizes = Math.Round((filesizes / 1024), 0);

                if (Names.Length > 500)
                {
                    ClientMessaging("Photo File Name Should not be more than 500 characters.!");
                    uploadDocuments.Focus();
                    return false;
                }
                if (filesizes > (2 * 1024 * 1024))
                {
                    ClientMessaging("Upload documents size should not be more than 2 MB.!");
                    uploadDocuments.Focus();
                    return false;
                }
            }

            if (fileDisabilityDoc.HasFile)
            {
                FileTypes = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);

                if (FileTypes != null && FileTypes != "")
                {
                    if (FileTypes != ".pdf" && FileTypes != ".PDF" && FileTypes != ".jpg" && FileTypes != ".JPG" && FileTypes != ".jpeg" && FileTypes != ".JPEG"
                && FileTypes != ".bmp" && FileTypes != ".BMP" && FileTypes != ".gif" && FileTypes != ".GIF" && FileTypes != ".png" && FileTypes != ".PNG")
                    {
                        ClientMessaging("Special Ability Document Should be in required format.!");
                        lblMsg.Text = "Special Ability Document Should be in required format.!";
                        fileDisabilityDoc.Focus();
                        return false;
                    }
                }

                filesizes = fileDisabilityDoc.PostedFile.ContentLength;
                Names = fileDisabilityDoc.PostedFile.FileName;

                if (Names.Length > 500)
                {
                    ClientMessaging("Special Ability Document File Name Should not be more than 500 characters.!");
                    fileDisabilityDoc.Focus();
                    return false;
                }
                if (filesizes > (2 * 1024 * 1024))
                {
                    ClientMessaging("Upload Special Ability documents size should not be more than 2 MB.!");
                    fileDisabilityDoc.Focus();
                    return false;
                }
            }

            if (Session["DDFile"] == null)
            {
                ClientMessaging("Special Ability Document File is Required to Upload!");
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void update()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = Dobj.GetSchema("ALM_AlumniRegistration");
            DataRow dr = ds.Tables[0].NewRow();
            dr["alumnino"] = R_txtAlumnino.Text.Trim();
            dr["Alumni_Sal"] = Drp_Alumni_Name.SelectedValue.ToString().Trim();
            dr["alumni_name"] = txtAlumniName.Text.Trim();
            dr["Father_Sal"] = Drp_FatherName.SelectedValue.ToString().Trim();
            dr["fathername"] = R_txtFatherName.Text.Trim();
            dr["Mother_Sal"] = Drp_MotherName.SelectedValue.ToString().Trim();
            dr["mothername"] = R_txtMotherName.Text.Trim();
            dr["fk_collegeid"] = 0;
            dr["fk_degreeid"] = Convert.ToInt32(ddldegree.SelectedValue);
            dr["Fk_subjectid"] = Convert.ToInt32(subjectlist.SelectedValue);//txtdegree.Text.Trim()
            dr["yearofpassing"] = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text);
            dr["fk_pyearid"] = Convert.ToInt32(D_ddlYeofPass.SelectedValue);
            dr["email"] = E_txtEmail.Text.Trim();
            dr["currentaddress"] = txtCurrentAddress.Text.Trim();
            dr["per_address"] = txtperadd.Text.Trim();
            dr["currentoccupation"] = txtCurrentOccupation.Text.Trim();
            dr["contactno"] = R_txtContactno.Text.Trim();
            dr["special_interest"] = txtsplinterest.Text.Trim();
            dr["Achievement"] = txtachievement.Text.Trim();
            dr["remarks"] = txtremarks.Text.Trim();
            dr["dob"] = R_txtPostedDate.Text.Trim();
            dr["fk_Deptid"] = Convert.ToInt32(ddlDepartment.SelectedValue);
            dr["designation"] = txtDesignation.Text.Trim();
            dr["mothername"] = R_txtMotherName.Text.Trim();
            dr["fathername"] = R_txtFatherName.Text.Trim();
            dr["loginname"] = R_txtAlumnino.Text.Trim();
            dr["password"] = R_txtPostedDate.Text.Trim();

            if (Request.QueryString["ID"] != null)
            {
                dr["alumnitype"] = alumni_Type;
            }

            if (Membershiptype != null)
            {
                dr["Membership_Type"] = Membershiptype;
            }

            if (rdbGender.SelectedValue.ToString() == "M")
            {
                dr["gender"] = "M";
            }
            else if (rdbGender.SelectedValue.ToString() == "F")
            {
                dr["gender"] = "F";
            }
            else if (rdbGender.SelectedValue.ToString() == "O")
            {
                dr["gender"] = "O";
            }

            dr["isMentor"] = chkmentor.Checked;

            if (rdbIsPersonDisability.SelectedValue.ToString() == "Y")
            {
                dr["isDisabled"] = "Y";

            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                dr["isDisabled"] = "N";
                dr["isDisabilityPercentage"] = isChkDisability.Checked;
                dr["disabiltyRemarks"] = "";
            }

            if (isChkDisability.Checked)
            {
                dr["isDisabilityPercentage"] = isChkDisability.Checked;
                dr["disabiltyRemarks"] = txtDisabilityRemark.Text.Trim().ToString();
            }
            else
            {
                dr["isDisabilityPercentage"] = isChkDisability.Checked;
                dr["disabiltyRemarks"] = "";
            }

            string filepath = ""; string fileName = ""; string contenttype = ""; byte[] fileBytes = null;

            #region "For Profile Pic"

            Random randomNo = new Random();
            string Message = "";
            string FileType = "";
            double filesize = 0;
            DataRow drimg = null;
            string Fileuniquename = string.Empty;

            DataSet dsImg = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
            dsImg.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";

            if (flUpload.HasFile == true && checkValidPhotoOrNot())
            {
                string Name = flUpload.PostedFile.FileName;
                FileType = Path.GetExtension(flUpload.PostedFile.FileName);

                UploadFiles();
                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                drimg = dsImg.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 1;
                drimg["Files_Name"] = Name;
                drimg["Files_Unique_Name"] = "HPU_Alumni_PP_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;
                drimg["FilesFor"] = "PP";
                dsImg.Tables[0].Rows.Add(drimg);

                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                flUpload.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            else
            {
                if (ViewState["id"] != null)
                {
                    DataSet dsS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsS != null && dsS.Tables[1].Rows.Count > 0)
                    {
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsS.Tables[1].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsS.Tables[1].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsS.Tables[1].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsS.Tables[1].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsS.Tables[1].Rows[0]["File_Path"].ToString();
                        drimg["FilesFor"] = dsS.Tables[1].Rows[0]["FilesFor"].ToString();
                        dsImg.Tables[0].Rows.Add(drimg);
                    }
                }
            }

            #endregion

            #region "Document Upload"

            if (uploadDocuments.HasFile == true && checkValidDocOrNot())
            {
                string DocumentName = uploadDocuments.PostedFile.FileName;
                FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);
                drimg = dsImg.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 0;
                UploadFiles();
                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                Random randomNo1 = new Random();
                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = "HPU_Alumni_AD_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "AD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;
                drimg["FilesFor"] = "AD";
                dsImg.Tables[0].Rows.Add(drimg);
                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                uploadDocuments.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            else
            {
                if (ViewState["id"] != null)
                {
                    DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsSS != null && dsSS.Tables[2].Rows.Count > 0)
                    {
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsSS.Tables[2].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsSS.Tables[2].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsSS.Tables[2].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsSS.Tables[2].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsSS.Tables[2].Rows[0]["File_Path"].ToString();
                        drimg["FilesFor"] = "AD";
                        dsImg.Tables[0].Rows.Add(drimg);
                    }
                }
            }

            #endregion

            #region "Disability Document Upload"

            if (fileDisabilityDoc.HasFile == true && checkValidDocOrNot())
            {
                string DocumentName = fileDisabilityDoc.PostedFile.FileName;

                FileType = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);

                drimg = dsImg.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 0;
                UploadFiles();

                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                Random randomNo1 = new Random();
                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = "HPU_Alumni_DD_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = upldPath;
                drimg["FilesFor"] = "DD";
                dsImg.Tables[0].Rows.Add(drimg);
                bool IsExistPath = System.IO.Directory.Exists(upldPath);
                if (!IsExistPath)
                    System.IO.Directory.CreateDirectory(upldPath);
                fileDisabilityDoc.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            }
            else
            {
                if (ViewState["id"] != null)
                {
                    DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsSS != null && dsSS.Tables[2].Rows.Count > 0)
                    {
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsSS.Tables[2].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsSS.Tables[2].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsSS.Tables[2].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsSS.Tables[2].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsSS.Tables[2].Rows[0]["File_Path"].ToString();
                        drimg["FilesFor"] = "DD";
                        dsImg.Tables[0].Rows.Add(drimg);
                    }
                }
            }

            #endregion

            ds.Tables[0].Rows.Add(dr);
            if (dsImg != null)
            {
                ds.Merge(dsImg);
            }

            ArrayList Result = new ArrayList();
            //IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
            string XmlDoc1 = ds.GetXml();

            //string xmlstr = ds.GetXml();
            int AlumniI = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Upd(Convert.ToInt32(ViewState["id"]), XmlDoc1, fileBytes).Execute();

            if (AlumniI > 0)
            {
                //lblMsg.Text = "Record Updated Successfully!";
                //ClientMessaging("Record Update Successfully");

                if (Session["RegFees"] == null)
                {
                    //lblPaymentMsg.Text = "Fee is not available. Please Try Again or contact to University!";
                    return;
                }

                if (hdnId.Value == "0")
                {
                    ClientMessaging("Alumni Profile Updated Successfully.!!!");
                    lblMsg.Text = "Alumni Profile Updated Successfully.!!!";
                    clear1();
                    return;
                }
                else
                {
                    DataSet dsMain = GetMain();
                    IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                    _xmlDoc = dsMain.GetXml();

                    if (InsertpaymentRecord(ref Message, ref Result) > 0)
                    {
                        if (Result.Count > 0)
                        {
                            Session["Pk_purchaseid"] = Result[1].ToString().Trim();
                            Session["temp_Pk_purchaseid"] = Result[1].ToString().Trim();
                            Session["regid"] = Session["AlumniID"].ToString();
                            //Response.Redirect("~/Onlinepayment/ALM_Common_PaymentGateway.aspx", false);
                            ClientMessagingss("Alumni Profile Updated Successfully!");
                        }
                    }
                    else
                    {
                        //lblPaymentMsg.Text = Message;
                    }
                }
            }
            else
            {
                lblMsg.Text = "Retry!";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ClientMessagingss(string msg)
    {
        string url = "../Onlinepayment/ALM_Common_PaymentGateway.aspx";
        string script = "window.onload = function(){ alert('";
        script += msg;
        script += "');";
        script += "window.location = '";
        script += url;
        script += "'; }";
        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
    }

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        if (!validateDetails())
        {
            return;
        }
        update();
        //ForPayment();
    }

    protected void ForPayment()
    {
        btnPay();
    }

    protected void btnPay()
    {
        //SAVE HERE DETAILS IN PURCHASE MST AS USER IS GOING TO CHOOSE PAYMENT GATEWAY OPTIONS OPTIONS
        //try
        //{
        //    if (Session["RegFees"] == null)
        //    {
        //        //lblPaymentMsg.Text = "Fee is not available. Please Try Again or contact to University!";
        //        return;
        //    }
        //    string Message = "";
        //    ArrayList Result = new ArrayList();
        //    DataSet dsMain = GetMain();

        //    IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
        //    XmlDoc = dsMain.GetXml();

        //    if (InsertpaymentRecord(ref Message, ref Result) > 0)
        //    {
        //        if (Result.Count > 0)
        //        {
        //            Session["Pk_purchaseid"] = Result[1].ToString().Trim();
        //            Session["temp_Pk_purchaseid"] = Result[1].ToString().Trim();
        //            Session["regid"] = Session["AlumniID"].ToString();
        //            Response.Redirect("~/Onlinepayment/ALM_Common_PaymentGateway.aspx", false);
        //        }
        //    }
        //    else
        //    {
        //        //lblPaymentMsg.Text = Message;
        //    }
        //}
        //catch (Exception ex)
        //{

        //}
    }

    protected DataSet GetMain()
    {
        DataSet ds = null;
        try
        {
            RegId = Convert.ToInt32(Session["AlumniID"].ToString());

            DataSet dsDetails = Get_All_Details_Of_Alumni();

            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                ds = GetSchema("HPU_Alumni_eCoupon_Purchase_Mst");
                DataRow dr = ds.Tables[0].NewRow();
                dr["pk_purchaseid"] = "0";
                dr["fk_regid"] = Session["AlumniID"].ToString();
                dr["Entrydate"] = DateTime.Now;

                decimal regFees;

                if (decimal.TryParse(lblOnlineFees.Text, out regFees))
                {
                    dr["RegFees"] = regFees;
                }
                else
                {
                    dr["RegFees"] = DBNull.Value;
                }

                //dr["RegFees"] = lblMsgPAmount.Text.ToString(); // Session["RegFees"];

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

    protected void rbdAlumniMemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeeDetails();
    }

    /// <summary>
    /// Sp of btn pay click (Insert Record)
    /// </summary>
    /// <param name="Message"></param>
    /// <param name="Result"></param>
    /// <returns></returns>
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

    public DataSet GetSchema(string TableName)
    {
        try
        {
            Clear();
            return DAobj.GetSchema(TableName);
        }
        catch { throw; }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear1();
        lblMsg.Text = "";
        //lblPaymentMsg.Text = "";
    }

    void clear1()
    {
        txtAlumniName.Text = "";
        R_txtMotherName.Text = string.Empty;
        R_txtFatherName.Text = string.Empty;
        R_txtPostedDate.Text = string.Empty;
        // txtdepartment.Text = string.Empty;// ClearSelection();
        //txtdegree.Text = string.Empty;
        // ddl_alumniType.ClearSelection();
        E_txtEmail.Text = string.Empty;
        R_txtContactno.Text = string.Empty;
        txtCurrentOccupation.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        txtperadd.Text = string.Empty;
        txtCurrentAddress.Text = string.Empty;
        txtsplinterest.Text = string.Empty;
        txtachievement.Text = string.Empty;
        //txttelephoneno.Text = string.Empty;
        txtremarks.Text = string.Empty;
        //rdbmale.Checked = true;
        //rdbfemale.Checked = false;
        rdbGender_SelectedIndexChanged(null, null);
        ddldegree.ClearSelection();
        //ddldegree_SelectedIndexChanged(null, null);
        ddldegree.ClearSelection();
        subjectlist.ClearSelection();
        subjectlist.Items.Clear();
        subjectlist.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        D_ddlYeofPass.ClearSelection();
        ddlDepartment.ClearSelection();
        //binalumnino();
        ViewState["FileDtl"] = null;
        permanentaddchk.Checked = false;
        //rdalumnitype.ClearSelection();
        rbdAlumniMemType_SelectedIndexChanged(null, null);
        //rdalumnitype.Items.FindByValue("SM").Selected = true;
        //lblOnlineFees.Text = "1000";
        //FeeDetails();
    }

    private void FeeDetails()
    {
        if (alumni_Type == "F" || alumni_Type == "ExStu")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rbdAlumniMemType.Items.FindByValue("LM").Selected = true;
                Membershiptype = rbdAlumniMemType.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y")
            {
                rbdAlumniMemType.Items.FindByValue("LM").Selected = true;
                Membershiptype = rbdAlumniMemType.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        else if (alumni_Type == "S")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rbdAlumniMemType.Items.FindByValue("SM").Selected = true;
                Membershiptype = rbdAlumniMemType.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y")
            {
                rbdAlumniMemType.Items.FindByValue("SM").Selected = true;
                Membershiptype = rbdAlumniMemType.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void rdPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeeDetails();
    }

    public StoredProcedure Alm_GetMembershipFee()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_FeeCollection", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@membershipType", Membershiptype, DbType.String);
        return sp;
    }

    public StoredProcedure Alm_GetMembershipFee(string membershiptype)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_FeeCollection", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@membershipType", Membershiptype, DbType.String);
        return sp;
    }

    protected void rdbIsPersonDisability_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string dectype2 = cpt.Decrypt(Session["RBValue"].ToString());
        //alumniType = dectype2.ToString();

        if (Request.QueryString["ID"] != null)
        {
            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(cpt.DecodeString(Request.QueryString["ID"].ToString()))).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["alumnitype"].ToString()))
                {
                    alumni_Type = ds.Tables[0].Rows[0]["alumnitype"].ToString();
                }
            }
        }

        if (rdbIsPersonDisability.SelectedValue == "Y")
        {
            isChkDisabilityPnl.Visible = true;
            disabilitySection.Visible = true;
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);
            btnPayment.Text = "SUBMIT";
        }
        else if (rdbIsPersonDisability.SelectedValue == "N")
        {
            isChkDisabilityPnl.Visible = false;
            disabilitySection.Visible = false;
            isChkDisability.Checked = false;
            txtDisabilityRemark.Text = "";
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);
            btnPayment.Text = "PAY NOW";
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void isChkDisability_CheckedChanged(object sender, EventArgs e)
    {
        if (isChkDisability.Checked && rdbIsPersonDisability.SelectedValue == "Y")
        {
            Anthem.Manager.IncludePageScripts = true;
            isChkDisabilityPnl.Visible = true;
            disabilityRemarkSection.Style["display"] = "block";
            isChkDisability.Checked = true;
            //disabilityDocuments.Visible = true;
            fileDisabilityDoc.Visible = true;
            btnDisabilityDoc.Visible = true;
            //divDisabilityDoc.Style["display"] = "block";
            lblOnlineFees.Text = "0";
            Session["RegFees"] = "0";
            hdnId.Value = "0";
        }
        else
        {
            Anthem.Manager.IncludePageScripts = true;
            disabilityRemarkSection.Style["display"] = "none";
            isChkDisability.Checked = false;
            //disabilityDocuments.Visible = false;
            fileDisabilityDoc.Visible = false;
            btnDisabilityDoc.Visible = false;
            //divDisabilityDoc.Style["display"] = "none";
            txtDisabilityRemark.Text = "";
            FeeDetails();

            if (Session["filePath_refletter_delete"] != null && Session["filePath_refletter_delete"].ToString() != "")
            {
                // Check if the file exists before attempting to delete it
                string filePathToDelete = Session["filePath_refletter_delete"].ToString();
                if (System.IO.File.Exists(filePathToDelete))
                {
                    try
                    {
                        System.IO.File.Delete(filePathToDelete);
                    }
                    catch (Exception ex)
                    {
                        Label1.Text = "Error deleting file: " + ex.Message;
                    }
                }
            }
            hyperDisabilityDoc.Text = "";
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    string FileType = "";
    double filesize = 0;
    DataRow drimg = null;
    string Fileuniquename = string.Empty;

    protected void btnDisabilityDoc_Click(object sender, EventArgs e)
    {
        if (fileDisabilityDoc.HasFile)
        {
            FileType = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);
            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpeg" && FileType != ".JPEG" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".png")
            {
                ClientMessaging("This should be in pdf, jpeg, jpg!");
                return;
            }
            else
            {
                string lFileName = "";
                Random rand = new Random();
                int n = rand.Next();

                string filename = "HPU_Alumni_DD_" + Guid.NewGuid().ToString() + "_" + rand.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + rand.Next(33333, 454545454) + "_" + rand.Next(999999, 15215454) + FileType;

                lFileName = FU_physicalPath(fileDisabilityDoc, "Alumni\\StuImage\\", filename);
                Session["DDFile"] = lFileName;
                fileDisabilityDoc.SaveAs(Server.MapPath("ALM_Reports//" + lFileName));
                ClientMessaging("file uploaded Successfully..");
                string filePathforhyperlink = "ALM_Reports//" + Session["DDFile"].ToString();
                string filePath = Server.MapPath("ALM_Reports/") + Session["DDFile"].ToString();
                Session["filePath_refletter_delete"] = filePath;
                hyperDisabilityDoc.Visible = true;
                hyperDisabilityDoc.NavigateUrl = filePathforhyperlink;
                hyperDisabilityDoc.Text = fileDisabilityDoc.FileName;
            }
        }
        else
        {
            ClientMessaging("Please Upload Special Disabilities Documents.!!!");
        }
    }

    public string FU_physicalPath(Anthem.FileUpload flu, string FolderName, string FileName)
    {
        try
        {
            if (flu.PostedFile != null && flu.HasFile && flu.PostedFile.ContentLength > 0)
            {
                string host = HttpContext.Current.Request.Url.Host;
                string upldPath = "";
                DataSet dsFilepath = new DataSet();
                dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
                foreach (DataRow dr in dsFilepath.Tables[0].Rows)
                {
                    if (host == dr["Server_Ip"].ToString().Trim())
                    {
                        upldPath = dr["Physical_Path"].ToString().Trim();
                        upldPath = upldPath + "Alumni\\StuImage\\";
                        Session["filepath"] = upldPath;
                        // upldPath = upldPath + "\\" + FolderName + "\\" + FileName;
                        upldPath = upldPath + FileName;
                        flu.SaveAs(upldPath);
                        return FileName;
                    }
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            throw new Exception("Upload Fails!");
        }
    }

}