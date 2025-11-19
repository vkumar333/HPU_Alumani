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

public partial class Alumni_ALM_Published_Jobs : System.Web.UI.Page
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
            FillGrid();
            vacancciesRepeter();
            FillGridJobs();
        }
    }

    private void FillGrid()
    {
        try
        {
            DataSet ds;
            ds = Job_Lists_for_Portal();
            //gvVaccancies.DataSource = ds.Tables[0];
            //gvVaccancies.DataBind();
            //gvVaccancies.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message;
        }
    }

    private void vacancciesRepeter()
    {
        //DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        //if (ds.Tables[4].Rows.Count > 0)
        //{
        //    vaccanciesRepeater.DataSource = ds.Tables[4];
        //    vaccanciesRepeater.DataBind();
        //}

        DataSet dsV = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        dsV.Tables[4].Columns.Add("encId");
        int rnum = (dsV.Tables[4].Rows.Count) + 1;

        if (dsV.Tables[4].Rows.Count > 0)
        {
            for (int x = 0; x < dsV.Tables[4].Rows.Count; x++)
            {
                string pkid = dsV.Tables[4].Rows[x]["Pk_JobPostedId"].ToString();
                string encId = crp.EncodeString(Convert.ToInt32(pkid));
                dsV.Tables[4].Rows[x]["encId"] = encId;
            }
            //vaccanciesRepeater.DataSource = dsV.Tables[4];
            //vaccanciesRepeater.DataBind();
        }
    }

    private void FillGridJobs()
    {
        DataSet dsV = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        dsV.Tables[4].Columns.Add("encId");
        int rnum = (dsV.Tables[4].Rows.Count) + 1;

        if (dsV.Tables[4].Rows.Count > 0)
        {
            for (int x = 0; x < dsV.Tables[4].Rows.Count; x++)
            {
                string pkid = dsV.Tables[4].Rows[x]["Pk_JobPostedId"].ToString();
                string encId = crp.EncodeString(Convert.ToInt32(pkid));
                dsV.Tables[4].Rows[x]["encId"] = encId;
            }
            vacanciesRepeater.DataSource = dsV.Tables[4];
            vacanciesRepeater.DataBind();
        }
    }

    private DataSet Job_Lists_for_Portal()
    {
        ClearArrayLists();
        return objDAO.GetDataSet("ALM_Alumni_Posted_Jobs_On_Portal", values, names, types);
    }

    //private DataSet ALM_Alumni_Posted_Jobs_Get_JobID(int publishedJobid)
    //{
    //    ClearArrayLists();
    //    names.Add("@publishedJobid"); types.Add(SqlDbType.Int); values.Add(_JobId);
    //    return objDAO.GetDataSet("ALM_Alumni_Posted_Jobs_Get_JobID", values, names, types);
    //}

    public static StoredProcedure ALM_Alumni_Posted_Jobs_Get_JobID(int publishedJobid)
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Alumni_Posted_Jobs_Get_JobID", DataService.GetInstance("IUMSNXG"), "");
        sp.Command.AddParameter("@publishedJobid", publishedJobid, DbType.Int32);
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



    [WebMethod]
    public static List<GetCureentVacancy> GetCurrentVaCancy()
    {
        List<GetCureentVacancy> list = new List<GetCureentVacancy>();
        DataSet ds = IUMSNXG.SP.ALM_Select_Alumni_DashBoard(0).GetDataSet();
        if (ds.Tables[4].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[4].Rows)
            {
                GetCureentVacancy obj = new GetCureentVacancy();
                obj.Pk_JobPostedId = Convert.ToInt32(dr["Pk_JobPostedId"]);
                obj.CompanyName = dr["CompanyName"].ToString();
                obj.Designation = dr["Designation"].ToString();
                obj.OpenFrom = dr["OpenFrom"].ToString();
                obj.OpenTo = dr["OpenTo"].ToString();
                list.Add(obj);
                //id.HRef = "MyPage.aspx?id=" + obj.Pk_JobPostedId;
            }
        }
        return list;
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

    protected void gvVaccancies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int publishedJobid = int.Parse(Request.QueryString["id"]);
            if (e.CommandName.ToString().ToUpper() == "APPLY")
            {
                DataSet ds = ALM_Alumni_Posted_Jobs_Get_JobID(publishedJobid).GetDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string a = crp.Encrypt(publishedJobid.ToString());
                    a = a.Replace("+", "%2B");
                    Response.Redirect("~//Alumni//Alm_ViewPublishedJobs.aspx?id=" + a + "");
                    //Response.Redirect("~//Alumni//Alm_ViewPublishedJobs.aspx?id=" + publishedJobid + "");
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(exsql.Message);
        }
    }
}

public class GetCureentVacancy
{
    public int Pk_JobPostedId { get; set; }
    public string CompanyName { get; set; }
    public string Designation { get; set; }
    public string OpenFrom { get; set; }
    public string OpenTo { get; set; }
}

public class Getimg
{
    public int pk_id { get; set; }
    public string image { get; set; }
}