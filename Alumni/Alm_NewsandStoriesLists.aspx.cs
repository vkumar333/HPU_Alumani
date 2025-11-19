using DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Alumni_Alm_NewsandStoriesLists : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    public object Heading;
    public int Fk_Ctaegory_Id { get; set; }
    public string  AlumniStories_Values { get;  set; }


    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable NewsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_GetNewsNdstory", values, names, types);
    }

    private DataTable SearchEventsDetails()
    {
        ClearArrayLists();
        names.Add("@search"); types.Add(SqlDbType.NVarChar); values.Add(Heading);
        return Dobj.GetDataTable("ALM_Seach_news_Details", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }
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
        DataTable dt = new DataTable();
        dt = NewsDetails();

        if (dt.Rows.Count > 0)
        {
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }

    private void LatestNewsandStoriesRepeter()
    {
        DataTable dt = new DataTable();
        dt = NewsDetails();
        {
            RepLatestnews.DataSource = dt;
            RepLatestnews.DataBind();
        }
    }
    
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SearchEvents();
    }

    private void SearchEvents()
    {
        Heading = txtsearch.Text;
        DataTable dt = new DataTable();
        dt = SearchEventsDetails();
        if (dt.Rows.Count > 0)
        {
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }
     
    protected void lnj_Click(object sender, EventArgs e)
    {
        DataTable dt=  fillTest(lnj.CommandArgument.ToString());       
        RepeventsAll.DataSource = dt;
        RepeventsAll.DataBind();

        //Response.Redirect("Alm_NewsandStoriesList.aspx?Id=" + int.Parse(((LinkButton)sender).CommandArgument));
        //fillTest();
    }

    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        DataTable dt= fillTest(lnkbtn.CommandArgument.ToString());
        RepeventsAll.DataSource = dt;
        RepeventsAll.DataBind();
    }

    private void ClickCategory()
    {
       // DataSet ds = fillTest(temp2);
        //Session["id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Fk_CompReq_Id"]);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    RepLatestnews.DataSource = ds.Tables[0];
        //    RepLatestnews.DataBind();
        //}
    }

    private DataTable fillTest(string temp2)
    {
        ClearArrayLists();       
        names.Add("@Fk_Ctaegory_Id"); types.Add(SqlDbType.NVarChar); values.Add(temp2);
        return Dobj.GetDataTable("Alm_Category_Details", values, names, types);
    }
}