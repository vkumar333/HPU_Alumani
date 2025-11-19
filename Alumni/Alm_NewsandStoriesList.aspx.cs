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

public partial class Alumni_Alm_NewsandStoriesList : System.Web.UI.Page
{
    crypto cpt = new crypto();
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    public object Heading;
    public int Fk_Ctaegory_Id { get; set; }
    public string AlumniStories_Values { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            EventsRepeter();
            LatestNewsandStoriesRepeter();
        }
    }

    private void EventsRepeter()
    {
        DataSet ds = new DataSet();
        ds = NewsDetails();
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
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
            storiesRepeater.DataSource = dt;
            storiesRepeater.DataBind();
        }
    }

    private void LatestNewsandStoriesRepeter()
    {
        DataSet ds = new DataSet();
        ds = GetLatestNewsandStoriesDetails();
        DataTable dt = new DataTable();
        dt = ds.Tables[1];
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
            newsRepeater.DataSource = dt;
            newsRepeater.DataBind();
        }
    }

    private DataSet NewsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_GetNews_Latest", values, names, types);
    }

    private DataTable SearchEventsDetails()
    {
        ClearArrayLists();
        names.Add("@search"); types.Add(SqlDbType.NVarChar); values.Add(Heading);
        return Dobj.GetDataTable("ALM_Seach_news_Details", values, names, types);
    }

    private DataSet GetLatestNewsandStoriesDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_GetNews_Latest", values, names, types);
    }

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }
}