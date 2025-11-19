using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using DataAccessLayer;
//using CrystalDecisions.CrystalReports.Engine;
using System.Drawing;
using System.Web.Hosting;
using System.Linq;
using System.Data.SqlClient;
using Anthem;
using System.Data;
using System.Collections;
using System;

public partial class Alumni_ALM_PublishedViewJob_Details : System.Web.UI.Page
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
    /// <summary>
    /// Sp to Check job eligibility
    /// </summary>
    /// <returns></returns>
    private DataSet Checkeligibility()
    {
        clear();
        names.Add("@fk_appplied_alumni_Id"); values.Add(AlumniID); types.Add(SqlDbType.Int);
        names.Add("@JobPostedId"); values.Add(pkpublisjJobid); types.Add(SqlDbType.Int);
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
        return DAobj.GetDataSet("ALM_GetPopup_Details", values, names, types);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")//Query String
            {
                try
                {
                    int.Parse((crp.Decrypt((Request.QueryString["id"].ToString()).ToString())));
                    pkpublisjJobid = int.Parse(crp.Decrypt(Request.QueryString["id"]));
                    // pkpublisjJobid = int.Parse(Request.QueryString["id"]);
                }
                catch
                {
                    //Response.Redirect("../Alumin_Loginpage.aspx");
                }
            }
            else
            {
                //Response.Redirect(Page.ResolveUrl("../Alumin_Loginpage.aspx"));
            }
            BindJobCategory();
            filldata();
            ////ChkJobappliedStatus();
        }
    }
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
    ///  bind ChkJobappliedStatus
    /// </summary>
    

    /// <summary>
    /// To fill Form
    /// </summary>
    public void filldata()
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
                V_txtStartDate.Text = ds.Tables[0].Rows[0]["JobOpenFrom"].ToString();
                V_txtEndDate.Text = ds.Tables[0].Rows[0]["JobOpenTo"].ToString();
                //CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["JobOpenFrom"].ToString());
                //CommonCode.DateFormats.Date_DBToFront(ds.Tables[0].Rows[0]["JobOpenTo"].ToString());

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

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }







    protected void lnkSearch_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("~//Alumni//ALM_Publish_Job_Report.aspx");
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



    protected void lnkViewDoc_Click(object sender, System.EventArgs e)
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