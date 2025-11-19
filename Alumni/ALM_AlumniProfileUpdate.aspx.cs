/*    
==================================================================================    
Modified By                                               : Aditya Sharma   
On Date                                                   :10 feb 2023    
Name                                                      :ALM_AlumniProfileUpdate.aspx    
Purpose                                                   : To Search Record    
Tables used                                               : ALM_AlumniRegistration    
Stored Procedures used                                    :    
Modules                                                   :Alumni    
Form                                                      :ALM_AlumniProfileUpdate.aspx    
Last Updated Date                                         :    
Last Updated By                                           :    
==================================================================================    
*/

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

public partial class Alumni_ALM_AlumniProfileUpdate : System.Web.UI.Page
{
    #region Data_Work

    DataAccess Dobj = new DataAccess();
    string upldPath = "";
    DataSet dsFile = null;

    DataAccess DAobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();
    ArrayList size = new ArrayList(); ArrayList outtype = new ArrayList();
    void Clear()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }
    public int pk_degreeid { get; private set; }
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
                if (Request.QueryString["u"] != null && Request.QueryString["u"] == "1")
                {
                    lblMsg.Text = "Record Updated Successfully!";
                }

                if (Request.QueryString["id"] != null)
                {
                    Session["EmpView_AlumniID"] = Request.QueryString["id"].ToString();
                }

                if (Session["AlumniID"] == null)
                {
                    Response.Redirect("../Alumin_Loginpage.aspx");
                }
                // FillCollege();
                if (Session["AlumniID"] != null)
                {
                    FillDegree();
                    pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
                    getsubject(pk_degreeid);
                    Anthem.Manager.IncludePageScripts = true;
                    txtAlumniName.Focus();
                    FillForUpdate();
                }
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

    protected void FillForUpdate()
    {
        try
        {
            //lblMsg.Text = "";
            btnRegister.Text = "UPDATE";
            btnRegister.CommandName = "UPDATE";
            int alumniId = 0;

            if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
                alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
            else
                alumniId = Convert.ToInt32(Session["AlumniID"].ToString());

            DataSet ds = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(alumniId).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["isapproved"]) == 1)
                {
                    chkmentor.Enabled = false;
                }
                else
                {
                    chkmentor.Enabled = true;
                }
                //   GetImageDetail(ds.Tables[0]);//change home page image as per latest image
                R_txtAlumnino.Text = ds.Tables[0].Rows[0]["alumnino"].ToString();
                FillSalution();
                Drp_Alumni_Name.SelectedValue = ds.Tables[0].Rows[0]["Alumni_Sal"].ToString();
                txtAlumniName.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();
                // txtdegree.Text = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
                //string pk_alumniid = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();

                ddldegree.SelectedValue = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
                ddldegree_SelectedIndexChanged(null, null);
                subjectlist.SelectedValue = ds.Tables[0].Rows[0]["Fk_subjectid"].ToString();
                FillDepartment();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();
                //if (ds.Tables[0].Rows[0]["alumnitype"].ToString() == "F")
                //{
                //TxtDepartment.Text = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();
                //TxtDepartment.Enabled = true;
                //}
                //else
                //{
                //    TxtDepartment.Enabled = false;
                //}

                ViewState["id"] = ds.Tables[0].Rows[0]["pk_alumniid"].ToString();

                //if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
                //{
                //    rdbmale.Checked = true;
                //    rdbfemale.Checked = false;
                //}
                //else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
                //{
                //    rdbfemale.Checked = true;
                //    rdbmale.Checked = false;
                //}

                if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
                {
                    rdbGender.SelectedValue = "M";
                }
                else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
                {
                    rdbGender.SelectedValue = "F";
                }

                //D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["yearofpassing"].ToString();
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

                Drp_MotherName.SelectedValue = ds.Tables[0].Rows[0]["Mother_Sal"].ToString();
                R_txtMotherName.Text = ds.Tables[0].Rows[0]["mothername"].ToString();
                Drp_FatherName.SelectedValue = ds.Tables[0].Rows[0]["Father_Sal"].ToString();
                R_txtFatherName.Text = ds.Tables[0].Rows[0]["fathername"].ToString();

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

                if (ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim() != "")
                {
                    Session["IsProfilec"] = "1";
                    imgProfileimg.ImageUrl = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + ds.Tables[1].Rows[0]["Files_Name"].ToString();
                }
                else
                {
                    Session["IsProfilec"] = "0";
                    imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                    hdPath.Text = "";
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    if (ds.Tables[2].Rows[0]["Files_Name"] != null && ds.Tables[2].Rows[0]["Files_Name"].ToString() != "")
                    {
                        ViewState["File2"] = ds.Tables[2].Rows[0]["Files_Name"].ToString();
                        lnkDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                        lnkDoc.CommandName = ds.Tables[2].Rows[0]["Files_Name"].ToString();
                        lnkDoc.Visible = true;
                        getFileName();
                    }
                }

                if (ds.Tables[1].Rows[0]["Files_Name"] != null && ds.Tables[1].Rows[0]["Files_Name"].ToString() != "")
                {
                    ViewState["File1"] = ds.Tables[1].Rows[0]["Files_Name"].ToString();
                    lnkprofile.CommandArgument = (ViewState["id"]) == null ? "1" : (ViewState["id"].ToString());
                    lnkprofile.CommandName = ds.Tables[1].Rows[0]["Files_Name"].ToString();
                    lnkprofile.Visible = true;
                    getProfileName();
                }
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
        FileRealName = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
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
        FileRealName = "https://backoffice.hpushimla.in/FTPSITE/HPU_DOC/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);
        //   }
        // FileUrl = FileUrl + FileName;
        lnkDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
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

            if (R_txtMotherName.Text == "")
            {
                ClientMessaging("Please enter  Mother Name");
                R_txtMotherName.Focus();
                return;
            }
            if (R_txtFatherName.Text == "")
            {
                ClientMessaging("Please enter  Father Name");
                R_txtFatherName.Focus();
                return;
            }
            if (R_txtPostedDate.Text.Trim() == "")
            {
                ClientMessaging("Please enter Date of Birth");
                R_txtPostedDate.Focus();
                return;
            }
            //if (ddl_alumniType.SelectedIndex == 0)
            //{
            //    ClientMessaging("Please Select Alumni Type");
            //    ddl_alumniType.Focus();
            //    return;
            //}
            //if (ddl_alumniType.SelectedIndex==1 && txtdepartment.Text == "")
            //{
            //    ClientMessaging("Please Enter Department Name");
            //    txtdepartment.Focus();
            //    return;
            //}
            //if (txtdegree.Text == "")
            //{
            //    ClientMessaging("Please Enter Degree");
            //    txtdegree.Focus();
            //    return;
            //}

            if (D_ddlYeofPass.SelectedIndex == 0)
            {
                ClientMessaging("Please select Year of Passing");
                D_ddlYeofPass.Focus();
                return;
            }
            if (E_txtEmail.Text == "")
            {
                ClientMessaging("Please enter Email Id");
                E_txtEmail.Focus();
                return;
            }

            E_txtEmail_TextChanged(null, null);//to check email id format

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
            DataSet ds = new DataSet();
            ds = Dobj.GetSchema("ALM_AlumniRegistration");
            DataRow dr = ds.Tables[0].NewRow();
            dr["alumnino"] = R_txtAlumnino.Text.Trim();

            dr["Alumni_Sal"] = Drp_Alumni_Name.SelectedValue.ToString().Trim();
            dr["alumni_name"] = txtAlumniName.Text.Trim();

            dr["Mother_Sal"] = Drp_MotherName.SelectedValue.ToString().Trim();
            dr["mothername"] = R_txtMotherName.Text.Trim();

            dr["Father_Sal"] = Drp_FatherName.SelectedValue.ToString().Trim();
            dr["fathername"] = R_txtFatherName.Text.Trim();

            //  dr["regno"] = 0;// R_txtReg.Text.Trim();
            dr["fk_collegeid"] = 0;
            // dr["fk_collegeid"] = Convert.ToInt32(D_ddlCollege.SelectedValue);
            // dr["Batchyear"] = Convert.ToInt32(D_ddlByear.SelectedValue);
            dr["fk_degreeid"] = Convert.ToInt32(ddldegree.SelectedValue);
            dr["Fk_subjectid"] = Convert.ToInt32(subjectlist.SelectedValue);//txtdegree.Text.Trim()
            dr["yearofpassing"] = Convert.ToInt32(D_ddlYeofPass.SelectedValue);
            dr["fk_pyearid"] = Convert.ToInt32(D_ddlYeofPass.SelectedValue);
            dr["email"] = E_txtEmail.Text.Trim();
            dr["currentaddress"] = txtCurrentAddress.Text.Trim();
            dr["per_address"] = txtperadd.Text.Trim();
            dr["currentoccupation"] = txtCurrentOccupation.Text.Trim();
            dr["contactno"] = R_txtContactno.Text.Trim();
            dr["special_interest"] = txtsplinterest.Text.Trim();
            dr["Achievement"] = txtachievement.Text.Trim();
            dr["remarks"] = txtremarks.Text.Trim();
            //dr["amount"] = txtamt.Text.Trim();
            //dr["receiptno"] = txtreceiptno.Text.Trim();
            dr["dob"] = R_txtPostedDate.Text.Trim();
            //CommonCode.DateFormats.Date_FrontToDB_R(R_txtPostedDate.Text.Trim());
            // Added By Manoj
            // dr["fk_Deptid"] = Convert.ToInt32(ddldegree.SelectedValue);
            //dr["fk_Deptid"] = TxtDepartment.Text.Trim();
            dr["fk_Deptid"] = Convert.ToInt32(ddlDepartment.SelectedValue);
            dr["designation"] = txtDesignation.Text.Trim();
            dr["mothername"] = R_txtMotherName.Text.Trim();
            dr["fathername"] = R_txtFatherName.Text.Trim();
            //dr["telephoneno"] = txttelephoneno.Text.Trim();
            dr["loginname"] = R_txtAlumnino.Text.Trim();
            dr["password"] = R_txtPostedDate.Text.Trim();//crp.Encrypt(R_txtPassword.Text.Trim());

            //if (rdbmale.Checked == true)
            //{
            //    dr["gender"] = "M";
            //}
            //else
            //    if (rdbfemale.Checked == true)
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
            //Code Added By Santosh sah
            string filepath = ""; string fileName = ""; string contenttype = ""; byte[] fileBytes = null;


            #region" "For Profile Pic"

            Random randomNo = new Random();
            string Message = "";
            string FileType = "";
            double filesize = 0;
            DataRow drimg = null;
            string Fileuniquename = string.Empty;

            //  dsImg = null;

            DataSet dsImg = Dobj.GetSchema("ALM_AlumniRegistration_File_dtl");
            dsImg.Tables[0].TableName = "ALM_AlumniRegistration_File_dtl";




            if (flUpload.HasFile == true)
            {
                filesize = flUpload.PostedFile.ContentLength;
                string Name = flUpload.PostedFile.FileName;

                filesize = Math.Round((filesize / 1024), 0);
                if (Name.Length > 800)
                {
                    ClientMessaging("Photo File Name Should not be more than 50 characters!");
                    return;
                }
                if (filesize > 2048)
                {
                    ClientMessaging("Photo size should not be more than 2 MB !");
                    return;
                }

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

            if (uploadDocuments.HasFile == true)
            {

                filesize = uploadDocuments.PostedFile.ContentLength;
                string DocumentName = uploadDocuments.PostedFile.FileName;
                filesize = Math.Round((filesize / 1024), 0);

                if (DocumentName.Length > 60)
                {
                    ClientMessaging("HPU Document File Name Should not be more than 60 characters!");
                    return;
                }
                if (filesize > 10240)
                {
                    ClientMessaging(" Document size should not be more than 10MB !");
                    return;
                }
                FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);
                if (FileType != null && FileType != "")
                {
                    if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
                && FileType != ".pjpeg" && FileType != ".PJPEG" && FileType != ".bmp" && FileType != ".BMP"
                && FileType != ".gif" && FileType != ".GIF" && FileType != ".png" && FileType != ".PNG")
                    {
                        ClientMessaging("HPU Document Should be in PDF and Image and  format!");
                        lblMsg.Text = "Document Should be in pdf format!";
                        return;
                    }
                }
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

            //}

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
                //clear1();
                FillForUpdate();
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
        else
        {
            rdbGender.SelectedValue = "F";
        }
    }
}
