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
using System.IO;
using System.Data.SqlClient;

public partial class Alumni_ALM_Notable_Lists : System.Web.UI.Page
{
    public string xmldoc { get; private set; }
    public int mode { get; private set; }
    public int PK_Events_id { get; set; }
    public int pk_Alumniid { get; set; }

    #region "Global Declaration"

    private Boolean IsPageRefresh = false;
    DataAccess Dobj = new DataAccess();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    bool active = false;
    ArrayList names = new ArrayList(); ArrayList types = new ArrayList(); ArrayList values = new ArrayList();

    #endregion
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AlumniID"].ToString() == "" || Session["AlumniID"] == null)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }

        int check = int.Parse(alm_alumni_check(int.Parse(Session["AlumniID"].ToString())).GetDataSet().Tables[0].Rows[0][0].ToString());

        if (check < 1)
        {
            Response.Redirect("../Alumin_Loginpage.aspx");
        }

        if (Session["AlumniID"] != null)
        {
            pk_Alumniid = Convert.ToInt32(Session["AlumniID"].ToString());
        }

        if (!IsPostBack)
        {
            FillGrid();
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }

    public static StoredProcedure alm_alumni_check(int pk_alumniid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("alm_alumni_check", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@pk_alumniid", pk_alumniid, DbType.Int32);
        return sp;
    }

    public static StoredProcedure Get_All_Notable_Alumni()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Notable_Alumni_SelectAll", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    private void FillGrid()
    {
        try
        {
            crypto cpt = new crypto();
            DataSet dtN = Get_All_Notable_Alumni().GetDataSet();
            dtN.Tables[0].Columns.Add("encId");
            int rnum = (dtN.Tables[0].Rows.Count) + 1;

            if (dtN.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < dtN.Tables[0].Rows.Count; x++)
                {
                    string pkid = dtN.Tables[0].Rows[x]["PK_NAID"].ToString();
                    string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                    dtN.Tables[0].Rows[x]["encId"] = encId;
                }
                rptNotableAll.DataSource = dtN.Tables[0];
                rptNotableAll.DataBind();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    public string FU_physicalPath(FileUpload flu, string FolderName, string FileName)
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
                        upldPath = upldPath + "\\Alumni\\" + "";
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
}