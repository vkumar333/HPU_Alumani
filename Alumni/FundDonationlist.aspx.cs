using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Collections;
using System.Data;
using System.IO;
 

public partial class Alumni_FundDonationlist : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    DataAccess Dobj = new DataAccess();
    public ArrayList names = new ArrayList();
    public ArrayList values = new ArrayList();
    public ArrayList types = new ArrayList();
    private object lblmsg;

    protected void ClearArrayLists()
    {
        names.Clear(); values.Clear(); types.Clear();
    }

    private DataTable FundDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("Show_Crowd_Fund_Details", values, names, types);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Details_Repeter();
            CountRepeter();
        }
    }


    private void Details_Repeter()
    {
        DataTable dt = new DataTable();
        dt = FundDetails();
        //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");       
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            rep.DataSource = dt;
            rep.DataBind();
        }
    }
    private DataSet CountallDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("Count_News_Details", values, names, types);
    }
    //private DataSet CountallDetails1()
    //{
    //    ClearArrayLists();
    //    return Dobj.GetDataSet("past_Events_Details", values, names, types);
    //}
    private void CountRepeter()
    {
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        ds = CountallDetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            Repcountall.DataSource = ds;
            Repcountall.DataBind();
        }
        //ds1 = CountallDetails1();
        //if (ds1.Tables[0].Rows.Count >= 0)
        //{
        //    string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
        //    //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
        //    Repeater1.DataSource = ds1;
        //    Repeater1.DataBind();
        //}
        //ds2 = CountEventsDetails2();
        //if (ds2.Tables[0].Rows.Count > 0)
        //{
        //    string[] filePathss = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
        //    //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
        //    Repeater2.DataSource = ds2;
        //    Repeater2.DataBind();
        //}
    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("FundDonationlist.aspx");
    }

    //public void secondrep()
    //{
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        ds = pastevents();
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
    //            string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
    //            rep.DataSource = null;
    //            rep.DataBind();
    //            rep.DataSource = ds;
    //            rep.DataBind();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        //lblmsg.Text = CommonCode.ExceptionMessage.SqlExceptionHandling(Ex.Message);
    //    }
    //}

    protected void lnkbtnsecond_Click(object sender, EventArgs e)
    {
       // secondrep();
    }
}