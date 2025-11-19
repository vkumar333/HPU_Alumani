using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using DataAccessLayer;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Linq;
using SubSonic;
using System.Net.Mail;
using System.Net.Security;
using System.Web.UI;
using System.Net.Mime;

public partial class Alumni_ALM_AlumniRegistration : System.Web.UI.Page
{
    #region "Common Object Declation"

    //crypto cpt = new crypto();
    DataAccess Dobj = new DataAccess();
    private Boolean IsPageRefresh = false;

    #endregion
	
    /// <summary>
    /// SP Section
    /// </summary>
    /// <returns></returns>

    public StoredProcedure ALM_ALM_ACD_Salutation_fill()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ACD_Salutation_Sel", DataService.GetInstance("IUMSNXG"), "");
        //sp.Command.AddParameter("@C_id", C_id, DbType.Int32);
        return sp;
    }
	
    public StoredProcedure ALM_ACD_Degree_Mst_sel()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_ACD_Degree_Mst_sel", DataService.GetInstance("IUMSNXG"), "");
        //sp.Command.AddParameter("@C_id", C_id, DbType.Int32);
        return sp;
    }

    public StoredProcedure Bind_subject()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_Bind_subject", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_degreeid", pk_degreeid, DbType.Int32);
        return sp;
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

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        string alumniTypeValue = null;

    //        // First priority: Session
    //        if (Session["RBValue"] != null)
    //        {
    //            alumniTypeValue = Session["RBValue"].ToString();
    //        }
    //        // If not in Session, check QueryString
    //        else if (!string.IsNullOrEmpty(Request.QueryString["type"]))
    //        {
    //            alumniTypeValue = Request.QueryString["type"];
    //        }

    //        if (!string.IsNullOrEmpty(alumniTypeValue))
    //        {
    //            string selectedAlumniType;

    //            switch (alumniTypeValue.ToLower())
    //            {
    //                case "faculty":
    //                    selectedAlumniType = "F";
    //                    break;
    //                case "currentstudent":
    //                    selectedAlumniType = "S";
    //                    break;
    //                case "exstudent":
    //                    selectedAlumniType = "ExStu";
    //                    break;
    //                case "studentchapter":
    //                    selectedAlumniType = "StuCh";
    //                    break;
    //                default:
    //                    Response.Redirect("../Alumin_Loginpage.aspx", false);
    //                    return;
    //            }

    //            alumniType = selectedAlumniType;

    //            // Initialize controls
    //            clear1();
    //            txtAlumniName.Focus();
    //            lblMsg.Text = string.Empty;
    //            Anthem.Manager.IncludePageScripts = true;
    //        }
    //        else
    //        {
    //            Response.Redirect("../Alumin_Loginpage.aspx", false);
    //            return;
    //        }

    //        Anthem.Manager.IncludePageScripts = true;
    //    }
    //    else
    //    {
    //        string alumniTypeValue = null;

    //        // First priority: Session
    //        if (Session["RBValue"] != null)
    //        {
    //            alumniTypeValue = Session["RBValue"].ToString();
    //        }
    //        // If not in Session, check QueryString
    //        else if (!string.IsNullOrEmpty(Request.QueryString["type"]))
    //        {
    //            alumniTypeValue = Request.QueryString["type"];
    //        }

    //        if (!string.IsNullOrEmpty(alumniTypeValue))
    //        {
    //            string selectedAlumniType;

    //            switch (alumniTypeValue.ToLower())
    //            {
    //                case "faculty":
    //                    selectedAlumniType = "F";
    //                    break;
    //                case "currentstudent":
    //                    selectedAlumniType = "S";
    //                    break;
    //                case "exstudent":
    //                    selectedAlumniType = "ExStu";
    //                    break;
    //                case "studentchapter":
    //                    selectedAlumniType = "StuCh";
    //                    break;
    //                default:
    //                    Response.Redirect("../Alumin_Loginpage.aspx", false);
    //                    return;
    //            }

    //            alumniType = selectedAlumniType;
    //        }
    //        else
    //        {
    //            Response.Redirect("../Alumin_Loginpage.aspx", false);
    //            return;
    //        }
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        string selectedAlumniType = null;

        if (!IsPostBack)
        {
            if (TryGetAlumniType(out selectedAlumniType))
            {
                alumniType = selectedAlumniType;

                // Initialize controls
                clear1();
                txtAlumniName.Focus();
                lblMsg.Text = string.Empty;
                Anthem.Manager.IncludePageScripts = true;
            }
            else
            {
                Response.Redirect("../Alumin_Loginpage.aspx", false);
                return;
            }
        }
        else
        {
            if (TryGetAlumniType(out selectedAlumniType))
            {
                alumniType = selectedAlumniType;
            }
            else
            {
                Response.Redirect("../Alumin_Loginpage.aspx", false);
                return;
            }
        }
    }

    /// <summary>
    /// Tries to get the alumni type from session or query string and maps it to a code.
    /// </summary>
    /// <param name="selectedAlumniType">The mapped alumni type code.</param>
    /// <returns>True if valid alumni type found; otherwise false.</returns>
    private bool TryGetAlumniType(out string selectedAlumniType)
    {
        selectedAlumniType = null;

        string alumniTypeValue = null;

        // First priority: Session
        if (Session["RBValue"] != null)
        {
            alumniTypeValue = Session["RBValue"].ToString();
        }
        // If not in Session, check QueryString
        else if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            alumniTypeValue = Request.QueryString["type"];
        }

        if (string.IsNullOrEmpty(alumniTypeValue))
            return false;

        switch (alumniTypeValue.ToLower())
        {
            case "faculty":
                selectedAlumniType = "F";
                break;
            case "currentstudent":
                selectedAlumniType = "S";
                break;
            case "exstudent":
                selectedAlumniType = "ExStu";
                break;
            case "studentchapter":
                selectedAlumniType = "StuCh";
                break;
            default:
                return false;
        }

        return true;
    }

    #region "Methods for Binding data of DropDown and Autono on pageLoad"

    /// <summary>
    /// Bind Auto Alumni Number.
    /// </summary>
    void binalumnino()
    {
        DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_acode().GetDataSet();
        //R_txtAlumnino.Text = ds.Tables[0].Rows[0]["alumcode"].ToString();
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void Fill_PassingYear()
    {
        //D_ddlYeofPass.Items.Clear();
        DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();
        //if (ds.Tables[2].Rows.Count > 0)
        //{
        //    D_ddlYeofPass.DataSource = ds.Tables[2];
        //    D_ddlYeofPass.DataTextField = "PassYear_Name";
        //    D_ddlYeofPass.DataValueField = "PassYear_Name";
        //    D_ddlYeofPass.DataBind();
        //    D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
        //}
        //else
        //{
        //    D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
        //}

        //D_ddlYeofPass.Items.Clear();
        //DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();
        //if (ds != null && ds.Tables[2].Rows.Count > 0)
        //{
        //    D_ddlYeofPass.DataSource = ds.Tables[2];
        //    D_ddlYeofPass.DataTextField = "PassYear_Name";
        //    D_ddlYeofPass.DataValueField = "Pk_pass_id";
        //    D_ddlYeofPass.DataBind();
        //    D_ddlYeofPass.Items.Insert(0, new ListItem("-- Select Passing Year -- ", "0"));
        //}
        //else
        //{
        //    D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
        //}
    }

    /// <summary>
    /// Bind Department From Degree Wise
    /// </summary>
    protected void filldegree()
    {
        DataSet ds = ALM_ACD_Degree_Mst_sel().GetDataSet();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddldegree.DataValueField = "pk_degreeid";
        //    ddldegree.DataTextField = "degree";
        //    ddldegree.DataSource = ds;
        //    ddldegree.DataBind();
        //    ddldegree.Items.Insert(0, new ListItem("-- Select Degree --", "0"));
        //}
        //else
        //{
        //    ddldegree.DataSource = null;
        //    ddldegree.DataBind();
        //}
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

            //Drp_FatherName.DataValueField = "PK_Salutation_ID";
            //Drp_FatherName.DataTextField = "Salutation_Name";
            //Drp_FatherName.DataSource = ds;
            //Drp_FatherName.DataBind();
            //Drp_FatherName.Items.Insert(0, new ListItem("- Title -", "0"));

            //Drp_MotherName.DataTextField = "Salutation_Name";
            //Drp_MotherName.DataValueField = "PK_Salutation_ID";
            //Drp_MotherName.DataSource = ds;
            //Drp_MotherName.DataBind();
            //Drp_MotherName.Items.Insert(0, new ListItem("- Title -", "0"));
        }
    }

    private void getsubject(int pk_degreeid)
    {
        DataSet ds = Bind_subject().GetDataSet();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    subjectlist.DataValueField = "pk_subjectid";
        //    subjectlist.DataTextField = "subject";
        //    subjectlist.DataSource = ds;
        //    subjectlist.DataBind();
        //    subjectlist.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        //}
        //else
        //{
        //    subjectlist.DataSource = null;
        //    subjectlist.DataBind();
        //    subjectlist.SelectedIndex = 0;
        //}
        //Anthem.Manager.IncludePageScripts = true;
    }

    private void FillDepartment()
    {
        DataSet ds = new DataSet();
        ds = ALM_Department_sel();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlDepartment.DataValueField = "Pk_deptId";
        //    ddlDepartment.DataTextField = "description";
        //    ddlDepartment.DataSource = ds;
        //    ddlDepartment.DataBind();
        //    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        //}
        //else
        //{
        //    ddlDepartment.DataSource = null;
        //    ddlDepartment.DataBind();
        //}
    }

    /// <summary>
    /// Bind Alumni Dropdown
    /// modified By Aditya Sharma
    /// </summary>
    //private void FillAlumniType()
    //{
    //    DataSet ds = new DataSet();
    //    ds = ALM_GetAlumni();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddl_alumniType.DataValueField = "PK_Alumni_TypeID";
    //        ddl_alumniType.DataTextField = "Alumni_type";
    //        ddl_alumniType.DataSource = ds;
    //        ddl_alumniType.DataBind();
    //        ddl_alumniType.Items.Insert(0, new ListItem("--Select Alumni Type--", "0"));
    //    }
    //    else
    //    {
    //        ddl_alumniType.DataSource = null;
    //        ddl_alumniType.DataBind();
    //    }
    //}

    public DataSet ALM_Department_sel()
    {
        Clear();
        return DAobj.GetDataSet("ALM_Department_sel", values, names, types);
    }

    //public DataSet ALM_ACD_Degree_Mst_sel()
    //{
    //    Clear();
    //    return DAobj.GetDataSet("ALM_ACD_Degree_Mst_sel", values, names, types);
    //}

    #endregion


    #region "Dropdown Change events"
    //protected void D_ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (D_ddlCollege.SelectedIndex > 0)
    //    {
    //        FillDegrees(Convert.ToInt32(D_ddlCollege.SelectedValue));
    //    }
    //}
    #endregion

    #region "Other common Events"

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    /// <summary>
    /// Cleaer Function
    /// </summary>
    void clear1()
    {
        FillSalution();
        txtAlumniName.Text = "";
        //R_txtMotherName.Text = string.Empty;
        //R_txtFatherName.Text = string.Empty;
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
        txtLocation.Text = string.Empty;
        txtCountry.Text = string.Empty;
        //rdbmale.Checked = true;
        //rdbfemale.Checked = false;
        rdbGender.SelectedIndex = -1;
        //ddldegree.ClearSelection();
        //ddldegree_SelectedIndexChanged(null, null);
        //ddldegree.ClearSelection();
        //subjectlist.ClearSelection();
        //subjectlist.Items.Clear();
        //subjectlist.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        //D_ddlYeofPass.ClearSelection();
        //ddlDepartment.ClearSelection();

        generate_GridView(gvQualifications, 1);
        BindGridViewDropdown();
        ViewState["PreviousRow"] = null;

        binalumnino();
        ViewState["FileDtl"] = null;
        permanentaddchk.Checked = false;
        txtperadd_TextChanged(null, null);
        //rdalumnitype.ClearSelection();
        rdalumnitype_SelectedIndexChanged(null, null);
        rdbIsPersonDisability.SelectedValue = "N";
        rdbIsPersonDisability_SelectedIndexChanged(null, null);
        //rdalumnitype.Items.FindByValue("SM").Selected = true;
        //lblOnlineFees.Text = "1000";
        //FeeDetails();
        Session["DDFile"] = null;
        Session["PPFile"] = null;
        Session["ADFile"] = null;

        if (Session["DDfilePath_refletter_delete"] != null && Session["DDfilePath_refletter_delete"].ToString() != "")
        {
            // Check if the file exists before attempting to delete it
            string filePathToDelete = Session["DDfilePath_refletter_delete"].ToString();
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

        if (Session["PPfilePath_refletter_delete"] != null && Session["PPfilePath_refletter_delete"].ToString() != "")
        {
            // Check if the file exists before attempting to delete it
            string PPfilePathToDelete = Session["PPfilePath_refletter_delete"].ToString();
            if (System.IO.File.Exists(PPfilePathToDelete))
            {
                try
                {
                    System.IO.File.Delete(PPfilePathToDelete);
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error deleting file: " + ex.Message;
                }
            }
        }
        anchorPath.Text = "";
        imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";

        if (Session["ADfilePath_refletter_delete"] != null && Session["ADfilePath_refletter_delete"].ToString() != "")
        {
            // Check if the file exists before attempting to delete it
            string ADfilePathToDelete = Session["ADfilePath_refletter_delete"].ToString();
            if (System.IO.File.Exists(ADfilePathToDelete))
            {
                try
                {
                    System.IO.File.Delete(ADfilePathToDelete);
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error deleting file: " + ex.Message;
                }
            }
        }
        HyperLink1.Text = "";
    }

    #endregion

    #region "Save and update Methods"
    /// <summary>
    /// Get the xml schema of ALM_AlumniRegistration and [ALM_AlumniRegistration_File_dtl] tables and assing value
    /// </summary>	
    protected void Save()
    {
        DataSet ds = new DataSet();
        ds = Dobj.GetSchema("ALM_AlumniRegistration");
        DataRow dr = ds.Tables[0].NewRow();
        dr["alumnino"] = (object)DBNull.Value; //R_txtAlumnino.Text.Trim();
        dr["alumni_name"] = txtAlumniName.Text.Trim();
        dr["fk_collegeid"] = 0;
        dr["fk_degreeid"] = (object)DBNull.Value;//ddldegree.SelectedValue;
        dr["Fk_subjectid"] = (object)DBNull.Value; //subjectlist.SelectedValue;
        //  dr["fk_degreeid"] = txtdegree.Text.Trim();//
        dr["yearofpassing"] = (object)DBNull.Value; //Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text);
        dr["fk_pyearid"] = (object)DBNull.Value; //Convert.ToInt32(D_ddlYeofPass.SelectedValue);
        dr["email"] = E_txtEmail.Text.Trim();
        dr["currentaddress"] = txtCurrentAddress.Text.Trim();
        dr["per_address"] = txtperadd.Text.Trim();
        dr["currentoccupation"] = txtCurrentOccupation.Text.Trim();
        dr["contactno"] = R_txtContactno.Text.Trim();
        dr["special_interest"] = txtsplinterest.Text.Trim();
        dr["Achievement"] = txtachievement.Text.Trim();
        dr["remarks"] = txtremarks.Text.Trim();
        dr["dob"] = R_txtPostedDate.Text.Trim();
        dr["Current_Location"] = txtLocation.Text.Trim();
        dr["Current_Country"] = txtCountry.Text.Trim();
        // Added By Aditya sharma
        //dr["alumnitype"] = Session["RBValue"].ToString();

        if (Session["RBValue"] != null)
        {
            //crypto cpt = new crypto();
            //string dectype = cpt.Decrypt(Session["RBValue"].ToString());
            //string dectype = AesEnc.DecryptStringFromBytes_Aes(Session["RBValue"].ToString());
            //alumniType = dectype.ToString();

            //string decrypted = CryptoAESDES.DecryptStringFromBase64_Aes(Session["RBValue"].ToString(), aesKey, aesIV);
            //alumniType = decrypted;
            dr["alumnitype"] = alumniType;
        }
        else if (Request.QueryString["type"] != null)
        {
            //crypto cpt = new crypto();
            //string dectype = cpt.Decrypt(Request.QueryString["type"].ToString());
            //string dectype = AesEnc.DecryptStringFromBytes_Aes(Request.QueryString["type"].ToString());
            //alumniType = dectype.ToString();

            //string decrypted = CryptoAESDES.DecryptStringFromBase64_Aes(Request.QueryString["type"].ToString(), aesKey, aesIV);
            //alumniType = decrypted;
            dr["alumnitype"] = alumniType;
        }

        //Convert.ToInt32(ddl_alumniType.SelectedValue);
        //if (ddl_alumniType.SelectedValue == "1")
        //dr["fk_Deptid"] = txtdepartment.Text.Trim();//
        dr["fk_Deptid"] = (object)DBNull.Value; //Convert.ToInt32(ddlDepartment.SelectedValue);
        //}
        //else
        //{
        //    dr["fk_Deptid"] = null;
        //}      
        dr["designation"] = txtDesignation.Text.Trim();
        //dr["mothername"] = R_txtMotherName.Text.Trim();
        //dr["fathername"] = R_txtFatherName.Text.Trim();
        dr["mothername"] = DBNull.Value;
        dr["fathername"] = DBNull.Value;
        // dr["telephoneno"] = txttelephoneno.Text.Trim();
        dr["loginname"] = (object)DBNull.Value; //R_txtAlumnino.Text.Trim();
        dr["password"] = R_txtPostedDate.Text.Trim();//crp.Encrypt(R_txtPassword.Text.Trim());

        //if (rdbmale.Checked == true)
        //{
        //    dr["gender"] = "M";
        //}
        //else if (rdbfemale.Checked == true)
        //{
        //    dr["gender"] = "F";
        //}

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

        dr["Membership_Type"] = rdalumnitype.SelectedValue;

        if (Drp_Alumni_Name.SelectedIndex > 0)
        {
            dr["Alumni_Sal"] = Drp_Alumni_Name.SelectedValue.ToString().Trim();
        }
        else
        {
            dr["Alumni_Sal"] = DBNull.Value;
        }

        //if (Drp_FatherName.SelectedIndex > 0)
        //{
        //    dr["Father_Sal"] = Drp_FatherName.SelectedValue.ToString().Trim();
        //}
        //else
        //{
        //    dr["Father_Sal"] = DBNull.Value;
        //}

        //if (Drp_MotherName.SelectedIndex > 0)
        //{
        //    dr["Mother_Sal"] = Drp_MotherName.SelectedValue.ToString().Trim();
        //}
        //else
        //{
        //    dr["Mother_Sal"] = DBNull.Value;
        //}

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

        ds.Tables[0].Rows.Add(dr);


        #region "For GridView Qualifications Details"

        DataSet dsQual = Dobj.GetSchema("ALM_EducationQualifications");
        dsQual.Tables[0].TableName = "ALM_EducationQualifications";

        //dsQual.Tables[0].Columns.Add("fk_DegreeID", typeof(int));
        //dsQual.Tables[0].Columns.Add("fk_SubjectID", typeof(int));
        //dsQual.Tables[0].Columns.Add("fk_PassingYearID", typeof(int));
        //dsQual.Tables[0].Columns.Add("fk_DeptID", typeof(string));

        foreach (GridViewRow item in gvQualifications.Rows)
        {
            DropDownList ddlDegree = item.FindControl("ddldegree") as DropDownList;
            DropDownList ddlSubject = item.FindControl("subjectlist") as DropDownList;
            DropDownList ddlPassingYear = item.FindControl("D_ddlYeofPass") as DropDownList;
            DropDownList ddlDepartment = item.FindControl("ddlDepartment") as DropDownList;

            DataRow drQual;
            drQual = dsQual.Tables[0].NewRow();

            drQual["fk_DegreeID"] = ddlDegree.SelectedValue;
            drQual["fk_SubjectID"] = ddlSubject.SelectedValue;
            drQual["fk_PassingYearID"] = ddlPassingYear.SelectedValue;
            drQual["fk_DeptID"] = ddlDepartment.SelectedValue;
            dsQual.Tables[0].Rows.Add(drQual);
        }

        #endregion

        #region "For Profile Pic"

        Random randomNo = new Random();
        string Message = "";
        string FileType = "";
        double filesize = 0;
        DataRow drimg = null;
        string Fileuniquename = string.Empty;

        DataSet dsImg = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
        dsImg.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";
        //drimg = dsImg.Tables[0].NewRow();

        #region "Pic Upload"

        if (Session["PPFile"] != null)
        {
            string PicName = Session["PPFile"].ToString();
            FileType = Path.GetExtension(Session["PPFile"].ToString());
            drimg = dsImg.Tables[0].NewRow();
            drimg["IsProfilePicOrDoc"] = 1;
            drimg["Files_Name"] = anchorPath.Text.Trim().ToString();
            drimg["Files_Unique_Name"] = Session["PPFile"].ToString();
            drimg["FileExtension"] = FileType;
            drimg["File_Path"] = Session["filepath"].ToString();
            drimg["FilesFor"] = "PP";
            dsImg.Tables[0].Rows.Add(drimg);
        }

        if (flUpload.HasFile == true)
        {
            string Name = flUpload.PostedFile.FileName;
            FileType = Path.GetExtension(flUpload.PostedFile.FileName);
            UploadFiles(); //to get the physical path of server 
            string upldPath = "";
            string currDir = System.IO.Directory.GetCurrentDirectory();
            upldPath = this.upldPath;

            drimg["IsProfilePicOrDoc"] = 1;
            drimg["Files_Name"] = Name;
            drimg["Files_Unique_Name"] = "HPU_Alumni_PP_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
            Fileuniquename = "HPU_Alumni_PP_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
               DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
            drimg["FileExtension"] = FileType;
            drimg["File_Path"] = upldPath;
            drimg["FilesFor"] = "PP";

            // save only file unique name so that we can upload this seperately
            dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds

            bool IsExistPath = System.IO.Directory.Exists(upldPath);
            if (!IsExistPath)
                System.IO.Directory.CreateDirectory(upldPath);
            flUpload.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
        }

        #endregion

        #region "Document Upload"

        if (Session["ADFile"] != null)
        {
            string DocName = Session["ADFile"].ToString();
            FileType = Path.GetExtension(Session["ADFile"].ToString());
            drimg = dsImg.Tables[0].NewRow();
            drimg["IsProfilePicOrDoc"] = 0;
            drimg["Files_Name"] = HyperLink1.Text.Trim().ToString(); ;
            drimg["Files_Unique_Name"] = Session["ADFile"].ToString();
            drimg["FileExtension"] = FileType;
            drimg["File_Path"] = Session["filepath"].ToString();
            drimg["FilesFor"] = "AD";
            dsImg.Tables[0].Rows.Add(drimg);
        }

        if (uploadDocuments.HasFile == true)
        {
            string DocumentName = uploadDocuments.PostedFile.FileName;
            FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);
            drimg = dsImg.Tables[0].NewRow();
            drimg["IsProfilePicOrDoc"] = 0;
            UploadFiles();//to get the physical path of server 

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

            dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds

            bool IsExistPath = System.IO.Directory.Exists(upldPath);
            if (!IsExistPath)
                System.IO.Directory.CreateDirectory(upldPath);
            uploadDocuments.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
        }

        #endregion

        #region "Disability Document Upload"

        if (Session["DDFile"] != null)
        {
            string DocumentName = Session["DDFile"].ToString();
            FileType = Path.GetExtension(Session["DDFile"].ToString());
            drimg = dsImg.Tables[0].NewRow();
            drimg["IsProfilePicOrDoc"] = 0;
            drimg["Files_Name"] = hyperDisabilityDoc.Text.Trim().ToString();
            drimg["Files_Unique_Name"] = Session["DDFile"].ToString();
            drimg["FileExtension"] = FileType;
            drimg["File_Path"] = Session["filepath"].ToString();
            drimg["FilesFor"] = "DD";
            dsImg.Tables[0].Rows.Add(drimg);
        }

        if (fileDisabilityDoc.HasFile == true)
        {
            string DocumentName = fileDisabilityDoc.PostedFile.FileName;
            FileType = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);
            drimg = dsImg.Tables[0].NewRow();
            drimg["IsProfilePicOrDoc"] = 0;
            UploadFiles(); //to get the physical path of server 

            string upldPath = "";
            string currDir = System.IO.Directory.GetCurrentDirectory();
            upldPath = this.upldPath;

            Random randomNo1 = new Random();

            drimg["Files_Name"] = DocumentName;
            drimg["Files_Unique_Name"] = "HPU_Alumni_DD_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
            drimg["FileExtension"] = FileType;
            drimg["File_Path"] = upldPath;
            drimg["FilesFor"] = "DD";

            dsImg.Tables[0].Rows.Add(drimg);

            //bool IsExistPath = System.IO.Directory.Exists(upldPath);
            //if (!IsExistPath)
            //    System.IO.Directory.CreateDirectory(upldPath);
            //fileDisabilityDoc.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
        }

        #endregion

        #endregion

        if (dsQual != null)
        {
            ds.Merge(dsQual);
        }

        if (dsImg != null)
        {
            ds.Merge(dsImg);
        }

        //  string Message = "";
        ArrayList Result = new ArrayList();
        IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
        string XmlDoc1 = ds.GetXml();

        if (InsertAlumniRecord(XmlDoc1, ref Message, ref Result) > 0)
        {
            if (Result.Count > 0)
            {
                //ClientMessaging("Your Basic Detail has Been Saved! Login ID and Password will be Genterated after the Payment");
                //Session["regid"] = Result[1].ToString().Trim();
                //lblMsg.Text = "Your Basic Detail has Been Saved! Please Payment For the Further prosess";
                ////ClientMessaging("Your Basic Detail has Been Saved! Login ID and Password will be Genterated after the Payment");
                //// SendMail(); 

                if (hdnId.Value == "0")
                {
                    ClientMessaging("Alumni is Successfully Registered.!!!");
                    lblMsg.Text = "Alumni is Successfully Registered.!!!";

                    int alumniid = Convert.ToInt32(Result[1].ToString().Trim());
                    DataSet dsS = GetUserCredentials(alumniid);

                    if (dsS.Tables[0].Rows.Count > 0)
                    {
                        Session["Alumni_Name"] = dsS.Tables[0].Rows[0]["alumni_name"].ToString();
                        Session["AlumniNo"] = dsS.Tables[0].Rows[0]["alumnino"].ToString();
                        Session["EmailID"] = dsS.Tables[0].Rows[0]["email"].ToString();
                        Session["Password"] = dsS.Tables[0].Rows[0]["password"].ToString();
                        Session["MobileNo"] = dsS.Tables[0].Rows[0]["contactno"].ToString();
                    }

                    string splitMailCC = "";
                    SendMail("a", splitMailCC);
                    updateAlumniApproved(alumniid);
                    clear1();
                    return;

                    //string script = "alert('Alumni is Successfully Registered.!!!'); window.location='../Alumin_Loginpage.aspx';";
                    //Anthem.Manager.AddScriptForClientSideEval(script);

                    //ClientMessagingShownAndRedirectedToPage("Alumni is Successfully Registered.!!!");
                }
                else
                {
                    ClientMessaging("Your Basic Detail has Been Saved! Login ID and Password will be Genterated after the Payment");
                    Session["regid"] = Result[1].ToString().Trim();
                    lblMsg.Text = "Your Basic Detail has Been Saved! Please Payment For the Further prosess";
                }
            }
        }
        else
        {
            lblMsg.Text = "Retry!";
        }
        //}
        //catch (Exception ex)
        //{
        //     lblMsg.Text = ex.Message.ToString();
        //}
    }

    /// <summary>
    /// Mail Integration
    /// </summary>
    private void SendMail()
    {
        try
        {
            Session["TempRegId"] = null;//clear here tempsession so that this page can't be loaded again.
            RegId = Convert.ToInt32(Session["RegId"].ToString());
            DataSet ds = Get_All_DetailsAfterRegistration();
            if (ds.Tables[0].Rows.Count > 0)
            {

                dvMsg.Style.Remove("display");
                Session["regno"] = ds.Tables[0].Rows[0]["alumnino"].ToString();
                //Session["RegFees"] = ds.Tables[0].Rows[0]["RegFees"].ToString();
                Session["SName"] = ds.Tables[0].Rows[0]["alumni_name"].ToString();
                //send here sms and put also code for sending Email
                #region "Sms sent Code"

                string Message = "All candidates shall appear Personally along with certificates as per the following schedule:" +
                                  "23 - 08 - 2021:  General Category who have scored in 10 + 2 examination 85 % 25 or more than 85 % 25 marks." +
                                  "24 - 08 - 2021: General Category who have scored in 10 + 2 examination less than 85 % 25 marks." +
                                  "25 - 08 - 2021: All SC, ST, Single Girl Child, Physically Disable, University Wards, J% 26K Migrant candidates"
                                  + "Venue:" + "University Institute of Legal Studies, Ava Lodge, Chaura Maidan, Shimla - 4" + "0177 - 2651586 - IUMS & dlt_entity_id = 1601100000000007973 & dlt_template_id = 1607100000000135515";
                sendSingleSMS(ds.Tables[0].Rows[0]["contactno"].ToString().Trim(), Message);
                string Msg = "";
                RegId = Convert.ToInt32(Session["regid"].ToString());
                IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                //  DegreeType = Session["DegreeType"].ToString();
                Step = "1";
                IsSmsOrEmail = "SMS";
                MobileNo = ds.Tables[0].Rows[0]["contactno"].ToString().Trim();
                EmailId = ds.Tables[0].Rows[0]["email"].ToString().Trim();
                Details = Message;

                #endregion

                #region "Email Sent Code"

                if (SentMail("", EmailMsgBody(ds, "")) > 0)
                {
                    RegId = Convert.ToInt32(Session["regid"].ToString());
                    IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                    // DegreeType = Session["DegreeType"].ToString();
                    Step = "1";
                    IsSmsOrEmail = "EMAIL";
                    MobileNo = ds.Tables[0].Rows[0]["contactno"].ToString().Trim();
                    EmailId = ds.Tables[0].Rows[0]["email"].ToString().Trim();
                    Details = EmailMsgBody(ds, "");

                    //if (Insert_Registration_Sms_Email_SentDtls(ref Msg) > 0)
                    //{
                    //    //lblSmsEmailMsg.Text = "<B>Login Credentials has been sent to your Registered Mobile No. and Email.</B>";
                    //}
                }

                #endregion
            }
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// Email Message Body
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="ProgrammeName"></param>
    /// <returns></returns>
    private string EmailMsgBody(DataSet ds, string ProgrammeName)
    {
        StringBuilder strMail = new StringBuilder();

        strMail.Append("<Table width='100%' border='1' style='border:solid 1px #000000; border-collapse: collapse;' cellpadding='10' cellspacing='0'>");

        //--Header Of Mail Start
        strMail.Append("<Tr>");
        strMail.Append("<td colspan='2' style='text-align:center; border:solid 1px #000000; border-collapse: collapse;background:#d1faff'>");
        strMail.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");

        strMail.Append("<tr><td width='25%' align='right' style='padding-right:15px;'></td><td align='left'><span style='font-size:28px;'>Himachal Pradesh University</span></td> </tr>");

        strMail.Append("</table>");
        strMail.Append("</Td>");
        strMail.Append("</Tr>");

        //Student Name ,Registration No and Password, Program Tr START ==============
        strMail.Append("<tr>");
        strMail.Append("<td style='border:solid 1px #000000; border-collapse: collapse; background:#d1faff;font-size:20px' colspan='2'>");
        strMail.Append("<p> Dear <span style='color:#0033FF'><strong> " + ds.Tables[0].Rows[0]["alumni_name"].ToString() + ", </strong></span></p>");
        strMail.Append("<p>Your are Registered to <span style='color:#0033FF'> <strong> for Alumni </strong> </span>.</p>");
        strMail.Append("<p>Your User ID (Registration No.) is <span style='color:#0033FF'><strong>" + ds.Tables[0].Rows[0]["alumnino"].ToString() + " </strong> </span> &amp; Password is <span style='color:#0033FF'> <strong>" + ds.Tables[0].Rows[0]["dob"].ToString() + "</strong> </span></p>");
        strMail.Append("</Td>");
        strMail.Append("</tr>");

        //Footer of the Mail START-------------------
        strMail.Append("<tr>");
        strMail.Append("<td style='border:solid 1px #000000; background:#d1faff; font-size:20px' colspan='2'>");
        strMail.Append("<strong>Thanks   &amp; Regards</strong> <br />");
        strMail.Append("<strong>HPU</strong>");
        strMail.Append("<br /><br />");
        strMail.Append("<strong>NOTE: DO NOT REPLY TO THIS MAIL.</strong>");
        strMail.Append("</Td>");
        strMail.Append("</tr>");
        strMail.Append("</Table>");

        return strMail.ToString();
    }

    private void sendSingleSMS(string MobileNo, string Message)
    {
        try
        {
            //Send message
            string URL = "https://api.instaalerts.zone/SendSMS/sendmsg.php?uname=expediensadmin&pass=Pass@123&send=IUMSAP&dest=" + MobileNo + "&msg=" + Message;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected String encryptedPasswod(String password)
    {
        byte[] encPwd = Encoding.UTF8.GetBytes(password);
        //static byte[] pwd = new byte[encPwd.Length];
        HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
        byte[] pp = sha1.ComputeHash(encPwd);
        // static string result = System.Text.Encoding.UTF8.GetString(pp);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in pp)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    protected String hashGenerator(String Username, String sender_id, String message, String secure_key)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Username).Append(sender_id).Append(message).Append(secure_key);
        byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());
        //static byte[] pwd = new byte[encPwd.Length];
        HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");
        byte[] sec_key = sha1.ComputeHash(genkey);

        StringBuilder sb1 = new StringBuilder();
        for (int i = 0; i < sec_key.Length; i++)
        {
            sb1.Append(sec_key[i].ToString("x2"));
        }
        return sb1.ToString();
    }

    private int SentMail(string ProgrammeName, string EmailMsgBody)
    {
        int IsEmailSent = 0;
        try
        {

        }
        catch (Exception ex)
        {
            IsEmailSent = 0;
            string Msg = "";
        }
        return IsEmailSent;
    }

    public DataSet Get_All_DetailsAfterRegistration()
    {
        Clear();
        names.Add("@pk_alumniid"); types.Add(SqlDbType.BigInt); values.Add(_RegId);
        return DAobj.GetDataSet("[HPU_ALM_AlumniRegistration_Sel]", values, names, types);
    }

    public int InsertAlumniRecord(string xmlDoc, ref string Message, ref ArrayList Result)
    {
        Clear();
        names.Add("@xmlDoc"); values.Add(xmlDoc); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
        //  names.Add("@IpAddress"); values.Add(_IpAddress); types.Add(SqlDbType.VarChar); size.Add("MAX"); outtype.Add("false");
        names.Add("@pk_alumniid"); values.Add(regid); types.Add(SqlDbType.VarChar); size.Add("10"); outtype.Add("true");

        if (DAobj.ExecuteTransactionMsgIO("ALM_SP_AlumniRegistration_Ins", values, names, types, size, outtype, ref Message, ref Result) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int UpdateEmailSentDtl(int Pk_EmailId, long pk_Regid, string EmailSentFor, ref string Message)
    {
        Clear();
        names.Add("@Pk_EmailId"); values.Add(Pk_EmailId); types.Add(SqlDbType.VarChar);
        names.Add("@pk_Regid"); values.Add(pk_Regid); types.Add(SqlDbType.VarChar);
        names.Add("@EmailSentFor"); values.Add(EmailSentFor); types.Add(SqlDbType.VarChar);

        if (DAobj.ExecuteTransactionMsg("HPU_ALM_EmailCounter_Ins", values, names, types, ref Message) > 0)
        {
            Message = DAobj.ShowMessage("S", "");
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void UpdateEmailSentDtl(int Pk_EmailId, long pk_Regid, string EmailSentFor)
    {
        string msg = "";
        // objEmail.UpdateEmailSentDtl(Pk_EmailId, pk_Regid, EmailSentFor, ref msg);
    }

    protected void Update()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = Dobj.GetSchema("ALM_AlumniRegistration");
            DataRow dr = ds.Tables[0].NewRow();

            dr["alumni_name"] = txtAlumniName.Text.Trim();

            //dr["yearofpassing"] = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text);
            //dr["fk_pyearid"] = Convert.ToInt32(D_ddlYeofPass.SelectedValue);

            dr["yearofpassing"] = (object)DBNull.Value;
            dr["fk_pyearid"] = (object)DBNull.Value;

            dr["currentaddress"] = txtCurrentAddress.Text.Trim();
            dr["contactno"] = R_txtContactno.Text.Trim();
            dr["email"] = E_txtEmail.Text.Trim();
            dr["currentoccupation"] = txtCurrentOccupation.Text.Trim();
            dr["special_interest"] = txtsplinterest.Text.Trim();
            dr["Achievement"] = txtachievement.Text.Trim();
            dr["remarks"] = txtremarks.Text.Trim();
            dr["per_address"] = txtperadd.Text.Trim();
            dr["alumnino"] = (object)DBNull.Value; //R_txtAlumnino.Text.Trim();
            dr["designation"] = txtDesignation.Text.Trim();
            //dr["mothername"] = R_txtMotherName.Text.Trim();
            //dr["fathername"] = R_txtFatherName.Text.Trim();
            dr["mothername"] = DBNull.Value;
            dr["fathername"] = DBNull.Value;
            dr["Current_Location"] = txtLocation.Text.Trim();
            dr["Current_Country"] = txtCountry.Text.Trim();
            //dr["telephoneno"] = txttelephoneno.Text.Trim();

            //if (rdbmale.Checked == true)
            //{
            //    dr["gender"] = "M";
            //}
            //if (rdbfemale.Checked == true)
            //{
            //    dr["gender"] = "F";
            //}

            if (rdbGender.SelectedValue.ToString() == "M")
            {
                dr["gender"] = "M";
            }
            else if (rdbGender.SelectedValue.ToString() == "F")
            {
                dr["gender"] = "F";
            }

            dr["isMentor"] = chkmentor.Checked;

            if (Drp_Alumni_Name.SelectedIndex > 0)
            {
                dr["Alumni_Sal"] = Drp_Alumni_Name.SelectedValue.ToString();
            }
            else
            {
                dr["Alumni_Sal"] = DBNull.Value;
            }
            //dr["Alumni_Sal"] = Drp_Alumni_Name.SelectedValue;

            //if (Drp_FatherName.SelectedIndex > 0)
            //{
            //    dr["Father_Sal"] = Drp_FatherName.SelectedValue.ToString();
            //}
            //else
            //{
            //    dr["Father_Sal"] = DBNull.Value;
            //}
            ////dr["Father_Sal"] = Drp_FatherName.SelectedValue;

            //if (Drp_MotherName.SelectedIndex > 0)
            //{
            //    dr["Mother_Sal"] = Drp_MotherName.SelectedValue.ToString();
            //}
            //else
            //{
            //    dr["Mother_Sal"] = DBNull.Value;
            //}
            //dr["Mother_Sal"] = Drp_MotherName.SelectedValue;

            string filepath = ""; string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
            filepath = "";// hdPath.Text.ToString().Trim();

            if (filepath != "")
            {
                FileInfo imageInfo = new FileInfo(filepath);
                fileBytes = new byte[imageInfo.Length];
                FileStream imagestream = imageInfo.OpenRead();
                imagestream.Read(fileBytes, 0, fileBytes.Length);
                imagestream.Close();

                fileName = Path.GetFileName(imageInfo.Name);
                contenttype = imageInfo.Extension;
                dr["filename"] = fileName;
                dr["Contenttype"] = contenttype;
            }

            ds.Tables[0].Rows.Add(dr);
            string xmlstr = ds.GetXml();
            int AlumniI = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Upd(Convert.ToInt32(ViewState["id"]), xmlstr, fileBytes).Execute();

            if (AlumniI > 0)
            {
                lblMsg.Text = "Record Updated Successfully!";
                clear1();
            }
            else
            {
                lblMsg.Text = "Retry!";
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    #region "Change events of LoginName Declaration and Enrollmentdtls"

    protected int check_enrollment(string dob, string enrollmentno, int fk_degreeid)
    {

        IDataReader rdr = IUMSNXG_ALM.SP.ALM_checkEnrollment(dob, enrollmentno, fk_degreeid).GetReader();
        DataTable dt = new DataTable();
        dt.Load(rdr);
        rdr.Close(); rdr.Dispose();
        if (dt.Rows.Count == 0)
        {
            return 1;
        }
        if (dt.Rows[0]["pp"].ToString() != "1")
        {
            return 2;
        }
        if (dt.Rows[0]["pp"].ToString() != "0")
        {
            lblMsg.Text = "Either Enrollment No, Degree Name or Date of Birth is Incorrect!";

        }
        return 0;
    }

    #endregion

    #region "Register and Reset Event"

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Random randomNo = new Random();
        string Message = "";
        string FileTypes = "";
        string FileType = "";
        double filesize = 0;
        double filesizes = 0;
        string Name = "";
        string Names = "";
        DataRow drimg = null;
        string Fileuniquename = string.Empty;

        if (rdalumnitype.SelectedValue != "LM" && rdalumnitype.SelectedValue != "SM" && rdalumnitype.SelectedValue != "SC")
        {
            ClientMessaging("Please Choose Membership Type.!");
            rdalumnitype.Focus();
            return;
        }
        if (txtAlumniName.Text == "")
        {
            ClientMessaging("Please enter Alumni Name.!");
            txtAlumniName.Focus();
            return;
        }
        //if (R_txtFatherName.Text == "")
        //{
        //    ClientMessaging("Please enter Father Name.!");
        //    R_txtFatherName.Focus();
        //    return;
        //}
        //if (R_txtMotherName.Text == "")
        //{
        //    ClientMessaging("Please enter Mother Name.!");
        //    R_txtMotherName.Focus();
        //    return;
        //}
        if (R_txtPostedDate.Text.Trim() == "")
        {
            ClientMessaging("Please enter Date of Birth.!");
            R_txtPostedDate.Focus();
            return;
        }

        if (E_txtEmail.Text == "")
        {
            ClientMessaging("Please enter Email.!");
            E_txtEmail.Focus();
            return;
        }
        else
        {
            E_txtEmail_TextChanged(null, null);
        }

        if (R_txtContactno.Text.Trim().Length < 10)
        {
            ClientMessaging("Please enter valid Mobile No.!");
            R_txtContactno.Focus();
            return;
        }

        if (string.IsNullOrEmpty(rdbGender.SelectedValue))
        {
            ClientMessaging("Please select gender.!");
            rdbGender.Focus();
            return;
        }

        //if (rdbIsPersonDisability.SelectedValue == "Y" && isChkDisability.Checked == false)
        //{
        //    ClientMessaging("Please Check Special Ability More than 40% ?.");
        //    Anthem.Manager.IncludePageScripts = true;
        //    isChkDisability.Focus();
        //    return;
        //}

        //if (rdbIsPersonDisability.SelectedValue == "Y" && isChkDisability.Checked == true && txtDisabilityRemark.Text == "")
        //{
        //    ClientMessaging("Please Enter Special Ability Description.");
        //    Anthem.Manager.IncludePageScripts = true;
        //    txtDisabilityRemark.Focus();
        //    return;
        //}

        #region "Special Ability Validation"

        if (rdbIsPersonDisability.SelectedValue == "Y" && isChkDisability.Checked)
        {
            if (!isChkDisability.Checked)
            {
                ClientMessaging("Please Check Special Ability More than 40% ?.");
                Anthem.Manager.IncludePageScripts = true;
                isChkDisability.Focus();
                return;
            }

            if (!fileDisabilityDoc.HasFile && (Session["DDFile"] == null || Session["DDFile"].ToString() == ""))
            {
                ClientMessaging("Please choose a Special Ability Document.");
                return;
            }

            //string fileExtension = Path.GetExtension(fileDisabilityDoc.FileName).ToLower();

            string fileExtension = !fileDisabilityDoc.HasFile ? Path.GetExtension(Session["DDFile"].ToString()) : Path.GetExtension(fileDisabilityDoc.FileName).ToLower();

            if (fileDisabilityDoc.PostedFile.ContentLength > 2097152) // 2MB in bytes
            {
                ClientMessaging("File size should not exceed 2MB.");
                return;
            }

            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".bmp" && fileExtension != ".png" && fileExtension != ".pdf")
            {
                ClientMessaging("Supported file types are .jpg, .jpeg, .bmp, .png, and .pdf.");
                return;
            }

            // If Special Ability is Yes, check if remark and file are provided
            if (string.IsNullOrWhiteSpace(txtDisabilityRemark.Text))
            {
                ClientMessaging("Please Enter Special Ability Description.");
                return;
            }
        }

        #endregion

        //if (ddldegree.SelectedIndex == 0)
        //{
        //    ClientMessaging("Please Enter Degree.!");
        //    Anthem.Manager.IncludePageScripts = true;
        //    ddldegree.Focus();
        //    return;
        //}
        //if (subjectlist.SelectedIndex == 0)
        //{
        //    ClientMessaging("Please Enter Subject.!");
        //    Anthem.Manager.IncludePageScripts = true;
        //    subjectlist.Focus();
        //    return;
        //}
        //if (D_ddlYeofPass.SelectedIndex == 0)
        //{
        //    ClientMessaging("Please select Passing Year.!");
        //    Anthem.Manager.IncludePageScripts = true;
        //    D_ddlYeofPass.Focus();
        //    return;
        //}

        //if (R_txtPostedDate.Text.Trim() != "")
        //{
        //    DateTime dtDob = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txtPostedDate.Text.Trim()));
        //    int DobYear = dtDob.Year;
        //    int PassYear = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text.ToString());
        //    PassYear = PassYear - 18;
        //    if (DobYear > PassYear)
        //    {
        //        ClientMessaging("Alumni age should be greater than 18 years as per Passing year.!");
        //        R_txtPostedDate.Focus();
        //        return;
        //    }
        //}

        //if (ddlDepartment.SelectedIndex == 0)
        //{
        //    ClientMessaging("Please Select Department.!");
        //    ddlDepartment.Focus();
        //    return;
        //}        

        #endregion

        if (!flUpload.HasFile && Session["PPFile"] == null)
        {
            ClientMessaging("Please select the required photo.!");
            flUpload.Focus();
            return;
        }

        //FileType = Path.GetExtension(flUpload.PostedFile.FileName);

        //if (FileType != null && FileType != "")
        //{
        //    if (FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
        //&& FileType != ".bmp" && FileType != ".BMP" && FileType != ".gif" && FileType != ".GIF" && FileType != ".png" && FileType != ".PNG")
        //    {
        //        ClientMessaging("Photo Should be in required format.!");
        //        lblMsg.Text = "Photo Should be in required format.!";
        //        flUpload.Focus();
        //        return;
        //    }
        //}

        //filesize = flUpload.PostedFile.ContentLength;
        //Name = flUpload.PostedFile.FileName;
        ////filesize = Math.Round((filesize / 1024), 0);
        //if (Name.Length > 500)
        //{
        //    ClientMessaging("Photo File Name Should not be more than 500 characters.!");
        //    flUpload.Focus();
        //    return;
        //}
        //if (filesize > (1024 * 1024))
        //{
        //    ClientMessaging("Photo size should not be more than 1 MB.!");
        //    flUpload.Focus();
        //    return;
        //}

        if (uploadDocuments.HasFile)
        {
            //ClientMessaging("Please upload the required HPU documents.!!!");
            //uploadDocuments.Focus();
            //return;

            //FileTypes = Path.GetExtension(uploadDocuments.PostedFile.FileName);

            //if (FileTypes != null && FileTypes != "")
            //{
            //    if (FileTypes != ".pdf" && FileTypes != ".PDF" && FileTypes != ".jpg" && FileTypes != ".JPG" && FileTypes != ".jpeg" && FileTypes != ".JPEG"
            //&& FileTypes != ".bmp" && FileTypes != ".BMP" && FileTypes != ".gif" && FileTypes != ".GIF" && FileTypes != ".png" && FileTypes != ".PNG")
            //    {
            //        ClientMessaging("HPU Document Should be in required format.!");
            //        lblMsg.Text = "HPU Document Should be in required format.!";
            //        uploadDocuments.Focus();
            //        return;
            //    }
            //}

            //filesizes = uploadDocuments.PostedFile.ContentLength;
            //Names = uploadDocuments.PostedFile.FileName;
            ////filesizes = Math.Round((filesizes / 1024), 0);

            //if (Names.Length > 500)
            //{
            //    ClientMessaging("Photo File Name Should not be more than 500 characters.!");
            //    uploadDocuments.Focus();
            //    return;
            //}
            //if (filesizes > (2 * 1024 * 1024))
            //{
            //    ClientMessaging("Upload documents size should not be more than 2 MB.!");
            //    uploadDocuments.Focus();
            //    return;
            //}
        }

        if (fileDisabilityDoc.HasFile)
        {
            //FileTypes = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);

            //if (FileTypes != null && FileTypes != "")
            //{
            //    if (FileTypes != ".pdf" && FileTypes != ".PDF" && FileTypes != ".jpg" && FileTypes != ".JPG" && FileTypes != ".jpeg" && FileTypes != ".JPEG"
            //&& FileTypes != ".bmp" && FileTypes != ".BMP" && FileTypes != ".gif" && FileTypes != ".GIF" && FileTypes != ".png" && FileTypes != ".PNG")
            //    {
            //        ClientMessaging("Special Ability Document Should be in required format.!");
            //        lblMsg.Text = "Special Ability Document Should be in required format.!";
            //        fileDisabilityDoc.Focus();
            //        return;
            //    }
            //}

            //filesizes = fileDisabilityDoc.PostedFile.ContentLength;
            //Names = fileDisabilityDoc.PostedFile.FileName;

            //if (Names.Length > 500)
            //{
            //    ClientMessaging("Special Ability Document File Name Should not be more than 500 characters.!");
            //    fileDisabilityDoc.Focus();
            //    return;
            //}
            //if (filesizes > (2 * 1024 * 1024))
            //{
            //    ClientMessaging("Upload Special Ability documents size should not be more than 2 MB.!");
            //    fileDisabilityDoc.Focus();
            //    return;
            //}
        }

        Save();
        ForPayment();
    }

    public void getalumnidetails()
    {
        DataSet ds = new DataSet();
        ds = Get_alumni();
        alumniid = Convert.ToInt32(ds.Tables[0].Rows[0]["pk_alumniid"]);
    }

    public DataSet Get_alumni()
    {
        Clear();
        //names.Add("@"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Eventsid);
        return DAobj.GetDataSet("Get_alumni_Details", values, names, types);
    }

    public DataSet Get_credential()
    {
        Clear();
        names.Add("@alumniid"); types.Add(SqlDbType.NVarChar); values.Add(alumniid);
        return DAobj.GetDataSet("Get_Alumni_Credential", values, names, types);
    }

    public DataSet GetUserCredentials()
    {
        Clear();
        names.Add("@alumniid"); types.Add(SqlDbType.Int); values.Add(alumniid);
        return DAobj.GetDataSet("ALM_GetUserCredentials", values, names, types);
    }

    public DataSet GetUserCredentials(int alumniid)
    {
        Clear();
        names.Add("@alumniid"); types.Add(SqlDbType.Int); values.Add(alumniid);
        return DAobj.GetDataSet("ALM_GetUserCredentials", values, names, types);
    }

    public void Get_username_Pass()
    {
        DataSet ds = new DataSet();
        ds = Get_credential();
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                Session["username"] = ds.Tables[0].Rows[0]["loginname"];
                Session["psw"] = ds.Tables[0].Rows[0]["password"];
                Session["AlumniName"] = ds.Tables[0].Rows[0]["alumni_name"];
                Session["emailId"] = ds.Tables[0].Rows[0]["email"];
            }
        }
    }

    #region SendEmail

    protected void SendMail(string values, string splitMailCC)
    {
        try
        {
            // StringBuilder strBody = new StringBuilder();
            // string emailTo = Session["EmailID"].ToString().Trim();
            // // Session["AlumniName"]= txtAlumniName.Text.Trim();
            // strBody.Append("<Table border='0' cellpadding='0' cellspacing='0' style='width:75%;' align='left'>");

            // strBody.Append("<Tr>");
            // strBody.Append("<Td style='font-size: 15px;font-family: Verdana;height: 14px;'>");
            // strBody.Append("<B>" + "Dear " + Session["Alumni_Name"] + "," + "</B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");

            // strBody.Append("<Tr>");
            // strBody.Append("<Td>");
            // strBody.Append("<B>" + "</B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");

            // strBody.Append("<Tr>");
            // strBody.Append("<Td>");
            // strBody.Append("<B>Your Alumni Registration has been done successfully, Please find your User Name & Password below:-  </B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");

            // strBody.Append("<Tr>");
            // strBody.Append("<Td  style='font-size: 15px;font-family: Verdana;height: 14px;'>");
            // strBody.Append("<B>" + "User Name - " + Session["EmailID"].ToString() + "" + "</B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");

            // strBody.Append("<Tr>");
            // strBody.Append("<Td  style='font-size: 15px;font-family: Verdana;height: 14px;'>");
            // strBody.Append("<B>" + " & Password - " + Session["Password"].ToString() + "" + "</B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");

            // strBody.Append("<Tr>");
            // strBody.Append("<Td colspan='4'>");
            // strBody.Append("<B>" + "</B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");

            // strBody.Append("<Tr>");
            // strBody.Append("<Td>");
            // strBody.Append("<B> In case of any query please contact college </B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");

            // strBody.Append("<Tr>");
            // strBody.Append("<Td align=left style='font-size: 12px;font-family: Verdana;height: 14px;'>");
            // strBody.Append("<B>" + "Thanks and Regards ," + "</B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");
            // strBody.Append("<Tr>");
            // strBody.Append("<Td align=left style='font-size: 12px;font-family: Verdana;height: 14px;'>");
            // strBody.Append("<B>" + "HPU" + "</B>");
            // strBody.Append("</Td>");
            // strBody.Append("</Tr>");
            // strBody.Append("</Table>");
            // SendMail(splitMailCC, emailTo, strBody);

            StringBuilder strBody = new StringBuilder();
            string emailTo = Session["EmailID"].ToString().Trim();

            // HTML Email Formatting
            strBody.Append(@"
			<table border='0' cellpadding='10' cellspacing='0' style='width:100%; font-family:Verdana, sans-serif; font-size:14px; color:#333; background-color:#f9f9f9; border:1px solid #ddd; padding:15px;'>
				<tr>
					<td style='text-align:left;'>
						<h2 style='color:#0056b3;'>Dear " + Session["Alumni_Name"] + @",</h2>
						<p>Your Alumni Registration has been successfully completed. Please find your login credentials below:</p>
							<table border='0' cellpadding='5' cellspacing='0' style='width:100%; background-color:#ffffff; border:1px solid #ddd; padding:10px;'>
								<tr>
									<td style='width:30%; font-weight:bold;'>User Name:</td>
									<td style='color:#0056b3;'>" + Session["EmailID"].ToString() + @"</td>
								</tr>
								<tr>
									<td style='width:30%; font-weight:bold;'>Password:</td>
									<td style='color:#0056b3;'>" + Session["Password"].ToString() + @"</td>
								</tr>
							</table>
						<p>If you have any queries, please send an email to hpualumniassociation@gmail.com. </p>
						<p style='margin-top:20px; font-weight:bold; color:#555;'>Thanks and Regards,</p>
						<p style='font-weight:bold; color:#0056b3;'>HPU</p>
					</td>
				</tr>
			</table>
			");
            SendMail(splitMailCC, emailTo, strBody);
        }
        catch (Exception ex)
        {

        }
    }

    private void SendMail(string splitMailCC, string email, StringBuilder strBody)
    {
        try
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("alumni.hpushimla@gmail.com");

            mail.To.Add(email);
            mail.Subject = "Registration to HPU Alumni Cell";
            mail.Body = strBody.ToString();
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

    #endregion

    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear1();
        lblMsg.Text = "";
    }
    #endregion

    #region "All Code for Multiple file uploader by A.K.Singh @ 5 jan 2017"
    #region "Global Variable"
    string upldPath = "";
    DataSet dsFile = null;

    #endregion

    public void UploadFiles()
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
                    upldPath = upldPath + "Alumni\\StuImage" + "\\";
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    //it will return file based on file unique name
    public string SetServiceDoc(string FileName)
    {
        string FolderName = @"/Alumni";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = "";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/lblPassword"));
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

    #endregion

    /// <summary>
    /// Text changed On Email
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region "Email and mobile no duplicate found Event"

    protected void E_txtEmail_TextChanged(object sender, EventArgs e)
    {
        lblEmailMsg.Text = "";
        if (E_txtEmail.Text.Trim() != "")
        {
            if (IsEmailIdValid(E_txtEmail.Text.Trim()))
            {
                DataSet ds = IUMSNXG.SP.ALM_SP_GetDuplicate_Email_or_MobileNo(E_txtEmail.Text.Trim(), "").GetDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ClientMessaging("Email Already Exists!");
                    //lblEmailMsg.Text = "Email Already Exists!";
                    E_txtEmail.Text = "";
                    Anthem.Manager.IncludePageScripts = true;
                    E_txtEmail.Focus();
                    return;
                }
            }
            else
            {
                ClientMessaging("Invalid EmailId!");
                //lblEmailMsg.Text = "Invalid EmailId!";
                E_txtEmail.Text = "";
                Anthem.Manager.IncludePageScripts = true;
                E_txtEmail.Focus();
                return;
            }
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    /// <summary>
    /// Text changed On Contact
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void R_txtContactno_TextChanged(object sender, EventArgs e)
    {
        lblMobleNoMsg.Text = "";
        if (R_txtContactno.Text.Trim() != "" && R_txtContactno.Text.Length == 10)
        {
            DataSet ds = IUMSNXG.SP.ALM_SP_GetDuplicate_Email_or_MobileNo("", R_txtContactno.Text.Trim()).GetDataSet();
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblMobleNoMsg.Text = "Already Exists!";
                R_txtContactno.Text = "";
                Anthem.Manager.IncludePageScripts = true;
                R_txtContactno.Focus();
                return;
            }
        }
        else if (R_txtContactno.Text.Length < 10)
        {
            lblMobleNoMsg.Text = "Invalid Mobile Number!";
            Anthem.Manager.IncludePageScripts = true;
            R_txtContactno.Focus();
            return;
        }
        Anthem.Manager.IncludePageScripts = true;
    }
    #endregion

    /// <summary>
    /// Validation on Email
    /// </summary>
    /// <param name="EmailId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Bind Membership Fee
    /// </summary>
    #region "Fee Details"

    private void FeeDetails()
    {
        //Membershiptype = rdalumnitype.SelectedValue;
        //DataSet Ds = Alm_GetMembershipFee().GetDataSet();
        //if (Ds.Tables[0].Rows.Count > 0)
        //{
        //    //DataRow dr = Ds.Tables[0].NewRow();
        //    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
        //    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
        //}

        if (alumniType == "F" || alumniType == "ExStu")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rdalumnitype.Items.FindByValue("LM").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y" && !isChkDisability.Checked)
            {
                rdalumnitype.Items.FindByValue("LM").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y" && isChkDisability.Checked)
            {
                rdalumnitype.Items.FindByValue("LM").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        else if (alumniType == "S")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rdalumnitype.Items.FindByValue("SM").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y" && !isChkDisability.Checked)
            {
                rdalumnitype.Items.FindByValue("SM").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y" && isChkDisability.Checked)
            {
                rdalumnitype.Items.FindByValue("SM").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        else if (alumniType == "StuCh")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rdalumnitype.Items.FindByValue("SC").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y" && !isChkDisability.Checked)
            {
                rdalumnitype.Items.FindByValue("SC").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                DataSet Ds = Alm_GetMembershipFee().GetDataSet();
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblOnlineFees.Text = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    Session["RegFees"] = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                    hdnId.Value = Ds.Tables[0].Rows[0]["RegFees"].ToString();
                }
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "Y" && isChkDisability.Checked)
            {
                rdalumnitype.Items.FindByValue("SC").Selected = true;
                Membershiptype = rdalumnitype.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    #endregion

    /// <summary>
    /// Index Changed On MeberShip Type Radio Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rdPaymentOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeeDetails();
    }

    protected void ForPayment()
    {
        if (!IsPageRefresh)
        {
            if (hdnId.Value == "0")
            {
                //ClientMessaging("Alumni is Successfully Registered.!!!");
                //lblPaymentMsg.Text = "Alumni is Successfully Registered.!!!";
                clear1();
                return;
            }
            else
            {
                clear1();
                btnPay();
            }
        }
    }

    /// <summary>
    /// Send On Payment Gateway Page
    /// </summary>
    protected void btnPay()
    {
        //SAVE HERE DETAILS IN PURCHASE MST AS USER IS GOING TO CHOOSE PAYMENT GATEWAY OPTIONS OPTIONS
        try
        {
            if (Session["RegFees"] == null)
            {
                lblPaymentMsg.Text = "Fee is not available. Please Try Again or contact to University!";
                return;
            }
            string Message = "";
            ArrayList Result = new ArrayList();
            DataSet dsMain = GetMain();

            IpAddress = HttpContext.Current.Request.UserHostAddress.ToString();
            XmlDoc = dsMain.GetXml();
            // FreshOrReAppear = Convert.ToInt32(Session["Examtypeid"]);
            // DegreeYear = Session["DegreeYear"].ToString();
            if (InsertpaymentRecord(ref Message, ref Result) > 0)
            {
                if (Result.Count > 0)
                {
                    Session["Pk_purchaseid"] = Result[1].ToString().Trim();
                    Session["temp_Pk_purchaseid"] = Result[1].ToString().Trim();
                    Response.Redirect("~/Onlinepayment/ALM_Common_PaymentGateway.aspx", false);
                }
            }
            else
            {
                lblPaymentMsg.Text = Message;
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
            RegId = Convert.ToInt32(Session["regid"].ToString());

            DataSet dsDetails = Get_All_Details_Of_Alumni();

            if (dsDetails.Tables[0].Rows.Count > 0)
            {
                ds = GetSchema("HPU_Alumni_eCoupon_Purchase_Mst");
                DataRow dr = ds.Tables[0].NewRow();
                dr["pk_purchaseid"] = "0";
                dr["fk_regid"] = Session["regid"].ToString();
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

    public DataSet GetSchema(string TableName)
    {
        try
        {
            Clear();
            return DAobj.GetSchema(TableName);
        }
        catch { throw; }
    }

    public DataSet Get_All_Details_Of_Alumni()
    {
        Clear();
        names.Add("@Pk_Regid");
        types.Add(SqlDbType.Int);
        values.Add(_RegId);
        return DAobj.GetDataSet("ALM_AlumniRegistration_Details", values, names, types);
    }

    void Clear()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    #region DATA ACCESS LAYER CODE========================
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

    protected void btnpayment_Click(object sender, EventArgs e)
    {
        //ForPayment();
    }

    /// <summary>
    /// Set Property
    /// </summary>
    public string Step
    {
        get { return _step; }
        set { _step = value; }
    }
    public string IsSmsOrEmail
    {
        get { return _IsSmsOrEmail; }
        set { _IsSmsOrEmail = value; }
    }
    public string MobileNo
    {
        get { return _MobileNo; }
        set { _MobileNo = value; }
    }
    public string EmailId
    {
        get { return _EmailId; }
        set { _EmailId = value; }
    }
    public string Details
    {
        get { return _Details; }
        set { _Details = value; }
    }

    public int alumniid { get; private set; }
    public int pk_degreeid { get; private set; }
    public string Membershiptype { get; private set; }
    public string alumniType { get; private set; }

    //protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    pk_degreeid = ddldegree.SelectedValue != "" ? Convert.ToInt32(ddldegree.SelectedValue) : 0;
    //    getsubject(pk_degreeid);
    //    subjectlist.Focus();

    //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", "BindEvents();", true);
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "alert('Hello!!!');", true);
    //}

    protected void rdalumnitype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeeDetails();
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
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void txtCurrentAddress_TextChanged(object sender, EventArgs e)
    {
        if (permanentaddchk.Checked)
        {
            txtCurrentAddress.Text = txtperadd.Text.ToString();
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    private void sendSMS(string mobiles)
    {
        try
        {
            string smsMessage = "Dear Visitor, Congratulations! You have been successfully registered in the alumni portal of Himachal Pradesh University, Shimla. Your credentials are mentioned as under: ID : " + ViewState["Email"] + " Password : " + ViewState["pwd"] + ".  HPU";
            sendSingleSMS_after_Reg(mobiles.ToString(), smsMessage);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string sendSingleSMS_after_Reg(string MobileNo, string Message)
    {
        string responseFromServer = "";

        try
        {
            String username = "hpgovt-hpu";
            String password = "Shimlahpu@321";
            String senderid = "hpgovt";
            String secureKey = "b8112c28-7aae-47cf-b790-cbc04170bbae";
            string templateid = "1107171809166658903";

            Stream dataStream;
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072 | // TLS 1.2
             (SecurityProtocolType)768 | // TLS 1.1
             (SecurityProtocolType)192;   // TLS 1.0

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequestDLT");

            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String encryptedPassword = encryptedPasswod(password);
            String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), Message.Trim(), secureKey.Trim());
            String smsservicetype = "singlemsg"; //For single message.  Service Implicit
            //String smsservicetype = "Service Implicit";
            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                "&content=" + HttpUtility.UrlEncode(Message.Trim()) +
                "&mobileno=" + HttpUtility.UrlEncode(MobileNo) +
                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
                "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +
                "&templateid=" + HttpUtility.UrlEncode(templateid.Trim());

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
            responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message.ToString());
        }
        return responseFromServer;
    }

    //protected void Button111_Click(object sender, EventArgs e)
    //{
    //   // ../../ default.aspx
    //    Response.Redirect("~/Onlinepayment/Test.aspx");
    //}

    protected void rdbIsPersonDisability_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string dectype2 = cpt.Decrypt(Session["RBValue"].ToString());
        //string dectype2 = AesEnc.DecryptStringFromBytes_Aes(Session["RBValue"].ToString());
        //alumniType = dectype2.ToString();
        if (rdbIsPersonDisability.SelectedValue == "Y")
        {
            isChkDisabilityPnl.Visible = true;
            disabilitySection.Visible = true;
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);
            btnRegister.Text = "REGISTER";
        }
        else if (rdbIsPersonDisability.SelectedValue == "N")
        {
            isChkDisabilityPnl.Visible = false;
            disabilitySection.Visible = false;
            isChkDisability.Checked = false;
            fileDisabilityDoc.Dispose();
            txtDisabilityRemark.Text = "";
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);
            btnRegister.Text = "REGISTER AND PAY";
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void isChkDisability_CheckedChanged(object sender, EventArgs e)
    {
        //string dectype2 = cpt.Decrypt(Session["RBValue"].ToString());
        //string dectype2 = AesEnc.DecryptStringFromBytes_Aes(Session["RBValue"].ToString());
        //alumniType = dectype2.ToString();

        //string decrypted = CryptoAESDES.DecryptStringFromBase64_Aes(Session["RBValue"].ToString(), aesKey, aesIV);
        //alumniType = decrypted.ToString();

        if (isChkDisability.Checked == true && rdbIsPersonDisability.SelectedValue == "Y")
        {
            Anthem.Manager.IncludePageScripts = true;
            isChkDisabilityPnl.Visible = true;
            disabilityRemarkSection.Style["display"] = "block";
            isChkDisability.Checked = true;
            disabilityDocuments.Visible = true;
            fileDisabilityDoc.Visible = true;
            btnDisabilityDoc.Visible = true;
            divDisabilityDoc.Style["display"] = "block";
            lblOnlineFees.Text = "0";
            Session["RegFees"] = "0";
            hdnId.Value = "0";
        }
        else if (isChkDisability.Checked == false && rdbIsPersonDisability.SelectedValue == "Y")
        {
            Anthem.Manager.IncludePageScripts = true;
            disabilityRemarkSection.Style["display"] = "none";
            isChkDisability.Checked = false;
            disabilityDocuments.Visible = false;
            fileDisabilityDoc.Dispose();
            fileDisabilityDoc.Visible = false;
            btnDisabilityDoc.Visible = false;
            divDisabilityDoc.Style["display"] = "none";
            txtDisabilityRemark.Text = "";
            FeeDetails();

            if (Session["DDfilePath_refletter_delete"] != null && Session["DDfilePath_refletter_delete"].ToString() != "")
            {
                // Check if the file exists before attempting to delete it
                string filePathToDelete = Session["DDfilePath_refletter_delete"].ToString();
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
        else if (isChkDisability.Checked == false && rdbIsPersonDisability.SelectedValue == "N")
        {
            Anthem.Manager.IncludePageScripts = true;
            disabilityRemarkSection.Style["display"] = "none";
            isChkDisability.Checked = false;
            disabilityDocuments.Visible = false;
            fileDisabilityDoc.Dispose();
            fileDisabilityDoc.Visible = false;
            btnDisabilityDoc.Visible = false;
            divDisabilityDoc.Style["display"] = "none";
            txtDisabilityRemark.Text = "";
            FeeDetails();

            if (Session["DDfilePath_refletter_delete"] != null && Session["DDfilePath_refletter_delete"].ToString() != "")
            {
                // Check if the file exists before attempting to delete it
                string filePathToDelete = Session["DDfilePath_refletter_delete"].ToString();
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

            if (fileDisabilityDoc.PostedFile.ContentLength > 2097152) // 2MB in bytes
            {
                ClientMessaging("Special ability document file size should not exceed 2MB.");
                return;
            }

            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpeg" && FileType != ".JPEG" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".png" && FileType != ".PNG" && FileType != ".bmp" && FileType != ".BMP")
            {
                ClientMessaging("Please select special ability document should be in required format!");
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
                ClientMessaging("Specially Abled Document Uploaded Successfully.");
                string DDfilePathforhyperlink = "ALM_Reports//" + Session["DDFile"].ToString();
                string DDfilePath = Server.MapPath("ALM_Reports/") + Session["DDFile"].ToString();
                Session["DDfilePath_refletter_delete"] = DDfilePath;
                hyperDisabilityDoc.Visible = true;
                hyperDisabilityDoc.NavigateUrl = DDfilePathforhyperlink;
                hyperDisabilityDoc.Text = fileDisabilityDoc.FileName;
            }
        }
        else
        {
            ClientMessaging("Please select special ability documents.!!!");
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

    protected void btnImgUpload_Click(object sender, EventArgs e)
    {
        if (!flUpload.HasFile)
        {
            ClientMessaging("Please select required photo.!!!");
            flUpload.Focus();
            return;
        }

        if (flUpload.HasFile)
        {
            FileType = Path.GetExtension(flUpload.PostedFile.FileName);

            if (flUpload.PostedFile.ContentLength > 1048576) // 1MB in bytes
            {
                ClientMessaging("Photo size should not be more than 1 MB.!");
                return;
            }

            if (flUpload.PostedFile.FileName.Length > 500)
            {
                ClientMessaging("Photo file name should not be more than 500 characters.!");
                flUpload.Focus();
                return;
            }

            if (FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
            && FileType != ".bmp" && FileType != ".BMP" && FileType != ".png" && FileType != ".PNG")
            {
                ClientMessaging("Photo should be in required format.!");
                return;
            }
            else
            {
                string lFileName = "";
                Random rand = new Random();
                int n = rand.Next();

                string filename = "HPU_Alumni_PP_" + Guid.NewGuid().ToString() + "_" + rand.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + rand.Next(33333, 454545454) + "_" + rand.Next(999999, 15215454) + FileType;

                lFileName = FU_physicalPath(flUpload, "Alumni\\StuImage\\", filename);
                Session["PPFile"] = lFileName;
                flUpload.SaveAs(Server.MapPath("ALM_Reports//" + lFileName));
                ClientMessaging("Photo Uploaded Successfully!!!.");
                string PPfilePathforhyperlink = "ALM_Reports//" + Session["PPFile"].ToString();
                string PPfilePath = Server.MapPath("ALM_Reports/") + Session["PPFile"].ToString();
                Session["PPfilePath_refletter_delete"] = PPfilePath;
                //anchorPath.Visible = true;
                //anchorPath.NavigateUrl = filePathforhyperlink;
                anchorPath.Text = flUpload.FileName;
                imgProfileimg.ImageUrl = PPfilePathforhyperlink;
            }
        }
        else
        {
            ClientMessaging("Please select required photo.!!!");
        }
    }

    protected void btnDocUpload_Click(object sender, EventArgs e)
    {
        if (uploadDocuments.HasFile)
        {
            FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);

            if (uploadDocuments.PostedFile.ContentLength > 2097152) // 2MB in bytes
            {
                ClientMessaging("Documents size should not be more than 2 MB.!");
                return;
            }

            if (uploadDocuments.PostedFile.FileName.Length > 500)
            {
                ClientMessaging("Document file name should not be more than 500 characters.!");
                uploadDocuments.Focus();
                return;
            }

            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
                && FileType != ".bmp" && FileType != ".BMP" && FileType != ".png" && FileType != ".PNG")
            {
                ClientMessaging("Document should be in required format.!");
                return;
            }
            else
            {
                string lFileName = "";
                Random rand = new Random();
                int n = rand.Next();

                string filename = "HPU_Alumni_AD_" + Guid.NewGuid().ToString() + "_" + rand.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "AD" + "_" + rand.Next(33333, 454545454) + "_" + rand.Next(999999, 15215454) + FileType;

                lFileName = FU_physicalPath(uploadDocuments, "Alumni\\StuImage\\", filename);
                Session["ADFile"] = lFileName;
                uploadDocuments.SaveAs(Server.MapPath("ALM_Reports//" + lFileName));
                ClientMessaging("Documents Uploaded Successfully!!.");
                string ADfilePathforhyperlink = "ALM_Reports//" + Session["ADFile"].ToString();
                string ADfilePath = Server.MapPath("ALM_Reports/") + Session["ADFile"].ToString();
                Session["filePath_refletter_delete"] = ADfilePath;
                HyperLink1.Visible = true;
                HyperLink1.NavigateUrl = ADfilePathforhyperlink;
                HyperLink1.Text = uploadDocuments.FileName;
            }
        }
    }

    protected void txtperadd_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtperadd.Text.Trim()))
        {
            permanentaddchk.Enabled = false;
            permanentaddchk.Checked = false;
            permanentaddchk_CheckedChanged(null, null);
        }
        else
        {
            permanentaddchk.Enabled = true;
            permanentaddchk.Checked = true;
            permanentaddchk_CheckedChanged(null, null);
        }
    }

    #region "Education Qualifications"

    protected DataTable QualificationTable
    {
        get
        {
            if (ViewState["Qualifications"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Degree");
                dt.Columns.Add("Subject");
                dt.Columns.Add("Year");
                dt.Columns.Add("Department");
                dt.Rows.Add("", "", "", "");
                ViewState["Qualifications"] = dt;
            }
            return (DataTable)ViewState["Qualifications"];
        }
        set { ViewState["Qualifications"] = value; }
    }

    private void BindGrid()
    {
        gvQualifications.DataSource = QualificationTable;
        gvQualifications.DataBind();

        PopulateYearDropdowns();
        PopulateDegreeDropdowns();
        PopulateDepartmentDropdowns();
    }

    private void PopulateDegreeDropdowns()
    {
        foreach (GridViewRow row in gvQualifications.Rows)
        {
            DropDownList ddlDegree = (DropDownList)row.FindControl("ddldegree");

            DataSet ds = ALM_ACD_Degree_Mst_sel().GetDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDegree.DataValueField = "pk_degreeid";
                ddlDegree.DataTextField = "degree";
                ddlDegree.DataSource = ds;
                ddlDegree.DataBind();
                ddlDegree.Items.Insert(0, new ListItem("-- Select Degree --", "0"));
            }
            else
            {
                ddlDegree.DataSource = null;
                ddlDegree.DataBind();
            }

            //if (ddlDegree != null && ddlDegree.Items.Count == 0)
            //{
            //    ddlDegree.Items.Add(new ListItem("-- Select Degree --", ""));
            //    ddlDegree.Items.Add(new ListItem("BA", "BA"));
            //    ddlDegree.Items.Add(new ListItem("BSc", "BSc"));
            //    ddlDegree.Items.Add(new ListItem("B.Com", "BCom"));
            //    ddlDegree.Items.Add(new ListItem("MA", "MA"));
            //    ddlDegree.Items.Add(new ListItem("MSc", "MSc"));
            //    ddlDegree.Items.Add(new ListItem("PhD", "PhD"));
            //}
        }
    }

    private void PopulateYearDropdowns()
    {
        foreach (GridViewRow row in gvQualifications.Rows)
        {
            DropDownList ddlPassingYear = (DropDownList)row.FindControl("D_ddlYeofPass");
            ddlPassingYear.Items.Clear();
            DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();
            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                ddlPassingYear.DataSource = ds.Tables[2];
                ddlPassingYear.DataTextField = "PassYear_Name";
                ddlPassingYear.DataValueField = "Pk_pass_id";
                ddlPassingYear.DataBind();
                ddlPassingYear.Items.Insert(0, new ListItem("-- Select Passing Year -- ", "0"));
            }
            else
            {
                ddlPassingYear.Items.Insert(0, "-- Select Passing Year -- ");
            }

            //if (ddlYear != null && ddlYear.Items.Count == 0)
            //{
            //    ddlYear.Items.Add(new ListItem("-- Select Year --", ""));
            //    for (int year = DateTime.Now.Year; year >= 1950; year--)
            //    {
            //        ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            //    }
            //}
        }
    }

    private void PopulateDepartmentDropdowns()
    {
        foreach (GridViewRow row in gvQualifications.Rows)
        {
            DropDownList ddlDepartment = (DropDownList)row.FindControl("ddlDepartment");
            DataSet ds = ALM_Department_sel();
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

            //if (ddlDepartment != null && ddlDepartment.Items.Count == 0)
            //{
            //    ddlDepartment.Items.Add(new ListItem("-- Select Department --", ""));
            //    ddlDepartment.Items.Add(new ListItem("Science", "Science"));
            //    ddlDepartment.Items.Add(new ListItem("Arts", "Arts"));
            //    ddlDepartment.Items.Add(new ListItem("Commerce", "Commerce"));
            //    ddlDepartment.Items.Add(new ListItem("Engineering", "Engineering"));
            //    ddlDepartment.Items.Add(new ListItem("Law", "Law"));
            //    ddlDepartment.Items.Add(new ListItem("Medicine", "Medicine"));
            //}
        }
    }

    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //#region "Comments"

        //GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;

        //DropDownList ddlDegree = row.FindControl("ddldegree") as DropDownList;
        //DropDownList ddlSubject = row.FindControl("subjectlist") as DropDownList;

        //if (ddlDegree == null || ddlSubject == null) return;

        //ddlSubject.Items.Clear();
        //ddlSubject.Items.Add(new ListItem("-- Select Subject --", ""));

        //pk_degreeid = ddlDegree.SelectedValue != "" ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;

        //int pk_degreeidd;
        //int.TryParse(ddlDegree.SelectedValue, out pk_degreeidd);

        ////getsubject(pk_degreeid);

        //BindSubjects(pk_degreeid, ddlSubject);

        //ddlSubject.Focus();

        //#endregion

        //////DataSet dtStaus = new DataSet();
        //////DataTable dtStructure = new DataTable();
        //////dtStructure.TableName = "ALM_Status";
        //////dtStructure.Columns.Add("fk_DegreeID");
        //////dtStaus.Tables.Add(dtStructure);

        //////int rowcount = gvQualifications.Rows.Count;
        //////foreach (GridViewRow gr in gvQualifications.Rows)
        //////{
        //////    DropDownList ddlDegree = gr.FindControl("ddldegree") as DropDownList;
        //////    DropDownList ddlSubject = gr.FindControl("subjectlist") as DropDownList;
        //////    DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as DropDownList;
        //////    DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as DropDownList;

        //////    DataRow drQual = dtStaus.Tables[0].NewRow();
        //////    drQual["fk_DegreeID"] = ddlDegree.SelectedIndex == 0 ? "0" : ddlDegree.SelectedValue;

        //////    dtStaus.Tables[0].Rows.Add(drQual);
        //////    xmlDocQual = dtStaus.GetXml();

        //////    DataSet ds = getSubjectsByDegreeID(xmlDocQual).GetDataSet();

        //////    using (DataSet ds1 = getSubjectsByDegree(Convert.ToInt32(ddlDegree.SelectedValue)).GetDataSet())
        //////    {
        //////        if (ddlSubject.SelectedValue == "0")
        //////        {
        //////            ddlSubject.DataTextField = "subject";
        //////            ddlSubject.DataValueField = "pk_subjectid";
        //////            ddlSubject.DataSource = ds1;
        //////            ddlSubject.DataBind();
        //////            ddlSubject.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        //////        }
        //////    }

        //////    DataTable dt1 = ViewState["PreviousRow"] as DataTable;
        //////    if (dt1 != null)
        //////    {
        //////        for (int i = 0; i < dt1.Rows.Count; i++)
        //////        {
        //////            if (gr.RowIndex == i)
        //////            {
        //////                ddlDegree.SelectedValue = dt1.Rows[i]["fk_DegreeID"].ToString();
        //////                ddlSubject.SelectedValue = dt1.Rows[i]["fk_SubjectID"].ToString();
        //////                ddlPassingYear.SelectedValue = dt1.Rows[i]["fk_PassingYearID"].ToString();
        //////                ddlDepartment.SelectedValue = dt1.Rows[i]["fk_DeptID"].ToString();
        //////            }
        //////        }
        //////    }
        //////}

        //////Anthem.Manager.IncludePageScripts = true;

        // Save current values (including the newly changed degree)
        DataTable dt1 = GetEduQualGridData();
        ViewState["PreviousRow"] = dt1;

        Anthem.DropDownList ddlDegreeChanged = (Anthem.DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlDegreeChanged.NamingContainer;

        DropDownList ddlSubject = row.FindControl("subjectlist") as DropDownList;

        // Load new subjects for selected degree
        using (DataSet ds1 = getSubjectsByDegree(Convert.ToInt32(ddlDegreeChanged.SelectedValue)).GetDataSet())
        {
            ddlSubject.DataTextField = "subject";
            ddlSubject.DataValueField = "pk_subjectid";
            ddlSubject.DataSource = ds1;
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        }

        // Re-bind dropdowns for all rows
        BindGridViewDropdown();

        // Restore latest selections (including new degree change)
        FillQualInfoGrid(dt1);

        Anthem.Manager.IncludePageScripts = true;
    }

    #region "Validation for Qualifications"

    private bool ValidateQualifications()
    {
        bool isAtLeastOneRowFilled = false;

        foreach (GridViewRow row in gvQualifications.Rows)
        {
            DropDownList ddlDegree = row.FindControl("ddldegree") as DropDownList;
            DropDownList ddlSubject = row.FindControl("subjectlist") as DropDownList;
            DropDownList ddlYear = row.FindControl("D_ddlYeofPass") as DropDownList;
            DropDownList ddlDept = row.FindControl("ddlDepartment") as DropDownList;

            if (ddlDegree == null || ddlSubject == null || ddlYear == null || ddlDept == null)
                continue; // Skip if controls not found

            bool isRowFilled = !string.IsNullOrEmpty(ddlDegree.SelectedValue) &&
                               !string.IsNullOrEmpty(ddlSubject.SelectedValue) &&
                               !string.IsNullOrEmpty(ddlYear.SelectedValue) &&
                               !string.IsNullOrEmpty(ddlDept.SelectedValue);

            if (isRowFilled)
                isAtLeastOneRowFilled = true;
            else
            {
                // If row partially filled, show message immediately
                bool anyFieldFilled = !string.IsNullOrEmpty(ddlDegree.SelectedValue) ||
                                      !string.IsNullOrEmpty(ddlSubject.SelectedValue) ||
                                      !string.IsNullOrEmpty(ddlYear.SelectedValue) ||
                                      !string.IsNullOrEmpty(ddlDept.SelectedValue);

                if (anyFieldFilled)
                {
                    ClientMessaging("Please fill all fields for the partially filled row.");
                    return false;
                }
            }
        }

        if (!isAtLeastOneRowFilled)
        {
            ClientMessaging("Please fill all fields for at least one row of Education Details.");
            return false;
        }

        return true; // Validation passed
    }

    //protected void btnAddRow_Click(object sender, EventArgs e)
    //{
    //    if (!ValidateQualifications()) { return; }

    //    DataTable qualificationTable = QualificationTable;

    //    // Save existing data
    //    for (int i = 0; i < gvQualifications.Rows.Count; i++)
    //    {
    //        GridViewRow row = gvQualifications.Rows[i];

    //        DropDownList ddlDegree = row.FindControl("ddldegree") as DropDownList;
    //        DropDownList ddlSubject = row.FindControl("subjectlist") as DropDownList;
    //        DropDownList ddlYear = row.FindControl("D_ddlYeofPass") as DropDownList;
    //        DropDownList ddlDepartment = row.FindControl("ddlDepartment") as DropDownList;

    //        if (ddlDegree != null)
    //            qualificationTable.Rows[i]["fk_DegreeID"] = string.IsNullOrEmpty(ddlDegree.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlDegree.SelectedValue);

    //        if (ddlSubject != null)
    //            qualificationTable.Rows[i]["fk_SubjectID"] = string.IsNullOrEmpty(ddlSubject.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlSubject.SelectedValue);

    //        if (ddlYear != null)
    //            qualificationTable.Rows[i]["fk_PassingYearID"] = string.IsNullOrEmpty(ddlYear.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlYear.SelectedValue);

    //        if (ddlDepartment != null)
    //            qualificationTable.Rows[i]["fk_DeptID"] = string.IsNullOrEmpty(ddlDepartment.SelectedValue) ? (object)DBNull.Value : ddlDepartment.SelectedValue.Trim();
    //    }

    //    // Add empty row
    //    DataRow newRow = qualificationTable.NewRow();
    //    qualificationTable.Rows.Add(newRow);

    //    QualificationTable = qualificationTable;
    //    BindGrid();
    //}
	
    protected void ClearQualificationsGrid()
    {
        ViewState["QualificationTable"] = null;
        gvQualifications.DataSource = null;
        gvQualifications.DataBind();
    }

    #endregion

    protected void gvQualifications_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DELETEREC")
            {
                // Get the row where the button clicked.

                if (gvQualifications.Rows.Count > 1)
                {
                    GridViewRow rowSelect = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    int rowindex = rowSelect.RowIndex;

                    DataTable dt = GetEduQualGridData();
                    dt.Rows[rowindex].Delete();
                    dt.AcceptChanges();
					ViewState["PreviousRow"] = dt;
                    ViewState["Rows"] = dt.Rows.Count;
                    generate_GridView(gvQualifications, Convert.ToInt32(ViewState["Rows"]));
                    BindGridViewDropdown();
                    FillQualInfoGrid(dt);
                    Anthem.Manager.IncludePageScripts = true;
                }
                else
                {
                    ClientMessaging("Atleast One Details is Required !");
                }
            }
            else if (e.CommandName == "SELECT")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                GridViewRow rw = (GridViewRow)(((Anthem.LinkButton)e.CommandSource).NamingContainer);
                int rowindex = rw.RowIndex;

                DropDownList ddlDegree = gvQualifications.Rows[rowindex].FindControl("ddldegree") as DropDownList;
                DropDownList ddlSubject = gvQualifications.Rows[rowindex].FindControl("subjectlist") as DropDownList;
                DropDownList ddlPassingYear = gvQualifications.Rows[rowindex].FindControl("D_ddlYeofPass") as DropDownList;
                DropDownList ddlDepartment = gvQualifications.Rows[rowindex].FindControl("ddlDepartment") as DropDownList;

                string degreeID = string.Empty; string subjectID = string.Empty; string passingYearID = string.Empty; string departmentID = string.Empty;

                degreeID = ddlDegree.SelectedIndex == 0 ? "0" : ddlDegree.SelectedValue;
                subjectID = ddlSubject.SelectedIndex == 0 ? "0" : ddlSubject.SelectedValue;
                passingYearID = ddlPassingYear.SelectedIndex == 0 ? "0" : ddlPassingYear.SelectedValue;
                departmentID = ddlDepartment.SelectedIndex == 0 ? "0" : ddlDepartment.SelectedValue;

                ViewState["degreeID"] = degreeID;
                ViewState["subjectID"] = subjectID;
                ViewState["passingYearID"] = passingYearID;
                ViewState["departmentID"] = departmentID;

                gvQualifications.SelectedIndex = -1;
                Anthem.Manager.IncludePageScripts = true;
                btnAddRow.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message);
        }
    }
	
    public static void generate_GridView(GridView SourceID, int Rows)
    {
        SourceID.SelectedIndex = -1;
        SourceID.DataSource = null;
        SourceID.DataBind();

        DataTable dt = new DataTable();
        for (int i = 0; i < Rows; i++)
        {
            dt.Rows.Add(dt.NewRow());
        }
        SourceID.DataSource = dt;
        SourceID.DataBind();
    }
	
    protected void FillQualInfoGrid(DataTable dtQualInfo)
    {
        try
        {
            ////if (dtQualInfo.Rows.Count == 0)
            ////{
            ////    generate_GridView(gvQualifications, 1);
            ////    BindGridViewDropdown();
            ////}
            ////else
            ////    for (int i = 0; i < dtQualInfo.Rows.Count; i++)
            ////    {
            ////        DropDownList ddlDegree = gvQualifications.Rows[i].FindControl("ddldegree") as DropDownList;
            ////        DropDownList ddlSubject = gvQualifications.Rows[i].FindControl("subjectlist") as DropDownList;
            ////        DropDownList ddlPassingYear = gvQualifications.Rows[i].FindControl("D_ddlYeofPass") as DropDownList;
            ////        DropDownList ddlDepartment = gvQualifications.Rows[i].FindControl("ddlDepartment") as DropDownList;

            ////        ddlDegree.SelectedValue = Convert.ToString(dtQualInfo.Rows[i]["fk_DegreeID"]) == "0" ? "-- Select Degree --" : Convert.ToString(dtQualInfo.Rows[i]["fk_DegreeID"]).Trim();
            ////        ddldegree_SelectedIndexChanged(null, null);
            ////        ddlSubject.SelectedValue = Convert.ToString(dtQualInfo.Rows[i]["fk_SubjectID"]) == "0" ? "-- Select Subject --" : Convert.ToString(dtQualInfo.Rows[i]["fk_SubjectID"]).Trim();
            ////        ddlPassingYear.SelectedValue = Convert.ToString(dtQualInfo.Rows[i]["fk_PassingYearID"]) == "0" ? "-- Select Passing Year --" : Convert.ToString(dtQualInfo.Rows[i]["fk_PassingYearID"]).Trim();
            ////        ddlDepartment.SelectedValue = Convert.ToString(dtQualInfo.Rows[i]["fk_DeptID"]) == "0" ? "-- Select Department --" : Convert.ToString(dtQualInfo.Rows[i]["fk_DeptID"]).Trim();
            ////    }

            if (dtQualInfo == null || dtQualInfo.Rows.Count == 0) return;

            for (int i = 0; i < gvQualifications.Rows.Count; i++)
            {
                GridViewRow gr = gvQualifications.Rows[i];

                DropDownList ddlDegree = gr.FindControl("ddldegree") as DropDownList;
                DropDownList ddlSubject = gr.FindControl("subjectlist") as DropDownList;
                DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as DropDownList;
                DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as DropDownList;

                if (i < dtQualInfo.Rows.Count)
                {
                    string degreeId = dtQualInfo.Rows[i]["fk_DegreeID"].ToString();
                    string subjectId = dtQualInfo.Rows[i]["fk_SubjectID"].ToString();
                    string passYearId = dtQualInfo.Rows[i]["fk_PassingYearID"].ToString();
                    string deptId = dtQualInfo.Rows[i]["fk_DeptID"].ToString();

                    // Restore Degree
                    if (ddlDegree.Items.FindByValue(degreeId) != null)
                        ddlDegree.SelectedValue = degreeId;

                    // Refresh subjects ONLY for this row
                    if (!string.IsNullOrEmpty(degreeId) && degreeId != "0")
                    {
                        using (DataSet dsSub = getSubjectsByDegree(Convert.ToInt32(degreeId)).GetDataSet())
                        {
                            ddlSubject.DataTextField = "subject";
                            ddlSubject.DataValueField = "pk_subjectid";
                            ddlSubject.DataSource = dsSub;
                            ddlSubject.DataBind();
                            ddlSubject.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
                        }

                        if (ddlSubject.Items.FindByValue(subjectId) != null)
                            ddlSubject.SelectedValue = subjectId;
                    }

                    // Restore Passing Year
                    if (ddlPassingYear.Items.FindByValue(passYearId) != null)
                        ddlPassingYear.SelectedValue = passYearId;

                    // Restore Department
                    if (ddlDepartment.Items.FindByValue(deptId) != null)
                        ddlDepartment.SelectedValue = deptId;
                }
            }
        }
        catch (Exception exp)
        {
            ClientMessaging(exp.Message);
        }
    }

    public void BindGridViewDropdown()
    {
        DataSet ds = getAllRecords().GetDataSet();

        ////foreach (GridViewRow item in gvQualifications.Rows)
        ////{
        ////    DropDownList ddlDegree = item.FindControl("ddldegree") as DropDownList;
        ////    ddlDegree.DataTextField = "degree";
        ////    ddlDegree.DataValueField = "pk_degreeid";
        ////    ddlDegree.DataSource = ds.Tables[0];
        ////    ddlDegree.DataBind();
        ////    ddlDegree.Items.Insert(0, new ListItem("-- Select Degree --", "0"));

        ////    DropDownList ddlSubject = item.FindControl("subjectlist") as DropDownList;
        ////    using (DataSet dsSub = getSubjectsByDegree(Convert.ToInt32(ddlDegree.SelectedValue)).GetDataSet())
        ////    {
        ////        ddlSubject.DataTextField = "subject";
        ////        ddlSubject.DataValueField = "pk_subjectid";
        ////        ddlSubject.DataSource = dsSub;
        ////        ddlSubject.DataBind();
        ////        ddlSubject.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        ////    }

        ////    DropDownList ddlPassingYear = item.FindControl("D_ddlYeofPass") as DropDownList;
        ////    ddlPassingYear.DataTextField = "PassYear_Name";
        ////    ddlPassingYear.DataValueField = "PK_Pass_ID";
        ////    ddlPassingYear.DataSource = ds.Tables[1];
        ////    ddlPassingYear.DataBind();
        ////    ddlPassingYear.Items.Insert(0, new ListItem("-- Select PassingYear --", "0"));

        ////    DropDownList ddlDepartment = item.FindControl("ddlDepartment") as DropDownList;
        ////    ddlDepartment.DataTextField = "description";
        ////    ddlDepartment.DataValueField = "Pk_deptId";
        ////    ddlDepartment.DataSource = ds.Tables[2];
        ////    ddlDepartment.DataBind();
        ////    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        ////}

        // Load static lists only once
        DataTable dtDegrees = ds.Tables[0];   // Degrees
        DataTable dtYears = ds.Tables[1];     // Passing Years
        DataTable dtDepts = ds.Tables[2];     // Departments

        foreach (GridViewRow row in gvQualifications.Rows)
        {
            // Degree dropdown
            DropDownList ddlDegree = row.FindControl("ddldegree") as DropDownList;
            if (ddlDegree != null && ddlDegree.Items.Count == 0) // bind only first time
            {
                ddlDegree.DataTextField = "degree";
                ddlDegree.DataValueField = "pk_degreeid";
                ddlDegree.DataSource = dtDegrees;
                ddlDegree.DataBind();
                ddlDegree.Items.Insert(0, new ListItem("-- Select Degree --", "0"));
            }

            // Passing year dropdown
            DropDownList ddlPassingYear = row.FindControl("D_ddlYeofPass") as DropDownList;
            if (ddlPassingYear != null && ddlPassingYear.Items.Count == 0)
            {
                ddlPassingYear.DataTextField = "PassYear_Name";
                ddlPassingYear.DataValueField = "PK_Pass_ID";
                ddlPassingYear.DataSource = dtYears;
                ddlPassingYear.DataBind();
                ddlPassingYear.Items.Insert(0, new ListItem("-- Select Passing Year --", "0"));
            }

            // Department dropdown
            DropDownList ddlDepartment = row.FindControl("ddlDepartment") as DropDownList;
            if (ddlDepartment != null && ddlDepartment.Items.Count == 0)
            {
                ddlDepartment.DataTextField = "description";
                ddlDepartment.DataValueField = "Pk_deptId";
                ddlDepartment.DataSource = dtDepts;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
            }
            Anthem.Manager.IncludePageScripts = true;
        }
    }

    protected DataTable GetEduQualGridData()
    {
        DataSet dsQual = DAobj.GetSchema("ALM_EducationQualifications");
        foreach (GridViewRow gr in gvQualifications.Rows)
        {
            DropDownList ddlDegree = gr.FindControl("ddldegree") as DropDownList;
            DropDownList ddlSubject = gr.FindControl("subjectlist") as DropDownList;
            DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as DropDownList;
            DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as DropDownList;

            DataRow drQual = dsQual.Tables[0].NewRow();

            if (ddlDegree != null)
                drQual["fk_DegreeID"] = string.IsNullOrEmpty(ddlDegree.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlDegree.SelectedValue);

            if (ddlSubject != null)
                drQual["fk_SubjectID"] = string.IsNullOrEmpty(ddlSubject.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlSubject.SelectedValue);

            if (ddlPassingYear != null)
                drQual["fk_PassingYearID"] = string.IsNullOrEmpty(ddlPassingYear.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlPassingYear.SelectedValue);

            if (ddlDepartment != null)
                drQual["fk_DeptID"] = string.IsNullOrEmpty(ddlDepartment.SelectedValue) ? (object)DBNull.Value : ddlDepartment.SelectedValue.Trim();

            dsQual.Tables[0].Rows.Add(drQual);
        }
        return dsQual.Tables[0];
    }

    private void BindSubjects(int degreeId, DropDownList subjectDropdown)
    {
        DataSet ds = Bind_subject().GetDataSet();

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "fk_degreeid = " + degreeId;

            subjectDropdown.DataSource = dv;
            subjectDropdown.DataTextField = "subject";
            subjectDropdown.DataValueField = "pk_subjectid";
            subjectDropdown.DataBind();
            subjectDropdown.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        }
        else
        {
            subjectDropdown.Items.Clear();
            subjectDropdown.Items.Add(new ListItem("-- Select Subject --", "0"));
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ValidQualEduList())
            {
                DataTable dt = GetEduQualGridData();
                ViewState["PreviousRow"] = dt;
                ViewState["Rows"] = dt.Rows.Count + 1;
                generate_GridView(gvQualifications, Convert.ToInt32(ViewState["Rows"]));
                BindGridViewDropdown();
                FillQualInfoGrid(dt);
                foreach (GridViewRow gr in gvQualifications.Rows)
                {
                    DropDownList ddlDegree = gr.FindControl("ddldegree") as DropDownList;
                    DropDownList ddlSubject = gr.FindControl("subjectlist") as DropDownList;
                    DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as DropDownList;
                    DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as DropDownList;

                    int index = gr.RowIndex;
                    if (index < Convert.ToInt32(ViewState["Rows"]) - 1)
                    {
                        ddlDegree.Enabled = false;
                        ddlSubject.Enabled = false;
                        ddlPassingYear.Enabled = false;
                        ddlDepartment.Enabled = false;
                    }
                }
            }
        }
        catch (Exception exp)
        {
            lblMsg.Text = exp.Message;
        }
    }

    public bool ValidQualEduList()
    {
        foreach (GridViewRow gr in gvQualifications.Rows)
        {
            DropDownList ddlDegree = gr.FindControl("ddldegree") as DropDownList;
            DropDownList ddlSubject = gr.FindControl("subjectlist") as DropDownList;
            DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as DropDownList;
            DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as DropDownList;

            if (ddlDegree.SelectedIndex == 0)
            {
                ClientMessaging("Degree is required.");
                ddlDegree.Focus();
                return false;
            }

            if (ddlSubject.SelectedIndex == 0)
            {
                ClientMessaging("Subject is required.");
                ddlSubject.Focus();
                return false;
            }

            if (ddlPassingYear.SelectedIndex == 0)
            {
                ClientMessaging("Passing Year is required.");
                ddlPassingYear.Focus();
                return false;
            }

            if (ddlDepartment.SelectedIndex == 0)
            {
                ClientMessaging("Department is required.");
                ddlDepartment.Focus();
                return false;
            }
        }
        return true;
    }  

    public static StoredProcedure getSubjectsByDegree(int pk_degreeid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_Bind_subject", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_degreeid", pk_degreeid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure getAllRecords()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Get_Degrees_PassingYears_Departments", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public static StoredProcedure getSubjectsByDegreeID(string xmlDocQ)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_GetSubjects_ByDegreeID", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@xmlDocQual", xmlDocQ, DbType.String);
        return sp;
    }

    public string xmlDocQual { get; set; }

    protected void ClientMessagingShownAndRedirectedToPage(string msg)
    {
        string url = "../Alumin_Loginpage.aspx";
        string script = "window.onload = function(){ alert('";
        script += msg;
        script += "');";
        script += "window.location = '";
        script += url;
        script += "'; }";
        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
    }

    protected void updateAlumniApproved(int pk_AlumiId)
    {
        try
        {
            DataSet ds = AlumniApprovedAndAutoUpdateAlumniNoForDisabiledPerson(pk_AlumiId);
            if (ds == null)
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    public DataSet AlumniApprovedAndAutoUpdateAlumniNoForDisabiledPerson(int alumniId)
    {
        Clear();
        names.Add("@pk_alumniId"); values.Add(alumniId); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("ALM_RegistrationAutoApproved_And_GenerateAutoAlumniNo_UpdateForDisabiledPerson", values, names, types);
    }

    //private DataTable CaptureEduQualFromGrid()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("fk_DegreeID");
    //    dt.Columns.Add("fk_SubjectID");
    //    dt.Columns.Add("fk_PassingYearID");
    //    dt.Columns.Add("fk_DeptID");

    //    foreach (GridViewRow gr in gvQualifications.Rows)
    //    {
    //        Anthem.DropDownList ddlDegree = gr.FindControl("ddldegree") as Anthem.DropDownList;
    //        Anthem.DropDownList ddlSubject = gr.FindControl("subjectlist") as Anthem.DropDownList;
    //        Anthem.DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as Anthem.DropDownList;
    //        Anthem.DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as Anthem.DropDownList;

    //        DataRow dr = dt.NewRow();
    //        dr["fk_DegreeID"] = ddlDegree.SelectedValue;
    //        dr["fk_SubjectID"] = ddlSubject.SelectedValue;
    //        dr["fk_PassingYearID"] = ddlPassingYear.SelectedValue;
    //        dr["fk_DeptID"] = ddlDepartment.SelectedValue;

    //        dt.Rows.Add(dr);
    //    }

    //    return dt;
    //}


    #endregion

    public class CryptoAESDES
    {
        /// <summary>
        /// Encrypts a plain text string using AES.
        /// </summary>
        public static string EncryptStringToBase64_Aes(string plainText, string key, string iv)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("Plaintext is null or empty");

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts AES encrypted bytes to string.
        /// </summary>
        public static string DecryptStringFromBase64_Aes(string cipherTextBase64, string key, string iv)
        {
            if (string.IsNullOrEmpty(cipherTextBase64))
                throw new ArgumentException("Ciphertext is null or empty");

            byte[] cipherText = Convert.FromBase64String(cipherTextBase64);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}

class MyPolicy : ICertificatePolicy
{
    public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
    {
        return true;
    }
}