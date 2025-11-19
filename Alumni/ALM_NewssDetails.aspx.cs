using DataAccessLayer;
using SubSonic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Alumni_ALM_NewssDetails : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto cpto = new crypto();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable GetDetailsByid(int Pk_Stories_id)
    {
        ClearArrayLists();
        names.Add("@Pk_Stories_id"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Stories_id);
        return Dobj.GetDataTable("ALM_Show_Stories_Details_by_id", values, names, types);
    }

    private DataTable NewsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetNewsNdstoriess", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        EventsRepeter();
        LatestNewsandStoriesRepeter();
        if (Request.QueryString["ID"] != null)
        {
            int Pk_Stories_id = Convert.ToInt32(cpto.DecodeString(Request.QueryString["ID"].ToString()));
            ViewState["Pk_Stories_id"] = Pk_Stories_id;
            if (Pk_Stories_id != 0)
            {
                Repeterid(Pk_Stories_id);
                getSocialURLLinks();
            }
        }
    }

    private void EventsRepeter()
    {
        DataTable dt = new DataTable();
        dt = NewsDetails();

        if (dt.Rows.Count > 0)
        {
            //for (int x = 0; x < dt.Rows.Count; x++)
            //{
            //    string imgFile = dt.Rows[x]["Image"].ToString();
            //    string imgPath = SetServiceDoc(imgFile);
            //    string imgEncId = GetBase64Image(imgPath);
            //    dt.Rows[x]["ImageUrl"] = imgEncId;
            //}
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }

    private void Repeterid(int Pk_Stories_id)
    {
        DataTable dt = new DataTable();
        dt = GetDetailsByid(Pk_Stories_id);

        if (dt.Rows.Count > 0)
        {
            //for (int x = 0; x < dt.Rows.Count; x++)
            //{
            //    string imgFile = dt.Rows[x]["Image"].ToString();
            //    string imgPath = SetServiceDoc(imgFile);
            //    string imgEncId = GetBase64Image(imgPath);
            //    dt.Rows[x]["ImageUrl"] = imgEncId;
            //}
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }

    private void LatestNewsandStoriesRepeter()
    {
        DataTable dt = new DataTable();
        dt = NewsDetails();
        {
            //RepLatestnews.DataSource = dt;
            //RepLatestnews.DataBind();
        }
    }

    public string SetServiceDoc(string filename)
    {
        string FolderName = @"Alumni";
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
                    FilePath = @FilePath + FolderName + @"/" + filename;
                }
                else
                {
                    FilePath = dr["Physical_Path"].ToString().Trim();
                    FilePath = @FilePath + FolderName + @"/" + filename;
                }
                return FilePath;
            }
        }
        return FilePath;
    }

    public string GetBase64Image(string imagePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        string mimeType = System.Web.MimeMapping.GetMimeMapping(imagePath);
        return string.Format("data:{0};base64,{1}", mimeType, base64String);
    }

    #region "Get Social URL Links"

    private void getSocialURLLinks()
    {
        try
        {
            using (DataSet dsURL = getSocialMediaURLLinks().GetDataSet())
            {
                if (dsURL.Tables.Count > 0 && dsURL.Tables[0].Rows.Count > 0)
                {
                    int count = dsURL.Tables[0].Rows.Count;

                    if (dsURL.Tables[0].Rows.Count > 0)
                    {
                        facebookLink.HRef = dsURL.Tables[0].Rows[0]["facebookLink"].ToString();
                        facebookLink.Target = "_blank";
                        twitterLink.HRef = dsURL.Tables[0].Rows[0]["twitterLink"].ToString();
                        twitterLink.Target = "_blank";
                        linkedInLink.HRef = dsURL.Tables[0].Rows[0]["linkedInLink"].ToString();
                        linkedInLink.Target = "_blank";
                        youtubeLink.HRef = dsURL.Tables[0].Rows[0]["youtubeLink"].ToString();
                        youtubeLink.Target = "_blank";
                    }
                    else
                    {
                        facebookLink.HRef = "#";
                        twitterLink.HRef = "#";
                        linkedInLink.HRef = "#";
                        youtubeLink.HRef = "#";
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            // Handle SQL related errors here
            ClientMessaging("SQL Error: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            // Handle any other errors here
            ClientMessaging("Error: " + ex.Message);
        }
    }

    public StoredProcedure getSocialMediaURLLinks()
    {
        SubSonic.StoredProcedure sp = new SubSonic.StoredProcedure("ALM_Get_Social_Media_Links_Info", DataService.GetInstance("IUMSNXG"), "");
        return sp;
    }

    protected void ClientMessaging(string msg)
    {
        String script = String.Format("alert('{0}');", msg);
        Anthem.Manager.IncludePageScripts = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "errMsg", script, true);
    }

    #endregion
}