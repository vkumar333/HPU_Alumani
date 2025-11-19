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

public partial class Alumni_ALM_FundDonationlist_Contributions : System.Web.UI.Page
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
        return Dobj.GetDataTable("ALM_Show_Crowd_Fund_Details", values, names, types);
    }

    private DataTable FetchContributeamountDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataTable("ALM_ContrAmtfetch", values, names, types);
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
        //dt1 = FetchContributeamountDetails();

        //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");       
        if (dt.Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            rep.DataSource = dt;
            rep.DataBind();
        }
    }
    private DataSet CountallDetails()
    {
        ClearArrayLists();
        return Dobj.GetDataSet("ALM_Count_News_Details", values, names, types);
    }
    //private DataSet CountallAcadm()
    //{
    //    ClearArrayLists();
    //    return Dobj.GetDataSet("past_Events_Details", values, names, types);
    //}
    private void CountRepeter()
    {
        DataSet ds = new DataSet();
        //DataSet ds1 = new DataSet();
        //DataSet ds2 = new DataSet();
       
        ds = CountallDetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            Repcountall.DataSource = ds;
            Repcountall.DataBind();
        }
        DataTable dt = ds.Tables[1];
        DataTable dt2 = ds.Tables[2];
        DataTable dt3 = ds.Tables[3];
        if (dt.Rows.Count >= 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        if (dt2.Rows.Count >= 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            Repeater2.DataSource = dt2;
            Repeater2.DataBind();
        }

        if (dt3.Rows.Count >= 0)
        {
            //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
            //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
            Repeater3.DataSource = dt3;
            Repeater3.DataBind();
        }
     
    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ALM_FundDonationlist_Contributions.aspx");
    }
     
    private DataTable fillTest(string id)
    {
        try
        {
            ClearArrayLists();
            names.Add("@fk_cat_id"); types.Add(SqlDbType.NVarChar); values.Add(id);
            return Dobj.GetDataTable("ALM_Show_Crowd_Fund_Details_count", values, names, types);
        }
        catch (Exception)
        {

            throw;
        }
      
    }
    //protected void lnkbtnsecond_Click(object sender, EventArgs e)
    //{
    //    var pp = "";
    //    //secondrep();
    //    //DataTable dt = fillTest(lnkbtnAcad.CommandArgument.ToString());
    //    //Repeater1.DataSource = dt;
    //    //Repeater1.DataBind();
    //}

    //protected void lnkbtndevelopmennt_Click(object sender, EventArgs e)
    //{

    //}



    //protected void lnkbtnsocial_Click(object sender, EventArgs e)
    //{

    //}

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "View") // Replace View with the name of your command
        {
             
            string id = Convert.ToInt32(e.CommandArgument).ToString();
            DataTable dt = fillTest(id);
            if (dt.Rows.Count > 0)
            {
                //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
                //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
                rep.DataSource = dt;
                rep.DataBind();
            }

        }
    }

    protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "View") // Replace View with the name of your command
        {

            string id = Convert.ToInt32(e.CommandArgument).ToString();
            DataTable dt = fillTest(id);
            if (dt.Rows.Count > 0)
            {
                //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
                //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
                rep.DataSource = dt;
                rep.DataBind();
            }

        }
    }

    protected void Repeater3_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "View") // Replace View with the name of your command
        {

            string id = Convert.ToInt32(e.CommandArgument).ToString();
            DataTable dt = fillTest(id);
            if (dt.Rows.Count > 0)
            {
                //string[] filePaths = Directory.GetFiles("D:\\PLACEMENT_DATA\\Company_Profile\\");
                //string[] filePaths = Directory.GetFiles("L:\\Published_APP\\FTPSITE\\HPU_DOC\\PLACEMENT_DATA\\Company_Profile\\");
                rep.DataSource = dt;
                rep.DataBind();
            }

        }
    }
}