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

public partial class Alumni_Alm_Events_Lists : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    public object description { get; private set; }
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable EventsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("Show_Events_Details_list", values, names, types);
    }

    private DataTable SearchEventsDetails()
    {
        ClearArrayLists();
        names.Add("@eventsname"); types.Add(SqlDbType.NVarChar); values.Add(description);
        return Dobj.GetDataTable("Search_Events_Details", values, names, types);
    }

    private DataSet CountEventsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Count_Events_Details", values, names, types);
    }

    private DataSet CountEventsDetails1()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("past_Events_Details", values, names, types);
    }
    private DataSet CountEventsDetails2()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("upcoming_Events_Details", values, names, types);
    }

    private DataSet pastevents()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Past_Events_Click", values, names, types);
    }

    private DataSet Upcomingevents()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Upcoming_Events_Click", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //pasteventsssss();  
        if (!IsPostBack)
        {
            EventsRepeter();
            CountEventsRepeter();
            LatestEventsRepeter();
        }
    }

    private void EventsRepeter()
    {
        DataTable dt = new DataTable();
        dt = EventsDetails();
        //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");       
        if (dt.Rows.Count > 0)
        {
         //   string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            rep.DataSource = dt;
            rep.DataBind();
        }
    }
    private void CountEventsRepeter()
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        ds = CountEventsDetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
         //   string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            Repcountevents.DataSource = ds;
            Repcountevents.DataBind();
        }
        ds1 = CountEventsDetails1();
        if (ds1.Tables[0].Rows.Count >= 0)
        {
          //  string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            Repeater1.DataSource = ds1;
            Repeater1.DataBind();
        }
        ds2 = CountEventsDetails2();
        if (ds2.Tables[0].Rows.Count > 0)
        {
         //   string[] filePathss = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            Repeater2.DataSource = ds2;
            Repeater2.DataBind();
        }
    }


    private void LatestEventsRepeter()
    {
        DataTable dt = new DataTable();
        dt = EventsDetails();
        if (dt.Rows.Count > 0)
        {
            string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
          //  string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\PLACEMENT_DATA\\Company_Profile\\");
            RepLatestEvents.DataSource = dt;
            RepLatestEvents.DataBind();
        }
    }


    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SearchEvents();
    }
    private void SearchEvents()
    {
        description = txtsearch.Text;
        DataTable dt = new DataTable();
        dt = SearchEventsDetails();
        if (dt.Rows.Count > 0)
        {
             string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
          //  string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\PLACEMENT_DATA\\Company_Profile\\");
            rep.DataSource = dt;
            rep.DataBind();
            //Repcountevents.DataSource = null;
            //RepLatestEvents.DataSource = null;

        }
    }

    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alm_Events_List.aspx");
    }

    protected void lnkbtnpast_Click(object sender, EventArgs e)
    {
        pasteventsssss();
    }

    public void pasteventsssss()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = pastevents();
            if (ds.Tables[0].Rows.Count > 0)
            {
                string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
             //  string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
                rep.DataSource = null;
                rep.DataBind();                
                rep.DataSource = ds;
                rep.DataBind();
            }
        }
        catch (Exception Ex)
        {
            lblmsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(Ex.Message);
        }
    }


    protected void lnkUpcoming_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = Upcomingevents();
            if (ds.Tables[0].Rows.Count > 0)
            {
            //    string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
                string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
                rep.DataSource = null;
                rep.DataBind();               
                rep.DataSource = ds;
                rep.DataBind();
            }
            else
            {
                rep.DataSource = null;
                rep.DataBind();
            }
        }
        catch (Exception Ex)
        {
            lblmsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(Ex.Message);
        }
    }
}