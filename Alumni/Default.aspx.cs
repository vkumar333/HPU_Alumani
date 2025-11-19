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

public partial class Alumni_Default : System.Web.UI.Page
{
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
        return Dobj.GetDataTable("Show_Events_Details", values, names, types);
    }

    private DataTable GetNewsNdstories()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("GetNewsNdstories", values, names, types);
    }
    private DataTable GetGellary()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("GetGellaryRecord", values, names, types);
    }
    private DataSet FillBoardDtls()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Get_Notiboard_Dtls", values, names, types);
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        EventsRepeter();
        ImagesRepeter();
        NewsNsStoriesRepeter();
        RepeterData();
        RepeterData1();

    }

    private void EventsRepeter()
    {
        DataTable dt = new DataTable();
        dt = EventsDetails();
        {         
             string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            RepeaterEvents.DataSource = dt;
            RepeaterEvents.DataBind();
        }
    }

    private void NewsNsStoriesRepeter()
    {
        DataTable dt = new DataTable();
        dt = GetNewsNdstories();          
        if (dt.Rows.Count > 0)
        {
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
        //Session["id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["Fk_CompReq_Id"]);
        if (ds.Tables[0].Rows.Count > 0)
        {
            RepterDetails.DataSource = ds.Tables[0];
            RepterDetails.DataBind();
            //GetDate();
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
}