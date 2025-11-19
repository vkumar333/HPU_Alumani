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

public partial class Alumni_ALM_NotableAlumni_Lists : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    crypto cpt = new crypto();

    public object Heading;
    public int Fk_Ctaegory_Id { get; set; }
    public string AlumniStories_Values { get; set; }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable NotableDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetAll_Notable_Alumni_Details", values, names, types);
    }

    private DataTable SearchEventsDetails()
    {
        ClearArrayLists();
        names.Add("@search"); types.Add(SqlDbType.NVarChar); values.Add(Heading);
        return Dobj.GetDataTable("ALM_Seach_news_Details", values, names, types);
    }

    private DataTable GetLatestNewsandStoriesDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetLatestNewsandStoriesDetails", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rptGetNotableAlumni();
            //LatestNewsandStoriesRepeter();
        }
    }

    private void rptGetNotableAlumni()
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
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }

    private void LatestNewsandStoriesRepeter()
    {
        DataTable dt = new DataTable();
        dt = GetLatestNewsandStoriesDetails();
        dt.Columns.Add("encId");

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["Pk_Stories_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;

                //string imgFile = dt.Rows[x]["Image"].ToString();
                //string imgPath = SetServiceDoc(imgFile);
                //string imgEncId = GetBase64Image(imgPath);
                //dt.Rows[x]["ImageUrl"] = imgEncId;
            }
            //RepLatestnews.DataSource = dt;
            //RepLatestnews.DataBind();
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SearchEvents();
    }

    private void SearchEvents()
    {
        //Heading = txtsearch.Text;
        DataTable dt = new DataTable();
        dt = SearchEventsDetails();
        dt.Columns.Add("encId");

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                string pkid = dt.Rows[x]["Pk_Stories_id"].ToString();
                string encId = cpt.EncodeString(Convert.ToInt32(pkid));
                dt.Rows[x]["encId"] = encId;

                //string imgFile = dt.Rows[x]["Image"].ToString();
                //string imgPath = SetServiceDoc(imgFile);
                //string imgEncId = GetBase64Image(imgPath);
                //dt.Rows[x]["ImageUrl"] = imgEncId;
            }
            //RepeventsAll.DataSource = dt;
            //RepeventsAll.DataBind();
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

}