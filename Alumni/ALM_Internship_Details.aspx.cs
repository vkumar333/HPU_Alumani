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
using Anthem;

public partial class Alumni_ALM_Internship_Details : System.Web.UI.Page
{
    public int InternID { get; private set; }
    public int AlumniID { get; private set; }
    public string xmldoc { get; private set; }
    crypto crp = new crypto();

    public int ins_upd_delevents_dtls(ref string Message)
    {
        try
        {
            names.Clear(); values.Clear(); types.Clear();
            names.Add("@doc"); types.Add(SqlDbType.VarChar); values.Add(xmldoc);
            if (Dobj.ExecuteTransactionMsg("ALM_JobApplied_Dtls_Insert", values, names, types, ref Message) > 0)
            {
                Message = Dobj.ShowMessage("S", "");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch { throw; }
    }

    DataAccess DAobj = new DataAccess();
    DataAccess Dobj = new DataAccess();
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    /// <summary>
    /// Clear function of Sp
    /// </summary>
    void clear()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    /// <summary>
    /// Sp to fill Controls
    /// </summary>
    /// <returns></returns>
    private DataSet FillInternshipsDetails()
    {
        clear();
        names.Add("@pk_InternId"); values.Add(InternID); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("ALM_Get_Internships_Dtl", values, names, types);
    }
    /// <summary>
    /// Sp to Check job eligibility
    /// </summary>
    /// <returns></returns>
    private DataSet Checkeligibility()
    {
        clear();
        names.Add("@fk_appplied_alumni_Id"); values.Add(AlumniID); types.Add(SqlDbType.Int);
        names.Add("@JobPostedId"); values.Add(InternID); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("chkjobstatus", values, names, types);
    }
    /// <summary>
    /// Sp to fill Controls
    /// </summary>
    /// <returns></returns>
    private DataSet GetautoJobNo()
    {
        clear();

        return DAobj.GetDataSet("ALM_SP_AutoGenrateJobNo", values, names, types);
    }

    /// <summary>
    /// Sp to Fill Pop-up Details
    /// </summary>
    /// <returns></returns>
    private DataSet FillApplierDetails()
    {
        clear();
        names.Add("@AlumniID"); values.Add(AlumniID); types.Add(SqlDbType.Int);
        //return DAobj.GetDataSet("ALM_GetPopup_Details", values, names, types);
        return DAobj.GetDataSet("ALM_SP_GetPopup_Details", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")//Query String
            {
                try
                {
                    crypto cpt = new crypto();
                    string decId = cpt.DecodeString(Request.QueryString["id"].ToString());
                    ViewState["JobPostedId"] = Convert.ToInt32(decId);
                    InternID = int.Parse(decId.ToString());
                }
                catch
                {
                    Response.Redirect("../Alumin_Loginpage.aspx");
                }
            }
            else
            {
                Response.Redirect(Page.ResolveUrl("../Alumin_Loginpage.aspx"));
            }
            BindJobCategory();
            filldata();
        }
    }

    /// <summary>
    /// Bind Category
    /// </summary>
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

    /// <summary>
    /// To fill Form
    /// </summary>
    public void filldata()
    {
        try
        {
            DataSet ds = FillInternshipsDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                R_txtCompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                R_txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                D_ddlJobCat.SelectedValue = ds.Tables[0].Rows[0]["fk_JobCId"].ToString();
                R_txtVacancyDtl.Text = ds.Tables[0].Rows[0]["VacancyDtl"].ToString();
                R_txtSkillReq.Text = ds.Tables[0].Rows[0]["SkillsReq"].ToString();
                txt_SelectionProc.Text = ds.Tables[0].Rows[0]["SelectionProcess"].ToString();
                txtStipend.Text = ds.Tables[0].Rows[0]["Stipend"].ToString();
                txtDuration.Text = ds.Tables[0].Rows[0]["Duration"].ToString();
                txt_JobVacncyUrl.Text = ds.Tables[0].Rows[0]["JobVacancyUrl"].ToString();
                //CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["JobOpenFrom"].ToString());
                //CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["JobOpenTo"].ToString());
                V_txtStartDate.Text = ds.Tables[0].Rows[0]["JobOpenFrom"].ToString();
                V_txtEndDate.Text = ds.Tables[0].Rows[0]["JobOpenTo"].ToString();

                if (ds.Tables[0].Rows[0]["File_Unique_Name"] != null && ds.Tables[0].Rows[0]["File_Unique_Name"].ToString() != "")
                {
                    lnkViewDoc.CommandName = ds.Tables[0].Rows[0]["File_Unique_Name"].ToString();
                    lnkViewDoc.Visible = true;
                    getFileName();
                }
                else
                {
                    lnkViewDoc.Visible = false;
                }

                //if (Convert.ToDateTime(V_txtEndDate.Text) < DateTime.Now)
                //{
                //    Btnapply.Visible = false;
                //}
                //else
                //{
                //    Btnapply.Visible = true;
                //}

                string jobOpenTo = ds.Tables[0].Rows[0]["JobOpenTo"].ToString();
                Anthem.TextBox currDate = new Anthem.TextBox();
                currDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                //if (Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_R(V_txtEndDate.Text)) < Convert.ToDateTime(CommonCode.DateFormats.Date_FrontToDB_R(currDate.Text)))
                //{
                //    Btnapply.Visible = false;
                //}
                //else
                //{
                //    Btnapply.Visible = true;
                //}
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
    }

    /// <summary>
    /// Apply Job Btn Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btnapply_Click(object sender, EventArgs e)
    {
        getApplierDetails();//Get Applier Details
    }

    /// <summary>
    /// Fill Details on Pop Up
    /// </summary>
    public void getApplierDetails()
    {
        AlumniID = Convert.ToInt32(Session["AlumniID"]);
        DataSet ds = FillApplierDetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
            getJobNo();
            lbl_Fullname.Text = ds.Tables[0].Rows[0]["alumni_name"].ToString();
            lbl_mail.Text = ds.Tables[0].Rows[0]["email"].ToString();
            lbl_address.Text = ds.Tables[0].Rows[0]["currentaddress"].ToString();
            lbldegree.Text = ds.Tables[0].Rows[0]["description"].ToString();
            lblcontact.Text = ds.Tables[0].Rows[0]["contactno"].ToString();
        }
    }

    /// <summary>
    /// Auto Generated Job no
    /// </summary>
    public void getJobNo()
    {
        DataSet ds = GetautoJobNo();
        lbl_ReqNo.Text = ds.Tables[0].Rows[0]["alumcode"].ToString();
    }

    /// <summary>
    /// Save event on pop up 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkbtn_Save_Click(object sender, EventArgs e)
    {
        Save();
    }

    //Messages
    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    /// <summary>
    /// Validation on Submit Button
    /// </summary>
    /// <returns></returns>
    public bool Validation()
    {
        if (flUploadCV.HasFile == false)
        {
            ClientMessaging("Kindly Upload Your CV.!!!");
            flUploadCV.Focus();
            return false;

        }
        if (flUploadCV.HasFile && (System.IO.Path.GetExtension(flUploadCV.FileName) != ".pdf") && (System.IO.Path.GetExtension(flUploadCV.FileName) != ".PDF"))
        {
            ClientMessaging("Only PDF files can be uploaded...!");
            flUploadCV.Focus();
            return false;
        }

        string FileType = Path.GetExtension(flUploadCV.PostedFile.FileName);

        if (FileType != null && FileType != "")
        {
            if (FileType != ".pdf" && FileType != ".PDF" && FileType != ".jpg" && FileType != ".JPG" && FileType != ".jpeg" && FileType != ".JPEG"
        && FileType != ".bmp" && FileType != ".BMP" && FileType != ".gif" && FileType != ".GIF" && FileType != ".png" && FileType != ".PNG")
            {
                ClientMessaging("Upload CV file should be in required format!");
                //lblMsg.Text = "Upload CV file should be in required format!";
                flUploadCV.Focus();
                return false;
            }
        }

        double filesize = flUploadCV.PostedFile.ContentLength;
        //filesize = Math.Round((filesize / 1024), 0);

        if (filesize > (5 * 1024 * 1024))
        {
            ClientMessaging("Photo size should not be more than 5 MB !");
            flUploadCV.Focus();
            return false;
        }

        return true;
    }

    /// <summary>
    /// Insert operation on pop up save Button
    /// </summary>
    public void Save()
    {
        if (Validation())
        {
            try
            {
                string Message = "";
                DataSet ds_details = new DataSet();
                ds_details = Getmain();
                string doc = "";
                doc = ds_details.GetXml();
                xmldoc = doc;
                if (ins_upd_delevents_dtls(ref Message) > 0)
                {
                    ClientMessaging("Record Save Successfully.");

                    //getUploaded_details();
                    // R_txtStuyDesc.Text = "";
                    // ctctxt.Text = string.Empty;
                    //D_ddlJobList.SelectedIndex = 0;
                }
                else
                {
                    ClientMessaging("Record not saved......!");
                }
            }

            catch (Exception Ex)
            {
                //lblMsg.Text = CommonCode.ExceptionHandling.SqlExceptionHandling(Ex.Message);
            }
        }
    }

    public DataSet Getmain()
    {
        DataSet ds_master = Dobj.GetSchema("Alm_JobApplied_Details");
        DataRow dr = ds_master.Tables[0].NewRow();
        dr["fk_appplied_alumni_Id"] = Convert.ToInt32(Session["AlumniID"]);
        //dr["Fk_JobPostedId"] = Request.QueryString["id"];

        if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")//Query String
        {
            crypto cpt = new crypto();
            string decId = cpt.DecodeString(Request.QueryString["id"].ToString());
            ViewState["JobPostedId"] = Convert.ToInt32(decId);
            InternID = int.Parse(decId.ToString());
            dr["Fk_JobPostedId"] = InternID;
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }

        dr["Job_No"] = lbl_ReqNo.Text.ToString();

        ds_master.Tables[0].Rows.Add(dr);
        /////////////// Upload Document////////////////////
        if (flUploadCV.HasFile)
        {
            if (flUploadCV.PostedFile.FileName != "")
            {
                string FileType = Path.GetExtension(flUploadCV.PostedFile.FileName);
                if (FileType != null)
                {
                    string lFileName = "";

                    Random r = new Random();
                    int n = r.Next();

                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + flUploadCV.FileName.Substring(flUploadCV.FileName.LastIndexOf("."));
                    lFileName = FU_physicalPath(flUploadCV, "PLACEMENT_DATA/Company_Profile", "BRO" + n.ToString() + filename);
                    // dr["Student_img"] = flUploadLogo.FileName + "/" + lFileName;
                    dr["filename"] = lFileName;
                    dr["FilePath"] = Session["filepath"];
                }
            }
        }
        return ds_master;
    }

    /// <summary>
    /// physical path
    /// </summary>
    /// <param name="flu"></param>
    /// <param name="FolderName"></param>
    /// <param name="FileName"></param>
    /// <returns></returns>
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
                        upldPath = upldPath + "\\Alumni\\";
                        Session["filepath"] = upldPath;
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

    protected void lnkviewBrc_Click(object sender, EventArgs e)
    {

    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Alumni/ALM_Alumni_VacanciesLists.aspx");
    }

    protected void getFileName()
    {
        string FileName = lnkViewDoc.CommandName;
        string FileUrl = ReturnPath();
        string FileDisplayName = "";
        string FileRealName = "";
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
}