using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CuteWebUI;
using DataAccessLayer;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SubSonic;




public partial class Alumni_ALM_AlumniProfileShow : System.Web.UI.Page
{
	#region Data_Work

    crypto cpt = new crypto();
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
    public string alumniType { get; private set; }
    public string memType { get; private set; }

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

                if (Session["AlumniID"] != null)   //(!string.IsNullOrEmpty(Session["AlumniID"].ToString()))
                {
                    //FillDegree();
                    //pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
                    //getsubject(pk_degreeid);

                    Anthem.Manager.IncludePageScripts = true;

                    txtAlumniName.Focus();
                    FillForUpdate();
                    ControlReadonly();
                }
                else
                {
                    Response.Redirect("../Alumin_Loginpage.aspx");
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
            Drp_Alumni_Name.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Title -", "0"));

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

    //private void FillDegree()
    //{
    //    DataSet ds = new DataSet();
    //    ds = ALM_ACD_Degree_Mst_sel();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddldegree.DataValueField = "pk_degreeid";
    //        ddldegree.DataTextField = "degree";
    //        ddldegree.DataSource = ds;
    //        ddldegree.DataBind();
    //        ddldegree.Items.Insert(0, new ListItem("-- Select Degree --", "0"));
    //    }
    //    else
    //    {
    //        ddldegree.DataSource = null;
    //        ddldegree.DataBind();
    //    }
    //}

    //private void getsubject(int pk_degreeid)
    //{
    //    DataSet ds = Bind_subject().GetDataSet();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        subjectlist.DataValueField = "pk_subjectid";
    //        subjectlist.DataTextField = "subject";
    //        subjectlist.DataSource = ds;
    //        subjectlist.DataBind();
    //        subjectlist.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
    //    }
    //    else
    //    {
    //        subjectlist.DataSource = null;
    //        subjectlist.DataBind();
    //    }
    //}

    //private void FillDepartment()
    //{
    //    DataSet ds = new DataSet();
    //    ds = ALM_Department_sel();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlDepartment.DataValueField = "Pk_deptId";
    //        ddlDepartment.DataTextField = "description";
    //        ddlDepartment.DataSource = ds;
    //        ddlDepartment.DataBind();
    //        ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
    //    }
    //    else
    //    {
    //        ddlDepartment.DataSource = null;
    //        ddlDepartment.DataBind();
    //    }
    //}

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

    //protected void Fill_PassingYear()
    //{
    //    D_ddlYeofPass.Items.Clear();
    //    DataSet ds = IUMSNXG.SP.ALM_Sp_AlumniBatchYear_Passing_Year().GetDataSet();
    //    if (ds.Tables[2].Rows.Count > 0)
    //    {
    //        D_ddlYeofPass.DataSource = ds.Tables[2];
    //        D_ddlYeofPass.DataTextField = "PassYear_Name";
    //        D_ddlYeofPass.DataValueField = "Pk_pass_id";
    //        D_ddlYeofPass.DataBind();
    //        D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
    //    }
    //    else
    //    {
    //        D_ddlYeofPass.Items.Insert(0, "-- Select Passing Year -- ");
    //    }
    //}

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

            DataSet ds = ALM_SP_AlumniRegistration_Select(alumniId).GetDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["isapproved"].ToString()) || Convert.ToBoolean(ds.Tables[0].Rows[0]["isDisabilityApproved"]) == true)
                {
                    chkmentor.Enabled = false;
                }
                else
                {
                    chkmentor.Enabled = false;
                }

                //if (ds.Tables[0].Rows[0]["alumnitype"].ToString() != "" || ds.Tables[0].Rows[0]["alumnitype"].ToString() != null)
                //{
                //    alumniType = ds.Tables[0].Rows[0]["alumnitype"].ToString();
                //    rdalumnitype.SelectedValue = ds.Tables[0].Rows[0]["alumnitype"].ToString();
                //    rdalumnitype.Items.FindByValue(alumniType).Selected = true;
                //}
                //else
                //{
                //    alumniType = string.Empty;
                //    rdalumnitype.SelectedValue = "";
                //}

                //if (ds.Tables[0].Rows[0]["Membership_Type"].ToString() != "" || ds.Tables[0].Rows[0]["Membership_Type"].ToString() != null)
                //{
                //    memType = ds.Tables[0].Rows[0]["Membership_Type"].ToString();
                //    rbdAlumniMemtype.SelectedValue = ds.Tables[0].Rows[0]["Membership_Type"].ToString();
                //    rdalumnitype.Items.FindByValue(memType).Selected = true;
                //}
                //else
                //{
                //    memType = string.Empty;
                //    rbdAlumniMemtype.SelectedValue = "";
                //}

                var row = ds.Tables[0].Rows[0];

                // Check and assign alumniType
                object alumniTypeObj = row["alumnitype"];
                string alumniTypeValue = alumniTypeObj != null ? alumniTypeObj.ToString() : null;

                if (!string.IsNullOrWhiteSpace(alumniTypeValue))
                {
                    alumniType = alumniTypeValue;
                    rdalumnitype.SelectedValue = alumniTypeValue;
                }
                else
                {
                    alumniType = string.Empty;
                    rdalumnitype.ClearSelection();
                }

                // Check and assign memType
                object memTypeObj = row["Membership_Type"];
                string memTypeValue = memTypeObj != null ? memTypeObj.ToString() : null;

                if (!string.IsNullOrWhiteSpace(memTypeValue))
                {
                    memType = memTypeValue;
                    rbdAlumniMemtype.SelectedValue = memTypeValue;
                }
                else
                {
                    memType = string.Empty;
                    rbdAlumniMemtype.ClearSelection();
                }

                //   GetImageDetail(ds.Tables[0]);//change home page image as per latest image
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

                //if (ds.Tables[0].Rows[0]["Father_Sal"].ToString() != null || ds.Tables[0].Rows[0]["Father_Sal"].ToString() != "")
                //{
                //    Drp_FatherName.SelectedValue = ds.Tables[0].Rows[0]["Father_Sal"].ToString();
                //}
                //else
                //{
                //    Drp_FatherName.SelectedIndex = 0;
                //}

                //R_txtFatherName.Text = ds.Tables[0].Rows[0]["fathername"].ToString();

                //if (ds.Tables[0].Rows[0]["Mother_Sal"].ToString() != null || ds.Tables[0].Rows[0]["Mother_Sal"].ToString() != "")
                //{
                //    Drp_MotherName.SelectedValue = ds.Tables[0].Rows[0]["Mother_Sal"].ToString();
                //}
                //else
                //{
                //    Drp_MotherName.SelectedIndex = 0;
                //}

                //R_txtMotherName.Text = ds.Tables[0].Rows[0]["mothername"].ToString();

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
				
                txtLocation.Text = ds.Tables[0].Rows[0]["Current_Location"].ToString();
                txtCountry.Text = ds.Tables[0].Rows[0]["Current_Country"].ToString();
				
				Anthem.Manager.IncludePageScripts = true;
                ViewState["Rows"] = ds.Tables[1].Rows.Count;
                generate_GridView(gvQualifications, Convert.ToInt32(ViewState["Rows"]));
                BindGridViewDropdown();
                FillQualInfoGrid(ds.Tables[1]);

                //if (ViewState["Rows"] == null)
                //{
                //    btnAddRow_Click(null, null);
                //}
                //else
                //{
                //    Anthem.Manager.IncludePageScripts = true;
                //    ViewState["Rows"] = ds.Tables[1].Rows.Count;
                //    generate_GridView(gvQualifications, Convert.ToInt32(ViewState["Rows"]));
                //    BindGridViewDropdown();
                //    FillQualInfoGrid(ds.Tables[1]);
                //}
				
                if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "Y")
                {
                    rdbIsPersonDisability.SelectedValue = "Y";
                    rdbIsPersonDisability_SelectedIndexChanged(null, null);
                }
                else if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "N")
                {
                    rdbIsPersonDisability.SelectedValue = "N";
                    rdbIsPersonDisability_SelectedIndexChanged(null, null);
                }

                if (ds.Tables[0].Rows[0]["isDisabled"].ToString() == "Y" && Convert.ToBoolean(ds.Tables[0].Rows[0]["isDisabilityPercentage"].ToString()) == true)
                {
                    isChkDisability.Checked = true; isChkDisability_CheckedChanged(null, null);
                    isChkDisability.Enabled = false;

                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        if (ds.Tables[4].Rows[0]["Files_Unique_Name"] != null && ds.Tables[4].Rows[0]["Files_Unique_Name"].ToString() != "")
                        {
                            ViewState["File3"] = ds.Tables[4].Rows[0]["Files_Unique_Name"].ToString();
                            Session["DDFile"] = ds.Tables[4].Rows[0]["Files_Unique_Name"].ToString();
                            Session["filePath"] = ds.Tables[4].Rows[0]["file_Path"].ToString();
                            lnkDisabilityDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                            lnkDisabilityDoc.CommandName = ds.Tables[4].Rows[0]["Files_Unique_Name"].ToString();
                            lnkDisabilityDoc.Visible = true;
                            getDDocFileName();
                            btnDisabilityDoc.Visible = false;
                            btnDisabilityDoc.Enabled = false;
                        }
                    }
                    else
                    {
                        Session["DDFile"] = null;
                        Session["filePath"] = null;
                        lnkDisabilityDoc.Text = "";
                        lnkDisabilityDoc.Visible = false;
                        btnDisabilityDoc.Visible = false;
                        btnDisabilityDoc.Enabled = false;
                    }
                }
                else
                {
                    rdbIsPersonDisability.SelectedValue = "N";
                    Session["DDFile"] = null;
                    Session["filePath"] = null;
                    lnkDisabilityDoc.Text = "";
                    lnkDisabilityDoc.Visible = false;
                    btnDisabilityDoc.Visible = false;
                    btnDisabilityDoc.Enabled = false;
                }

                //rdbIsPersonDisability_SelectedIndexChanged(null, null);

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

                //D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["yearofpassing"].ToString();
                //Fill_PassingYear();
                //D_ddlYeofPass.SelectedValue = ds.Tables[0].Rows[0]["fk_pyearid"].ToString();
                //
                //ddldegree.SelectedValue = ds.Tables[0].Rows[0]["fk_degreeid"].ToString();
                //ddldegree.CssClass = "ChosenSelector";
                //ddldegree_SelectedIndexChanged(null, null);
                //subjectlist.SelectedValue = ds.Tables[0].Rows[0]["Fk_subjectid"].ToString();
                //FillDepartment();
                //ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["fk_Deptid"].ToString();


                //string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
                //fileName = ds.Tables[1].Rows[0]["Files_Name"].ToString().Trim();

                if (ds.Tables[2].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString().Trim()))
                    {
                        Session["IsProfilec"] = "1";
                        imgProfileimg.ImageUrl = ReturnPath() + "/Alumni/StuImage/" + ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                    }
                    else
                    {
                        Session["IsProfilec"] = "0";
                        imgProfileimg.ImageUrl = "https://alumni.hpushimla.in/Alumni/StuImage/default-user.jpg";
                        hdPath.Text = "";
                    }
                }
                else
                {
                    Session["IsProfilec"] = "0";
                    imgProfileimg.ImageUrl = "https://alumni.hpushimla.in/Alumni/StuImage/default-user.jpg";
                    hdPath.Text = "";
                }

                //if (!string.IsNullOrEmpty(ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString().Trim()))
                //{
                //    Session["IsProfilec"] = "1";
                //    imgProfileimg.ImageUrl = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                //}
                //else
                //{
                //    Session["IsProfilec"] = "0";
                //    imgProfileimg.ImageUrl = "https://alumni.hpushimla.in/Alumni/StuImage/default-user.jpg";
                //    hdPath.Text = "";
                //}

                //if (!string.IsNullOrEmpty(ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString().Trim()))
                //{
                //    string fileUrl = "https://ftp.hpushimla.in/HPU_DOC/Alumni/StuImage/" + ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();

                //    if (IsFileAvailable(fileUrl))
                //    {
                //        Session["IsProfilec"] = "1";
                //        imgProfileimg.ImageUrl = fileUrl;
                //    }
                //    else
                //    {
                //        Session["IsProfilec"] = "0";
                //        imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                //        hdPath.Text = "";
                //    }
                //}
                //else
                //{
                //    Session["IsProfilec"] = "0";
                //    imgProfileimg.ImageUrl = "~/Online/NoImage/default-icon.jpg";
                //    hdPath.Text = "";
                //}

                if (ds.Tables[2].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString()))
                    {
                        ViewState["File1"] = ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                        lnkprofile.CommandArgument = (ViewState["id"]) == null ? "1" : (ViewState["id"].ToString());
                        lnkprofile.CommandName = ds.Tables[2].Rows[0]["Files_Unique_Name"].ToString();
                        lnkprofile.Visible = true;
                        getProfileName();
                    }
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[3].Rows[0]["Files_Unique_Name"].ToString()))
                    {
                        ViewState["File2"] = ds.Tables[3].Rows[0]["Files_Unique_Name"].ToString();
                        lnkDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                        lnkDoc.CommandName = ds.Tables[3].Rows[0]["Files_Unique_Name"].ToString();
                        lnkDoc.Visible = true;
                        getADocFileName();
                    }
                }

                R_txtLoginName1.Text = ds.Tables[0].Rows[0]["email"].ToString();

                if (ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    R_txtPassword.Attributes.Add("value", ds.Tables[0].Rows[0]["password"].ToString());
                    R_txtPassword.Text = ds.Tables[0].Rows[0]["password"].ToString();
                }
                Anthem.Manager.IncludePageScripts = true;
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    // Method to check if a file is available at a given URL
    private bool IsFileAvailable(string url)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD"; // Only request the header, not the whole file
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
        catch
        {
            // If the file doesn't exist or there's an error, return false
            return false;
        }
    }

    protected void getProfileName()
    {
        string FileName = lnkprofile.CommandName;
        string ftpUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";

        FileDisplayName = FileName;
        FileRealName = ftpUrl + "/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);

        lnkprofile.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }

    protected void getADocFileName()
    {
        string FileName = lnkDoc.CommandName;
        string ftpUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";

        FileDisplayName = FileName;
        FileRealName = ftpUrl + "/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);

        lnkDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
    }

    protected void getDDocFileName()
    {
        string FileName = lnkDisabilityDoc.CommandName;
        string ftpUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";

        FileDisplayName = FileName;
        FileRealName = ftpUrl + "/Alumni/StuImage/" + FileName.Substring(FileName.IndexOf("/") + 1);

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

            //if (R_txtFatherName.Text == "")
            //{
            //    ClientMessaging("Please enter  Father Name");
            //    R_txtFatherName.Focus();
            //    return;
            //}

            //if (R_txtMotherName.Text == "")
            //{
            //    ClientMessaging("Please enter  Mother Name");
            //    R_txtMotherName.Focus();
            //    return;
            //}

            if (R_txtPostedDate.Text.Trim() == "")
            {
                ClientMessaging("Please enter Date of Birth");
                R_txtPostedDate.Focus();
                return;
            }

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

            //if (R_txtPostedDate.Text.Trim() != "")
            //{
            //    DateTime dtDob = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(R_txtPostedDate.Text.Trim()));
            //    int DobYear = dtDob.Year;
            //    int PassYear = Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text.ToString());
            //    PassYear = PassYear - 18;
            //    if (DobYear > PassYear)
            //    {
            //        ClientMessaging("Alumni age should be greater than 18 years as per Passing year!");
            //        R_txtPostedDate.Focus();
            //        return;
            //    }
            //}

            //if (ddldegree.SelectedIndex == 0)
            //{
            //    ClientMessaging("Please Select Degree");
            //    ddldegree.Focus();
            //    return;
            //}
            //if (D_ddlYeofPass.SelectedIndex == 0)
            //{
            //    ClientMessaging("Please select Year of Passing");
            //    D_ddlYeofPass.Focus();
            //    return;
            //}

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

            //dr["Alumni_Sal"] = Drp_Alumni_Name.SelectedValue.ToString().Trim();
            dr["alumni_name"] = txtAlumniName.Text.Trim();

            //if (Drp_FatherName.SelectedIndex > 0)
            //{
            //    dr["Father_Sal"] = Drp_FatherName.SelectedValue.ToString().Trim();
            //}
            //else
            //{
            //    dr["Father_Sal"] = DBNull.Value;
            //}

            ////dr["Father_Sal"] = Drp_FatherName.SelectedValue.ToString().Trim();
            //dr["fathername"] = R_txtFatherName.Text.Trim();

            //if (Drp_MotherName.SelectedIndex > 0)
            //{
            //    dr["Mother_Sal"] = Drp_MotherName.SelectedValue.ToString().Trim();
            //}
            //else
            //{
            //    dr["Mother_Sal"] = DBNull.Value;
            //}

            ////dr["Mother_Sal"] = Drp_MotherName.SelectedValue.ToString().Trim();
            //dr["mothername"] = R_txtMotherName.Text.Trim();

            //  dr["regno"] = 0;// R_txtReg.Text.Trim();
            dr["fk_collegeid"] = 0;
            // dr["fk_collegeid"] = Convert.ToInt32(D_ddlCollege.SelectedValue);
            // dr["Batchyear"] = Convert.ToInt32(D_ddlByear.SelectedValue);
            dr["fk_degreeid"] = (object)DBNull.Value; //Convert.ToInt32(ddldegree.SelectedValue);
            dr["Fk_subjectid"] = (object)DBNull.Value;  //Convert.ToInt32(subjectlist.SelectedValue);//txtdegree.Text.Trim()
            dr["yearofpassing"] = (object)DBNull.Value;  //Convert.ToInt32(D_ddlYeofPass.SelectedItem.Text);
            dr["fk_pyearid"] = (object)DBNull.Value;  //Convert.ToInt32(D_ddlYeofPass.SelectedValue);
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
            dr["fk_Deptid"] = (object)DBNull.Value; //Convert.ToInt32(ddlDepartment.SelectedValue);
            dr["designation"] = txtDesignation.Text.Trim();
            dr["mothername"] = (object)DBNull.Value; //R_txtMotherName.Text.Trim();
            dr["fathername"] = (object)DBNull.Value; //R_txtFatherName.Text.Trim();
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

            if (rdbIsPersonDisability.SelectedValue.ToString() == "Y")
            {
                dr["isDisabled"] = "Y";
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                dr["isDisabled"] = "N";
            }

            if (rdbIsPersonDisability.SelectedValue.ToString() == "Y" && isChkDisability.Checked)
            {
                dr["isDisabilityPercentage"] = Convert.ToBoolean(1);
                dr["disabiltyRemarks"] = txtDisabilityRemark.Text.Trim().ToString();
            }
            else if (rdbIsPersonDisability.SelectedValue.ToString() == "N" && !isChkDisability.Checked)
            {
                dr["isDisabilityPercentage"] = Convert.ToBoolean(0);
                dr["disabiltyRemarks"] = "";
            }

            dr["Current_Location"] = txtLocation.Text.Trim();
            dr["Current_Country"] = txtCountry.Text.Trim();

            ds.Tables[0].Rows.Add(dr);

            #region "For GridView Qualifications Details By Indra"

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

            //Code Added By Santosh sah
            string filepath = ""; string fileName = ""; string contenttype = ""; byte[] fileBytes = null;

            #region "For Profile Pic"

            Random randomNo = new Random();
            string Message = "";
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
                FileType = Path.GetExtension(flUpload.PostedFile.FileName);

                UploadFiles(); //to get the physical path of server 
                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                drimg = dsImg.Tables[0].NewRow();

                drimg["IsProfilePicOrDoc"] = 1;
                //string filetype = aItem.FileName.Substring(aItem.FileName.LastIndexOf("."));
                drimg["Files_Name"] = Name;
                drimg["Files_Unique_Name"] = "HPU_Alumni_PP" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
                    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PP" + "_" + randomNo.Next(33333, 454545454) + "_" + randomNo.Next(999999, 15215454) + FileType;
                Fileuniquename = "HPU_Alumni_PP_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo.Next(50000, 1000000) + "_" +
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
            else
            {
                if (ViewState["File1"] != null)
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
                //filesize = uploadDocuments.PostedFile.ContentLength;
                string DocumentName = uploadDocuments.PostedFile.FileName;
                ///filesize = Math.Round((filesize / 1024), 0);

                FileType = Path.GetExtension(uploadDocuments.PostedFile.FileName);

                drimg = dsImg.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 0;
                UploadFiles();//to get the physical path of server 

                string upldPath = "";
                string currDir = System.IO.Directory.GetCurrentDirectory();
                upldPath = this.upldPath;
                Random randomNo1 = new Random();
                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = "HPU_Alumni_AD" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
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
            else
            {
                if (ViewState["File2"] != null)
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

            //if (fileDisabilityDoc.HasFile == true && checkValidDocOrNot())
            //{
            //    if (ViewState["File3"].ToString() != null || ViewState["File3"].ToString() != "")
            //    {
            //        DeleteFile(ViewState["File3"].ToString());
            //    }

            //    //filesize = uploadDocuments.PostedFile.ContentLength;
            //    string DocumentName = fileDisabilityDoc.PostedFile.FileName;
            //    ///filesize = Math.Round((filesize / 1024), 0);

            //    FileType = Path.GetExtension(fileDisabilityDoc.PostedFile.FileName);

            //    drimg = dsImg.Tables[0].NewRow();
            //    drimg["IsProfilePicOrDoc"] = 0;
            //    UploadFiles();//to get the physical path of server 

            //    string upldPath = "";
            //    string currDir = System.IO.Directory.GetCurrentDirectory();
            //    upldPath = this.upldPath;
            //    Random randomNo1 = new Random();
            //    drimg["Files_Name"] = DocumentName;
            //    drimg["Files_Unique_Name"] = "HPU_Alumni_DD" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
            //        DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
            //    drimg["FileExtension"] = FileType;
            //    drimg["File_Path"] = upldPath;
            //    drimg["FilesFor"] = "DD";
            //    dsImg.Tables[0].Rows.Add(drimg);// to add all control value to ds
            //    bool IsExistPath = System.IO.Directory.Exists(upldPath);
            //    if (!IsExistPath)
            //        System.IO.Directory.CreateDirectory(upldPath);
            //    fileDisabilityDoc.SaveAs(upldPath + drimg["Files_Unique_Name"].ToString());
            //}
            if (Session["DDFile"] != null)
            {
                string DocumentName = Session["DDFile"].ToString();
                FileType = Path.GetExtension(Session["DDFile"].ToString());
                drimg = dsImg.Tables[0].NewRow();
                drimg["IsProfilePicOrDoc"] = 0;
                drimg["Files_Name"] = DocumentName;
                drimg["Files_Unique_Name"] = Session["DDFile"].ToString();
                drimg["FileExtension"] = FileType;
                drimg["File_Path"] = Session["filepath"].ToString();
                drimg["FilesFor"] = "DD";
                dsImg.Tables[0].Rows.Add(drimg);
            }
            else
            {
                if (ViewState["File3"] != null)
                {
                    DataSet dsSS = IUMSNXG_ALM.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"].ToString())).GetDataSet();

                    if (dsSS != null && dsSS.Tables[5].Rows.Count > 0)
                    {
                        drimg = dsImg.Tables[0].NewRow();
                        drimg["Fk_alumniid"] = Convert.ToInt64(ViewState["id"].ToString());
                        drimg["IsProfilePicOrDoc"] = dsSS.Tables[5].Rows[0]["IsProfilePicOrDoc"].ToString();
                        drimg["Files_Name"] = dsSS.Tables[5].Rows[0]["Files_Name"].ToString();
                        drimg["Files_Unique_Name"] = dsSS.Tables[5].Rows[0]["Files_Unique_Name"].ToString(); ;
                        drimg["FileExtension"] = dsSS.Tables[5].Rows[0]["FileExtension"].ToString(); ;
                        drimg["File_Path"] = dsSS.Tables[5].Rows[0]["File_Path"].ToString();
                        drimg["FilesFor"] = "DD";
                        dsImg.Tables[0].Rows.Add(drimg);
                    }
                }
            }

            #endregion

            if (dsQual != null)
            {
                ds.Merge(dsQual);
            }

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
                upldPath = upldPath + "\\Alumni\\StuImage" + "\\";
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
        string FolderName = @"/Alumni//";
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
                    FilePath = @FilePath + FolderName + FileName;
                    // FilePath = FolderName + FileName;
                }
                else
                {
                    FilePath = dr["Physical_Path"].ToString().Trim();
                    FilePath = @FilePath + FolderName + FileName;
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

    //protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    pk_degreeid = Convert.ToInt32(ddldegree.SelectedValue);
    //    getsubject(pk_degreeid);
    //}

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
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
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

        if (Name.Length > 500)
        {
            ClientMessaging("Photo File Name Should not be more than 500 characters!");
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
            case ".bmp":
                return true;
            case ".png":
                return true;
            default:
                ClientMessaging("Only files .jpg, .jpeg, .bmp and .png extension are allowed.");
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

        if (Names.Length > 500)
        {
            ClientMessaging("Documents File Name Should not be more than 500 characters!");
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
            case ".bmp":
                return true;
            case ".png":
                return true;
            case ".pdf":
                return true;
            default:
                ClientMessaging("Only files .jpg, .jpeg, .bmp, .png and .pdf extension are allowed.");
                return false;
        }
        return true;
    }

    public void ControlReadonly()
    {
        Drp_Alumni_Name.Enabled = true;
        txtAlumniName.Enabled = true;
        //Drp_FatherName.Enabled = true;
        //R_txtFatherName.Enabled = true;
        //Drp_MotherName.Enabled = true;
        //R_txtMotherName.Enabled = true;
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
        //ddldegree.Enabled = true;
        //subjectlist.Enabled = true;
        //D_ddlYeofPass.Enabled = true;
        //ddlDepartment.Enabled = true;

        //generate_GridView(gvQualifications, 1);
        //BindGridViewDropdown();
        ViewState["PreviousRow"] = null;
        ViewState["Rows"] = null;
        Anthem.Manager.IncludePageScripts = true;

        txtLocation.Enabled = true;
        txtCountry.Enabled = true;
        rdbIsPersonDisability.Enabled = false;
        isChkDisability.Enabled = false;
        txtDisabilityRemark.Enabled = false;
        btnDisabilityDoc.Enabled = false;
        fileDisabilityDoc.Enabled = false;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewReport();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    //protected void ViewReport()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        ReportDocument objRptDoc = new ReportDocument();
    //        string filename = "";

    //        try
    //        {
    //            int alumniId = 0;

    //            if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
    //                alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
    //            else
    //                alumniId = Convert.ToInt32(Session["AlumniID"].ToString());

    //            //DataSet dsAR = ALM_SP_AlumniRegistration_Details_Report_Print_By_Alumni(alumniId).GetDataSet();
    //            DataSet dsAR = ALM_SP_AlumniRegistrationDetails_Print_Report(alumniId).GetDataSet();

    //            if (dsAR.Tables[0].Rows.Count > 0)
    //            {
    //                dsAR.Tables[0].TableName = "Alumni_Registration_Details";
    //                dsAR.Tables[1].TableName = "Company_Details";
    //                dsAR.Tables[2].TableName = "Qualifications_Details";
    //                //dsAR.WriteXml(Server.MapPath("~/Alumni/ALM_XML/Alumni_Registration_Details_Report.xml"));
    //                filename = Server.MapPath("~/Alumni/ALM_Reports/Alumni_Registration_Details_Report.rpt");
    //                objRptDoc.Load(filename);
    //                objRptDoc.SetDataSource(dsAR);

    //                objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Alumni Registration Details Report");
    //            }
    //            else
    //            {
    //                lblMsg.Text = "No Records Found!";
    //            }
    //        }
    //        catch (System.Data.SqlClient.SqlException ex)
    //        {
    //            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
    //        }
    //        finally
    //        {
    //            objRptDoc.Close();
    //            objRptDoc.Dispose();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = ex.Message;
    //    }
    //}
    protected void ViewReport()
    {
        try
        {
            lblMsg.Text = "";
            ReportDocument objRptDoc = new ReportDocument();
            string filename = "";

            try
            {
                int alumniId = 0;

                if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
                    alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
                else
                    alumniId = Convert.ToInt32(Session["AlumniID"].ToString());

                //DataSet dsAR = ALM_SP_AlumniRegistration_Details_Report_Print_By_Alumni(alumniId).GetDataSet();
                DataSet dsAR = ALM_SP_AlumniRegistrationDetails_Print_Report(alumniId).GetDataSet();

                if (dsAR.Tables[0].Rows.Count > 0)
                {
                    dsAR.Tables[0].TableName = "Alumni_Registration_Details";
                    dsAR.Tables[1].TableName = "Company_Details";
                    dsAR.Tables[2].TableName = "Qualifications_Details";
                    //dsAR.WriteXml(Server.MapPath("~/Alumni/ALM_XML/Alumni_Registration_Details_Report.xml"));
                    filename = Server.MapPath("~/Alumni/ALM_Reports/Alumni_Registration_Details_Report.rpt");
                    objRptDoc.Load(filename);
                    objRptDoc.SetDataSource(dsAR);

                    objRptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "Alumni Registration Details Report");
                }
                else
                {
                    lblMsg.Text = "No Records Found!";
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
            catch (Exception ex)
            {
                lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            }
            finally
            {
                objRptDoc.Close();
                objRptDoc.Dispose();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    public StoredProcedure ALM_SP_AlumniRegistration_Details_Report_Print_By_Alumni(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Details_Report", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumniid", alumniid, DbType.Int32);
        return sp;
    }

    protected void rdbIsPersonDisability_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string dectype2 = cpt.Decrypt(Session["RBValue"].ToString());
        //alumniType = dectype2.ToString();
        if (rdbIsPersonDisability.SelectedValue == "Y")
        {
            isChkDisabilityPnl.Visible = true; isChkDisabilityPnl.Enabled = false;
            disabilitySection.Visible = true; disabilitySection.Enabled = false;
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);
            //btnRegister.Text = "REGISTER";
        }
        else if (rdbIsPersonDisability.SelectedValue == "N")
        {
            isChkDisabilityPnl.Visible = false; isChkDisabilityPnl.Enabled = false;
            disabilitySection.Visible = false; disabilitySection.Enabled = false;
            isChkDisability.Checked = false; isChkDisability.Enabled = false;
            txtDisabilityRemark.Text = "";
            Anthem.Manager.IncludePageScripts = true;
            isChkDisability_CheckedChanged(null, null);
            //btnRegister.Text = "REGISTER AND PAY";
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void isChkDisability_CheckedChanged(object sender, EventArgs e)
    {
        if (isChkDisability.Checked == true && rdbIsPersonDisability.SelectedValue == "Y")
        {
            Anthem.Manager.IncludePageScripts = true;
            isChkDisabilityPnl.Visible = true;
            disabilityRemarkSection.Style["display"] = "block";
            isChkDisability.Checked = true;
            isChkDisability.Enabled = false;
            //disabilityDocuments.Visible = true;
            fileDisabilityDoc.Visible = true;
            fileDisabilityDoc.Enabled = false;
            btnDisabilityDoc.Visible = true;
            btnDisabilityDoc.Enabled = false;
            //divDisabilityDoc.Style["display"] = "block";
            lblOnlineFees.Text = "0";
            Session["RegFees"] = "0";
            hdnId.Value = "0";
            txtDisabilityRemark.Enabled = false;
        }
        else if (isChkDisability.Checked == false && rdbIsPersonDisability.SelectedValue == "N")
        {
            Anthem.Manager.IncludePageScripts = true;
            disabilityRemarkSection.Style["display"] = "none";
            isChkDisability.Checked = false;
            isChkDisability.Enabled = false;
            //disabilityDocuments.Visible = false;
            fileDisabilityDoc.Visible = false;
            fileDisabilityDoc.Enabled = false;
            btnDisabilityDoc.Visible = false;
            btnDisabilityDoc.Enabled = false;
            //divDisabilityDoc.Style["display"] = "none";
            txtDisabilityRemark.Text = "";
            txtDisabilityRemark.Enabled = false;
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
                        Session["DDFile"] = null;
                        Session["filePath_refletter_delete"] = null;
                    }
                    catch (Exception ex)
                    {
                        Label1.Text = "Error deleting file: " + ex.Message;
                    }
                }
            }
            lnkDisabilityDoc.Text = "";
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    private void FeeDetails()
    {
        if (alumniType == "F" || alumniType == "ExStu")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rbdAlumniMemtype.Items.FindByValue("LM").Selected = true;
                memType = rbdAlumniMemtype.SelectedValue;
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
                rbdAlumniMemtype.Items.FindByValue("LM").Selected = true;
                memType = rbdAlumniMemtype.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        else if (alumniType == "S")
        {
            if (rdbIsPersonDisability.SelectedValue.ToString() == "N")
            {
                rbdAlumniMemtype.Items.FindByValue("SM").Selected = true;
                memType = rbdAlumniMemtype.SelectedValue;
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
                rbdAlumniMemtype.Items.FindByValue("SM").Selected = true;
                memType = rbdAlumniMemtype.SelectedValue;
                lblOnlineFees.Text = "0";
                Session["RegFees"] = "0";
                hdnId.Value = "0";
            }
        }
        Anthem.Manager.IncludePageScripts = true;
    }


    protected void rbdAlumniMemtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FeeDetails();
    }

    public StoredProcedure Alm_GetMembershipFee()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_FeeCollection", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@membershipType", memType, DbType.String);
        return sp;
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
            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpeg" && FileType != ".JPEG" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".png" && FileType != ".PNG" && FileType != ".bmp" && FileType != ".BMP")
            {
                ClientMessaging("This should be in pdf, jpeg, jpg!");
                return;
            }
            else
            {
                string lFileName = "";
                Random rand = new Random();
                int n = rand.Next();

                string filename = "HPU_Alumni_DD_" + "_" + Guid.NewGuid().ToString() + "_" + rand.Next(50000, 1000000) + "_" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + "DD" + "_" + rand.Next(33333, 454545454) + "_" + rand.Next(999999, 15215454) + FileType;

                lFileName = FU_physicalPath(fileDisabilityDoc, "\\Alumni\\StuImage\\", filename);
                Session["DDFile"] = lFileName;
                fileDisabilityDoc.SaveAs(Server.MapPath("ALM_Reports//" + lFileName));
                ClientMessaging("file uploaded Successfully..");
                string filePathforhyperlink = "ALM_Reports//" + Session["DDFile"].ToString();
                string filePath = Server.MapPath("ALM_Reports/") + Session["DDFile"].ToString();
                Session["filePath_refletter_delete"] = filePath;
                lnkDisabilityDoc.Visible = true;
                //lnkDisabilityDoc.href = filePathforhyperlink;
                lnkDisabilityDoc.Text = fileDisabilityDoc.FileName;
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

    public string SetServiceDocs(string FileName)
    {
        string FolderName = @"/Alumni/StuImage/";
        string host = HttpContext.Current.Request.Url.Host;
        string FilePath = ""; //"http://52.172.15.68/Published_App/FTPsite";
        DataSet dsFilepath = new DataSet();
        dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/UMM/IO_Config.xml"));
        foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        {
            if (host == dr["Server_Ip"].ToString().Trim())
            {
                if (host != "localhost")
                {
                    //FilePath = Server.MapPath("~/Published_App/FTPsite");
                    FilePath = dr["Physical_Path"].ToString().Trim();
                    FilePath = @FilePath + FolderName + FileName;
                }
                //else
                //{
                //    FilePath = dr["Physical_Path"].ToString().Trim();
                //    FilePath = @FilePath + FolderName + @"/" + FileName;
                //}
                return FilePath;
            }
        }
        return FilePath;
    }

    void DeleteFile(string file)
    {
        try
        {
            string path = SetServiceDocs(file);
            if (!string.IsNullOrEmpty(path))
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
        }
        catch
        {

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

            DataSet ds = ALM_ACD_Degree_Mst_sel();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDegree.DataValueField = "pk_degreeid";
                ddlDegree.DataTextField = "degree";
                ddlDegree.DataSource = ds;
                ddlDegree.DataBind();
                ddlDegree.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Degree --", "0"));
            }
            else
            {
                ddlDegree.DataSource = null;
                ddlDegree.DataBind();
            }
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
                ddlPassingYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Passing Year -- ", "0"));
            }
            else
            {
                ddlPassingYear.Items.Insert(0, "-- Select Passing Year -- ");
            }
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
                ddlDepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Department --", "0"));
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
        ////DataSet dtStaus = new DataSet();
        ////DataTable dtStructure = new DataTable();
        ////dtStructure.TableName = "ALM_Status";
        ////dtStructure.Columns.Add("fk_DegreeID");
        ////dtStaus.Tables.Add(dtStructure);

        ////int rowcount = gvQualifications.Rows.Count;
        ////foreach (GridViewRow gr in gvQualifications.Rows)
        ////{
        ////    Anthem.DropDownList ddlDegree = gr.FindControl("ddldegree") as Anthem.DropDownList;
        ////    Anthem.DropDownList ddlSubject = gr.FindControl("subjectlist") as Anthem.DropDownList;
        ////    Anthem.DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as Anthem.DropDownList;
        ////    Anthem.DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as Anthem.DropDownList;

        ////    DataRow drQual = dtStaus.Tables[0].NewRow();
        ////    drQual["fk_DegreeID"] = ddlDegree.SelectedIndex == 0 ? "0" : ddlDegree.SelectedValue;

        ////    dtStaus.Tables[0].Rows.Add(drQual);
        ////    xmlDocQual = dtStaus.GetXml();

        ////    DataSet ds = getSubjectsByDegreeID(xmlDocQual).GetDataSet();

        ////    using (DataSet ds1 = getSubjectsByDegree(Convert.ToInt32(ddlDegree.SelectedValue)).GetDataSet())
        ////    {
        ////        if (ddlSubject.SelectedValue == "0")
        ////        {
        ////            ddlSubject.DataTextField = "subject";
        ////            ddlSubject.DataValueField = "pk_subjectid";
        ////            ddlSubject.DataSource = ds1;
        ////            ddlSubject.DataBind();
        ////            ddlSubject.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        ////        }
        ////        Anthem.Manager.IncludePageScripts = true;
        ////    }

        ////    DataTable dt1 = ViewState["PreviousRow"] as DataTable;
        ////    if (dt1 != null)
        ////    {
        ////        for (int i = 0; i < dt1.Rows.Count; i++)
        ////        {
        ////            if (gr.RowIndex == i)
        ////            {
        ////                ddlDegree.SelectedValue = dt1.Rows[i]["fk_DegreeID"].ToString();
        ////                ddlSubject.SelectedValue = dt1.Rows[i]["fk_SubjectID"].ToString();
        ////                ddlPassingYear.SelectedValue = dt1.Rows[i]["fk_PassingYearID"].ToString();
        ////                ddlDepartment.SelectedValue = dt1.Rows[i]["fk_DeptID"].ToString();
        ////            }
        ////        }
        ////        Anthem.Manager.IncludePageScripts = true;
        ////    }
        ////}

        ////Anthem.Manager.IncludePageScripts = true;

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
            ddlSubject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Subject --", "0"));
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
                    ClientMessaging("Atleast One Seed Detail is Required !");
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

    public static void generate_GridView(Anthem.GridView SourceID, int Rows)
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
            ////        Anthem.DropDownList ddlDegree = gvQualifications.Rows[i].FindControl("ddldegree") as Anthem.DropDownList;
            ////        Anthem.DropDownList ddlSubject = gvQualifications.Rows[i].FindControl("subjectlist") as Anthem.DropDownList;
            ////        Anthem.DropDownList ddlPassingYear = gvQualifications.Rows[i].FindControl("D_ddlYeofPass") as Anthem.DropDownList;
            ////        Anthem.DropDownList ddlDepartment = gvQualifications.Rows[i].FindControl("ddlDepartment") as Anthem.DropDownList;

            ////        ddlDegree.SelectedValue = Convert.ToString(dtQualInfo.Rows[i]["fk_DegreeID"]) == "0" ? "-- Select Degree --" : Convert.ToString(dtQualInfo.Rows[i]["fk_DegreeID"]).Trim();
            ////        ddldegree_SelectedIndexChanged(null, null);
            ////        ddlSubject.SelectedValue = Convert.ToString(dtQualInfo.Rows[i]["fk_SubjectID"]) == "0" ? "-- Select Subject --" : Convert.ToString(dtQualInfo.Rows[i]["fk_SubjectID"]).Trim();
            ////        ddlPassingYear.SelectedValue = Convert.ToString(dtQualInfo.Rows[i]["fk_PassingYearID"]) == "0" ? "-- Select Passing Year --" : Convert.ToString(dtQualInfo.Rows[i]["fk_PassingYearID"]).Trim();
            ////        ddlDepartment.SelectedValue = Convert.ToString(dtQualInfo.Rows[i]["fk_DeptID"]) == "0" ? "-- Select Department --" : Convert.ToString(dtQualInfo.Rows[i]["fk_DeptID"]).Trim();
            ////    }
            ////Anthem.Manager.IncludePageScripts = true;

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
                            ddlSubject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Subject --", "0"));
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
                Anthem.Manager.IncludePageScripts = true;
            }
        }
        catch (Exception exp)
        {
            ClientMessaging(exp.Message);
        }
    }

    protected void BindGridViewDropdown()
    {
        DataSet ds = getAllRecords().GetDataSet();

        ////foreach (GridViewRow item in gvQualifications.Rows)
        ////{
        ////    Anthem.DropDownList ddlDegree = item.FindControl("ddldegree") as Anthem.DropDownList;
        ////    ddlDegree.DataTextField = "degree";
        ////    ddlDegree.DataValueField = "pk_degreeid";
        ////    ddlDegree.DataSource = ds.Tables[0];
        ////    ddlDegree.DataBind();
        ////    ddlDegree.Items.Insert(0, new ListItem("-- Select Degree --", "0"));

        ////    Anthem.DropDownList ddlSubject = item.FindControl("subjectlist") as Anthem.DropDownList;
        ////    using (DataSet dsSub = getSubjectsByDegree(Convert.ToInt32(ddlDegree.SelectedValue)).GetDataSet())
        ////    {
        ////        ddlSubject.DataTextField = "subject";
        ////        ddlSubject.DataValueField = "pk_subjectid";
        ////        ddlSubject.DataSource = dsSub;
        ////        ddlSubject.DataBind();
        ////        ddlSubject.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        ////    }

        ////    Anthem.DropDownList ddlPassingYear = item.FindControl("D_ddlYeofPass") as Anthem.DropDownList;
        ////    ddlPassingYear.DataTextField = "PassYear_Name";
        ////    ddlPassingYear.DataValueField = "PK_Pass_ID";
        ////    ddlPassingYear.DataSource = ds.Tables[1];
        ////    ddlPassingYear.DataBind();
        ////    ddlPassingYear.Items.Insert(0, new ListItem("-- Select PassingYear --", "0"));

        ////    Anthem.DropDownList ddlDepartment = item.FindControl("ddlDepartment") as Anthem.DropDownList;
        ////    ddlDepartment.DataTextField = "description";
        ////    ddlDepartment.DataValueField = "Pk_deptId";
        ////    ddlDepartment.DataSource = ds.Tables[2];
        ////    ddlDepartment.DataBind();
        ////    ddlDepartment.Items.Insert(0, new ListItem("-- Select Department --", "0"));
        ////}
        ////Anthem.Manager.IncludePageScripts = true;

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
                ddlDegree.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Degree --", "0"));
            }

            // Passing year dropdown
            DropDownList ddlPassingYear = row.FindControl("D_ddlYeofPass") as DropDownList;
            if (ddlPassingYear != null && ddlPassingYear.Items.Count == 0)
            {
                ddlPassingYear.DataTextField = "PassYear_Name";
                ddlPassingYear.DataValueField = "PK_Pass_ID";
                ddlPassingYear.DataSource = dtYears;
                ddlPassingYear.DataBind();
                ddlPassingYear.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Passing Year --", "0"));
            }

            // Department dropdown
            DropDownList ddlDepartment = row.FindControl("ddlDepartment") as DropDownList;
            if (ddlDepartment != null && ddlDepartment.Items.Count == 0)
            {
                ddlDepartment.DataTextField = "description";
                ddlDepartment.DataValueField = "Pk_deptId";
                ddlDepartment.DataSource = dtDepts;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Department --", "0"));
            }
            Anthem.Manager.IncludePageScripts = true;
        }
    }

    protected DataTable GetEduQualGridData()
    {
        DataSet dsQual = DAobj.GetSchema("ALM_EducationQualifications");
        foreach (GridViewRow gr in gvQualifications.Rows)
        {
            Anthem.DropDownList ddlDegree = gr.FindControl("ddldegree") as Anthem.DropDownList;
            Anthem.DropDownList ddlSubject = gr.FindControl("subjectlist") as Anthem.DropDownList;
            Anthem.DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as Anthem.DropDownList;
            Anthem.DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as Anthem.DropDownList;

            DataRow drQual = dsQual.Tables[0].NewRow();

            if (ddlDegree != null)
                drQual["fk_DegreeID"] = string.IsNullOrEmpty(ddlDegree.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlDegree.SelectedValue);

            if (ddlSubject != null)
                drQual["fk_SubjectID"] = string.IsNullOrEmpty(ddlSubject.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlSubject.SelectedValue);

            if (ddlPassingYear != null)
                drQual["fk_PassingYearID"] = string.IsNullOrEmpty(ddlPassingYear.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlPassingYear.SelectedValue);

            if (ddlDepartment != null)
                drQual["fk_DeptID"] = string.IsNullOrEmpty(ddlDepartment.SelectedValue) ? (object)DBNull.Value : ddlDepartment.SelectedValue.Trim().ToString();

            dsQual.Tables[0].Rows.Add(drQual);
        }
        return dsQual.Tables[0];
    }

    private void BindSubjects(int degreeId, Anthem.DropDownList subjectDropdown)
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
            subjectDropdown.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Subject --", "0"));
        }
        else
        {
            subjectDropdown.Items.Clear();
            subjectDropdown.Items.Add(new System.Web.UI.WebControls.ListItem("-- Select Subject --", "0"));
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
                    Anthem.DropDownList ddlDegree = gr.FindControl("ddldegree") as Anthem.DropDownList;
                    Anthem.DropDownList ddlSubject = gr.FindControl("subjectlist") as Anthem.DropDownList;
                    Anthem.DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as Anthem.DropDownList;
                    Anthem.DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as Anthem.DropDownList;

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

    protected bool ValidQualEduList()
    {
        foreach (GridViewRow gr in gvQualifications.Rows)
        {
            Anthem.DropDownList ddlDegree = gr.FindControl("ddldegree") as Anthem.DropDownList;
            Anthem.DropDownList ddlSubject = gr.FindControl("subjectlist") as Anthem.DropDownList;
            Anthem.DropDownList ddlPassingYear = gr.FindControl("D_ddlYeofPass") as Anthem.DropDownList;
            Anthem.DropDownList ddlDepartment = gr.FindControl("ddlDepartment") as Anthem.DropDownList;

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

    public static StoredProcedure ALM_SP_AlumniRegistration_Select(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Select", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public StoredProcedure ALM_SP_AlumniRegistrationDetails_Print_Report(int alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistrationDetails_Print_Report", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumniid", alumniid, DbType.Int32);
        return sp;
    }

    // --- PATCH: BaseColor fully qualified to fix CS0246 ---

    // Inside ViewReportITextSharp()
    //protected void ViewReportITextSharp()
    //{
    //    lblMsg.Text = "";
    //    MemoryStream ms = null;
    //    Document doc = null;
    //    try
    //    {
    //        int alumniId = 0;
    //        if (Session["AlumniID"].ToString() == null || Session["AlumniID"].ToString() == "")
    //            alumniId = Convert.ToInt32(Session["EmpView_AlumniID"].ToString());
    //        else
    //            alumniId = Convert.ToInt32(Session["AlumniID"].ToString());

    //        DataSet dsAR = ALM_SP_AlumniRegistrationDetails_Print_Report(alumniId).GetDataSet();

    //        if (dsAR == null || dsAR.Tables.Count == 0 || dsAR.Tables[0].Rows.Count == 0)
    //        {
    //            lblMsg.Text = "No Records Found!";
    //            return;
    //        }

    //        dsAR.Tables[0].TableName = "Alumni_Registration_Details";
    //        if (dsAR.Tables.Count > 1) dsAR.Tables[1].TableName = "Company_Details";
    //        if (dsAR.Tables.Count > 2) dsAR.Tables[2].TableName = "Qualifications_Details";

    //        ms = new MemoryStream();
    //        doc = new Document(PageSize.A4, 32f, 32f, 40f, 40f);
    //        PdfWriter writer = PdfWriter.GetInstance(doc, ms);
    //        writer.CloseStream = false;
    //        doc.AddAuthor("Alumni Portal");
    //        doc.AddTitle("Alumni Registration Details Report");
    //        doc.AddCreationDate();
    //        doc.Open();

    //        // Fonts (BaseColor fully qualified)
    //        var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, iTextSharp.text.Color.BLACK);
    //        var fontSection = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.Color.BLACK);
    //        var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, iTextSharp.text.Color.WHITE);
    //        var fontCell = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Color.BLACK);

    //        Paragraph title = new Paragraph("Alumni Registration Details Report", fontTitle)
    //        {
    //            Alignment = Element.ALIGN_CENTER,
    //            SpacingAfter = 10f
    //        };
    //        doc.Add(title);

    //        Paragraph generated = new Paragraph("Generated On: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture), fontCell)
    //        {
    //            Alignment = Element.ALIGN_RIGHT,
    //            SpacingAfter = 10f
    //        };
    //        doc.Add(generated);

    //        doc.Add(new Paragraph("Alumni Details", fontSection) { SpacingAfter = 6f });
    //        PdfPTable alumniTable = BuildKeyValueTable(dsAR.Tables["Alumni_Registration_Details"], fontHeader, fontCell);
    //        doc.Add(alumniTable);
    //        doc.Add(Chunk.NEWLINE);

    //        if (dsAR.Tables.Contains("Company_Details") && dsAR.Tables["Company_Details"].Rows.Count > 0)
    //        {
    //            doc.Add(new Paragraph("Company Details", fontSection) { SpacingAfter = 6f });
    //            PdfPTable companyTable = BuildFullTable(dsAR.Tables["Company_Details"], fontHeader, fontCell);
    //            doc.Add(companyTable);
    //            doc.Add(Chunk.NEWLINE);
    //        }

    //        if (dsAR.Tables.Contains("Qualifications_Details") && dsAR.Tables["Qualifications_Details"].Rows.Count > 0)
    //        {
    //            doc.Add(new Paragraph("Qualifications", fontSection) { SpacingAfter = 6f });
    //            PdfPTable qualTable = BuildFullTable(dsAR.Tables["Qualifications_Details"], fontHeader, fontCell);
    //            doc.Add(qualTable);
    //            doc.Add(Chunk.NEWLINE);
    //        }

    //        TryAddProfileImage(dsAR.Tables["Alumni_Registration_Details"], doc);

    //        doc.Close();

    //        Response.Clear();
    //        Response.ContentType = "application/pdf";
    //        Response.AddHeader("Content-Disposition", "attachment; filename=Alumni_Registration_Details_Report.pdf");
    //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        ms.Position = 0;
    //        ms.WriteTo(Response.OutputStream);
    //        Response.Flush();
    //        Response.End();
    //    }
    //    catch (System.Data.SqlClient.SqlException ex)
    //    {
    //        lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
    //    }
    //    catch (ThreadAbortException)
    //    {
    //        // Ignore
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
    //    }
    //    finally
    //    {
    //        if (doc != null)
    //        {
    //            if (doc.IsOpen())
    //                doc.Close();

    //            doc.Dispose();   // if doc implements IDisposable
    //        }

    //        ms?.Dispose();
    //    }
    //}
    protected void ViewReportITextSharp()
    {
        lblMsg.Text = "";
        MemoryStream ms = null;
        Document doc = null;

        try
        {
            int alumniId = 0;

            var alumniSession = Session["AlumniID"];
            if (alumniSession == null || string.IsNullOrWhiteSpace(alumniSession.ToString()))
                alumniId = Convert.ToInt32(Session["EmpView_AlumniID"]);
            else
                alumniId = Convert.ToInt32(alumniSession);

            DataSet dsAR = ALM_SP_AlumniRegistrationDetails_Print_Report(alumniId).GetDataSet();

            if (dsAR == null || dsAR.Tables.Count == 0 || dsAR.Tables[0].Rows.Count == 0)
            {
                lblMsg.Text = "No Records Found!";
                return;
            }

            dsAR.Tables[0].TableName = "Alumni_Registration_Details";
            if (dsAR.Tables.Count > 1) dsAR.Tables[1].TableName = "Company_Details";
            if (dsAR.Tables.Count > 2) dsAR.Tables[2].TableName = "Qualifications_Details";

            ms = new MemoryStream();
            doc = new Document(PageSize.A4, 32f, 32f, 40f, 40f);

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            writer.CloseStream = false;

            doc.AddAuthor("Alumni Portal");
            doc.AddTitle("Alumni Registration Details Report");
            doc.AddCreationDate();
            doc.Open();

            // Font Colors using iTextSharp.text.Color (compatible with 4.x/5.x)
            var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, Color.BLACK);
            var fontSection = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Color.BLACK);
            var fontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, Color.WHITE);
            var fontCell = FontFactory.GetFont(FontFactory.HELVETICA, 9, Color.BLACK);

            Paragraph title = new Paragraph("Alumni Registration Details Report", fontTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 10f
            };
            doc.Add(title);

            Paragraph generated = new Paragraph(
                "Generated On: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                fontCell
            )
            {
                Alignment = Element.ALIGN_RIGHT,
                SpacingAfter = 10f
            };
            doc.Add(generated);

            // Alumni Table
            doc.Add(new Paragraph("Alumni Details", fontSection) { SpacingAfter = 6f });
            PdfPTable alumniTable = BuildKeyValueTable(dsAR.Tables["Alumni_Registration_Details"], fontHeader, fontCell);
            doc.Add(alumniTable);
            doc.Add(Chunk.NEWLINE);

            // Company Details
            if (dsAR.Tables.Contains("Company_Details") && dsAR.Tables["Company_Details"].Rows.Count > 0)
            {
                doc.Add(new Paragraph("Company Details", fontSection) { SpacingAfter = 6f });
                PdfPTable companyTable = BuildFullTable(dsAR.Tables["Company_Details"], fontHeader, fontCell);
                doc.Add(companyTable);
                doc.Add(Chunk.NEWLINE);
            }

            // Qualifications
            if (dsAR.Tables.Contains("Qualifications_Details") && dsAR.Tables["Qualifications_Details"].Rows.Count > 0)
            {
                doc.Add(new Paragraph("Qualifications", fontSection) { SpacingAfter = 6f });
                PdfPTable qualTable = BuildFullTable(dsAR.Tables["Qualifications_Details"], fontHeader, fontCell);
                doc.Add(qualTable);
                doc.Add(Chunk.NEWLINE);
            }

            // Profile Image
            TryAddProfileImage(dsAR.Tables["Alumni_Registration_Details"], doc);

            doc.Close(); // Close PDF before writing to Response

            // Write PDF to browser
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Alumni_Registration_Details_Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            ms.Position = 0;
            ms.WriteTo(Response.OutputStream);
            Response.Flush();

            HttpContext.Current.ApplicationInstance.CompleteRequest(); // safer than Response.End()
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
        catch (Exception ex)
        {
            lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
        finally
        {
            // Safe cleanup: close Document if open, dispose MemoryStream
            try
            {
                if (doc != null && doc.IsOpen())
                    doc.Close();
            }
            catch { }

            ms.Dispose();
        }
    }

    // BuildKeyValueTable patch (BaseColor fully qualified)
    private PdfPTable BuildKeyValueTable(DataTable dt, Font fontHeader, Font fontCell)
    {
        PdfPTable table = new PdfPTable(2) { WidthPercentage = 100f };
        table.SetWidths(new float[] { 30f, 70f });

        if (dt.Rows.Count == 0) return table;
        DataRow row = dt.Rows[0];

        foreach (DataColumn col in dt.Columns)
        {
            if (IsSkippable(col.ColumnName)) continue;
            PdfPCell header = new PdfPCell(new Phrase(NormalizeHeader(col.ColumnName), fontHeader))
            {
                BackgroundColor = new iTextSharp.text.Color(60, 120, 180),
                Padding = 4f
            };
            PdfPCell value = new PdfPCell(new Phrase(Convert.ToString(row[col]) ?? "", fontCell))
            {
                Padding = 4f
            };
            table.AddCell(header);
            table.AddCell(value);
        }
        return table;
    }

    // BuildFullTable patch (BaseColor fully qualified)
    private PdfPTable BuildFullTable(DataTable dt, Font fontHeader, Font fontCell)
    {
        PdfPTable table = new PdfPTable(dt.Columns.Count) { WidthPercentage = 100f };
        float[] widths = Enumerable.Repeat(1f, dt.Columns.Count).ToArray();
        table.SetWidths(widths);

        foreach (DataColumn col in dt.Columns)
        {
            PdfPCell header = new PdfPCell(new Phrase(NormalizeHeader(col.ColumnName), fontHeader))
            {
                BackgroundColor = new iTextSharp.text.Color(60, 120, 180),
                Padding = 4f,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            table.AddCell(header);
        }

        foreach (DataRow dr in dt.Rows)
        {
            foreach (DataColumn col in dt.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(Convert.ToString(dr[col]) ?? "", fontCell))
                {
                    Padding = 4f
                };
                table.AddCell(cell);
            }
        }
        return table;
    }

    private bool IsSkippable(string columnName)
    {
        // Adjust if there are internal columns you don't want in key/value section
        string[] skip =
        {
            "password", "File_Path", "Files_Unique_Name", "Fk_alumniid"
        };
        return skip.Contains(columnName, StringComparer.OrdinalIgnoreCase);
    }

    private string NormalizeHeader(string raw)
    {
        // Simple header normalization (e.g., alumni_name -> Alumni Name)
        if (string.IsNullOrEmpty(raw)) return "";
        string[] parts = raw.Replace("_", " ").Split(' ');
        return string.Join(" ", parts.Select(p =>
        {
            if (p.Length == 0) return p;
            if (p.Length == 1) return p.ToUpperInvariant();
            return char.ToUpperInvariant(p[0]) + p.Substring(1).ToLowerInvariant();
        }));
    }

    private void TryAddProfileImage(DataTable alumniTable, Document doc)
    {
        try
        {
            if (alumniTable == null || alumniTable.Rows.Count == 0) return;

            // Expect column name like profile image unique file name; adjust if different
            string[] candidateCols = { "ProfileImage", "Files_Unique_Name", "Profile_Pic", "Photo" };
            string colName = candidateCols.FirstOrDefault(c => alumniTable.Columns.Contains(c));
            if (colName == null) return;

            string fileUnique = Convert.ToString(alumniTable.Rows[0][colName]);
            if (string.IsNullOrWhiteSpace(fileUnique)) return;

            string baseUrl = ReturnPath(); // Existing helper
            string imgUrl = baseUrl + "/Alumni/StuImage/" + fileUnique;

            // Download into memory if reachable
            using (var client = new System.Net.WebClient())
            {
                byte[] imgBytes = client.DownloadData(imgUrl);
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgBytes);
                img.Alignment = Element.ALIGN_RIGHT;
                img.ScaleToFit(120f, 120f);
                img.SpacingBefore = 10f;
                img.SpacingAfter = 10f;
                doc.Add(img);
            }
        }
        catch
        {
            // Ignore image failures silently
        }
    }

    // Optional new button handler
    protected void btnPrintITextSharp_Click(object sender, EventArgs e)
    {
        ViewReportITextSharp();
    }
    #endregion

    protected void BtnCopilot_Click(object sender, EventArgs e)
    {
        ViewReportITextSharp();
    }
}