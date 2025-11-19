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
using System.Data.SqlClient;
using SubSonic;
using IUMSNXG;
using DataAccessLayer;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.IO;


public partial class Alumni_ALM_Published_Internships : System.Web.UI.Page
{
    #region "Global Declaration"

    DataAccess objDAO = new DataAccess();
    CustomMessaging eobj = new CustomMessaging();
    CommonFunction cfObj = new CommonFunction();
    crypto crp = new crypto();

    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    int _JobId;

    public int JobId
    {
        get { return _JobId; }
        set { _JobId = value; }
    }

    #endregion

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGridInternShips();
        }
    }

    private void FillGridInternShips()
    {
        try
        {
            DataSet dsInternship = ALM_Alumni_Posted_Jobs_GetAll_Internships().GetDataSet();
            dsInternship.Tables[0].Columns.Add("encId");
            int rnum = (dsInternship.Tables[0].Rows.Count) + 1;

            if (dsInternship.Tables[0].Rows.Count > 0)
            {
                for (int x = 0; x < dsInternship.Tables[0].Rows.Count; x++)
                {
                    string pkid = dsInternship.Tables[0].Rows[x]["pk_InternshipId"].ToString();
                    string encId = crp.EncodeString(Convert.ToInt32(pkid));
                    dsInternship.Tables[0].Rows[x]["encId"] = encId;
                }
                vacanciesRepeater.DataSource = dsInternship.Tables[0];
                vacanciesRepeater.DataBind();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    //public static StoredProcedure ALM_Alumni_Posted_Jobs_Get_JobID(int publishedJobid)
    //{
    //    SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Posted_Jobs_Get_JobID", DataService.GetInstance("IUMSNXG"), "");
    //    sp.Command.AddParameter("@publishedJobid", publishedJobid, DbType.Int32);
    //    return sp;
    //}

    public static StoredProcedure ALM_Alumni_Posted_Jobs_GetAll_Internships()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Posted_Internships_GetAll_Internships", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    protected void lnkApplyJobs_Click(object sender, EventArgs e)
    {
        string a = crp.Encrypt(((LinkButton)sender).CommandArgument.ToString());
        a = a.Replace("+", "%2B");
        Response.Redirect("~//Alumni//Alm_ViewPublished_JobDetails.aspx?id=" + a + "");
    }

    public static dynamic GetImageUrl(byte[] ImageAttachedBytes, string ContentType)
    {
        string src = null;
        if (ImageAttachedBytes != null)
        {
            string base64String = Convert.ToBase64String(ImageAttachedBytes);
            src = "data:image/jpg;base64," + base64String;
        }
        else
        {
            src = "~/alumni/stuimage/No_image.png";
        }
        return src;
    }

    protected void Client_Messaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
        //Anthem.Manager.AddScriptForClientSideEval("alert('" + msg + "');");
    }

    //it will return file based on file unique name
    public static string SetServiceDoc(string FileName)
    {
        if (FileName.Length > 0)
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
        else
        {
            return "stuimage/No_image.png";
        }
    }
}