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
using System.IO;
using DataAccessLayer;
using System.Drawing;
using SubSonic;
using Anthem;
using System.Linq;
using System.Collections.Generic;

public partial class Alumni_ALM_Chapterwise_Alumnis : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    #region "Common Objects"

    DataAccess Dobj = new DataAccess();
    CommonFunction cfobj = new CommonFunction();
    DataAccess DAobj = new DataAccess();

    ArrayList names = new ArrayList();
    ArrayList types = new ArrayList();
    ArrayList values = new ArrayList();
    ArrayList size = new ArrayList();
    ArrayList outtype = new ArrayList();

    crypto cpt = new crypto();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear(); size.Clear(); outtype.Clear();
    }

    #endregion

    public static StoredProcedure ALM_SP_AlumniRegistration_Edit(int? pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_Edit_search", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public DataSet ALM_ACD_Degree_Mst_sel()
    {
        ClearArrayLists();
        return DAobj.GetDataSet("ALM_ACD_Degree_Mst_sel", values, names, types);
    }

    public static StoredProcedure SMS_SP_College_Mst_SelForDDL1()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("SMS_SP_College_Mst_SelForDDL_New", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public static StoredProcedure ALM_SP_Alumni_Search(string alumni_name, string year, string gender, string occupation, string fk_degreeid, string fk_collegeid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_Alumni_Search_New", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@alumni_name", alumni_name, DbType.String);
        sp.Command.AddParameter("@year", year, DbType.String);
        sp.Command.AddParameter("@gender", gender, DbType.String);
        sp.Command.AddParameter("@occupation", @occupation, DbType.String);
        sp.Command.AddParameter("@fk_degreeid", fk_degreeid, DbType.String);
        sp.Command.AddParameter("@fk_collegeid", fk_collegeid, DbType.String);
        return sp;
    }

    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() != "" && Session["AlumniID"] != null)
        {
            if (!IsPostBack)
            {
                int AlumniID = Convert.ToInt32(Session["AlumniID"].ToString());
                clear();
                BindPassingYearsDDL();
                BindPassingYears();
                ProfileRepeter(AlumniID);
                BindDropdown();

                if (Request.QueryString["ID"] != null)
                {
                    int ChapterID = Convert.ToInt32(cpt.DecodeString(Request.QueryString["ID"].ToString()));

                    if (ChapterID != 0)
                    {
                        ProfileRepeter(ChapterID);
                    }
                }
            }
        }
        else
        {
            ProfileRepeter();
            CountRepeter();
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }
    private void ProfileRepeter(int ChapterID)
    {
        try
        {
            DataSet dsS = GetMemberProfile(ChapterID);
            dsS.Tables[0].Columns.Add("encId");

            if (dsS.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < dsS.Tables[0].Rows.Count; x++)
                {
                    string pkid = dsS.Tables[0].Rows[x]["pk_alumniid"].ToString();
                    string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                    dsS.Tables[0].Rows[x]["encId"] = encId;
                }
                RepProfile.DataSource = dsS.Tables[0];
                RepProfile.DataBind();
                lblProfileCnt.Text = dsS.Tables[0].Rows.Count.ToString() + " Profile Records";
            }
        }
        catch (Exception ex)
        {
            //throw;
            ClientMessaging(ex.Message);
        }
    }

    public static StoredProcedure ALM_Chapters_Mst_CRUD_Operations(string xmldoc, int mode, int ChapterID, string actionBy)
    {
        SubSonic.StoredProcedure sp = new StoredProcedure("ALM_Chapters_Mst_CRUD", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@xmlDoc", xmldoc, DbType.String);
        sp.Command.AddParameter("@mode", mode, DbType.Int32);
        sp.Command.AddParameter("@ChapterID", ChapterID, DbType.Int32);
        sp.Command.AddParameter("@ActionBy", actionBy, DbType.String);
        return sp;
    }

    private void ProfileRepeter()
    {
        DataTable dt = new DataTable();
        DataSet ds = ALM_SP_AlumniRegistration_withoutLoginAuthentication().GetDataSet();
        if (ds.Tables[0].Rows[0]["cnt"].ToString() != "0")
        {
            string type = "P";
            Session["AlumniID"] = ds.Tables[1].Rows[0]["pk_alumniid"].ToString();
            Session["Alumnino"] = ds.Tables[1].Rows[0]["alumnino"].ToString();
            Session["AlumniName"] = ds.Tables[1].Rows[0]["alumni_name"].ToString();
            Session["Alumnitype"] = type.ToString();
            string Query = "";
            string a = "";
            if (Session["pk_alumniid"] != null)
            {
                a = Session["pk_alumniid"].ToString();
            }
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            RepProfile.DataSource = ds.Tables[1];
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[1].Rows.Count.ToString() + " Profile Records";
        }
    }

    //private void ProfileRepeter()
    //{
    //    //DataTable dt = new DataTable();
    //    //dt = Getmemberprofile();
    //    //if (dt.Rows.Count > 0)
    //    //{
    //    //    // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
    //    //    // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
    //    //    RepProfile.DataSource = dt;
    //    //    RepProfile.DataBind();
    //        DataSet ds = ALM_SP_AlumniRegistration_withoutLoginAuthentication().GetDataSet();
    //        if (ds.Tables[0].Rows[0]["cnt"].ToString() != "0")
    //        {
    //            string type = "P";
    //            Session["AlumniID"] = ds.Tables[1].Rows[0]["pk_alumniid"].ToString();
    //            Session["Alumnino"] = ds.Tables[1].Rows[0]["alumnino"].ToString();
    //            Session["AlumniName"] = ds.Tables[1].Rows[0]["alumni_name"].ToString();
    //            Session["Alumnitype"] = type.ToString();
    //            //a = a.Replace("+", "%2B");
    //            //b = b.Replace("+", "%2B");
    //            string Query = "";
    //            string a = "";
    //            if (Session["pk_alumniid"] != null)
    //            {
    //                a = Session["pk_alumniid"].ToString();
    //            }

    //        //    if (!string.IsNullOrEmpty(a))
    //        //    {

    //        //        Query = "~//Alumni//Alm_Alumni_Show_Alumni_Profile.aspx?Alumni=" + a.ToString();
    //        //    }
    //        //    else
    //        //    {
    //        //     Query = "~//Alumni//ALM_AlumniSearch.aspx";
    //        //}
    //        //Response.Redirect(Query);
    //        //     Response.Redirect("ALM_AlumniSearch.aspx");
    //        if (ds.Tables[1].Rows.Count > 1)
    //        {
    //            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
    //        }

    //        RepProfile.DataSource = ds.Tables[1];
    //        RepProfile.DataBind();
    //    }
    //    else
    //        {
    //            Manager.IncludePageScripts = true;
    //            ClientMessaging("Invalid User Name or Password! Please contact your college for approval");
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "id", "ShowRoomDetails()", true);
    //            Session["AlumniID"] = "";
    //            Session["Alumnino"] = "";
    //            Session["AlumniName"] = "";
    //            Session["Emailid"] = "";
    //        }
    //        //catch (Exception ex)
    //        //{
    //        //R_txtLogin.Text = "";
    //        //R_txtPass.Text = "";
    //        //R_txtLogin.Focus();
    //        //}
    //    }
    //}

    private void CountRepeter()
    {
        DataTable dtT = new DataTable();
        dtT = Getmtotalcount();
        if (dtT.Rows.Count > 0)
        {
            //RepCount.DataSource = dtT;
            //RepCount.DataBind();
            //lblProfileCnt.Text = dtT.Rows.Count.ToString() + "All Profiles";
            lblProfileCnt.Text = dtT.Rows.Count.ToString() + " Profile Records";
        }
        else
        {
            lblProfileCnt.Text = "All Profiles";
        }
    }

    private DataSet Get_SelectionCriteria()
    {
        ClearArrayLists();
        // return Dobj.GetDataTable("ALM_GetAlumniprofile", values, names, types);
        return Dobj.GetDataSet("Search_AlumniCriteria", values, names, types);
    }

    private DataSet GetMemberProfile(int chapterID)
    {
        ClearArrayLists();
        names.Add("@ChapterID"); types.Add(SqlDbType.NVarChar); values.Add(chapterID);
        return Dobj.GetDataSet("ALM_GetProfile_By_ChapterID", values, names, types);
    }

    private DataTable Getmtotalcount()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GettotalAlumni_Count", values, names, types);
    }

    public static StoredProcedure ALM_SP_AlumniRegistration_withoutLoginAuthentication()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_SP_AlumniRegistration_withoutLoginAuthentication", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    //protected void FillDegrees()
    //{
    //    ddlDegree.Items.Clear();
    //    IDataReader rdr = IUMSNXG.SP.ALM_SP_Degree_Mst_SelAll().GetReader();
    //    DataTable dt = new DataTable();
    //    dt.Load(rdr);
    //    ddlDegree.DataSource = dt;
    //    ddlDegree.DataTextField = "degreename";
    //    ddlDegree.DataValueField = "pk_degreeid";
    //    ddlDegree.DataBind();
    //    ddlDegree.Items.Insert(0, "-- Select Degree -- ");
    //    rdr.Close(); rdr.Dispose();
    //}

    #endregion

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        // Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }

    #region  

    protected void FillGrid()
    {
        try
        {
            // lblMsg.Text = "";
            //if (txtAlumniName.Text == "")
            //{
            //    lblMsg.Text = "Alumni Name Blank";
            //    txtAlumniName.Focus();
            //    return;
            //    txtAlumniName.Focus();
            //}
            // gvAlumni.SelectedIndex = -1;
            string degreeid = "", collegeid = "", gender = "";

            //if (ddlDegree.SelectedIndex != 0)
            //    degreeid = ddlDegree.SelectedValue;

            //if (ddlcollege.SelectedIndex != 0)
            //    collegeid = ddlcollege.SelectedValue;

            //if (rdbmale.Checked == true)
            //{
            //    gender = "M";
            //}
            //else if (rdbfemale.Checked == true)
            //{
            //    gender = "F";
            //}
            //DataSet ds = ALM_SP_Alumni_Search(txtAlumniName.Text.Trim(), txtyearofpassing.Text.Trim(), gender, txtCurrentOccupation.Text.Trim(), degreeid, collegeid).GetDataSet();
            ////DataSet ds = IUMSNXG.SP.ALM_SP_Alumni_Search(txtAlumniName.Text.Trim(), txtyearofpassing.Text.Trim(), gender, txtCurrentOccupation.Text.Trim(), degreeid, collegeid).GetDataSet();

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    gvAlumni.DataSource = ds;
            //    gvAlumni.DataBind();
            //}
            //else
            //{
            //    gvAlumni.DataSource = null;
            //    gvAlumni.DataBind();
            //    ClientMessaging("No Record Found!");
            //    lblMsg.Text = "No Record Found!";
            //}
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message;
        }
    }

    #endregion

    #region "Button Search, Clear and Reset Event"

    void clear()
    {
        cfobj.ClearTextSetDropDown(this);
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
        // lblMsg.Text = "";
    }

    #endregion

    #region "Bind Image of Alumni"

    public void setimageonedit(byte[] fileBytes, string contenttype, string filename)
    {
        #region "Code Commented By AKS"

        //string filepath = "";
        //Response.ContentType = contenttype;
        //Response.BinaryWrite(fileBytes);
        //filepath = HttpContext.Current.Server.MapPath("~/Alumni/StuImage/" + filename.ToString().Trim());
        //FileStream fstream = new FileStream(filepath, FileMode.Create);
        //byte[] bt = fileBytes;
        //fstream.Write(bt, 0, bt.Length);
        //fstream.Close();

        //string host = HttpContext.Current.Request.Url.Host;
        //string upldPath = "";
        //DataSet dsFilepath = new DataSet();
        //dsFilepath.ReadXml(HttpContext.Current.Server.MapPath("~/IO_Config.xml"));
        //foreach (DataRow dr in dsFilepath.Tables[0].Rows)
        //{
        //    if (host == dr["Server_Ip"].ToString().Trim())
        //    {
        //        upldPath = dr["Physical_Path"].ToString().Trim();
        //        upldPath = upldPath + "\\" + "Alumni\\StuImage" + "\\" + filename.ToString().Trim();
        //        //filepath = upldPath;
        //    }
        //}
        //stuimage.ImageUrl = upldPath;// "~/Alumni/StuImage/" + filename;
        //hdPath.Text = upldPath;

        #endregion

        string base64String = Convert.ToBase64String(fileBytes, 0, fileBytes.Length);
        //img_mast_user.Src = null;
        //img_mast_user.Src = "data:image/" + contenttype + ";base64," + base64String;
    }

    #endregion

    #region "GridView Row Command and Page change events"

    protected void gvAlumni_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Panel1.Visible = true;
        //lblMsg.Text = "";

        if (e.CommandName.ToUpper().ToString() == "SELECT")
        {
            //try
            //{
            //    ViewState["id"] = e.CommandArgument.ToString().Trim();
            // btnRegister.Text = "UPDATE";
            //btnRegister.CommandName = "UPDATE";
            //IDataReader rdr = IUMSNXG.SP.ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"])).GetReader();
            DataSet ds = ALM_SP_AlumniRegistration_Edit(Convert.ToInt32(ViewState["id"])).GetDataSet();
            // DataTable dt = new DataTable();
            //dt = ds.Tables[0].Rows[0].
            //dt.Load(ds.Tables[0].Rows[0]);
            // rdr.Close(); rdr.Dispose();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    lblAlumniname.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();
            //    if (ds.Tables[0].Rows[0]["gender"].ToString() == "M")
            //    {
            //        lblMF.Text = "Male";

            //    }
            //    else if (ds.Tables[0].Rows[0]["gender"].ToString() == "F")
            //    {
            //        lblMF.Text = "Female";
            //    }
            //    //lblYOP.Text = ds.Tables[0].Rows[0]["yearofpassing"].ToString();
            //    //lblcollege.Text = ds.Tables[0].Rows[0]["collegename"].ToString();
            //    //lblDegree.Text = ds.Tables[0].Rows[0]["degreename"].ToString();
            //    //lblCurrentAdd.Text = ds.Tables[0].Rows[0]["currentaddress"].ToString();
            //    //lblContactno.Text = ds.Tables[0].Rows[0]["contactno"].ToString();
            //    //lblEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
            //    //lblCurrentOccupation.Text = ds.Tables[0].Rows[0]["currentoccupation"].ToString();
            //    //R_txtLoginName.Text = dt.Rows[0]["loginname"].ToString();
            //    //R_txtPassword.Text = dt.Rows[0]["password"].ToString();

            //    string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
            //    fileName = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString().Trim();

            //    if (fileName != "")
            //    {
            //        fileName = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString().Trim();
            //        if (ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString() != "None")
            //        {
            //            img_mast_user.Src = ds.Tables[1].Rows[0]["Files_Unique_Name"].ToString();
            //        }
            //    }
            //    else
            //    {
            //        img_mast_user.Src = "~/alumni/stuimage/No_image.png";
            //        hdPath.Text = "";
            //    }
            // string fileName = ""; string contenttype = ""; byte[] fileBytes = null;
            // }
            //}
            //catch (Exception ex)
            //{
            //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
            //}
        }
    }

    //protected void gvAlumni_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvAlumni.PageIndex = e.NewPageIndex;
    //    FillGrid();
    //    Panel1.Visible = false;       
    //    lblMsg.Text = "";
    //}

    #endregion

    //protected void btnname_Click(object sender, EventArgs e)
    //{
    //    Anthem.LinkButton button = (sender as Anthem.LinkButton);
    //    //Get the command argument
    //    string commandArgument = button.CommandArgument;
    //    //Get the Repeater Item reference
    //    RepeaterItem item = button.NamingContainer as RepeaterItem;
    //    //Get the repeater item index
    //    int index = item.ItemIndex;
    //   // Session["pk_alumniid"] = commandArgument;
    //}  

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
        //gvAlumni.PageIndex = 0;
        // Panel1.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int Mode = 1;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(txtsearch.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count.ToString() + " Profile Records";
            clear();
        }
    }

    protected void brnsearchpassing_Click(object sender, EventArgs e)
    {
        int Mode = 2;
        //DataSet ds = ALM_SP_Alumni_GlobalSearch(D_ddlPassingYear.Text.Trim(), Mode).GetDataSet();
        //if (ds.Tables.Count > 0)
        //{
        //    // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\Alumni\\StuImage\\");
        //    // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\StuImage\\");
        //    RepProfile.DataSource = ds;
        //    RepProfile.DataBind();
        //    clear();
        //}

        //var selectedItems = chkPassingYear.Items.Cast<ListItem>()
        //                              .Where(i => i.Selected)
        //                              .Select(i => i.Value)
        //                              .ToList();

        //if (selectedItems.Count > 0)
        //{
        //    string selectedLocations = string.Join(",", selectedItems);

        //    DataSet ds = GetAll_ProfileRepeter_By_PassingYears(selectedLocations, Mode).GetDataSet();

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        RepProfile.DataSource = ds.Tables[0];
        //        RepProfile.DataBind();
        //        lblProfileCnt.Text = ds.Tables[0].Rows.Count.ToString() + " Profile Records";
        //    }
        //    else
        //    {
        //        RepProfile.DataSource = null;
        //        RepProfile.DataBind();
        //        lblProfileCnt.Text = "No Profile Records";
        //    }
        //}

        // Get selected passing years from CheckBoxList
        var selectedYears = chkPassingYear.Items.Cast<ListItem>()
                             .Where(i => i.Selected)
                             .Select(i => i.Value)
                             .ToList();

        // If no years are selected, return
        if (selectedYears.Count == 0)
        {
            lblProfileCnt.Text = "No records found.";
            return;
        }

        // Convert list to comma-separated string
        string passingYears = string.Join(",", selectedYears);

        // Call procedure with selected passing years
        DataSet ds = GetAll_ProfileRepeter_By_PassingYears(passingYears, Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds.Tables[0];
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
        }
        else
        {
            RepProfile.DataSource = null;
            RepProfile.DataBind();
            lblProfileCnt.Text = "No Profile Records Found";
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    public static StoredProcedure ALM_SP_Alumni_GlobalSearch(string Keyword, int Mode)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Search_ChapterAlumniby_Keywords", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Keyword", Keyword, DbType.String);
        sp.Command.AddParameter("@Mode", Mode, DbType.Int16);
        return sp;
    }


    protected void btndegreesearch_Click(object sender, EventArgs e)
    {
        int Mode = 3;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(D_DrpDegree.SelectedItem.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void Btndeptsearch_Click(object sender, EventArgs e)
    {
        int Mode = 4;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_Dept.SelectedValue.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void btnaddress_Click(object sender, EventArgs e)
    {
        int Mode = 5;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_DAddress.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void btncomp_Click(object sender, EventArgs e)
    {
        int Mode = 6;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_Comp.SelectedItem.Text.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void btndesig_Click(object sender, EventArgs e)
    {
        int Mode = 7;
        Anthem.Manager.IncludePageScripts = true;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_Desig.SelectedItem.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    protected void Btnskill_Click(object sender, EventArgs e)
    {
        int Mode = 8;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(Drp_Skills.SelectedItem.Text.Trim(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
    }

    private void BindDropdown()
    {
        DataSet ds = new DataSet();
        ds = Get_SelectionCriteria();
        if (ds.Tables[0].Rows.Count > 0)
        {

            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                //D_ddlPassingYear.DataSource = ds.Tables[1];
                //D_ddlPassingYear.DataTextField = "PassYear_Name";
                //D_ddlPassingYear.DataValueField = "Pk_pass_id";
                //D_ddlPassingYear.DataBind();
                //D_ddlPassingYear.Items.Insert(0, "-- Select Passing Year -- ");
            }
            else
            {
                //D_ddlPassingYear.Items.Insert(0, "-- Select Passing Year -- ");
            }

            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                Drp_Dept.DataSource = ds.Tables[2];
                Drp_Dept.DataTextField = "description";
                Drp_Dept.DataValueField = "description";
                Drp_Dept.DataBind();
                Drp_Dept.Items.Insert(0, new ListItem("-- Department --", "0"));
            }
            else
            {
                Drp_Dept.Items.Insert(0, new ListItem("-- Department --", "0"));
            }

            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                Drp_DAddress.DataSource = ds.Tables[3];
                Drp_DAddress.DataTextField = "Current_location";
                Drp_DAddress.DataValueField = "Current_location";
                Drp_DAddress.DataBind();
                Drp_DAddress.Items.Insert(0, new ListItem("-- Location --", "0"));
            }
            else
            {
                Drp_DAddress.Items.Insert(0, new ListItem("-- Location --", "0"));
            }

            if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
            {
                Drp_Desig.DataSource = ds.Tables[4];
                Drp_Desig.DataTextField = "designation";
                Drp_Desig.DataValueField = "designation";
                Drp_Desig.DataBind();
                Drp_Desig.Items.Insert(0, new ListItem("-- Designation --", "0"));
            }
            else
            {
                Drp_Desig.Items.Insert(0, new ListItem("-- Designation --", "0"));
            }

            if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
            {
                Drp_Skills.DataSource = ds.Tables[5];
                Drp_Skills.DataTextField = "special_interest";
                Drp_Skills.DataValueField = "special_interest";
                Drp_Skills.DataBind();
                Drp_Skills.Items.Insert(0, new ListItem("-- Skill --", "0"));
            }
            else
            {
                Drp_Skills.Items.Insert(0, new ListItem("-- Skill --", "0"));
            }


            if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
            {
                D_DrpDegree.DataSource = ds.Tables[6];
                D_DrpDegree.DataTextField = "description";
                D_DrpDegree.DataValueField = "pk_degreeid";
                D_DrpDegree.DataBind();
                D_DrpDegree.Items.Insert(0, new ListItem("-- Degree --", "0"));
            }
            else
            {
                D_DrpDegree.Items.Insert(0, new ListItem("-- Degree --", "0"));
            }

            if (ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
            {
                Drp_Comp.DataSource = ds.Tables[7];
                Drp_Comp.DataTextField = "currentoccupation";
                Drp_Comp.DataValueField = "currentoccupation";
                Drp_Comp.DataBind();
                Drp_Comp.Items.Insert(0, new ListItem("-- Company --", "0"));
            }
            else
            {
                Drp_Comp.Items.Insert(0, new ListItem("-- Company --", "0"));
            }
        }
    }

    //private void BindPassingYears()
    //{
    //    try
    //    {
    //        DataSet ds = GetAll_Alumni_PassingYears().GetDataSet();
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            chkPassingYear.DataSource = ds.Tables[0];
    //            chkPassingYear.DataTextField = "PassYear_Name";
    //            chkPassingYear.DataValueField = "PK_Pass_ID";
    //            chkPassingYear.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientMessaging(ex.Message);
    //    }
    //}

    private void BindPassingYears()
    {
        // Predefined degree list
        List<string> passYears = new List<string> { "1992", "2010", "2014", "2015" };

        // Bind CheckBoxList
        chkPassingYear.DataSource = passYears;
        chkPassingYear.DataBind();
    }

    private void BindPassingYearsDDL()
    {
        try
        {
            DataSet ds = GetAll_Alumni_YearsOfPassing().GetDataSet();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlYearOfPassing.DataSource = ds.Tables[0];
                ddlYearOfPassing.DataTextField = "PassYear_Name";
                ddlYearOfPassing.DataValueField = "PK_Pass_ID";
                ddlYearOfPassing.DataBind();
                ddlYearOfPassing.Items.Insert(0, new ListItem("-- Passing Year --", "0"));
            }
            Anthem.Manager.IncludePageScripts = true;
        }
        catch (Exception ex)
        {
            ClientMessaging(ex.Message);
        }
    }

    public static StoredProcedure GetAll_Alumni_PassingYears()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_GetAll_Alumni_PassingYear", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public static StoredProcedure GetAll_Alumni_YearsOfPassing()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_GetAll_Alumni_PassingYears", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    public static StoredProcedure GetAll_ProfileRepeter_By_PassingYears(string passingYears, int mode)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("Alm_SearchAlumniby_Keywords", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@Keyword", passingYears, DbType.String);
        sp.Command.AddParameter("@mode", mode, DbType.Int32);
        return sp;
    }

    protected void btnSearchPassing_Click(object sender, EventArgs e)
    {

    }

    protected void ddlYearOfPassing_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string selectedYearOfPassing = ddlYearOfPassing.SelectedItem.Text.Trim().ToString();

        //if (!string.IsNullOrEmpty(selectedYearOfPassing))
        //{
        //    foreach (ListItem item in chkPassingYear.Items)
        //    {
        //        if (item.Value == selectedYearOfPassing)
        //        {
        //            item.Selected = true;
        //        }
        //        else
        //        {
        //            item.Selected = false;
        //        }
        //    }
        //}
        //Anthem.Manager.IncludePageScripts = true;

        string selectedYearOfPassing = ddlYearOfPassing.SelectedItem.Text.Trim().ToString();

        if (!string.IsNullOrEmpty(selectedYearOfPassing))
        {
            bool exists = false;

            foreach (ListItem item in chkPassingYear.Items)
            {
                if (item.Value == selectedYearOfPassing)
                {
                    item.Selected = true;
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                chkPassingYear.Items.Add(new ListItem(selectedYearOfPassing, selectedYearOfPassing) { Selected = true });
            }
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btnRoleSearch_Click(object sender, EventArgs e)
    {
        int Mode = 12;
        Anthem.Manager.IncludePageScripts = true;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(ddlRole.SelectedValue.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btnMemSearch_Click(object sender, EventArgs e)
    {
        int Mode = 13;
        Anthem.Manager.IncludePageScripts = true;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(ddlMembership.SelectedValue.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void btnGenderSearch_Click(object sender, EventArgs e)
    {
        int Mode = 14;
        Anthem.Manager.IncludePageScripts = true;
        DataSet ds = ALM_SP_Alumni_GlobalSearch(ddlGender.SelectedValue.Trim().ToString(), Mode).GetDataSet();
        //ds.Tables[0].Columns.Add("base64Image");

        if (ds.Tables.Count > 0)
        {
            //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            //{
            //    string imgPath = ds.Tables[0].Rows[x]["Filepath"].ToString();
            //    string base64Image = GetBase64Image(imgPath);
            //    ds.Tables[0].Rows[x]["base64Image"] = base64Image;
            //}
            RepProfile.DataSource = ds;
            RepProfile.DataBind();
            lblProfileCnt.Text = ds.Tables[0].Rows.Count + " Profile Records Found";
            clear();
        }
        Anthem.Manager.IncludePageScripts = true;
    }

    //public string GetBase64Image(string fPath, string fExtension)
    //{
    //    byte[] imageBytes = System.IO.File.ReadAllBytes(fPath);
    //    string base64String = Convert.ToBase64String(imageBytes);
    //    string mimeType = "";
    //    if (fExtension.Contains("jpg"))
    //    {
    //        mimeType = "image/jpg";
    //    }
    //    else if (fExtension.Contains("jpeg"))
    //    {
    //        mimeType = "image/jpeg";
    //    }
    //    else if (fExtension.Contains("png"))
    //    {
    //        mimeType = "image/png";
    //    }
    //    else if (fExtension.Contains("pdf"))
    //    {
    //        mimeType = "application/pdf";
    //    }
    //    return string.Format("data:{0}; base64, {1}", mimeType, base64String);
    //}

    public string GetBase64Image(string imagePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        string mimeType = System.Web.MimeMapping.GetMimeMapping(imagePath);
        return string.Format("data:{0}; base64, {1}", mimeType, base64String);
    }
}