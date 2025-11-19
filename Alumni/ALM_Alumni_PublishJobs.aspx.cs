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
using System.Web.Hosting;
using System.Linq;
using System.Data.SqlClient;

public partial class Alumni_ALM_Alumni_PublishJobs : System.Web.UI.Page
{
    public DateTime startDate { get; set; }
    public DateTime EndDate { get; set; }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    #region "Common Object Declation"

    DataAccess Dobj = new DataAccess();
    CommonFunction cfobj = new CommonFunction();
    CustomMessaging eobj = new CustomMessaging();

    // crypto crp = new crypto();

    #endregion

    private bool IspageReferesh = false;

    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postid"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postid"].ToString();
            if (Session["AlumniId"] != null)
                Clear();
        }
        else
        {
            if (ViewState["postid"].ToString() != Session["postid"].ToString())
            {
                IspageReferesh = true;
            }
            Session["postid"] = System.Guid.NewGuid().ToString();
            ViewState["postid"] = Session["postid"];
        }
    }

    #endregion

    #region "Button Update and Reset Work"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IspageReferesh)
            {
                if (btnSave.CommandName == "SAVE")
                {
                    Save();
                }
                else
                {
                    Update();
                }
            }
        }
        catch (SqlException exp)
        {
            lblMsg.Text = eobj.ShowSQLErrorMsg(exp.Message.ToString(), "", exp);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    private void Update()
    {
        if (txt_JobVacncyUrl.Text.Trim() != "")
        {
            Uri uriResult;
            bool result = Uri.TryCreate(txt_JobVacncyUrl.Text, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!result)
            {
                Client_Messaging("Invalid Job Vacancy URL !");
                txt_JobVacncyUrl.Focus();
                return;
            }
        }

        DateTime startDate = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txtStartDate.Text.Trim()));
        DateTime EndDate = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txtEndDate.Text.Trim()));

        if (startDate > EndDate)
        {
            Client_Messaging("Opening From Date Can not be greated than End Date.");
            Anthem.Manager.IncludePageScripts = true;
            V_txtStartDate.Focus();
            return;
        }
		
        DataSet ds = Dobj.GetSchema("ALM_Alumni_Posted_job");
        DataRow dr = ds.Tables[0].NewRow();
        dr["fk_AlumniId"] = Convert.ToInt32(Session["AlumniId"].ToString());
        dr["CompanyName"] = R_txtCompanyName.Text.Trim();
        dr["Designation"] = R_txtDesignation.Text.Trim();
        dr["fk_JobCId"] = D_ddlJobCat.SelectedValue;
        dr["VacancyDtl"] = R_txtVacancyDtl.Text.Trim();
        dr["SkillsReq"] = R_txtSkillReq.Text.Trim();
        dr["SelectionProcess"] = txt_SelectionProc.Text.Trim();
        dr["JobVacncyUrl"] = txt_JobVacncyUrl.Text.Trim();
        dr["JobOpenFrom"] = CommonCode.DateFormats.Date_FrontToDB_O(V_txtStartDate.Text.Trim());
        dr["JobOpenTo"] = CommonCode.DateFormats.Date_FrontToDB_O(V_txtEndDate.Text.Trim());

        ds.Tables[0].Rows.Add(dr);
        string xmlDoc = ds.GetXml();
        if (ViewState["id"] != null)
        {
            if ((IUMSNXG.SP.Alm_Alumni_PublishJob_ins_sel_Upd(xmlDoc, Convert.ToInt32(ViewState["id"].ToString()), 0, "U").Execute()) > 0)
            {
                //lblMsg.Text = eobj.ShowMessage("S");
                //  Anthem.Manager.IncludePageScripts = true;
                Client_Messaging("Job Post is Updated Successfully!");

                Clear();
            }
        }
    }

    private bool validate()
    {
        if (R_txtCompanyName.Text.Trim() == "")
        {
            Client_Messaging("Company is Required.");
            R_txtCompanyName.Focus();
            return false;
        }

        if (R_txtDesignation.Text.Trim() == "")
        {
            Client_Messaging("Designation is Required.");
            R_txtDesignation.Focus();
            return false;
        }

        if (D_ddlJobCat.SelectedIndex == 0)
        {
            Client_Messaging("Job Category is Required.");
            D_ddlJobCat.Focus();
            return false;
        }

        if (R_txtVacancyDtl.Text.Trim() == "")
        {
            Client_Messaging("Vacancy Details is Required.");
            R_txtVacancyDtl.Focus();
            return false;
        }

        if (R_txtSkillReq.Text.Trim() == "")
        {
            Client_Messaging("Skill is Required.");
            R_txtSkillReq.Focus();
            return false;
        }

        if (txt_SelectionProc.Text.Trim() == "")
        {
            Client_Messaging("Selection Procedure is Required.");
            txt_SelectionProc.Focus();
            return false;
        }

        if (txt_JobVacncyUrl.Text.Trim() != "")
        {
            Uri uriResult;
            bool result = Uri.TryCreate(txt_JobVacncyUrl.Text, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!result)
            {
                Client_Messaging("Invalid Job Vacancy URL !");
                txt_JobVacncyUrl.Focus();
                return false;
            }
        }

        DateTime startDate = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txtStartDate.Text.Trim()));
        DateTime EndDate = Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_O(V_txtEndDate.Text.Trim()));

        if (startDate > EndDate)
        {
            Client_Messaging("Opening From Date Can not be greated than End Date.");
            Anthem.Manager.IncludePageScripts = true;
            V_txtStartDate.Focus();
            return false;
        }

        if (!flUploadDocs.HasFile)
        {
            Client_Messaging("Please choose the required documents.!!!");
            flUploadDocs.Focus();
            return false;
        }

        string FileType = "";
        double filesize = 0;
        string Name = "";
        DataRow drimg = null;
        string Fileuniquename = string.Empty;

        FileType = Path.GetExtension(flUploadDocs.PostedFile.FileName);

        if (FileType != null && FileType != "")
        {
            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
        && FileType != ".bmp" && FileType != ".BMP" && FileType != ".gif" && FileType != ".GIF" && FileType != ".png" && FileType != ".PNG")
            {
                Client_Messaging("Document Should be in required format!");
                lblMsg.Text = "Document Should be in required format!";
                flUploadDocs.Focus();
                return false;
            }
        }

        filesize = flUploadDocs.PostedFile.ContentLength;
        Name = flUploadDocs.PostedFile.FileName;

        if (Name.Length > 100)
        {
            Client_Messaging("Document Name Should not be more than 100 characters!");
            flUploadDocs.Focus();
            return false;
        }
        if (filesize > (2 * 1024 * 1024))
        {
            Client_Messaging("Upload documents size should not be more than 2 MB !");
            flUploadDocs.Focus();
            return false;
        }
        return true;
    }

    private void Save()
    {
        if (!validate())
        {
            return;
        }

        DataSet ds = Dobj.GetSchema("ALM_Alumni_Posted_job");
        DataRow dr = ds.Tables[0].NewRow();
        dr["fk_AlumniId"] = Convert.ToInt32(Session["AlumniId"].ToString());
        dr["CompanyName"] = R_txtCompanyName.Text.Trim();
        dr["Designation"] = R_txtDesignation.Text.Trim();
        dr["fk_JobCId"] = D_ddlJobCat.SelectedValue;
        dr["VacancyDtl"] = R_txtVacancyDtl.Text.Trim();
        dr["SkillsReq"] = R_txtSkillReq.Text.Trim();
        dr["SelectionProcess"] = txt_SelectionProc.Text.Trim();
        dr["JobVacncyUrl"] = txt_JobVacncyUrl.Text.Trim();
        dr["JobOpenFrom"] = CommonCode.DateFormats.Date_FrontToDB_O(V_txtStartDate.Text.Trim());
        dr["JobOpenTo"] = CommonCode.DateFormats.Date_FrontToDB_O(V_txtEndDate.Text.Trim());

        #region "Document Uploader"

        if (flUploadDocs.HasFile == true)
        {
            string DocumentName = flUploadDocs.PostedFile.FileName;
            string FileType = Path.GetExtension(flUploadDocs.PostedFile.FileName);

            #region "To Get the Physical Path of Server"

            UploadFiles();

            #endregion

            string upldPath = "";
            string currDir = System.IO.Directory.GetCurrentDirectory();
            upldPath = this.upldPath;

            Random randomNo1 = new Random();

            dr["File_Name"] = DocumentName;
            //dr["File_Unique_Name"] = "DOC_" + "_" + Guid.NewGuid().ToString() + "_" + randomNo1.Next(50000, 1000000) + "_" +
            //    DateTime.Now.ToString("yyyyMMddHHmmssfff") + "PJC" + "_" + randomNo1.Next(33333, 454545454) + "_" + randomNo1.Next(999999, 15215454) + FileType;
            dr["File_Unique_Name"] = "DOC_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + FileType;
            dr["FileExtension"] = FileType;
            dr["File_Path"] = upldPath;

            bool IsExistPath = System.IO.Directory.Exists(upldPath);
            if (!IsExistPath)
                System.IO.Directory.CreateDirectory(upldPath);
            flUploadDocs.SaveAs(upldPath + dr["File_Unique_Name"].ToString());
        }
        else
        {
            if (ViewState["id"] != null)
            {
                DataSet dsS = IUMSNXG.SP.Alm_Alumni_PublishJob_ins_sel_Upd("", Convert.ToInt32(ViewState["id"].ToString()), 0, "S").GetDataSet();

                if (dsS != null && dsS.Tables[0].Rows.Count > 0)
                {
                    dr["File_Name"] = dsS.Tables[0].Rows[0]["File_Name"].ToString();
                    dr["File_Unique_Name"] = dsS.Tables[0].Rows[0]["File_Unique_Name"].ToString(); ;
                    dr["FileExtension"] = dsS.Tables[0].Rows[0]["FileExtension"].ToString(); ;
                    dr["File_Path"] = dsS.Tables[0].Rows[0]["File_Path"].ToString();
                }
            }
        }

        #endregion

        ds.Tables[0].Rows.Add(dr);
        string xmlDoc = ds.GetXml();
        if ((IUMSNXG.SP.Alm_Alumni_PublishJob_ins_sel_Upd(xmlDoc, 0, 0, "I").Execute()) > 0)
        {
            //lblMsg.Text = eobj.ShowMessage("S");
            Client_Messaging("Job is Posted Successfully!");
            Clear();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }

    #endregion

    #region "Bind Grid of Alumni's Request,Changed Request"

    private void BindJobCategory()
    {
        D_ddlJobCat.Items.Clear();
        DataSet ds = IUMSNXG.SP.ALM_SP_JobCategory_SelForDDL().GetDataSet();
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                D_ddlJobCat.DataSource = ds.Tables[0];
                D_ddlJobCat.DataValueField = "Pk_JobCId";
                D_ddlJobCat.DataTextField = "Name";
                D_ddlJobCat.DataBind();
            }
        }
        D_ddlJobCat.Items.Insert(0, "--Select Job Category--");
    }

    private void BindGrid()
    {
        DataSet ds = IUMSNXG.SP.Alm_Alumni_PublishJob_ins_sel_Upd("", 0, Session["AlumniId"] != null ? Convert.ToInt32(Session["AlumniId"].ToString()) : 0, "D").GetDataSet();

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = ds.Tables[0];
            gvDetails.DataBind();
        }
        else
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
        }
    }

    #endregion

    #region "Common Methods and Reset Event"

    private void Clear()
    {
        lblMsg.Text = "";
        cfobj.ClearTextSetDropDown(this);
        gvDetails.SelectedIndex = -1;
        D_ddlJobCat.Items.Clear();
        BindJobCategory();
        D_ddlJobCat.SelectedIndex = 0;
        BindGrid();
        gvDetails.SelectedIndex = -1;
        btnSave.CommandName = "SAVE";
        btnSave.Text = "PUBLISH";
        //V_txtEndDate.Text = "";
        //V_txtStartDate.Text = "";
        SetDefaultDate();
        ViewState["id"] = null;
        ViewState["File"] = null;
        lnkViewDoc.Visible = false;
        lnkViewDoc.Text = "";
        Anthem.Manager.IncludePageScripts = true;
    }

    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #endregion

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["id"] = null;
            lblMsg.Text = "";
            ViewState["id"] = e.CommandArgument.ToString().Trim();
            if (e.CommandName.ToUpper().ToString() == "SELECT")
            {
                Edit(Convert.ToInt32(ViewState["id"].ToString()));
                btnSave.CommandName = "UPDATE";
                btnSave.Text = "UPDATE";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void Edit(int Pk_Jobid)
    {
        DataSet ds = IUMSNXG.SP.Alm_Alumni_PublishJob_ins_sel_Upd("", Pk_Jobid, 0, "S").GetDataSet();
        if (ds.Tables[0].Rows.Count > 0)
        {
            R_txtCompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
            R_txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
            BindJobCategory();
            D_ddlJobCat.SelectedValue = ds.Tables[0].Rows[0]["fk_JobCId"].ToString();
            R_txtVacancyDtl.Text = ds.Tables[0].Rows[0]["VacancyDtl"].ToString();
            R_txtSkillReq.Text = ds.Tables[0].Rows[0]["SkillsReq"].ToString();
            txt_SelectionProc.Text = ds.Tables[0].Rows[0]["SelectionProcess"].ToString();
            txt_JobVacncyUrl.Text = ds.Tables[0].Rows[0]["JobVacncyUrl"].ToString();
            V_txtStartDate.Text = ds.Tables[0].Rows[0]["JobOpenFrom"].ToString();
            V_txtEndDate.Text = ds.Tables[0].Rows[0]["JobOpenTo"].ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["File_Unique_Name"] != null && ds.Tables[0].Rows[0]["File_Unique_Name"].ToString() != "")
                {
                    ViewState["File"] = ds.Tables[0].Rows[0]["File_Unique_Name"].ToString();
                    lnkViewDoc.CommandArgument = (ViewState["id"]) == null ? "0" : (ViewState["id"].ToString());
                    lnkViewDoc.CommandName = ds.Tables[0].Rows[0]["File_Unique_Name"].ToString();
                    lnkViewDoc.Visible = true;
                    getFileName();
                }
            }
        }
    }

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Clear();
        gvDetails.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_Alumni_Home.aspx");
    }

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
                    upldPath = upldPath + "\\Alumni\\";
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            string Msg = "";
        }
    }

    protected void getFileName()
    {
        string FileName = lnkViewDoc.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";

        //if (FileName.Contains("/"))
        //{
        //FileDisplayName = FileName;
        //FileRealName = "https://ftp.hpushimla.in/HPU_DOC/Alumni/" + FileName.Substring(FileName.IndexOf("/") + 1);

        FileDisplayName = FileName;
        FileRealName = ReturnPath() + "/Alumni/" + FileName.Substring(FileName.IndexOf("/") + 1);
        FileUrl = FileUrl + FileName;
        lnkViewDoc.Text = "<a target='_blank' style='color:Blue' href=" + FileRealName + ">" + FileDisplayName + "</a>";
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

    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        getFileName();
    }

    protected void SetDefaultDate()
    {
        try
        {
            //Setting Default values for From Date and To Date
            string StartDate, EndDate;
            StartDate = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            EndDate = DateTime.Now.ToString("MM/dd/yyyy");
            V_txtStartDate.Text = CommonCode.DateFormats.Date_DBToFront(StartDate).Replace("-", "/");
            V_txtEndDate.Text = CommonCode.DateFormats.Date_DBToFront(EndDate).Replace("-", "/");
        }
        catch (Exception)
        {
            throw;
        }
    }
}