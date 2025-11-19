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

public partial class Alumni_Alm_View_Events_Student_Details : System.Web.UI.Page
{
    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();

    public object description { get; private set; }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "CCSBLUE";
    }
    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }
    private DataTable EventsDetails(int Pk_Eventsid)
    {
        ClearArrayLists();
        names.Add("@Pk_Eventsid"); types.Add(SqlDbType.NVarChar); values.Add(Pk_Eventsid);
        return Dobj.GetDataTable("Show_Events_Details_by_id", values, names, types);
    }

    private DataTable EventslatestDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("Show_Events_Details", values, names, types);
    }

    private DataTable CountEventsDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("Count_Events_Details", values, names, types);
    }
    private DataTable SearchEventsDetails()
    {
        ClearArrayLists();
        names.Add("@eventsname"); types.Add(SqlDbType.NVarChar); values.Add(description);
        return Dobj.GetDataTable("Search_Events_Details", values, names, types);
    }

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    Page.Theme = "";
    //}
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["ID"] != null)
        {
            int Pk_Eventsid = Convert.ToInt32(Request.QueryString["ID"].ToString());
            ViewState["Eventid"] = Request.QueryString["ID"].ToString();
            if (Pk_Eventsid != 0)
            {
                EventsRepeter(Pk_Eventsid);

            }
        }
    }
    /// <summary>
    /// Bind Repeater
    /// </summary>
    /// <param name="Pk_Eventsid"></param>
    private void EventsRepeter(int Pk_Eventsid)
    {
        DataTable dt = new DataTable();
        dt = EventsDetails(Pk_Eventsid);
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            RepeventsAll.DataSource = dt;
            RepeventsAll.DataBind();
        }
    }
}