using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using SubSonic;
using System.Data.SqlClient;

public partial class Alumni_Default : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto cpt = new crypto();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable NotableDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetAll_Notable_Alumni_Details", values, names, types);
    }

    private DataTable EventsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Show_Events_Details", values, names, types);
    }

    private DataTable GetNewsNdstories()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetNewsNdstories", values, names, types);
    }

    private DataTable GetGellary()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetGellaryRecord", values, names, types);
    }

    private DataSet FillBoardDtls()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_Get_Notiboard_Dtls", values, names, types);
    }

    private DataTable SliderDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_Show_slider", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";

        //if (!HttpContext.Current.Request.IsSecureConnection)
        //{
        //    Response.Redirect("https://backoffice.hpushimla.in/Alumni/Alm_Default.aspx");
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SliderRepeter();
            NotableAlumni();
            EventsRepeter();
            ImagesRepeter();
            NewsNsStoriesRepeter();
            RepeterData();
            RepeterData1();
            VideosRepeter();
			getSocialURLLinks();
        }
    }

    private void SliderRepeter()
    {
        DataTable dt = new DataTable();
        dt = SliderDetails();
        {
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\Alumni\\");
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            sliderRepeater.DataSource = dt;
            sliderRepeater.DataBind();
        }
    }

    protected string GetActiveClass(int ItemIndex)
    {
        if (ItemIndex == 0)
        {
            return "active";
        }
        else
        {
            return "";
        }
    }

    private void NotableAlumni()
    {
        DataTable dt = new DataTable();
        dt = NotableDetails();
        dt.Columns.Add("encId");

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["PK_NAID"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;
            }

            rptNotableAlumni.DataSource = dt;
            rptNotableAlumni.DataBind();
        }
    }

    private void EventsRepeter()
    {
        DataTable dt = new DataTable();
        dt = EventsDetails();
        dt.Columns.Add("encId");

        if (dt.Rows.Count > 0)
        {
            // string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            // string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["PK_Events_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;
            }

            RepeaterEvents.DataSource = dt;
            RepeaterEvents.DataBind();
        }
    }

    private void NewsNsStoriesRepeter()
    {
        DataTable dt = new DataTable();
        dt = GetNewsNdstories();
        dt.Columns.Add("encId");

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["Pk_Stories_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;
            }

            RepeaterNewsStories.DataSource = dt;
            RepeaterNewsStories.DataBind();
        }
    }

    private void ImagesRepeter()
    {
        DataTable dt = new DataTable();
        dt = GetGellary();
        if (dt.Rows.Count > 0)
        {
            GalleryImages.DataSource = dt;
            GalleryImages.DataBind();
        }
    }

    private void RepeterData()
    {
        DataSet ds = FillBoardDtls();
        ds.Tables[0].Columns.Add("encId");
        //Session["id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Fk_CompReq_Id"]);
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                string pkid = ds.Tables[0].Rows[x]["Pk_Board_ID"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                ds.Tables[0].Rows[x]["encId"] = encId;
            }

            RepterDetails.DataSource = ds.Tables[0];
            RepterDetails.DataBind();
        }
    }

    private void RepeterData1()
    {
        DataSet ds = FillBoardDtls();
        //Session["id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Fk_CompReq_Id"]);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Repeater1.DataSource = ds.Tables[0];
            Repeater1.DataBind();
        }
    }

    public string SetServiceDoc(string FileName)
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
                    FilePath = @FilePath + FolderName + @"/" + FileName;
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

    private void VideosRepeter()
    {
        //DataTable dt = new DataTable();
        //dt = GetGellaryVideos();
        //if (dt.Rows.Count > 0)
        //{
        //    rptGalleryVideos.DataSource = dt;
        //    rptGalleryVideos.DataBind();
        //}

        DataTable dtVideos = new DataTable();
        dtVideos = GetGellaryVideos();

        List<VideoModel> videoList = new List<VideoModel>();

        foreach (DataRow row in dtVideos.Rows)
        {
            string originalUrl = row["videoURL"].ToString();
            string videoId = ExtractYouTubeVideoId(originalUrl);

            videoList.Add(new VideoModel
            {
                VideoId = videoId,
                Title = row["title"].ToString()
            });
        }

        rptGalleryVideos.DataSource = videoList;
        rptGalleryVideos.DataBind();
    }

    private DataTable GetGellaryVideos()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GallaryVideos_GetAll", values, names, types);
    }

    public string ExtractYouTubeVideoId(string url)
    {
        try
        {
            if (url.Contains("youtube.com/watch?v="))
            {
                // Format: https://www.youtube.com/watch?v=abc123
                var uri = new Uri(url);
                var query = HttpUtility.ParseQueryString(uri.Query);
                return query["v"];
            }
            else if (url.Contains("youtube.com/embed/"))
            {
                // Format: https://www.youtube.com/embed/abc123
                var segments = new Uri(url).Segments;
                return segments.Last().TrimEnd('/');
            }
            else if (url.Contains("youtu.be/"))
            {
                // Format: https://youtu.be/abc123
                var segments = new Uri(url).Segments;
                return segments.Last().TrimEnd('/');
            }
        }
        catch { }

        return string.Empty; // fallback
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

public class VideoModel
{
    public string VideoId { get; set; }
    public string Title { get; set; }
}