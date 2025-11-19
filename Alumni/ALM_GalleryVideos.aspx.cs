using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Data;

public partial class Alumni_ALM_GalleryVideos : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    crypto cpt = new crypto();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    int _pk_VideoID;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rptVideoData();
        }
    }

    private DataSet fillGalleryVideos()
    {
        return Dobj.GetDataSet("ALM_SP_GalleryVideos_GetAll", values, names, types);
    }

    private void rptVideoData()
    {
        //DataSet ds = fillGalleryVideos();

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    RepeaterVideoGallery.DataSource = ds.Tables[0];
        //    RepeaterVideoGallery.DataBind();
        //}

        DataSet dsVideos = fillGalleryVideos();

        List<GalleryVideoModel> videoList = new List<GalleryVideoModel>();

        foreach (DataRow row in dsVideos.Tables[0].Rows)
        {
            string originalUrl = row["videoURL"].ToString();
            string videoId = ExtractYouTubeVideoId(originalUrl);

            videoList.Add(new GalleryVideoModel
            {
                VideoId = videoId,
                Title = row["title"].ToString()
            });
        }

        RepeaterVideoGallery.DataSource = videoList;
        RepeaterVideoGallery.DataBind();
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
}

public class GalleryVideoModel
{
    public string VideoId { get; set; }
    public string Title { get; set; }
}