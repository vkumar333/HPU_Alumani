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

public partial class Alumni_Alm_ViewPublished_JobDetails : System.Web.UI.Page
{
    public int pkpublisjJobid { get; private set; }
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
    private DataSet FillJobDetails()
    {
        clear();
        names.Add("@pkpublisjJobid"); values.Add(pkpublisjJobid); types.Add(SqlDbType.Int);
        return DAobj.GetDataSet("ALM_GetPublished_Jobs", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
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
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            {
                try
                {
                    int.Parse((crp.Decrypt((Request.QueryString["id"].ToString()).ToString())));
                    pkpublisjJobid = int.Parse(crp.Decrypt(Request.QueryString["id"]));
                }
                catch (Exception ex)
                {
                    Response.Redirect("../Alumin_Loginpage.aspx");
                }
            }
            else
            {
                Response.Redirect(Page.ResolveUrl("../Alumin_Loginpage.aspx"));
            }
            bindJobCategory();
            fillData();
        }
        else
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }
    }

    /// <summary>
    /// Bind Category
    /// </summary>
    private void bindJobCategory()
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
    ///  bind ChkJobappliedStatus
    /// </summary>
    //public void ChkJobappliedStatus()
    //{
    //    AlumniID = Convert.ToInt32(Session["AlumniID"]);
    //    pkpublisjJobid = int.Parse(Request.QueryString["id"]);
    //    DataSet ds = Checkeligibility();
    //    if (ds.Tables.Count > 0)
    //    {
    //        if (Convert.ToInt32(ds.Tables[0].Rows[0]["Pk_Applied_JobId"]) != 0)
    //        {
    //            ClientMessaging("You have already applied for this job..!!");
    //            Btnapply.Visible = false;
    //        }
    //    }
    //}

    /// <summary>
    /// To fill Form
    /// </summary>
    public void fillData()
    {
        try
        {
            DataSet ds = FillJobDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                R_txtCompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                R_txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                D_ddlJobCat.SelectedValue = ds.Tables[0].Rows[0]["fk_JobCId"].ToString();
                R_txtVacancyDtl.Text = ds.Tables[0].Rows[0]["VacancyDtl"].ToString();
                R_txtSkillReq.Text = ds.Tables[0].Rows[0]["SkillsReq"].ToString();
                txt_SelectionProc.Text = ds.Tables[0].Rows[0]["SelectionProcess"].ToString();
                txt_JobVacncyUrl.Text = ds.Tables[0].Rows[0]["JobVacncyUrl"].ToString();
                V_txtStartDate.Text = CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["JobOpenFrom"].ToString());
                V_txtEndDate.Text = CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["JobOpenTo"].ToString());

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
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(ex.Message);
        }
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
                        upldPath = upldPath + "/" + "Alumni\\StuImage" + "/";
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

    protected void lnkViewDoc_Click(object sender, EventArgs e)
    {
        getFileName();
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
}